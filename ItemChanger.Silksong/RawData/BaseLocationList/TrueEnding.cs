using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Serialization;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
    public static Location Everbloom => new DualLocation()
    {
        Name = LocationNames.Everbloom,
        Test = new PDBool(nameof(PlayerData.CompletedRedMemory)),
        FalseLocation = new EverbloomLocation()
        {
            SceneName = SceneNames.Memory_Red,
            Name = LocationNames.Everbloom,
        },
        // Spawn in the ruined chapel if the memory has already been completed.
        TrueLocation = new CoordinateLocation()
        {
            SceneName = SceneNames.Tut_04,
            Name = LocationNames.Everbloom,
            X = 41.5f,
            Y = 7f,
            Managed = false,
        }
    };
}