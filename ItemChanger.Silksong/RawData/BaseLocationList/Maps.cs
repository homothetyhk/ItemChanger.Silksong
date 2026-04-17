using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Serialization;

namespace ItemChanger.Silksong.RawData;

// TODO: When https://github.com/homothetyhk/ItemChanger.Silksong/pull/159 is merged, add definitions for maps given by InteractEvents.
// TODO: Add map locations for Shakra maps (https://github.com/homothetyhk/ItemChanger.Silksong/issues/180).
internal static partial class BaseLocationList
{
    // Given by "/weaver_harp_sign_map/Get Map Inspect" in Abyss_12.  Controlled by `InteractEvents` component.
    // public static Location Map__Abyss => TODO();

    public static Location Map__Choral_Chambers => new MapMachineLocation()
    {
        SceneName = SceneNames.Song_01b,
        Name = LocationNames.Map__Choral_Chambers,
        ObjectName = "Map Machine (2)",
    };

    // Given by "/Group/Collectable Item Pickup Child" in Cog_Bench.  Controlled by `InteractEvents` component.
    // public static Location Map__Cogwork_Core => TODO();

    public static Location Map__Cradle => new DualLocation()
    {
        Name = LocationNames.Map__Cradle,
        Test = new PDBool(nameof(PlayerData.blackThreadWorld)),
        FalseLocation = new MapMachineLocation()
        {
            SceneName = SceneNames.Cradle_02,
            Name = LocationNames.Map__Cradle,
            ObjectName = "Map Machine (1)",
        },
        TrueLocation = new ObjectLocation()
        {
            SceneName = SceneNames.Tube_Hub,
            Name = LocationNames.Map__Cradle,
            ObjectName = "/Black Thread States/Black Thread World/Collectable Item Pickup",
            Correction = new(0, 1f, 0),
            FlingType = Enums.FlingType.Everywhere,
        },
    };

    public static Location Map__Grand_Gate => new MapMachineLocation()
    {
        SceneName = SceneNames.Song_19_entrance,
        Name = LocationNames.Map__Grand_Gate,
        ObjectName = "Map Machine (1)",
    };

    public static Location Map__High_Halls => new MapMachineLocation()
    {
        SceneName = SceneNames.Hang_06b,
        Name = LocationNames.Map__High_Halls,
        ObjectName = "Map Machine",
    };

    public static Location Map__Memorium => new MapMachineLocation()
    {
        SceneName = SceneNames.Arborium_11,
        Name = LocationNames.Map__Memorium,
        ObjectName = "Map Machine",
    };

    // Given by "/Aqueduct Map Inspect" in Aqueduct_07.  Controlled by `InteractEvents` component.
    // public static Location Map__Putrefied_Ducts => TODO();

    // Given by "Slab Map Inspect" in Slab_20.  Controlled by `InteractEvents` component.
    // public static Location Map__Slab => TODO();

    // Given by "/map_collectable/Understore Map Inspect" in Under_16.  Controlled by `InteractEvents` component.
    // public static Location Map__Underworks => TODO();

    // Given by "/weaver_harp_sign_map/Get Map Inspect" in Weave_12.  Controlled by `InteractEvents` component.
    // public static Location Map__Weavenest_Atla => TODO();

    public static Location Map__Whispering_Vaults => new MapMachineLocation()
    {
        SceneName = SceneNames.Library_04,
        Name = LocationNames.Map__Whispering_Vaults,
        ObjectName = "Map Machine (1)",
    };

    public static Location Map__Whiteward => new MapMachineLocation()
    {
        SceneName = SceneNames.Ward_01,
        Name = LocationNames.Map__Whiteward,
        ObjectName = "Map Machine (1)",
    };

    public static Location Map__Verdania => new ObjectLocation()
    {
        SceneName = SceneNames.Clover_20,
        Name = LocationNames.Map__Verdania,
        ObjectName = "/Collectable Item Pickup",
        Correction = new(0, 1f, 0),
        FlingType = Enums.FlingType.Everywhere,
    };
}
