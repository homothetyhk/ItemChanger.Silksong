using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Tags;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Costs;
using ItemChanger.Silksong.RawData;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
    public static Location Moss_Druid_Payout_1 => new MossDruidRosaryLocation
    {
        SceneName = SceneNames.Mosstown_02c,
        Name = LocationNames.Moss_Druid_Payout_1,
        Index = 1,
        FlingType = Enums.FlingType.DirectDeposit,
        PreviewIndex = 1,
    }.WithTag(new DefaultCostTag { Cost = new MossberryCost { Value = 4 } });

    public static Location Moss_Druid_Payout_2 => new MossDruidRosaryLocation
    {
        SceneName = SceneNames.Mosstown_02c,
        Name = LocationNames.Moss_Druid_Payout_2,
        Index = 2,
        FlingType = Enums.FlingType.DirectDeposit,
        PreviewIndex = 2,
    }.WithTag(new DefaultCostTag { Cost = new MossberryCost { Value = 5 } });

    public static Location Moss_Druid_Payout_3 => new MossDruidRosaryLocation
    {
        SceneName = SceneNames.Mosstown_02c,
        Name = LocationNames.Moss_Druid_Payout_3,
        Index = 3,
        FlingType = Enums.FlingType.DirectDeposit,
        PreviewIndex = 3,
    }.WithTag(new DefaultCostTag { Cost = new MossberryCost { Value = 6 } });
}
