using ItemChanger.Placements;
using ItemChanger.Silksong.Placements;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.Locations;

// Shakra map locations are single-cost placements.
public class ShakraMapLocation : ShakraShopLocation
{
    public override Placement Wrap() => new ShopFlexiblePlacement(Name)
    {
        Location = this,
        Cost = GetTag<CostTag>()?.Cost ?? DefaultCostTag.GetDefaultCost(this),
    };
}
