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
/// Singleton module that manages reward-icon and description preview for all
/// active <see cref="ItemChanger.Silksong.Locations.WishwallLocation"/> instances.
/// </summary>
/// <remarks>
/// Rather than each location registering its own hooks on
/// <see cref="FullQuestBase.RewardIcon"/>, <see cref="FullQuestBase.RewardIconType"/>,
/// <see cref="FullQuestBase.GetDescription"/>, and <see cref="QuestItemDescription"/>,
/// this module registers the hooks once and dispatches based on quest name, keeping
/// the hook chain O(1) regardless of how many wishwall locations are loaded.
///
/// <see cref="ItemChanger.Silksong.Locations.WishwallLocation"/> calls
/// <see cref="Register"/> and <see cref="Unregister"/> from its own
/// <c>DoLoad</c>/<c>DoUnload</c>. Consumers do not need to manage this module
/// directly — it is auto-added the first time a wishwall location loads.
/// </remarks>
[SingletonModule]
public sealed class WishwallPreviewModule : ItemChanger.Modules.Module
{
    // Registry: questName → Placement. Populated by WishwallLocation.DoLoad/DoUnload.
    private static readonly Dictionary<string, Placement> s_registry = new();

    // Per-QuestItemDescription instance: prefab-original localPosition of rewardGroup,
    // cached on first contact so we can restore it when the quest is not one of ours.
    private static readonly Dictionary<int, Vector3> s_rewardGroupOriginalPositions = new();

    // Horizontal offset (local-space units) used to shift rewardGroup clear of
    // donateCostGroup for donation quests. Positive = right.
    private const float RewardBesideDonateOffset = 3.0f;

    // ─── Registration API (called by WishwallLocation) ──────────────────────────

    internal static void Register(string questName, Placement placement) =>
        s_registry[questName] = placement;

    internal static void Unregister(string questName) =>
        s_registry.Remove(questName);

    // ─── Module lifecycle ────────────────────────────────────────────────────────

    protected override void DoLoad()
    {
        // Return the IC item's preview sprite instead of the vanilla reward sprite.
        // Also covers quests whose rewardIconType is None — they previously showed
        // no icon; we force one here.
        Using(new Hook(
            AccessTools.PropertyGetter(typeof(FullQuestBase), nameof(FullQuestBase.RewardIcon)),
            (Func<FullQuestBase, Sprite> orig, FullQuestBase self) =>
            {
                if (s_registry.TryGetValue(self.name, out Placement placement))
                    return GetPreviewSprite(placement);
                return orig(self);
            }
        ));

        // Return IconTypes.Image so QuestItemDescription can safely index
        // counterMaterials[(int)RewardIconType] without an out-of-range crash
        // (IconTypes.None == -1 would crash).
        Using(new Hook(
            AccessTools.PropertyGetter(typeof(FullQuestBase), nameof(FullQuestBase.RewardIconType)),
            (Func<FullQuestBase, FullQuestBase.IconTypes> orig, FullQuestBase self) =>
            {
                if (s_registry.ContainsKey(self.name))
                    return FullQuestBase.IconTypes.Image;
                return orig(self);
            }
        ));

        // Append IC item preview names to the quest description, supporting placements
        // with multiple items ("Rewards: A, B, C").
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

        // For donation quests, both rewardGroup and donateCostGroup become active
        // simultaneously, which causes them to overlap (the layout was never designed
        // for both to coexist). Shift rewardGroup to the right of donateCostGroup.
        Using(new Hook(
            AccessTools.Method(typeof(QuestItemDescription), "SetDisplay"),
            (Action<QuestItemDescription, BasicQuestBase> orig,
             QuestItemDescription self, BasicQuestBase quest) =>
            {
                int id = self.GetInstanceID();

                // Cache the prefab-original position the first time we see this instance.
                if (!s_rewardGroupOriginalPositions.ContainsKey(id))
                    s_rewardGroupOriginalPositions[id] = self.rewardGroup.transform.localPosition;

                // Restore to the prefab position before calling orig() so the vanilla
                // layout is used for non-wishwall quests.
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
