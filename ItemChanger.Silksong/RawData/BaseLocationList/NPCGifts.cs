using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.Locations;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
    public static Location Hermit_s_Soul => new BellHermitLocation
    {
        SceneName = SceneNames.Belltown_basement_03,
        Name = LocationNames.Hermit_s_Soul,
    };
}