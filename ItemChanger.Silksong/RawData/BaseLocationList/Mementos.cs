using Benchwarp.Data;
using ItemChanger.Enums;
using ItemChanger.Locations;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Locations;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
    public static Location Hunter_s_Memento => new DualLocation()
    {
        SceneName = SceneNames.Halfway_01,
        Name = LocationNames.Hunter_s_Memento,
        Test = new PlacementVisitStateBool()
        {
            PlacementName = LocationNames.Hunter_s_Memento,
            RequiredFlags = VisitState.ObtainedAnyItem
        },
        TrueLocation = new CoordinateLocation()
        {
            SceneName = SceneNames.Halfway_01,
            Name = LocationNames.Hunter_s_Memento,
            X = 33.66f,
            Y = 20.57f,
            Managed = false,
        },
        FalseLocation = new NuuMementoLocation()
        {
            RequiredBossKills = 30,
            SceneName = SceneNames.Halfway_01,
            Name = LocationNames.Hunter_s_Memento
        }
    };
}