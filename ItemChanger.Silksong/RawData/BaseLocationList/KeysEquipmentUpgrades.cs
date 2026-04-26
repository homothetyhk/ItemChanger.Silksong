using Benchwarp.Data;
using ItemChanger.Enums;
using ItemChanger.Locations;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Locations;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
    public static Location Tool_Pouch__Nuu => new DualLocation()
    {
        SceneName = SceneNames.Halfway_01,
        Name = LocationNames.Tool_Pouch__Nuu,
        Test = new PlacementVisitStateBool(
            placementName: LocationNames.Tool_Pouch__Nuu,
            requiredFlags: VisitState.ObtainedAnyItem,
            missingPlacementTest: null
        ),
        TrueLocation = new CoordinateLocation()
        {
            SceneName = SceneNames.Halfway_01,
            Name = LocationNames.Tool_Pouch__Nuu,
            X = 29.01f,
            Y = 20.57f,
            Managed = false,
        },
        FalseLocation = new NuuToolPouchLocation()
        {
            SceneName = SceneNames.Halfway_01,
            Name = LocationNames.Tool_Pouch__Nuu
        }
    };
}