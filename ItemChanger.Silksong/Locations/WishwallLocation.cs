using HarmonyLib;
using ItemChanger.Items;
using ItemChanger.Locations;
using ItemChanger.Silksong.Modules;
using MonoMod.RuntimeDetour;
using Newtonsoft.Json;
using UnityEngine;

namespace ItemChanger.Silksong.Locations;

/// <summary>
/// Location that intercepts reward delivery when a quest is completed via a
/// <see cref="QuestBoardInteractable"/> (Silksong's "wishwall" quest board).
/// When the specified quest is turned in, ItemChanger gives the placement's items
/// instead of the quest's vanilla reward.
/// </summary>
/// <remarks>
/// <para>
/// <see cref="QuestName"/> must equal the Unity Object name of the <see cref="FullQuestBase"/>
/// ScriptableObject — this is the <c>QuestCompletionDataName</c> field in
/// <c>quests.json</c>, and can be referenced via the generated
/// <see cref="RawData.Quests"/> constants.
/// </para>
/// <para>
/// Reward-icon and description preview (hooking <see cref="FullQuestBase.RewardIcon"/>,
/// <see cref="FullQuestBase.RewardIconType"/>, <see cref="FullQuestBase.GetDescription"/>,
/// and <see cref="QuestItemDescription"/>) is handled centrally by
/// <see cref="WishwallPreviewModule"/>, which this location registers with on load.
/// </para>
/// <para>
/// The hook intercepts <see cref="FullQuestBase.TryEndQuest"/> globally and wraps
/// the <c>afterPrompt</c> callback to inject <c>GiveAll()</c>. This approach works
/// for all completion paths: both the standard <c>ProcessQueuedCompletions</c> route
/// and FSM-driven donation quests that call <c>TryEndQuest</c> directly with an
/// <c>afterPrompt</c> that does not invoke <c>rewardItem.Get()</c>.
/// </para>
/// </remarks>
public class WishwallLocation : AutoLocation
{
    /// <summary>
    /// The Unity Object name of the <see cref="FullQuestBase"/> ScriptableObject.
    /// Equals <c>QuestCompletionDataName</c> in <c>quests.json</c>.
    /// Use the generated <see cref="RawData.Quests"/> constants, e.g.
    /// <c>Quests.Beastfly_Hunt</c> for the "Savage Beastfly" quest.
    /// </summary>
    public required string QuestName { get; init; }

    // Runtime state — created in DoLoad, destroyed in DoUnload.
    private WishwallSavedItem? _savedItem;
    // The FullQuestBase whose rewardItem we have replaced (null until first intercept).
    private FullQuestBase? _patchedQuest;
    // The original rewardItem, saved so we can restore it on DoUnload.
    private SavedItem? _originalRewardItem;
    // Transform of the QuestBoardInteractable captured on first interception,
    // used to populate GiveInfo so items fling from the correct scene position.
    private Transform? _boardTransform;

    // ─── Location lifecycle ─────────────────────────────────────────────────────

    protected override void DoLoad()
    {
        _savedItem = ScriptableObject.CreateInstance<WishwallSavedItem>();
        _savedItem.Location = this;

        // Auto-add the preview module if not already present, then register this
        // location so the module's hooks can look up our placement by quest name.
        ItemChangerHost.Singleton.ActiveProfile!.Modules.GetOrAdd<WishwallPreviewModule>();
        WishwallPreviewModule.Register(QuestName, Placement!);

        // Hook FullQuestBase.TryEndQuest globally.  This fires for every quest
        // completion path, including FSM-driven donation quests that bypass
        // QuestBoardInteractable.ProcessQueuedCompletions.
        Using(new Hook(
            AccessTools.Method(typeof(FullQuestBase), nameof(FullQuestBase.TryEndQuest)),
            (Func<FullQuestBase, Action, bool, bool, bool, bool> orig,
             FullQuestBase self, Action afterPrompt, bool consumeCurrency,
             bool forceEnd, bool showPrompt) =>
                InterceptTryEndQuest(orig, self, afterPrompt, consumeCurrency, forceEnd, showPrompt)
        ));
    }

