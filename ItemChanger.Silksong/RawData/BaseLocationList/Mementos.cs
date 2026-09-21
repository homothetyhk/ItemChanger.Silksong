using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Serialization;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
    public static Location Hunter_s_Memento => new DualLocation()
    {
        SceneName = SceneNames.Halfway_01,
        Name = LocationNames.Hunter_s_Memento,
        Test = new PDBool(nameof(PlayerData.nuuMementoAwarded)),
        TrueLocation = new CoordinateLocation()
        {
            SceneName = SceneNames.Halfway_01,
            Name = LocationNames.Hunter_s_Memento,
            X = 26.92f,
            Y = 20.57f,
            Managed = false,
            ForceDefaultContainer = true,
        },
        FalseLocation = new NuuMementoLocation()
        {
            RequiredBossKills = 30,
            SceneName = SceneNames.Halfway_01,
            Name = LocationNames.Hunter_s_Memento
        }
    };

    public static Location Surface_Memento => new DelayedShinyLocation
    {
        Name = LocationNames.Surface_Memento,
        SceneName = SceneNames.Abandoned_town,
        ObjectName = "Memory Group/abandoned_town_memento_dropper/collectable item fall",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        GiveEarly = new Negation { Bool = new SDBool(SceneNames.Abandoned_town, "Memory Group") }
    };
}