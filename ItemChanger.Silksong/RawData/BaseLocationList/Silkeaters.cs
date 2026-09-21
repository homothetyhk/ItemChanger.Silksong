using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Tags;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
    // public static Location Silkeater__Bilewater => TODO();

    // public static Location Silkeater__Blasted_Steps => TODO();

    // public static Location Silkeater__Choral_Chambers_East => TODO();

    // public static Location Silkeater__Choral_Chambers_West => TODO();

    // public static Location Silkeater__Deep_Docks => TODO();

    // public static Location Silkeater__Greymoor => TODO();

    public static Location Silkeater__Queen_s_Egg => new StyxCocoonLocation()
    {
        SceneName = SceneNames.Dust_11,
        Name = LocationNames.Silkeater__Queen_s_Egg,
        FarmLevel = 2,
    }.WithTag(new SilkgrubCocoonControlTag() { Info = new() { IgnoreOpenState = true } });

    public static Location Silkeater__Styx => new StyxCocoonLocation()
    {
        SceneName = SceneNames.Dust_11,
        Name = LocationNames.Silkeater__Styx,
        FarmLevel = 1,
    }.WithTag(new SilkgrubCocoonControlTag() { Info = new() { IgnoreOpenState = true } });

    // public static Location Silkeater__Whiteward => TODO();

    // public static Location Silkeater__Whispering_Vaults => TODO();
}
