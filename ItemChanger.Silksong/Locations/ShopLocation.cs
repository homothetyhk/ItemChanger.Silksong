using ItemChanger.Locations;
using ItemChanger.Placements;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Modules.ShopsModule;
using ItemChanger.Silksong.Placements;
using ItemChanger.Silksong.RawData;
using ItemChanger.Tags;
using Newtonsoft.Json;

namespace ItemChanger.Silksong.Locations;

public class ShopLocation : Location
{
    /// <summary>
    /// The name in BaseShopList identifying this shop.
    /// </summary>
    public required string BaseShopName;

    /// <summary>
    /// If specified, the inventory at this location is not available for purchase unless `Test` evaluates to true.
    /// </summary>
    public IValueProvider<bool>? Test = null;

    /// <summary>
    /// If set, player data bools with these names will always view as false.
    /// </summary>
    public List<string> SuppressedPDBools = [];

    /// <summary>
    /// Shop locations with the same BaseShopName are sorted first by priority (descending), then by name (ascending) in the shop menu.
    /// Vanilla inventory is always sorted last.
    /// </summary>
    public IValueProvider<int>? Priority = null;

    [JsonIgnore]
    public BaseShop BaseShop => BaseShopList.TryGetBaseShop(BaseShopName, out var shop) ? shop : throw new KeyNotFoundException($"{BaseShopName}");

    protected override void DoLoad() { }

    protected override void DoUnload() { }

    public override Placement Wrap() => new ShopMultiPlacement(Name) { Location = this };
}

// Shakra locations require the ShakraShopsModule.
public class ShakraShopLocation : ShopLocation
{
    protected override void DoLoad()
    {
        base.DoLoad();
        SilksongHost.Instance.ActiveProfile?.Modules.GetOrAdd<ShakraModule>();
    }
}

// Shakra map locations are single-cost placements.
public class ShakraMapLocation : ShakraShopLocation
{
    public override Placement Wrap() => new ShopFlexiblePlacement(Name)
    {
        Location = this,
        Cost = GetTag<CostTag>()?.Cost ?? DefaultCostTag.GetDefaultCost(this),
    };
}
