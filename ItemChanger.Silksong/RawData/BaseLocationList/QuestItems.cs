using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Serialization;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
    public static Location Hermit_s_Soul => new DualLocation()
    {
        Name = LocationNames.Hermit_s_Soul,
        TrueLocation = new CoordinateLocation()
        {
            Name = "Bell Hermit act 3 shiny",
            SceneName = SceneNames.Belltown_basement_03,
            X = 97.38f,
            Y = 102.57f,
            Managed = false,
        },
        FalseLocation = new BellHermitLocation()
        {
            SceneName = SceneNames.Belltown_basement_03,
            Name = LocationNames.Hermit_s_Soul,
        },
        Test = new Disjunction(
            new PDBool(nameof(PlayerData.blackThreadWorld)), new PDBool(nameof(PlayerData.soulSnareReady))
        ),
    };

    public static Location Diving_Bell_Key => new BallowLocation()
    {
        SceneName = SceneNames.Dock_12,
        Name = LocationNames.Diving_Bell_Key,
    };
}
