using ItemChanger.Locations;
using ItemChanger.Placements;
using ItemChanger.Silksong.Placements;

namespace ItemChanger.Silksong.Locations;

public class SkynxShopLocation : Location
{
    protected override void DoLoad() { }

    protected override void DoUnload() { }

    public override Placement Wrap() => new SkynxShopPlacement(Name) { Location = this };
}
