using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Locations;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
    public static Location Craw_Summons => new CrawSummonsLocation()
    {
        Name = LocationNames.Craw_Summons,
        FlingType = Enums.FlingType.Everywhere,
        ObjectName = "craw_court_summons_pin",
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.CrawSummons }]
    };
  
    public static Location Diving_Bell_Key => new BallowLocation()
    {
        SceneName = SceneNames.Dock_12,
        Name = LocationNames.Diving_Bell_Key,
    };

    public static Location Bellhome_Key => new PavoLocation()
    {
        SceneName = SceneNames.Belltown,
        Name = LocationNames.Bellhome_Key,
    };
}