using HarmonyLib;
using ItemChanger.Modules;
using ItemChanger.Silksong.Placements;
using ItemChanger.Silksong.RawData;
using Silksong.FsmUtil;
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
        modStock.BaseStock = ShopOwnerBase._spawnedShop;
        modStock.BuildItemList();
    }

    internal IEnumerable<ShopPlacement> ShopPlacements(BaseShop baseShop) => shopPlacements.TryGetValue(baseShop.Name, out var placements) ? placements.OrderBy(p => p.Name) : [];

    private readonly Harmony harmony = new(typeof(ShopsPatches).FullName);

    protected override void DoLoad()
    {
        harmony.PatchAll(typeof(ShopsPatches));
        PrepatcherPlugin.PlayerDataVariableEvents<bool>.OnGetVariable += SuppressPDBools;
        Using(new FsmEditGroup() { { new("*", "*", "shop_control"), SetVisitState } });
    }

    protected override void DoUnload()
    {
        harmony.UnpatchSelf();
        PrepatcherPlugin.PlayerDataVariableEvents<bool>.OnGetVariable -= SuppressPDBools;
    }

    private void SetVisitState(PlayMakerFSM fsm)
    {
        var afterShopUp = fsm.MustGetState("Stock?");
        afterShopUp.InsertMethod(0, action =>
        {
            if (action.Fsm.GameObject.TryGetComponent<ModShopMenuStock>(out var modStock)) modStock.SetPreviewed();
        });
    }

    private bool SuppressPDBools(PlayerData pd, string name, bool current) => current && !suppressedPDBools.Contains(name);
}
