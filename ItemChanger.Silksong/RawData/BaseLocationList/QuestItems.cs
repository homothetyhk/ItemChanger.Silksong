using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Serialization;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
    public static Location Maiden_s_Soul => new DualLocation()
    {
        Name = LocationNames.Maiden_s_Soul,
        SceneName = SceneNames.Bonetown,
        Test = new Disjunction(
            new PDBool(nameof(PlayerData.blackThreadWorld)), new PDBool(nameof(PlayerData.soulSnareReady))
        ),
        TrueLocation = new CoordinateLocation()
        {
            Name = "Chapel Maid act 3 shiny",
            SceneName = SceneNames.Bonetown,
            X = 72.07f,
            Y = 7.56f,
            Managed = false,
        },
        FalseLocation = new ChapelMaidLocation()
        {
            SceneName = SceneNames.Bonetown,
            Name = LocationNames.Maiden_s_Soul,
        },
    };
}