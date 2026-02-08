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
                new OriginalContainerTag() { ContainerType = ContainerNames.Flea, Force = !replaceable, LowPriority = replaceable },
                new VanillaFleaTag(),
                new OriginalFleaTypeTag() { FleaContainerType = fleaType }
            ]
        };

        return loc;
    }

    public static Location Flea__dust_12 => CreateFleaLocation(
        LocationNames.Flea__Sinner_s_Road,
        SceneNames.Dust_12,
        "Flea Rescue Sleeping",
        FleaContainerType.Sleeping,
        elevation: SLEEPING_ELEVATION
    );

    public static Location Flea__greymoor_06 => CreateFleaLocation(
        LocationNames.Flea__Greymoor_West,
        SceneNames.Greymoor_06,
        "Flea Rescue Sleeping",
        FleaContainerType.Sleeping,
        elevation: SLEEPING_ELEVATION
    );

    public static Location Flea__song_11 => CreateFleaLocation(
        LocationNames.Flea__Songclave_Wind_Column,
        SceneNames.Song_11,
        "Flea Rescue Sleeping",
        FleaContainerType.Sleeping,
        elevation: SLEEPING_ELEVATION
        );

    public static Location Flea__bone_east_05 => CreateFleaLocation(
        LocationNames.Flea__Docks_Upper,
        SceneNames.Bone_East_05,
        "Flea Rescue Barrel",
        FleaContainerType.Barrel,
        elevation: BARREL_ELEVATION
        );

    public static Location Flea__crawl_06 => CreateFleaLocation(
        LocationNames.Flea__Wormways_Aknid,
        SceneNames.Crawl_06,
        "Aspid Collector (3)",
        FleaContainerType.Aspid,
        replaceable: false
        );

    public static Location Flea__belltown_04 => CreateFleaLocation(
        LocationNames.Flea__Bellhart_Upper,
        SceneNames.Belltown_04,
        "Bell Wall Flea/Flea Rescue Generic",
        FleaContainerType.GenericWall,
        replaceable: false
    ).WithTag(new DestroyOnContainerReplaceTag() { ObjectPath = "Bell Wall Flea/Bell Wall Tall (5)" })
     .WithTag(new DeactivateIfPlacementCheckedTag() { ObjectName = "Bell Wall Flea/Bell Wall Tall (5)", SceneName = SceneNames.Belltown_04 });

    public static Location Flea__dock_16 => new DualLocation()
    {
        Name = LocationNames.Flea__Docks_Bellway,
        TrueLocation = CreateFleaLocation(
            LocationNames.Flea__Docks_Bellway,
            SceneNames.Dock_16,
            "Flea Rescue BlackSilk",
            FleaContainerType.BlackSilk,
            replaceable: false
        ),
        FalseLocation = CreateFleaLocation(
            LocationNames.Flea__Docks_Bellway,
            SceneNames.Dock_16,
            "Flea Rescue Sleeping",
            FleaContainerType.Sleeping,
            elevation: SLEEPING_ELEVATION
        ),
        Test = new PDBool(nameof(PlayerData.blackThreadWorld))
    };

    public static Location Flea__bone_east_10_church => new DualLocation()
    {
        Name = LocationNames.Flea__Pilgrim_s_Rest,
        TrueLocation = CreateFleaLocation(
            LocationNames.Flea__Pilgrim_s_Rest,
            SceneNames.Bone_East_10_Church,
            "Black Thread States Thread Only Variant/Black Thread World/Flea Rescue BlackSilk",
            FleaContainerType.BlackSilk,
            replaceable: false
        ),
        FalseLocation = CreateFleaLocation(
            LocationNames.Flea__Pilgrim_s_Rest,
            SceneNames.Bone_East_10_Church,
            "Black Thread States Thread Only Variant/Normal World/Flea Rescue Sleeping",
            FleaContainerType.Sleeping,
            elevation: SLEEPING_ELEVATION
        ),
        Test = new PDBool(nameof(PlayerData.blackThreadWorld))
    };

    public static Location Flea__shadow_28 => CreateFleaLocation(
        LocationNames.Flea__Bilewater_Lower,
        SceneNames.Shadow_28,
        "Rosary Thief Control/Flea Rescue Scene/Flea Rescue Scared",
        FleaContainerType.Scared,
        replaceable: false
        );

    public static Location Flea__ant_03 => CreateFleaLocation(
        LocationNames.Flea__Hunter_s_March,
        SceneNames.Ant_03,
        "Flea Rescue Cage",
        FleaContainerType.AntCage,
        elevation: ANT_CAGE_ELEVATION
        );

    public static Location Flea__slab_cell => CreateFleaLocation(
        LocationNames.Flea__Slab_Cell,
        SceneNames.Slab_Cell,
        "Flea Slab Cage",
        FleaContainerType.SlabCage,
        elevation: 7.01f
    ).WithTag(new RemoveComponentTag<DeactivateIfPlayerdataTrue>() { SceneName = SceneNames.Slab_13, ObjectName = "Audio Player Flea Distressed" })
     .WithTag(new DeactivateIfPlacementCheckedTag() { SceneName = SceneNames.Slab_13, ObjectName = "Audio Player Flea Distressed" })
     .WithTag(new DeactivateIfUnexpectedContainerTag() { SceneName = SceneNames.Slab_13, ObjectName = "Audio Player Flea Distressed", ExpectedContainerType = ContainerNames.Flea});

    public static Location Flea__dust_09 => CreateFleaLocation(
        LocationNames.Flea__Exhaust_Organ,
        SceneNames.Dust_09,
        "Flea Rescue Branches",
        FleaContainerType.Branches,
        replaceable: false
        );

    public static Location Flea__library_01 => CreateFleaLocation(
        LocationNames.Flea__Whispering_Vaults,
        SceneNames.Library_01,
        "Flea Rescue CitadelCage",
        FleaContainerType.CitadelCage,
        elevation: CITADELCAGE_ELEVATION
        );

    public static Location Flea__greymoor_15b => CreateFleaLocation(
        LocationNames.Flea__Greymoor_Craws,
        SceneNames.Greymoor_15b,
        "Flea Rescue Barrel",
        FleaContainerType.Barrel,
        elevation: BARREL_ELEVATION
        );

    public static Location Flea__dock_03d => CreateFleaLocation(
        LocationNames.Flea__Docks_East,
        SceneNames.Dock_03d,
        "Flea Rescue Sleeping",
        FleaContainerType.Sleeping,
        elevation: SLEEPING_ELEVATION
        );

    public static Location Flea__under_21 => CreateFleaLocation(
        LocationNames.Flea__Underworks_Lava,
        SceneNames.Under_21,
        "Flea Rescue Barrel",
        FleaContainerType.Barrel,
        elevation: BARREL_ELEVATION
        );

    public static Location Flea__shadow_10 => CreateFleaLocation(
        LocationNames.Flea__Bilewater_Upper,
        SceneNames.Shadow_10,
        "Flea Rescue Branches",
        FleaContainerType.Branches,
        replaceable: false
        );

    public static Location Flea__coral_35 => CreateFleaLocation(
        LocationNames.Flea__Blasted_Steps_Upper,
        SceneNames.Coral_35,
        "Flea Rescue Sleeping",
        FleaContainerType.Sleeping,
        elevation: SLEEPING_ELEVATION
        );

    public static Location Flea__coral_24 => CreateFleaLocation(
        LocationNames.Flea__Sands_of_Karak,
        SceneNames.Coral_24,
        "Flea Rescue Generic",
        FleaContainerType.GenericWall,
        replaceable: false
        );

    public static Location Flea__song_14 => CreateFleaLocation(
        LocationNames.Flea__Songclave_West,
        SceneNames.Song_14,
        "Flea Rescue CitadelCage",
        FleaContainerType.CitadelCage,
        elevation: CITADELCAGE_ELEVATION
        );

    public static Location Flea__under_23 => CreateFleaLocation(
        LocationNames.Flea__Underworks_Wisp,
        SceneNames.Under_23,
        "Flea Rescue Barrel",
        FleaContainerType.Barrel,
        elevation: BARREL_ELEVATION
        );

    public static Location Flea__library_09 => CreateFleaLocation(
        LocationNames.Flea__Songclave_East,
        SceneNames.Library_09,
        "Flea Rescue Sleeping",
        FleaContainerType.Sleeping,
        elevation: SLEEPING_ELEVATION
        );

    public static Location Flea__bone_east_17b => CreateFleaLocation(
        LocationNames.Flea__Far_Fields_West,
        SceneNames.Bone_East_17b,
        "Flea Scene/Flea Rescue Cage",
        FleaContainerType.AntCage,
        elevation: ANT_CAGE_ELEVATION
        );

    public static Location Flea__bone_06 => CreateFleaLocation(
        LocationNames.Flea__Marrow_Upper,
        SceneNames.Bone_06,
        "Flea Rescue Branches",
        FleaContainerType.Branches,
        replaceable: false
        );

    public static Location Flea__shellwood_03 => CreateFleaLocation(
        LocationNames.Flea__Shellwood_Central,
        SceneNames.Shellwood_03,
        "Flea Rescue Branches",
        FleaContainerType.Branches,
        replaceable: false
        );

    public static Location Flea__slab_06 => CreateFleaLocation(
        LocationNames.Flea__Slab_Bellway,
        SceneNames.Slab_06,
        "Flea Rescue Sleeping",
        FleaContainerType.Sleeping,
        elevation: SLEEPING_ELEVATION
        );

    public static Location Flea__peak_05c => CreateFleaLocation(
        LocationNames.Flea__Mount_Fay,
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
