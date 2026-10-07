using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Modules;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.Tags;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
    public static Location Tool_Pouch__Nuu => new DualLocation()
    {
        SceneName = SceneNames.Halfway_01,
        Name = LocationNames.Tool_Pouch__Nuu,
        Test = new PDBool(nameof(PlayerData.nuuMementoAwarded)),
        TrueLocation = new CoordinateLocation()
        {
            SceneName = SceneNames.Halfway_01,
            Name = LocationNames.Tool_Pouch__Nuu,
            X = 27f,
            Y = 20.57f,
            Managed = false,
            ForceDefaultContainer = true,
        },
        FalseLocation = new NuuToolPouchLocation()
        {
            RequiredBossKills = 10,
            SceneName = SceneNames.Halfway_01,
            Name = LocationNames.Tool_Pouch__Nuu,
            Tags = [
                new RequiredModuleTag<NuuIsHomeOverride>(),
                ]
        }
    };

    public static Location Hunter_s_Journal => new DualLocation()
    {
        SceneName = SceneNames.Halfway_01,
        Name = LocationNames.Hunter_s_Journal,
        Test = new PDBool(nameof(PlayerData.nuuMementoAwarded)),
        TrueLocation = new CoordinateLocation()
        {
            SceneName = SceneNames.Halfway_01,
            Name = LocationNames.Hunter_s_Journal,
            X = 29f,
            Y = 20.57f,
            Managed = false,
            ForceDefaultContainer = true,
        },
        FalseLocation = new NuuJournalLocation()
        {
            SceneName = SceneNames.Halfway_01,
            Name = LocationNames.Hunter_s_Journal,
            Tags = [
                new RequiredModuleTag<NuuIsHomeOverride>()
                ]
        },
    };

}