    protected override void DoUnload()
    {
        WishwallPreviewModule.Unregister(QuestName);

        // Restore the original rewardItem so replaying the game in the same session
        // doesn't leave the ScriptableObject permanently modified.
        if (_patchedQuest != null)
        {
            _patchedQuest.rewardItem = _originalRewardItem;
            _patchedQuest       = null;
            _originalRewardItem = null;
        }

        if (_savedItem != null)
        {
            UObject.Destroy(_savedItem);
            _savedItem = null;
        }

        _boardTransform = null;
    }

    // ─── GiveInfo ───────────────────────────────────────────────────────────────

    public override GiveInfo GetGiveInfo() => new()
    {
        Container   = "Wishwall",
        FlingType   = FlingType,
        Transform   = _boardTransform,
        MessageType = ItemChanger.Enums.MessageType.Any,
    };

    // ─── Hook implementation ────────────────────────────────────────────────────

    private bool InterceptTryEndQuest(
        Func<FullQuestBase, Action, bool, bool, bool, bool> orig,
        FullQuestBase self, Action afterPrompt, bool consumeCurrency,
        bool forceEnd, bool showPrompt)
    {
        if (_savedItem != null && self.name == QuestName)
        {
            // Capture the quest board's transform on first interception so GetGiveInfo()
            // can provide a meaningful fling origin for non-DirectDeposit fling types.
            _boardTransform ??= UObject.FindFirstObjectByType<QuestBoardInteractable>()?.transform;

            // Substitute rewardItem for completion paths that call rewardItem.Get()
            // inside afterPrompt (e.g. QuestBoardInteractable.ProcessQueuedCompletions).
            if (_patchedQuest == null)
            {
                _patchedQuest       = self;
                _originalRewardItem = self.rewardItem;
            }
            self.rewardItem = _savedItem;

            // Wrap afterPrompt so GiveAll() is called when the completion callback
            // fires (sync or async via ShowQuestCompleted).  This covers FSM-driven
            // donation paths whose afterPrompt does not call rewardItem.Get().
            // AllObtained() guards against double-give if both paths fire.
            Action? originalAfterPrompt = afterPrompt;
            afterPrompt = () =>
            {
                originalAfterPrompt?.Invoke();
                if (Placement?.AllObtained() == false)
                    GiveAll();
            };
        }

        return orig(self, afterPrompt, consumeCurrency, forceEnd, showPrompt);
    }

    // ─── WishwallSavedItem ──────────────────────────────────────────────────────

    /// <summary>
    /// Placeholder <see cref="SavedItem"/> that temporarily replaces the quest's
    /// vanilla reward. Forwards item delivery to ItemChanger's give flow for
    /// completion paths that call <c>rewardItem.Get()</c> inside <c>afterPrompt</c>.
    /// </summary>
    private class WishwallSavedItem : SavedItem
    {
        /// <summary>Set immediately after <c>ScriptableObject.CreateInstance</c>.</summary>
        [JsonIgnore]
        public WishwallLocation? Location { get; set; }

        public override void Get(bool showPopup = true)
        {
            // Guard against double-give in case the board calls Get() more than once.
            if (Location?.Placement?.AllObtained() == false)
                Location.GiveAll();
        }

        public override bool CanGetMore() =>
            Location?.Placement?.AllObtained() == false;

        /// <summary>
        /// Fallback icon for paths that call <c>rewardItem.GetPopupIcon()</c> directly
        /// rather than going through <see cref="FullQuestBase.RewardIcon"/> (which is
        /// intercepted by <see cref="WishwallPreviewModule"/>).
        /// </summary>
        public override Sprite GetPopupIcon()
        {
            if (Location?.Placement is not { } placement) return null!;
            Item? item = placement.Items.FirstOrDefault(i => !i.IsObtained());
            return item?.GetPreviewSprite(placement)!;
        }

        /// <summary>
        /// Fallback name for paths that call <c>rewardItem.GetPopupName()</c> directly.
        /// </summary>
        public override string GetPopupName()
        {
            if (Location?.Placement is not { } placement) return null!;
            Item? item = placement.Items.FirstOrDefault(i => !i.IsObtained());
            return (item?.GetResolvedUIDef(placement)?.GetPreviewName())!;
        }

        // GetTakesHeroControl() returns false (base default), so quest processing
        // continues normally after our give.
    }
}
