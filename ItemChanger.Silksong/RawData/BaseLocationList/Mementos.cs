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
        Test = new PlacementVisitStateBool(
            placementName: LocationNames.Hunter_s_Memento,
            requiredFlags: VisitState.ObtainedAnyItem,
            missingPlacementTest: null
        ),
        TrueLocation = new CoordinateLocation()
        {
            SceneName = SceneNames.Halfway_01,
            Name = LocationNames.Hunter_s_Memento,
            X = 33.66f,
            Y = 20.57f,
            Managed = false,
        },
        FalseLocation = new NuuMememtoLocation()
        {
            SceneName = SceneNames.Halfway_01,
            Name = LocationNames.Hunter_s_Memento
        }
    };
}