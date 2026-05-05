using HarmonyLib;
using UnityEngine;

namespace ItemChanger.Silksong.Modules.ShopsModule;

[HarmonyPatch]
internal static class ShopsPatches
{
    [HarmonyPatch(typeof(ShopOwnerBase), nameof(ShopOwnerBase.SpawnUpdateShop))]
    [HarmonyPostfix]
    private static void Postfix_SpawnUpdateShop(ShopOwnerBase __instance)
    {
        if (SilksongHost.Instance.ActiveProfile?.Modules.Get<ShopsModule>() is not ShopsModule module) return;
        module.OnSpawnUpdateShop(__instance);
    }

    [HarmonyPatch(typeof(ShopMenuStock), nameof(ShopMenuStock.BuildItemList))]
    [HarmonyPrefix]
    private static bool Prefix_BuildItemList(ShopMenuStock __instance) => Mod.Override(__instance, s => s.BuildItemList());

    [HarmonyPatch(typeof(ShopMenuStock), nameof(ShopMenuStock.DisplayCurrencyCounters))]
    [HarmonyPrefix]
    private static bool Prefix_DisplayCurrencyCounters(ShopMenuStock __instance) => Mod.Override(__instance, s => s.DisplayCurrencyCounters());

    [HarmonyPatch(typeof(ShopMenuStock), nameof(ShopMenuStock.EnumerateStock))]
    [HarmonyPrefix]
    private static bool Prefix_EnumerateStock(ShopMenuStock __instance, ref IEnumerable<ShopItem> __result) => Mod.Override(__instance, s => s.EnumerateStock(), ref __result);

    [HarmonyPatch(typeof(ShopMenuStock), nameof(ShopMenuStock.SetStock))]
    [HarmonyPrefix]
    private static bool Prefix_StockLeft(ShopMenuStock __instance, ShopItem[] newStock) => Mod.Override(__instance, s => s.SetStock(newStock));

    [HarmonyPatch(typeof(ShopMenuStock), nameof(ShopMenuStock.StockLeft))]
    [HarmonyPrefix]
    private static bool Prefix_StockLeft(ShopMenuStock __instance, ref bool __result) => Mod.Override(__instance, s => s.StockLeft(), ref __result);

    [HarmonyPatch(typeof(ShopMenuStock), nameof(ShopMenuStock.StockLeftNotInfinite))]
    [HarmonyPrefix]
    private static bool Prefix_StockLeftNotInfinite(ShopMenuStock __instance, ref bool __result) => Mod.Override(__instance, s => s.StockLeftNotInfinite(), ref __result);

    [HarmonyPatch(typeof(ShopItemStats), nameof(ShopItemStats.BuyFail))]
    [HarmonyPrefix]
    private static bool Prefix_BuyFail(ShopItemStats __instance) => Mod.Override(__instance.Item, i => i.BuyFail());

    [HarmonyPatch(typeof(ShopItemStats), nameof(ShopItemStats.CanBuy))]
    [HarmonyPrefix]
    private static bool Prefix_CanBuy(ShopItemStats __instance, ref bool __result) => Mod.Override(__instance.Item, i => i.CanBuy(), ref __result);

    [HarmonyPatch(typeof(ShopItem), nameof(ShopItem.Cost), MethodType.Getter)]
    [HarmonyPrefix]
    private static bool Prefix_Cost_get(ShopItem __instance, ref int __result) => Mod.Override(__instance, i => i.Cost, ref __result);

    [HarmonyPatch(typeof(ShopItem), nameof(ShopItem.DisplayName), MethodType.Getter)]
    [HarmonyPrefix]
    private static bool Prefix_DisplayName_get(ShopItem __instance, ref string __result) => Mod.Override(__instance, i => i.DisplayName, ref __result);

    [HarmonyPatch(typeof(ShopItem), nameof(ShopItem.Description), MethodType.Getter)]
    [HarmonyPrefix]
    private static bool Prefix_Description_get(ShopItem __instance, ref string __result) => Mod.Override(__instance, i => i.Description, ref __result);

    [HarmonyPatch(typeof(ShopItem), nameof(ShopItem.IsAvailable), MethodType.Getter)]
    [HarmonyPrefix]
    private static bool Prefix_IsAvailable_get(ShopItem __instance, ref bool __result) => Mod.Override(__instance, i => i.IsAvailable, ref __result);

    [HarmonyPatch(typeof(ShopItem), nameof(ShopItem.IsAvailableNotInfinite), MethodType.Getter)]
    [HarmonyPrefix]
    private static bool Prefix_IsAvailableNotInfinite_get(ShopItem __instance, ref bool __result) => Mod.Override(__instance, i => i.IsAvailableNotInfinite, ref __result);

    [HarmonyPatch(typeof(ShopItem), nameof(ShopItem.IsPurchased), MethodType.Getter)]
    [HarmonyPrefix]
    private static bool Prefix_IsPurchased_get(ShopItem __instance, ref bool __result) => Mod.Override(__instance, i => i.IsPurchased, ref __result);

    [HarmonyPatch(typeof(ShopItem), nameof(ShopItem.ItemSprite), MethodType.Getter)]
    [HarmonyPrefix]
    private static bool Prefix_ItemSprite_get(ShopItem __instance, ref Sprite __result) => Mod.Override(__instance, i => i.ItemSprite, ref __result);

    [HarmonyPatch(typeof(ShopItem), nameof(ShopItem.SetPurchased))]
    [HarmonyPrefix]
    private static bool Prefix_SetPurchased(ShopItem __instance, Action onComplete, int subItemIndex) => Mod.Override(__instance, i => i.SetPurchased(onComplete, subItemIndex));

    private static class Mod
    {
        internal static bool Override(ShopMenuStock self, Action<ModShopMenuStock> a)
        {
            if (!self.gameObject.TryGetComponent<ModShopMenuStock>(out var modded)) return true;

            a(modded);
            return false;
        }

        internal static bool Override<T>(ShopMenuStock self, Func<ModShopMenuStock, T> f, ref T result)
        {
            if (!self.gameObject.TryGetComponent<ModShopMenuStock>(out var modded)) return true;

            result = f(modded);
            return false;
        }

        internal static bool Override(ShopItem self, Action<ModShopItem> a)
        {
            if (self is not ModShopItem modded) return true;

            a(modded);
            return false;
        }

        internal static bool Override<T>(ShopItem self, Func<ModShopItem, T> f, ref T result)
        {
            if (self is not ModShopItem modded) return true;

            result = f(modded);
            return false;
        }
    }
}
