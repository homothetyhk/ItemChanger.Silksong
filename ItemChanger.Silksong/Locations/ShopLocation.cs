using ItemChanger.Locations;
using ItemChanger.Placements;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Placements;
using ItemChanger.Silksong.RawData;
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

    [JsonIgnore]
    public BaseShop BaseShop => BaseShopList.TryGetBaseShop(BaseShopName, out var shop) ? shop : throw new KeyNotFoundException($"{BaseShopName}");

    protected override void DoLoad() { }

    protected override void DoUnload() { }

    public override Placement Wrap() => new ShopPlacement(Name) { Location = this };
}
