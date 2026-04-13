using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Tags;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Costs;
using ItemChanger.Silksong.RawData;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
    public static Location Druid_s_Eye => new MossDruidMix1Location
    {
        Name = LocationNames.Druid_s_Eye,
        SceneName = SceneNames.Mosstown_02c,
    }.WithTag(new DefaultCostTag { Cost = new MossberryCost { Value = 3 } });

    public static Location Druid_s_Eyes => new MossDruidMix2Location
    {
        Name = LocationNames.Druid_s_Eyes,
        SceneName = SceneNames.Mosstown_02c,
    }.WithTag(new DefaultCostTag { Cost = new MossberryCost { Value = 7 } });
}
