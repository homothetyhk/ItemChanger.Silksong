using ItemChanger.Locations;
using ItemChanger.Silksong.Locations;
using Benchwarp.Data;
using ItemChanger.Enums;
using ItemChanger.Serialization;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
    public static Location Crest_of_Wanderer => new CrestCorpseLocation
    {
        SceneName = SceneNames.Chapel_Wanderer,
        Name = LocationNames.Crest_of_Wanderer,
    };

    public static Location Crest_of_Reaper => new CrestCorpseLocation
    {
        SceneName = SceneNames.Greymoor_20c,
        Name = LocationNames.Crest_of_Reaper,
    };

    public static Location Crest_of_Beast => new CrestCorpseLocation
    {
        SceneName = SceneNames.Ant_19,
        Name = LocationNames.Crest_of_Beast,
    };

    public static Location Crest_of_Architect => new CrestCorpseLocation
    {
        SceneName = SceneNames.Under_20,
        Name = LocationNames.Crest_of_Architect,
    };

    public static Location Crest_of_Shaman => new CrestCorpseLocation
    {
        SceneName = SceneNames.Tut_05,
        Name = LocationNames.Crest_of_Shaman,
    };

    public static Location Crest_of_Witch => new YarnabyLocation
    {
        SceneName = SceneNames.Belltown_Room_doctor,
        Name = LocationNames.Crest_of_Witch,
    };

    public static Location Eva => new EvaLocation
    {
        SceneName = SceneNames.Weave_10,
        Name = LocationNames.Eva,
    };

    public static Location Elegy_of_the_Deep => new DualLocation()
    {
        SceneName = SceneNames.Tut_04,
        Name = LocationNames.Elegy_of_the_Deep,
        Test = new PlacementVisitStateBool(
            placementName: LocationNames.Elegy_of_the_Deep,
            requiredFlags: VisitState.ObtainedAnyItem,
            missingPlacementTest: null
        ),
        TrueLocation = new CoordinateLocation()
        {
            SceneName = SceneNames.Tut_04,
            Name = LocationNames.Elegy_of_the_Deep,
            X = 37.90f,
            Y = 6.57f,
            Managed = false
        },
        FalseLocation = new ElegyOfTheDeepLocation()
        {
            SceneName = SceneNames.Tut_04,
            Name = LocationNames.Elegy_of_the_Deep,
        }
    };
}