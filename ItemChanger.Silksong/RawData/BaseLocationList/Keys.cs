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
}