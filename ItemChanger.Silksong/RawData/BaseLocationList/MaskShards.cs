using ItemChanger.Locations;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Tags;

namespace ItemChanger.Silksong.RawData;

// Scene + object data sourced from Br3zzly/silksong-completionist (maskShards.ts).
// ObjectName is "Heart Piece" (or "Heart Piece (1)" for Lava Arena) for all world-pickup entries.
// "Heart Piece" objects are NOT CollectableItemPickup — they are game-specific interactables.
// IC replaces them with a new shiny; FloatInPlace prevents the shiny falling off ceiling shards.
// Quest-reward shards (Gurr, Dark_Hearts, Savage_Beastfly, Sprintmaster) are handled separately.
// Shop shards (Pebb, Jubilana) are handled via shop locations.
internal static partial class BaseLocationList
{
    public static Location Mask_Shard__Bilewater => new ObjectLocation
    {
        Name = LocationNames.Mask_Shard__Bilewater,
        SceneName = "Shadow_13",
        ObjectName = "Heart Piece",
        Correction = default, // can fall
    };

    public static Location Mask_Shard__Blasted_Steps => new ObjectLocation
    {
        Name = LocationNames.Mask_Shard__Blasted_Steps,
        SceneName = "Coral_19b",
        ObjectName = "Heart Piece",
        Correction = default, // had it fall
    };

    // Mount Fay: Top of Brightvein
    public static Location Mask_Shard__Brightvein => new ObjectLocation
    {
        Name = LocationNames.Mask_Shard__Brightvein,
        SceneName = "Peak_06",
        ObjectName = "Heart Piece",
        Correction = default,
        Tags = [ContactFloat] // stay floating
    };

    // Cogwork Core: Top of left-most tunnel after arena battle
    public static Location Mask_Shard__Cogwork_Core => new ObjectLocation
    {
        Name = LocationNames.Mask_Shard__Cogwork_Core,
        SceneName = "Song_09",
        ObjectName = "Heart Piece",
        Correction = new(0f, 1.2f),
        Tags = [InteractFloat] // float to fit to nearby scenary
    };

    public static Location Mask_Shard__Deep_Docks => new ObjectLocation
    {
        Name = LocationNames.Mask_Shard__Deep_Docks,
        SceneName = "Dock_08",
        ObjectName = "Heart Piece",
        Correction = default, // can fall
    };

    public static Location Mask_Shard__Far_Fields => new ObjectLocation
    {
        Name = LocationNames.Mask_Shard__Far_Fields,
        SceneName = "Bone_East_20",
        ObjectName = "Heart Piece",
        Correction = default, // can fall
    };

    // Far Fields: Top of Skull Cavern lava arena — uses "Heart Piece (1)" object path
    public static Location Mask_Shard__Lava_Arena => new ObjectLocation
    {
        Name = LocationNames.Mask_Shard__Lava_Arena,
        SceneName = "Bone_East_LavaChallenge",
        ObjectName = "Heart Piece (1)",
        Correction = default, // can fall
    };

    public static Location Mask_Shard__Mount_Fay => new ObjectLocation
    {
        Name = LocationNames.Mask_Shard__Mount_Fay,
        SceneName = "Peak_04c",
        ObjectName = "Heart Piece",
        Correction = default,
        Tags = [ContactFloat] //can stay floating
    };

    public static Location Mask_Shard__Shellwood => new ObjectLocation
    {
        Name = LocationNames.Mask_Shard__Shellwood,
        SceneName = "Shellwood_14",
        ObjectName = "Heart Piece",
        Correction = default, // can fall
    };

    public static Location Mask_Shard__The_Slab => new BreakableWithDummyLocation // TODO: broken
    {
        Name = LocationNames.Mask_Shard__The_Slab,
        SceneName = "Slab_17",
        BreakablePath = "Slab Chain cage_small_break/lamp/Active",
        ObjectName = "Heart Piece",
        DummyPath = "Slab Chain cage_small_break/lamp/Active/Heart Piece Dummy",
        ReplaceWholePart = true,
    };

    // Behind a breakable wall in Weavenest Atla
    public static Location Mask_Shard__Weavenest_Atla => new ObjectLocation
    {
        Name = LocationNames.Mask_Shard__Weavenest_Atla,
        SceneName = "Weave_05b",
        ObjectName = "Heart Piece",
        Correction = default, // can fall
    };

    public static Location Mask_Shard__Whispering_Vaults => new BreakableWithDummyLocation
    {
        Name = LocationNames.Mask_Shard__Whispering_Vaults,
        SceneName = "Library_05",
        BreakablePath = "library_glass_heart_piece/Active/End/Lamp_Full",
        ObjectName = "Heart Piece",
        DummyPath = "library_glass_heart_piece/Active/End/Lamp_Full/mask sprite",
        ReplaceWholePart = false,
    };

    public static Location Mask_Shard__Wisp_Thicket => new ObjectLocation
    {
        Name = LocationNames.Mask_Shard__Wisp_Thicket,
        SceneName = "Wisp_07",
        ObjectName = "Heart Piece",
        Correction = new(0.2f, 1.4f),
        Tags = [InteractFloat] // float to fit to nearby scenary and improve visibility
    };

    public static Location Mask_Shard__Wormways => new ObjectLocation
    {
        Name = LocationNames.Mask_Shard__Wormways,
        SceneName = "Crawl_02",
        ObjectName = "Heart Piece",
        Correction = default, // can fall
    };
}
