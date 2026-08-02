using ItemChanger.Costs;
using ItemChanger.Locations;
using ItemChanger.Placements;
using ItemChanger.Silksong.Costs;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Modules;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.Placements;

public class SkynxShopPlacement(string Name) : Placement(Name), IPrimaryLocationPlacement, IMultiCostPlacement
{
    public required SkynxShopLocation Location;

    Location IPrimaryLocationPlacement.Location => Location;

    protected override void DoLoad()
    {
        foreach (var item in Items)
        {
            if (item.GetTag<CostTag>()?.Cost is Cost c && c is not SilkeaterCost) throw new ArgumentException("SkynxShopPlacement only supports SilkeaterCosts");
        }

        Location.Placement = this;
        Location.LoadOnce();

        ActiveProfile!.Modules.GetOrAdd<StyxAndSkynxModule>().RegisterPlacement(this);
    }

    protected override void DoUnload()
    {
        ActiveProfile!.Modules.Get<StyxAndSkynxModule>()?.UnregisterPlacement(this);

        Location.UnloadOnce();
        Location.Placement = null;
    }
}
