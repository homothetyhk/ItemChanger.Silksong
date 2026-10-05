using ItemChanger.Locations;
using ItemChanger.Placements;
using ItemChanger.Silksong.Placements;

namespace ItemChanger.Silksong.Locations;

public class StyxCocoonLocation : Location
{
    public required int FarmLevel;

    protected override void DoLoad() { }

    protected override void DoUnload() { }

    public override Placement Wrap() => new StyxCocoonPlacement(Name) { Location = this };
}
