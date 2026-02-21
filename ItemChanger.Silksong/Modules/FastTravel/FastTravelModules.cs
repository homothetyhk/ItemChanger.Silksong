using GlobalEnums;
using ItemChanger.Modules;

namespace ItemChanger.Silksong.Modules.FastTravel;

public static class FastTravelModules
{
    public static void CreateBellwayModules(this ModuleCollection mods)
    {
        mods.Add<FullMapDisplayModule<FastTravelLocations>>();
        mods.Add<CustomFastTravelLocationsModule<FastTravelLocations>>();
        mods.Add<BellwayAutoUnlockModule>();
        mods.Add(FastTravelSourceModule.BellwayType);
        mods.Add<MarrowBellwayTransitModule>();
    }

    public static void CreateVentricaModules(this ModuleCollection mods)
    {
        mods.Add<FullMapDisplayModule<TubeTravelLocations>>();
        mods.Add<CustomFastTravelLocationsModule<TubeTravelLocations>>();
        mods.Add<VentricaAutoUnlockModule>();
        mods.Add(FastTravelSourceModule.VentricaType);
    }
}
