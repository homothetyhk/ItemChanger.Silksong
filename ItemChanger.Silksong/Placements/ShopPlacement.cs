using ItemChanger.Locations;
using ItemChanger.Placements;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Modules.ShopsModule;
using ItemChanger.Silksong.RawData;
using Newtonsoft.Json;

namespace ItemChanger.Silksong.Placements;

public class ShopPlacement(string Name) : Placement(Name), IMultiCostPlacement, IPrimaryLocationPlacement
{
    public required ShopLocation Location;

    [JsonIgnore]
    public string BaseShopName => Location.BaseShopName;

    [JsonIgnore]
    public BaseShop BaseShop => Location.BaseShop;

    [JsonIgnore]
    Location IPrimaryLocationPlacement.Location => Location;

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
