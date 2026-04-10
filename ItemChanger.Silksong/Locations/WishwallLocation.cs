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
/// <see cref="QuestName"/> must equal the Unity Object name of the <see cref="FullQuestBase"/>
/// ScriptableObject — the <c>QuestCompletionDataName</c> field in <c>quests.json</c>.
/// Use the generated <see cref="RawData.Quests"/> constants.
/// Preview (icon, icon type, description) is handled by <see cref="WishwallPreviewModule"/>,
/// which is auto-installed and registered from <see cref="DoLoad"/>.
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

    private WishwallSavedItem? _savedItem;
    private FullQuestBase? _patchedQuest;
    private SavedItem? _originalRewardItem;
    // Captured on first TryEndQuest interception; used as the fling origin in GetGiveInfo.
    private Transform? _boardTransform;

    // ─── Location lifecycle ─────────────────────────────────────────────────────

    protected override void DoLoad()
    {
        _savedItem = ScriptableObject.CreateInstance<WishwallSavedItem>();
        _savedItem.Location = this;

        ItemChangerHost.Singleton.ActiveProfile!.Modules.GetOrAdd<WishwallPreviewModule>();
        WishwallPreviewModule.Register(QuestName, Placement!);

        Using(new Hook(
            AccessTools.Method(typeof(FullQuestBase), nameof(FullQuestBase.TryEndQuest)),
            (Func<FullQuestBase, Action, bool, bool, bool, bool> orig,
             FullQuestBase self, Action afterPrompt, bool consumeCurrency,
             bool forceEnd, bool showPrompt) =>
                InterceptTryEndQuest(orig, self, afterPrompt, consumeCurrency, forceEnd, showPrompt)
        ));

        // Give respawning items when the player opens the board but the quest is already
        // done (so TryEndQuest won't fire). Items are given before the board UI opens.
        Using(new Hook(
            AccessTools.Method(typeof(QuestBoardInteractable), "OnStartDialogue"),
            (Action<QuestBoardInteractable> orig, QuestBoardInteractable self) =>
            {
                if (_savedItem != null
                    && Placement?.AllObtained() == false
                    && BoardHasPendingRespawn(self))
                {
                    _boardTransform ??= self.transform;
                    GiveAll(() => orig(self));
                }
                else
                    orig(self);
            }
        ));
    }

    protected override void DoUnload()
    {
        WishwallPreviewModule.Unregister(QuestName);

        // Restore the original rewardItem to avoid leaving the ScriptableObject modified.
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
            _boardTransform ??= UObject.FindFirstObjectByType<QuestBoardInteractable>()?.transform;

            // Substitute rewardItem for paths that call rewardItem.Get() inside afterPrompt.
            if (_patchedQuest == null)
            {
                _patchedQuest       = self;
                _originalRewardItem = self.rewardItem;
            }
            self.rewardItem = _savedItem;

            // Give items before the afterPrompt callback (quest completion UI) fires.
            // Using GiveAll(callback) ensures the IC item popup appears first, then the
            // quest UI opens — including for respawning items that need to be given again
            // on subsequent turn-ins. If all items are already obtained, skip straight to
            // the original callback.
            Action? originalAfterPrompt = afterPrompt;
            afterPrompt = () =>
            {
                if (Placement?.AllObtained() == false)
                    GiveAll(() => originalAfterPrompt?.Invoke());
                else
                    originalAfterPrompt?.Invoke();
            };
        }

        return orig(self, afterPrompt, consumeCurrency, forceEnd, showPrompt);
    }

    // Returns true if the board contains our quest, the quest is NOT ready to turn in
    // (so TryEndQuest won't fire), and IC has already given at least one item from this
    // placement (ObtainedAnyItem guards against giving before the first turn-in).
    private bool BoardHasPendingRespawn(QuestBoardInteractable board)
    {
        if (Placement?.Visited.HasFlag(ItemChanger.Enums.VisitState.ObtainedAnyItem) != true)
            return false;

        FullQuestBase? quest = board.Quests
            .SelectMany(g => g.GetQuests())
            .OfType<FullQuestBase>()
            .FirstOrDefault(q => q.name == QuestName);
        return quest != null && !quest.GetIsReadyToTurnIn(atQuestBoard: true);
    }

    // ─── WishwallSavedItem ──────────────────────────────────────────────────────

    /// <summary>
    /// Placeholder <see cref="SavedItem"/> that replaces the quest's vanilla reward.
    /// Forwards item delivery to ItemChanger's give flow for paths that call
    /// <c>rewardItem.Get()</c> inside <c>afterPrompt</c>.
    /// </summary>
    private class WishwallSavedItem : SavedItem
    {
        /// <summary>Set immediately after <c>ScriptableObject.CreateInstance</c>.</summary>
        [JsonIgnore]
        public WishwallLocation? Location { get; set; }

        public override void Get(bool showPopup = true)
        {
            if (Location?.Placement?.AllObtained() == false)
                Location.GiveAll();
        }

        public override bool CanGetMore() =>
            Location?.Placement?.AllObtained() == false;

        /// <summary>Fallback for paths that call <c>rewardItem.GetPopupIcon()</c> directly.</summary>
        public override Sprite GetPopupIcon()
        {
            if (Location?.Placement is not { } placement) return null!;
            Item? item = placement.Items.FirstOrDefault(i => !i.IsObtained());
            return item?.GetPreviewSprite(placement)!;
        }

        /// <summary>Fallback for paths that call <c>rewardItem.GetPopupName()</c> directly.</summary>
        public override string GetPopupName()
        {
            if (Location?.Placement is not { } placement) return null!;
            Item? item = placement.Items.FirstOrDefault(i => !i.IsObtained());
            return (item?.GetResolvedUIDef(placement)?.GetPreviewName())!;
        }
    }
}
