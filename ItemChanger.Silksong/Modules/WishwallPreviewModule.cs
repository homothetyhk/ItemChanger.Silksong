using HarmonyLib;
using ItemChanger.Items;
using ItemChanger.Modules;
using ItemChanger.Placements;
using MonoMod.RuntimeDetour;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ItemChanger.Silksong.Modules;

/// <summary>
/// Singleton module that handles reward-icon and description preview for all active
/// <see cref="ItemChanger.Silksong.Locations.WishwallLocation"/> instances.
/// Registers one set of hooks and dispatches by quest name via a static registry,
/// keeping the hook chain O(1) regardless of how many wishwall locations are loaded.
/// Auto-installed and managed by <see cref="ItemChanger.Silksong.Locations.WishwallLocation"/>.
/// </summary>
[SingletonModule]
public sealed class WishwallPreviewModule : ItemChanger.Modules.Module
{
    // questName → Placement. Populated by WishwallLocation.DoLoad/DoUnload.
    private static readonly Dictionary<string, Placement> s_registry = new();

    // Per-QuestItemDescription instance: cached prefab-original localPosition of rewardGroup.
    private static readonly Dictionary<int, Vector3> s_rewardGroupOriginalPositions = new();

    // Local-space units to shift rewardGroup right of donateCostGroup on donation quests.
    private const float RewardBesideDonateOffset = 3.0f;

    // ─── Registration API (called by WishwallLocation) ──────────────────────────

    internal static void Register(string questName, Placement placement) =>
        s_registry[questName] = placement;

    internal static void Unregister(string questName) =>
        s_registry.Remove(questName);

    // ─── Module lifecycle ────────────────────────────────────────────────────────

    protected override void DoLoad()
    {
        // Return the IC item's preview sprite; also forces an icon for quests with rewardIconType == None.
        Using(new Hook(
            AccessTools.PropertyGetter(typeof(FullQuestBase), nameof(FullQuestBase.RewardIcon)),
            (Func<FullQuestBase, Sprite> orig, FullQuestBase self) =>
            {
                if (s_registry.TryGetValue(self.name, out Placement placement))
                    return GetPreviewSprite(placement);
                return orig(self);
            }
        ));

        // Return IconTypes.Image so QuestItemDescription doesn't crash indexing
        // counterMaterials with IconTypes.None (-1).
        Using(new Hook(
            AccessTools.PropertyGetter(typeof(FullQuestBase), nameof(FullQuestBase.RewardIconType)),
            (Func<FullQuestBase, FullQuestBase.IconTypes> orig, FullQuestBase self) =>
            {
                if (s_registry.ContainsKey(self.name))
                    return FullQuestBase.IconTypes.Image;
                return orig(self);
            }
        ));

        // Append IC item names to the quest description ("Reward: X" or "Rewards: X, Y").
        Using(new Hook(
            AccessTools.Method(typeof(FullQuestBase), nameof(FullQuestBase.GetDescription)),
            (Func<FullQuestBase, BasicQuestBase.ReadSource, string> orig,
             FullQuestBase self, BasicQuestBase.ReadSource readSource) =>
            {
                string baseDesc = orig(self, readSource);
                if (!s_registry.TryGetValue(self.name, out Placement placement))
                    return baseDesc;

                var unobtained = placement.Items.Where(i => !i.IsObtained()).ToList();
                if (unobtained.Count == 0)
                    return baseDesc;

                string label    = unobtained.Count == 1 ? "Reward" : "Rewards";
                string itemList = string.Join(", ", unobtained
                    .Select(i => i.GetResolvedUIDef(placement)?.GetPreviewName())
                    .Where(n => !string.IsNullOrEmpty(n)));

                return string.IsNullOrEmpty(baseDesc)
                    ? $"{label}: {itemList}"
                    : $"{baseDesc}\n{label}: {itemList}";
            }
        ));

        // For donation quests, rewardGroup and donateCostGroup are both active and overlap.
        // Shift rewardGroup to the right of donateCostGroup to avoid the overlap.
        Using(new Hook(
            AccessTools.Method(typeof(QuestItemDescription), "SetDisplay"),
            (Action<QuestItemDescription, BasicQuestBase> orig,
             QuestItemDescription self, BasicQuestBase quest) =>
            {
                int id = self.GetInstanceID();

                if (!s_rewardGroupOriginalPositions.ContainsKey(id))
                    s_rewardGroupOriginalPositions[id] = self.rewardGroup.transform.localPosition;

                // Restore prefab position before orig() so non-wishwall quests are unaffected.
                if (s_rewardGroupOriginalPositions.TryGetValue(id, out Vector3 saved))
                    self.rewardGroup.transform.localPosition = saved;

                orig(self, quest);

                if (quest is not FullQuestBase fq
                    || !s_registry.ContainsKey(fq.name)
                    || !fq.IsDonateType
                    || !self.rewardGroup.activeSelf
                    || !self.donateCostGroup.activeSelf)
                    return;

                Vector3 donatePos = self.donateCostGroup.transform.localPosition;
                s_rewardGroupOriginalPositions.TryGetValue(id, out Vector3 origPos);
                self.rewardGroup.transform.localPosition =
                    new Vector3(donatePos.x + RewardBesideDonateOffset, origPos.y, origPos.z);
            }
        ));
    }

    protected override void DoUnload()
    {
        s_registry.Clear();
        s_rewardGroupOriginalPositions.Clear();
    }

    // ─── Helpers ─────────────────────────────────────────────────────────────────

    private static Sprite GetPreviewSprite(Placement placement)
    {
        Item? item = placement.Items.FirstOrDefault(i => !i.IsObtained());
        return item?.GetPreviewSprite(placement)!;
    }
}
