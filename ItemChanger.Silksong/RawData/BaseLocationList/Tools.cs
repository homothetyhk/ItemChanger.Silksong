using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Tags;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Costs;
using ItemChanger.Silksong.RawData;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
    public static Location Druid_s_Eye => new MossDruidTool1Location
    {
        Name = LocationNames.Druid_s_Eye,
        SceneName = SceneNames.Mosstown_02c,
    }.WithTag(new ImplicitCostTag { Cost = CollectableItemCost.Mossberries(3), Inherent = true });

    public static Location Druid_s_Eyes => new MossDruidTool2Location
    {
        Name = LocationNames.Druid_s_Eyes,
        SceneName = SceneNames.Mosstown_02c,
    }.WithTag(new ImplicitCostTag { Cost = CollectableItemCost.Mossberries(1), Inherent = true });
}
