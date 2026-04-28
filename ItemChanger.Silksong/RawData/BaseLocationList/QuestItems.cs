using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.Locations;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
    public static Location Diving_Bell_Key => new BallowLocation()
    {
        SceneName = SceneNames.Dock_12,
        Name = LocationNames.Diving_Bell_Key,
    };
}