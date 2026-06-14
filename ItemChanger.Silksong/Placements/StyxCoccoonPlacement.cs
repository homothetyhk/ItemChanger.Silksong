using ItemChanger.Locations;
using ItemChanger.Placements;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Modules;

namespace ItemChanger.Silksong.Placements;

public class StyxCocoonPlacement(string Name) : Placement(Name), IPrimaryLocationPlacement
{
    public required StyxCocoonLocation Location;

    Location IPrimaryLocationPlacement.Location => Location;

    protected override void DoLoad()
    {
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
