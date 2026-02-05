using Benchwarp.Data;
using ItemChanger.Enums;
using ItemChanger.Locations;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.Tags;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
    private const float SLEEPING_ELEVATION = -0.29f;  // should this be + or - ?
    private const float BARREL_ELEVATION = 0f;
    private const float ANT_CAGE_ELEVATION = 0.67f;
    private const float CITADELCAGE_ELEVATION = 3.42f;

    private static Location CreateFleaLocation(
        string name,
        string sceneName,
        string objectName,
        FleaContainerType fleaType,
        // Null => I haven't checked what it should be
        // This should be set to non-null if the location is replaceable
        float? elevation = null,
        bool replaceable = true,
        FlingType flingType = FlingType.Everywhere
        )
    {
        if (elevation == null && replaceable)
        {
            ItemChangerPlugin.Instance.Logger.LogWarning($"Location {name} needs its elevation checked!");
        }

        Location loc = new ObjectLocation()
        {
            Name = name,
            SceneName = sceneName,
            ObjectName = objectName,
            FlingType = flingType,
            Correction = new UnityEngine.Vector3(0, elevation ?? 0, 0),
            Tags = [
                new VanillaFleaTag(),
                new OriginalFleaTypeTag() { FleaContainerType = fleaType }
            ]
        };

        if (!replaceable)
        {
            loc.AddTag(new OriginalContainerTag() { ContainerType = ContainerNames.Flea, Force = true });
        }

        return loc;
    }

    public static Location Flea__dust_12 => CreateFleaLocation(
        LocationNames.Flea__dust_12,
        SceneNames.Dust_12,
        "Flea Rescue Sleeping",
        FleaContainerType.Sleeping,
        elevation: SLEEPING_ELEVATION
    );

    public static Location Flea__greymoor_06 => CreateFleaLocation(
        LocationNames.Flea__greymoor_06,
        SceneNames.Greymoor_06,
        "Flea Rescue Sleeping",
        FleaContainerType.Sleeping,
        elevation: SLEEPING_ELEVATION
    );

    public static Location Flea__song_11 => CreateFleaLocation(
        LocationNames.Flea__song_11,
        SceneNames.Song_11,
        "Flea Rescue Sleeping",
        FleaContainerType.Sleeping,
        elevation: SLEEPING_ELEVATION
        );

    public static Location Flea__bone_east_05 => CreateFleaLocation(
        LocationNames.Flea__bone_east_05,
        SceneNames.Bone_East_05,
        "Flea Rescue Barrel",
        FleaContainerType.Barrel,
        elevation: BARREL_ELEVATION
        );

    public static Location Flea__crawl_06 => CreateFleaLocation(
        LocationNames.Flea__crawl_06,
        SceneNames.Crawl_06,
        "Aspid Collector (3)",
        FleaContainerType.Aspid,
        replaceable: false
        );

    public static Location Flea__belltown_04 => CreateFleaLocation(
        LocationNames.Flea__belltown_04,
        SceneNames.Belltown_04,
        "Bell Wall Flea/Flea Rescue Generic",
        FleaContainerType.GenericWall,
        replaceable: false
    ).WithTag(new DestroyOnContainerReplaceTag() { ObjectPath = "Bell Wall Flea/Bell Wall Tall (5)" })
     .WithTag(new DeactivateIfPlacementCheckedTag() { ObjectName = "Bell Wall Flea/Bell Wall Tall (5)", SceneName = SceneNames.Belltown_04 });

    // TODO - make sure this works (false location is replaced) post-IC.Core v0.5.0
    public static Location Flea__dock_16 => new DualLocation()
    {
        Name = LocationNames.Flea__dock_16,
        TrueLocation = CreateFleaLocation(
            LocationNames.Flea__dock_16,
            SceneNames.Dock_16,
            "Flea Rescue BlackSilk",
            FleaContainerType.BlackSilk,
            replaceable: false
        ),
        FalseLocation = CreateFleaLocation(
            LocationNames.Flea__dock_16,
            SceneNames.Dock_16,
            "Flea Rescue Sleeping",
            FleaContainerType.Sleeping,
            elevation: SLEEPING_ELEVATION
        ),
        Test = new PDBool(nameof(PlayerData.blackThreadWorld))
    };

    public static Location Flea__bone_east_10_church => new DualLocation()
    {
        Name = LocationNames.Flea__bone_east_10_church,
        TrueLocation = CreateFleaLocation(
            LocationNames.Flea__bone_east_10_church,
            SceneNames.Bone_East_10_Church,
            "Black Thread States Thread Only Variant/Black Thread World/Flea Rescue BlackSilk",
            FleaContainerType.BlackSilk,
            replaceable: false
        ),
        FalseLocation = CreateFleaLocation(
            LocationNames.Flea__bone_east_10_church,
            SceneNames.Bone_East_10_Church,
            "Black Thread States Thread Only Variant/Normal World/Flea Rescue Sleeping",
            FleaContainerType.Sleeping,
            elevation: SLEEPING_ELEVATION
        ),
        Test = new PDBool(nameof(PlayerData.blackThreadWorld))
    };

    public static Location Flea__shadow_28 => CreateFleaLocation(
        LocationNames.Flea__shadow_28,
        SceneNames.Shadow_28,
        "Rosary Thief Control/Flea Rescue Scene/Flea Rescue Scared",
        FleaContainerType.Scared,
        replaceable: false
        );

    public static Location Flea__ant_03 => CreateFleaLocation(
        LocationNames.Flea__ant_03,
        SceneNames.Ant_03,
        "Flea Rescue Cage",
        FleaContainerType.AntCage,
        elevation: ANT_CAGE_ELEVATION
        );

    public static Location Flea__slab_cell => CreateFleaLocation(
        LocationNames.Flea__slab_cell,
        SceneNames.Slab_Cell,
        "Flea Slab Cage",
        FleaContainerType.SlabCage,
        replaceable: false
        );
    // TODO - add tag for audio

    public static Location Flea__dust_09 => CreateFleaLocation(
        LocationNames.Flea__dust_09,
        SceneNames.Dust_09,
        "Flea Rescue Branches",
        FleaContainerType.Branches,
        replaceable: false
        );

    public static Location Flea__library_01 => CreateFleaLocation(
        LocationNames.Flea__library_01,
        SceneNames.Library_01,
        "Flea Rescue CitadelCage",
        FleaContainerType.CitadelCage,
        elevation: CITADELCAGE_ELEVATION
        );

    public static Location Flea__greymoor_15b => CreateFleaLocation(
        LocationNames.Flea__greymoor_15b,
        SceneNames.Greymoor_15b,
        "Flea Rescue Barrel",
        FleaContainerType.Barrel,
        elevation: BARREL_ELEVATION
        );

    public static Location Flea__dock_03d => CreateFleaLocation(
        LocationNames.Flea__dock_03d,
        SceneNames.Dock_03d,
        "Flea Rescue Sleeping",
        FleaContainerType.Sleeping,
        elevation: SLEEPING_ELEVATION
        );

    public static Location Flea__under_21 => CreateFleaLocation(
        LocationNames.Flea__under_21,
        SceneNames.Under_21,
        "Flea Rescue Barrel",
        FleaContainerType.Barrel,
        elevation: BARREL_ELEVATION
        );

    public static Location Flea__shadow_10 => CreateFleaLocation(
        LocationNames.Flea__shadow_10,
        SceneNames.Shadow_10,
        "Flea Rescue Branches",
        FleaContainerType.Branches,
        replaceable: false
        );

    public static Location Flea__coral_35 => CreateFleaLocation(
        LocationNames.Flea__coral_35,
        SceneNames.Coral_35,
        "Flea Rescue Sleeping",
        FleaContainerType.Sleeping,
        elevation: SLEEPING_ELEVATION
        );

    public static Location Flea__coral_24 => CreateFleaLocation(
        LocationNames.Flea__coral_24,
        SceneNames.Coral_24,
        "Flea Rescue Generic",
        FleaContainerType.GenericWall,
        replaceable: false
        );

    public static Location Flea__song_14 => CreateFleaLocation(
        LocationNames.Flea__song_14,
        SceneNames.Song_14,
        "Flea Rescue CitadelCage",
        FleaContainerType.CitadelCage,
        elevation: CITADELCAGE_ELEVATION
        );

    public static Location Flea__under_23 => CreateFleaLocation(
        LocationNames.Flea__under_23,
        SceneNames.Under_23,
        "Flea Rescue Barrel",
        FleaContainerType.Barrel,
        elevation: BARREL_ELEVATION
        );

    public static Location Flea__library_09 => CreateFleaLocation(
        LocationNames.Flea__library_09,
        SceneNames.Library_09,
        "Flea Rescue Sleeping",
        FleaContainerType.Sleeping,
        elevation: SLEEPING_ELEVATION
        );

    public static Location Flea__bone_east_17b => CreateFleaLocation(
        LocationNames.Flea__bone_east_17b,
        SceneNames.Bone_East_17b,
        "Flea Scene/Flea Rescue Cage",
        FleaContainerType.AntCage,
        elevation: ANT_CAGE_ELEVATION
        );

    public static Location Flea__bone_06 => CreateFleaLocation(
        LocationNames.Flea__bone_06,
        SceneNames.Bone_06,
        "Flea Rescue Branches",
        FleaContainerType.Branches,
        replaceable: false
        );

    public static Location Flea__shellwood_03 => CreateFleaLocation(
        LocationNames.Flea__shellwood_03,
        SceneNames.Shellwood_03,
        "Flea Rescue Branches",
        FleaContainerType.Branches,
        replaceable: false
        );

    public static Location Flea__slab_06 => CreateFleaLocation(
        LocationNames.Flea__slab_06,
        SceneNames.Slab_06,
        "Flea Rescue Sleeping",
        FleaContainerType.Sleeping,
        elevation: SLEEPING_ELEVATION
        );

    public static Location Flea__peak_05c => CreateFleaLocation(
        LocationNames.Flea__peak_05c,
        SceneNames.Peak_05c,
        "Snowflake Chunk - Flea",
        FleaContainerType.Ice,
        replaceable: false
        );
}

file static class Ext
{
    public static Location WithTag(this Location self, Tag t)
    {
        self.AddTag(t);
        return self;
    }
}
