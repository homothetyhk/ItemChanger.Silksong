using ItemChanger.Locations;
using ItemChanger.Silksong.Locations;
using Benchwarp.Data;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
    public static Location Mossberry_Stew => new MossDruidStewLocation
    {
        SceneName = SceneNames.Mosstown_02c,
        Name = LocationNames.Mossberry_Stew,
    };
}
