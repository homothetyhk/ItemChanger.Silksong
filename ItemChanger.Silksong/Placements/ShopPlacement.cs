using ItemChanger.Costs;
using ItemChanger.Items;
using ItemChanger.Locations;
using ItemChanger.Placements;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Modules.ShopsModule;
using ItemChanger.Silksong.RawData;
using ItemChanger.Tags;
using Newtonsoft.Json;

namespace ItemChanger.Silksong.Placements;

public abstract class ShopPlacement(string Name) : Placement(Name), IPrimaryLocationPlacement
{
    public required ShopLocation Location;

    [JsonIgnore]
    public string BaseShopName => Location.BaseShopName;

    [JsonIgnore]
    public BaseShop BaseShop => Location.BaseShop;

    [JsonIgnore]
    Location IPrimaryLocationPlacement.Location => Location;

    /// <summary>
    /// Split the contents of this placement into line-items to be displayed in a shop UI.
    /// </summary>
    public abstract IEnumerable<(IEnumerable<Item> items, Cost? cost)> GetItemsWithCosts();

    protected override void DoLoad()
    {
        Location.Placement = this;
        Location.LoadOnce();

        SilksongHost.Instance.ActiveProfile?.Modules.GetOrAdd<ShopsModule>()?.RegisterPlacement(this);
    }

    protected override void DoUnload()
    {
        SilksongHost.Instance.ActiveProfile?.Modules.Get<ShopsModule>()?.UnregisterPlacement(this);

        Location.UnloadOnce();
        Location.Placement = null;
    }
}

/// <summary>
/// A shop placement that applies costs individually to each contained item.
/// </summary>
public class ShopMultiPlacement(string Name) : ShopPlacement(Name), IMultiCostPlacement
{
    public override IEnumerable<(IEnumerable<Item> items, Cost? cost)> GetItemsWithCosts()
    {
        foreach (var item in Items) yield return ([item], item.GetTag<CostTag>()?.Cost);
    }
}

/// <summary>
/// A shop placement that groups all of its items under a single cost, rather than selling them under individual costs.
/// </summary>
public class ShopFlexiblePlacement(string Name) : ShopPlacement(Name), ISingleCostPlacement
{
    public Cost? Cost { get; set; }

    public override IEnumerable<(IEnumerable<Item> items, Cost? cost)> GetItemsWithCosts() => [(Items, Cost)];
}
