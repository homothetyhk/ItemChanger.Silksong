using HarmonyLib;
using ItemChanger.Costs;
using ItemChanger.Locations;
using ItemChanger.Placements;
using ItemChanger.Silksong.Costs;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Modules.ShopsModule;
using ItemChanger.Tags;
using Newtonsoft.Json;
using UnityEngine;

namespace ItemChanger.Silksong.Placements;

public class VogShopPlacement(string Name) : Placement(Name), IMultiCostPlacement, IPrimaryLocationPlacement
{
    public static VogShopPlacement? Instance { get; private set; }

    public required VogShopLocation Location;

    [JsonIgnore]
    Location IPrimaryLocationPlacement.Location => Location;

    protected override void DoLoad()
    {
        if (Instance != null) throw new InvalidOperationException("Cannot have two VogShopPlacements");
        foreach (var item in Items)
        {
            if (item.GetTag<CostTag>()?.Cost is Cost c && c is not RosaryCost) throw new ArgumentException("VogShopPlacement only supports RosaryCosts");
        }

        Harmony harmony = new(typeof(Patches).FullName);
        harmony.PatchAll(typeof(Patches));
        Using(harmony);

        Location.Placement = this;
        Location.LoadOnce();

        Instance = this;
    }

    protected override void DoUnload()
    {
        Instance = null;

        Location.UnloadOnce();
        Location.Placement = null;
    }

    private readonly List<ModShopItem> currentItems = [];

    internal List<ISimpleShopItem> GetItems()
    {
        currentItems.Clear();
        currentItems.AddRange(Items.Select(i => ModShopItem.CreateInstance(i, this)).Where(i => i.IsAvailable));
        return [.. currentItems];
    }

    internal void OnPurchasedItem(Transform transform, int itemIndex)
    {
        if (itemIndex < 0 || itemIndex >= currentItems.Count) return;

        currentItems[itemIndex].ICCost.Paid = true;
        GiveSome([currentItems[itemIndex].Item], new()
        {
            FlingType = Enums.FlingType.DirectDeposit,
            MessageType = Enums.MessageType.SmallPopup,
            Transform = transform,
        });
    }


    // The only other subclass of SimpleShopMenuOwner is the 'caravan spider', which is cut content??
    // So we handle CaravanTroupeHunter directly.
    [HarmonyPatch]
    private  static class Patches
    {
        [HarmonyPatch(typeof(CaravanTroupeHunter), nameof(CaravanTroupeHunter.GetItems))]
        [HarmonyPrefix]
        private static bool Prefix_GetItems(CaravanTroupeHunter __instance, ref List<ISimpleShopItem> __result)
        {
            if (Instance is not VogShopPlacement placement) return true;

            __result = placement.GetItems();
            return false;
        }

        [HarmonyPatch(typeof(CaravanTroupeHunter), nameof(CaravanTroupeHunter.OnPurchasedItem))]
        [HarmonyPrefix]
        private static bool Prefix_OnPurchasedItem(CaravanTroupeHunter __instance, int itemIndex)
        {
            var vog = __instance;
            if (Instance is not VogShopPlacement placement) return true;

            placement.OnPurchasedItem(vog.transform, itemIndex);
            return false;
        }

        [HarmonyPatch(typeof(SimpleShopMenuOwner), nameof(SimpleShopMenuOwner.OpenMenu))]
        [HarmonyPostfix]
        private static void Postfix_OpenMenu(SimpleShopMenuOwner __instance, ref bool __result)
        {
            if (!__result) return;
            if (__instance is not CaravanTroupeHunter) return;
            if (Instance is not VogShopPlacement placement) return;

            placement.AddVisitFlag(Enums.VisitState.Previewed);
        }
    }
}
