using ItemChanger.Locations;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Tags;

namespace ItemChanger.Silksong.RawData;

// Scene + object data sourced from Br3zzly/silksong-completionist (spoolFragments.ts).
// ObjectName is "Silk Spool" for all world-pickup entries.
// "Silk Spool" objects are NOT CollectableItemPickup — FloatInPlace prevents the shiny falling.
// Shop fragments (Frey, Grindle, Jubilana) are handled via shop locations.
// Quest fragment (Sherma/"Balm for The Wounded") is handled separately.
// Flea Caravan fragment is a fleamaster reward — handled separately.
internal static partial class BaseLocationList
{
    // Bone_11b is "Bone Bottom: Above the Bone Bottom settlement"
    public static Location Spool_Fragment__Mosshome => new ObjectLocation
    {
        Name = LocationNames.Spool_Fragment__Mosshome,
        SceneName = "Bone_11b",
        ObjectName = "Silk Spool",
        Correction = default, // can fall
    };

    // Bone_East_13 is in the Deep Docks approach from Far Fields
    public static Location Spool_Fragment__Deep_Docks_East => new ObjectLocation
    {
        Name = LocationNames.Spool_Fragment__Deep_Docks_East,
        SceneName = "Bone_East_13",
        ObjectName = "Silk Spool",
        Correction = default, // can fall
    };

    public static Location Spool_Fragment__Greymoor => new ObjectLocation
    {
        Name = LocationNames.Spool_Fragment__Greymoor,
        SceneName = "Greymoor_02",
        ObjectName = "Silk Spool",
        Correction = default, // can fall
    };

    // Peak_01 is The Slab: frosty section towards the left
    public static Location Spool_Fragment__The_Slab => new ObjectLocation
    {
        Name = LocationNames.Spool_Fragment__The_Slab,
        SceneName = "Peak_01",
        ObjectName = "Silk Spool",
        Correction = default, // can fall
    };

    public static Location Spool_Fragment__Weavenest_Atla => new ObjectLocation
    {
        Name = LocationNames.Spool_Fragment__Weavenest_Atla,
        SceneName = "Weave_11",
        ObjectName = "Silk Spool",
        Correction = default,
        Tags = [FloatShiny] // can float
    };

    public static Location Spool_Fragment__Cogwork_Core => new ObjectLocation
    {
        Name = LocationNames.Spool_Fragment__Cogwork_Core,
        SceneName = "Cog_07",
        ObjectName = "Silk Spool",
        Correction = default, // can fall
    };

    // Library_11b is labeled Underworks (bottom-right hidden area)
    public static Location Spool_Fragment__Underworks_Spikes => new ObjectLocation
    {
        Name = LocationNames.Spool_Fragment__Underworks_Spikes,
        SceneName = "Library_11b",
        ObjectName = "Silk Spool",
        Correction = default, // can fall
    };

    // Song_19_entrance is the Grand Gate scene (top of Choral Chambers entrance)
    public static Location Spool_Fragment__Grand_Gate => new ObjectLocation
    {
        Name = LocationNames.Spool_Fragment__Grand_Gate,
        SceneName = "Song_19_entrance",
        ObjectName = "Silk Spool",
        Correction = default, // can float because it would be obstructed - if we care about that
    };

    // Underworks: behind arena battle
    public static Location Spool_Fragment__Underworks_Arena => new ObjectLocation
    {
        Name = LocationNames.Spool_Fragment__Underworks_Arena,
        SceneName = "Under_10",
        ObjectName = "Silk Spool",
        Correction = default, // can fall
    };

    // Whiteward: bottom of the elevator shaft
    public static Location Spool_Fragment__Whiteward => new ObjectLocation
    {
        Name = LocationNames.Spool_Fragment__Whiteward,
        SceneName = "Ward_01",
        ObjectName = "Silk Spool",
        Correction = default, // can fall
    };

    public static Location Spool_Fragment__Deep_Docks_West => new ObjectLocation
    {
        Name = LocationNames.Spool_Fragment__Deep_Docks_West,
        SceneName = "Dock_03c",
        ObjectName = "Silk Spool",
        Correction = default, // can fall
    };

    public static Location Spool_Fragment__High_Halls => new ObjectLocation
    {
        Name = LocationNames.Spool_Fragment__High_Halls,
        SceneName = "Hang_03_top",
        ObjectName = "Silk Spool",
        Correction = default,
        Tags = [FloatShiny] // can float
    };

    public static Location Spool_Fragment__Memorium => new ObjectLocation
    {
        Name = LocationNames.Spool_Fragment__Memorium,
        SceneName = "Arborium_09",
        ObjectName = "Silk Spool",
        Correction = default, // can fall
    };
}
