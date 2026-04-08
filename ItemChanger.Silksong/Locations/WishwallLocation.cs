using HarmonyLib;
using ItemChanger.Locations;
using MonoMod.RuntimeDetour;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ItemChanger.Silksong.Locations;

/// <summary>
/// Location that intercepts reward delivery from a <see cref="QuestBoardInteractable"/>
/// (Silksong's "wishwall" quest board). When the specified quest is turned in at the
/// board, ItemChanger gives the placement's items instead of the quest's vanilla reward.
/// </summary>
/// <remarks>
/// <para>
/// <see cref="QuestName"/> must equal the Unity Object name of the <see cref="FullQuestBase"/>
/// ScriptableObject — this is the <c>QuestCompletionDataName</c> field in
/// <c>quests.json</c>, and can be referenced via the generated
/// <see cref="RawData.Quests"/> constants.
/// </para>
/// <para>
/// The hook is global: one <see cref="QuestBoardInteractable.ProcessQueuedCompletions"/>
/// hook is registered per location instance, so <see cref="QuestName"/> values must be
/// unique across all loaded <see cref="WishwallLocation"/> instances (which they are
/// because each FullQuestBase has a unique Unity name).
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

    private static readonly FieldInfo s_queuedCompletionsField =
        typeof(QuestBoardInteractable)
            .GetField("queuedCompletions", BindingFlags.NonPublic | BindingFlags.Instance)!;

    private static readonly FieldInfo s_rewardItemField =
        typeof(FullQuestBase)
            .GetField("rewardItem", BindingFlags.NonPublic | BindingFlags.Instance)!;

    // ─── Location lifecycle ─────────────────────────────────────────────────────

    protected override void DoLoad()
    {
        _savedItem = ScriptableObject.CreateInstance<WishwallSavedItem>();
        _savedItem.Location = this;

        // Register a global hook that fires whenever any QuestBoardInteractable
        // processes a completion.  We peek the queue and swap rewardItem for the
        // quest that matches ours.  Using() disposes the hook on DoUnload.
        Using(new Hook(
            AccessTools.Method(typeof(QuestBoardInteractable),
                               nameof(QuestBoardInteractable.ProcessQueuedCompletions)),
            (Action orig, QuestBoardInteractable self) =>
                InterceptProcessQueuedCompletions(orig, self)
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

        if (_savedItem != null)
        {
            UObject.Destroy(_savedItem);
            _savedItem = null;
        }
    }

    // ─── Hook implementation ────────────────────────────────────────────────────

    private void InterceptProcessQueuedCompletions(Action orig, QuestBoardInteractable self)
    {
        // Peek the next quest to be processed.  Only replace rewardItem when it
        // matches our QuestName; other locations' hooks will handle their quests.
        if (_savedItem != null &&
            s_queuedCompletionsField.GetValue(self) is Queue<FullQuestBase> queue &&
            queue.Count > 0)
        {
            FullQuestBase next = queue.Peek();
            if (next.name == QuestName)
            {
                // Save the original rewardItem the first time we intercept this quest.
                if (_patchedQuest == null)
                {
                    _patchedQuest       = next;
                    _originalRewardItem = s_rewardItemField.GetValue(next) as SavedItem;
                }
                s_rewardItemField.SetValue(next, _savedItem);
            }
        }

        orig();
    }

    // ─── WishwallSavedItem ──────────────────────────────────────────────────────

    /// <summary>
    /// Placeholder <see cref="SavedItem"/> that temporarily replaces the quest's
    /// vanilla reward. Forwards item delivery to ItemChanger's give flow.
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

        // GetTakesHeroControl() returns false (base default), so
        // ProcessQueuedCompletions continues to ExecuteDelayed(1f, ProcessQueuedCompletions)
        // after our give, allowing subsequent queued quests to be processed.
    }
}
