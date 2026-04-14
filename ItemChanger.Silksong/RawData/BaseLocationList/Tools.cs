using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Tags;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Costs;
using ItemChanger.Silksong.RawData;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
    public static Location Pollip_Pouch => new GreyrootPollipLocation
    {
        Name = LocationNames.Pollip_Pouch,
        SceneName = SceneNames.Room_Witch,
    };
}
