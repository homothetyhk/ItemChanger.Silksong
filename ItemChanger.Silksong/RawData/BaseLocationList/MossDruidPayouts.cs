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
    }.WithTag(new ImplicitCostTag { Cost = CollectableItemCost.Mossberries(1), Inherent = true });

    public static Location Moss_Druid_Payout_2 => new MossDruidRosaryLocation
    {
        SceneName = SceneNames.Mosstown_02c,
        Name = LocationNames.Moss_Druid_Payout_2,
        Index = 2,
    }.WithTag(new ImplicitCostTag { Cost = CollectableItemCost.Mossberries(1), Inherent = true });

    public static Location Moss_Druid_Payout_3 => new MossDruidRosaryLocation
    {
        SceneName = SceneNames.Mosstown_02c,
        Name = LocationNames.Moss_Druid_Payout_3,
        Index = 3,
    }.WithTag(new ImplicitCostTag { Cost = CollectableItemCost.Mossberries(1), Inherent = true });
}
