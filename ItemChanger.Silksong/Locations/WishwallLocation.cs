using HarmonyLib;
using ItemChanger.Items;
using ItemChanger.Locations;
using MonoMod.RuntimeDetour;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;
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
/// The hook intercepts <see cref="FullQuestBase.TryEndQuest"/> globally and wraps
/// the <c>afterPrompt</c> callback to inject <c>GiveAll()</c>. This approach works
/// for all completion paths: both the standard <c>ProcessQueuedCompletions</c> route
/// and FSM-driven donation quests that call <c>TryEndQuest</c> directly with an
/// <c>afterPrompt</c> that does not invoke <c>rewardItem.Get()</c>.
/// </para>
/// <para>
/// <see cref="QuestName"/> values must be unique across all loaded
/// <see cref="WishwallLocation"/> instances (which they are because each
/// <see cref="FullQuestBase"/> has a unique Unity Object name).
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

    // ─── Reflected fields (cached once) ────────────────────────────────────────

    private static readonly FieldInfo s_rewardItemField =
        typeof(FullQuestBase)
            .GetField("rewardItem", BindingFlags.NonPublic | BindingFlags.Instance)!;

    private static readonly FieldInfo s_qidRewardGroupField =
        typeof(QuestItemDescription)
            .GetField("rewardGroup", BindingFlags.NonPublic | BindingFlags.Instance)!;

    private static readonly FieldInfo s_qidDonateCostGroupField =
        typeof(QuestItemDescription)
            .GetField("donateCostGroup", BindingFlags.NonPublic | BindingFlags.Instance)!;

    // Horizontal distance (in local-space units) to place rewardGroup to the right of
    // donateCostGroup when both must coexist on a donation quest.
    // Positive = right, negative = left.
    private const float RewardBesideDonateOffset = 3.0f;

    // Per-QuestItemDescription instance: the prefab-original localPosition of rewardGroup,
    // cached the first time we see each instance so we can restore it for non-donate quests.
    private readonly Dictionary<int, Vector3> _rewardGroupOriginalPositions = new();

    // ─── Location lifecycle ─────────────────────────────────────────────────────

    protected override void DoLoad()
    {
        _savedItem = ScriptableObject.CreateInstance<WishwallSavedItem>();
        _savedItem.Location = this;

        // Hook RewardIcon and RewardIconType together so every consumer — the quest board
        // detail view, the turn-in confirmation box, the inventory view — sees the IC
        // item's sprite regardless of whether the vanilla quest has an icon configured.
        //
        // RewardIconType must also be hooked because QuestItemDescription indexes into
        // its counterMaterials array with (int)RewardIconType, and IconTypes.None == -1
        // would produce an out-of-range crash.  We return IconTypes.Image (0) so the
        // sprite is rendered with a plain image material.
        Using(new Hook(
            AccessTools.PropertyGetter(typeof(FullQuestBase), nameof(FullQuestBase.RewardIcon)),
            (Func<FullQuestBase, Sprite> orig, FullQuestBase self) =>
            {
                if (_savedItem != null && self.name == QuestName)
                {
                    return _savedItem.GetPopupIcon();
                }
                return orig(self);
            }
        ));

        Using(new Hook(
            AccessTools.PropertyGetter(typeof(FullQuestBase), nameof(FullQuestBase.RewardIconType)),
            (Func<FullQuestBase, FullQuestBase.IconTypes> orig, FullQuestBase self) =>
            {
                if (_savedItem != null && self.name == QuestName)
                {
                    return FullQuestBase.IconTypes.Image;
                }
                return orig(self);
            }
        ));

        // Hook QuestItemDescription.SetDisplay so that when a donation quest's reward
        // icon is forced visible (both rewardGroup and donateCostGroup active), we move
        // rewardGroup above donateCostGroup to avoid overlap.
        // The prefab-original localPosition of rewardGroup is cached per-instance on first
        // contact and restored before each orig() call so non-donate quests are unaffected.
        Using(new Hook(
            AccessTools.Method(typeof(QuestItemDescription), "SetDisplay"),
            (Action<QuestItemDescription, BasicQuestBase> orig,
             QuestItemDescription self, BasicQuestBase quest) =>
            {
                int id = self.GetInstanceID();
                var rewardGroup = s_qidRewardGroupField.GetValue(self) as GameObject;

                // Cache the prefab-original position the very first time we see this instance,
                // before any of our hooks have had a chance to move it.
                if (rewardGroup != null && !_rewardGroupOriginalPositions.ContainsKey(id))
                {
                    _rewardGroupOriginalPositions[id] = rewardGroup.transform.localPosition;
                }

                // Restore to prefab position before orig() so vanilla layout is always correct.
                if (rewardGroup != null && _rewardGroupOriginalPositions.TryGetValue(id, out Vector3 saved))
                {
                    rewardGroup.transform.localPosition = saved;
                }

                orig(self, quest);

                // If this is our donation quest and both groups ended up active, shift
                // rewardGroup above donateCostGroup to prevent the overlap.
                if (_savedItem == null
                    || quest is not FullQuestBase fq
                    || fq.name != QuestName
                    || !fq.IsDonateType)
                    return;

                var donateCostGroup = s_qidDonateCostGroupField.GetValue(self) as GameObject;
                if (rewardGroup  == null || !rewardGroup.activeSelf
                    || donateCostGroup == null || !donateCostGroup.activeSelf)
                    return;

                Vector3 donatePos = donateCostGroup.transform.localPosition;
                _rewardGroupOriginalPositions.TryGetValue(id, out Vector3 origPos);
                rewardGroup.transform.localPosition =
                    new Vector3(donatePos.x + RewardBesideDonateOffset, origPos.y, origPos.z);
            }
        ));

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
        // Restore the original rewardItem so replaying the game in the same session
        // doesn't leave the ScriptableObject permanently modified.
        if (_patchedQuest != null)
        {
            s_rewardItemField.SetValue(_patchedQuest, _originalRewardItem);
            _patchedQuest       = null;
            _originalRewardItem = null;
        }

        _rewardGroupOriginalPositions.Clear();

        if (_savedItem != null)
        {
            UObject.Destroy(_savedItem);
            _savedItem = null;
        }
    }

    // ─── Hook implementation ────────────────────────────────────────────────────

    private bool InterceptTryEndQuest(
        Func<FullQuestBase, Action, bool, bool, bool, bool> orig,
        FullQuestBase self, Action afterPrompt, bool consumeCurrency,
        bool forceEnd, bool showPrompt)
    {
        if (_savedItem != null && self.name == QuestName)
        {
            // Substitute rewardItem for completion paths that call rewardItem.Get()
            // inside afterPrompt (e.g. QuestBoardInteractable.ProcessQueuedCompletions).
            if (_patchedQuest == null)
            {
                _patchedQuest       = self;
                _originalRewardItem = s_rewardItemField.GetValue(self) as SavedItem;
            }
            s_rewardItemField.SetValue(self, _savedItem);

            // Wrap afterPrompt so GiveAll() is called when the completion callback
            // fires (sync or async via ShowQuestCompleted).  This covers FSM-driven
            // donation paths whose afterPrompt does not call rewardItem.Get().
            // AllObtained() guards against double-give if both paths fire.
            Action? originalAfterPrompt = afterPrompt;
            afterPrompt = () =>
            {
                originalAfterPrompt?.Invoke();
                if (Placement?.AllObtained() == false)
                {
                    GiveAll();
                }
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
            {
                Location.GiveAll();
            }
        }

        public override bool CanGetMore() =>
            Location?.Placement?.AllObtained() == false;

        /// <summary>
        /// Returns the preview sprite of the first unobtained IC item so the quest
        /// completion popup shows the randomized reward instead of the vanilla item.
        /// </summary>
        public override Sprite GetPopupIcon()
        {
            if (Location?.Placement is not { } placement) return null!;
            Item? item = placement.Items.FirstOrDefault(i => !i.IsObtained());
            return item?.GetPreviewSprite(placement)!;
        }

        /// <summary>
        /// Returns the preview name of the first unobtained IC item so the quest
        /// completion popup labels the randomized reward correctly.
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
