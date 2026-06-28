using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Tags;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Costs;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Locations.MultiLocationEnums;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
    public static Location Druid_s_Eye => new MossDruidMix1Location
    {
        Name = LocationNames.Druid_s_Eye,
        SceneName = SceneNames.Mosstown_02c,
        FlingType = Enums.FlingType.DirectDeposit,
        PreviewIndex = 0,
    }.WithTag(new DefaultCostTag { Cost = new MossberryCost { Value = 3 } });

    public static Location Druid_s_Eyes => new MossDruidMix2Location
    {
        Name = LocationNames.Druid_s_Eyes,
        SceneName = SceneNames.Mosstown_02c,
        FlingType = Enums.FlingType.DirectDeposit,
        PreviewIndex = 4,
    }.WithTag(new DefaultCostTag { Cost = new MossberryCost { Value = 7 } });

    public static Location Tacks => new MultiLocation<QuestCompletedOrAct3State>()
    {
        Name = LocationNames.Tacks,
        SceneName = SceneNames.Dust_Shack,

        Selector = new QuestCompletedOrAct3StateProvider() { Quest = Quests.Roach_Killing },
        Locations = new Dictionary<QuestCompletedOrAct3State, Location>()
        {
            [QuestCompletedOrAct3State.QuestIncomplete] = new BenjinAndCrullTacksLocation()
            {
                Name = LocationNames.Tacks,
                SceneName = SceneNames.Dust_Shack,
            },
            [QuestCompletedOrAct3State.QuestComplete] = new CoordinateLocation()
            {
                Name = LocationNames.Steel_Spines,
                SceneName = SceneNames.Dust_Shack,
                X = 15.94f,
                Y = 6.57f,
                Managed = false,
            },
            [QuestCompletedOrAct3State.Act3] = new ObjectLocation()
            {
                Name = LocationNames.Tacks,
                SceneName = SceneNames.Dust_Shack,
                ObjectName = "Collectable Item Dustpilo",
                FlingType = Enums.FlingType.Everywhere,
                Correction = default,
                Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
            }
        }
    };
}
