using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.Locations;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
    public static Location Tacks => new BenjinAndCrullTacksLocation()
    {
        Name = LocationNames.Tacks,
        SceneName = SceneNames.Dust_Shack,
    };
}
