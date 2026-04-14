using HarmonyLib;
using ItemChanger.Items;
using ItemChanger.Modules;
using ItemChanger.Silksong.Placements;
using ItemChanger.Silksong.RawData;
using Silksong.UnityHelper.Extensions;

namespace ItemChanger.Silksong.Modules.ShopsModule;

public class ShopsModule : Module
{
    // Any vanilla shop items that match this set will be removed.
    public HashSet<DefaultShopItems> RemovedCategories = [DefaultShopItems.StolenGoods];

    public bool IncludeCategory(DefaultShopItems category) => !RemovedCategories.Contains(category);

    private readonly Dictionary<string, HashSet<ShopPlacement>> shopPlacements = [];
    private readonly HashSet<string> suppressedPDBools = [];

    public void RegisterPlacement(ShopPlacement placement)
    {
        foreach (var pdBool in placement.Location.SuppressedPDBools) suppressedPDBools.Add(pdBool);

        var shopName = placement.BaseShopName;
        if (shopPlacements.TryGetValue(shopName, out var placements)) placements.Add(placement);
        else shopPlacements.Add(shopName, [placement]);
    }

    public void UnregisterPlacement(ShopPlacement placement)
    {
        foreach (var pdBool in placement.Location.SuppressedPDBools) suppressedPDBools.Remove(pdBool);

        var shopName = placement.BaseShopName;
        if (shopPlacements.TryGetValue(shopName, out var placements) && placements.Remove(placement) && placements.Count == 0) shopPlacements.Remove(shopName);
    }

    internal void OnSpawnUpdateShop(ShopOwnerBase shopOwner)
    {
        if (ShopOwnerBase._spawnedShop == null || !BaseShopList.TryGetBaseShop(shopOwner, out var baseShop) || !shopPlacements.ContainsKey(baseShop.Name)) return;

        var modStock = ShopOwnerBase._spawnedShop.gameObject.GetOrAddComponent<ModShopMenuStock>();
        modStock.Module = this;
        modStock.BaseShop = baseShop;
        modStock.BuildItemList();
    }

    internal IEnumerable<(Item item, ShopPlacement placement)> ICShopItems(BaseShop baseShop)
    {
        if (!shopPlacements.TryGetValue(baseShop.Name, out var placements))
            return [];
        else
            return placements.OrderBy(p => p.Name).SelectMany(p => p.Items.Select(i => (i, p)));
    }

    private Harmony? harmony;

    protected override void DoLoad()
    {
        harmony = new(typeof(ShopsPatches).FullName);
        harmony.PatchAll(typeof(ShopsPatches));

        PrepatcherPlugin.PlayerDataVariableEvents<bool>.OnGetVariable += SuppressPDBools;
    }

    protected override void DoUnload()
    {
        PrepatcherPlugin.PlayerDataVariableEvents<bool>.OnGetVariable -= SuppressPDBools;

        harmony?.UnpatchSelf();
        harmony = null;
    }

    private bool SuppressPDBools(PlayerData pd, string name, bool current) => current && !suppressedPDBools.Contains(name);
}
