using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Locations.MultiLocationEnums;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
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