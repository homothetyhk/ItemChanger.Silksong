using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Locations.MultiLocationEnums;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.Tags;
using ItemChanger.Silksong.Tags.SpecialLocationTags;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.RawData;

// TODO - think about how/whether the soul particles should appear for needolin tabs
// TODO - measure elevation of tablets where possible, and add the appropriate DestroyOnContainerReplace tags
// TODO - custom handling of lib-13b checks? (with plinks at "Desk Inspect and Quill/Group/desk_inspect/inspect plink")
// TODO - Mushroom tablet (most likely a special location)

internal static partial class BaseLocationList
{
    public static Location Lore_Tablet__Abyss_Top => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Abyss_Top,
        SceneName = SceneNames.Abyss_02b,
        ObjectName = "Abyss_lore_stone/Inspect Region",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true }
            ]
    };

    public static Location Lore_Tablet__Abyss_Bottom_Left => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Abyss_Bottom_Left,
        SceneName = SceneNames.Abyss_06,
        ObjectName = "Abyss_lore_stone/Inspect Region",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true }
            ]
    };

    private static Location CreateFleatopiaTabletLocation(string objName, string sceneName)
    {
        return new ObjectLocation()
        {
            Name = LocationNames.Lore_Tablet__Fleatopia_Weaver_Harp,
            SceneName = sceneName,
            ObjectName = objName,
            Correction = default,
            Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true }
            ]
        };
    }

    public static Location Lore_Tablet__Fleatopia_Weaver_Harp => new MultiLocation<FleatopiaState>()
    {
        Name = LocationNames.Lore_Tablet__Fleatopia_Weaver_Harp,
        Selector = new FleatopiaStateProvider(),
        Locations = new Dictionary<FleatopiaState, Location>()
        {
            [FleatopiaState.PreCaravan] = CreateFleatopiaTabletLocation(
                "Caravan_States/Pre_Fleatopia/weaver_harp_sign (1)/Inspect Region", SceneNames.Aqueduct_05_pre),
            [FleatopiaState.Caravan] = CreateFleatopiaTabletLocation(
                "Caravan_States/Fleatopia/weaver_harp_sign (2)/Inspect Region", SceneNames.Aqueduct_05_caravan),
            [FleatopiaState.Festival] = CreateFleatopiaTabletLocation(
                "Caravan_States/Flea_Festival_Intro/weaver_harp_sign (3)/Inspect Region", SceneNames.Aqueduct_05_festival)
                    .WithTag(new FleaFestivalTabletTag()),
        }
    };

    public static Location Lore_Tablet__Memorium_Entrance => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Memorium_Entrance,
        SceneName = SceneNames.Arborium_01,
        ObjectName = "Inspect Region",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true }
            ]
    };

    public static Location Lore_Tablet__Memorium_Orders => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Memorium_Orders,
        SceneName = SceneNames.Arborium_03,
        ObjectName = "Inspect Region",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true },
            new DisableObjectOnCheckTag() { ObjectPath = "inspect plink" },
            new DisableObjectOnCheckTag() { ObjectPath = "inspect plink (1)" },
            ]
    };

    public static Location Lore_Tablet__Bellhart_East => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Bellhart_East,
        SceneName = SceneNames.Belltown_06,
        ObjectName = "Inspect Region",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true },
            new DisableObjectOnCheckTag() { ObjectPath = "inspect plink" },
            ]
    };

    public static Location Lore_Tablet__Bellhart_West => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Bellhart_West,
        SceneName = SceneNames.Belltown_07,
        ObjectName = "Black Thread States Thread Only Variant/Normal World/Inspect Region",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true },
            new DisableObjectOnCheckTag() { ObjectPath = "Black Thread States Thread Only Variant/Normal World/inspect plink" },
            ]
    }.ToAct3DualLocation(57.76f, 7.57f);

    public static Location Lore_Tablet__Marrow_Start => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Marrow_Start,
        SceneName = SceneNames.Bone_01c,
        ObjectName = "Inspect Region",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true },
            new DisableObjectOnCheckTag() { ObjectPath = "inspect plink" },
            ]
    };

    public static Location Lore_Tablet__Marrow_Top => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Marrow_Top,
        SceneName = SceneNames.Bone_18,
        ObjectName = "Inspect Region",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true }
            ]
    };

    public static Location Lore_Tablet__Pilgrims_Rest => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Pilgrims_Rest,
        SceneName = SceneNames.Bone_East_10,
        ObjectName = "Black Thread States Thread Only Variant/Normal World/Group/Inspect Region",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true },
            new DisableObjectOnCheckTag() { ObjectPath = "Black Thread States Thread Only Variant/Normal World/Group/inspect plink" },
            ]
    }.ToAct3DualLocation(30.91f, 4.57f);

    public static Location Lore_Tablet__Weavenest_Cindril => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Weavenest_Cindril,
        SceneName = SceneNames.Bone_East_Weavehome,
        ObjectName = "Group/Inspect Region (2)",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true }
            ]
    };

    public static Location Lore_Tablet__Verdania_End => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Verdania_End,
        SceneName = SceneNames.Clover_10,
        ObjectName = "Plaque_Scene/Plaque/Inspect Region",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true }
            ]
    };

    public static Location Lore_Tablet__Verdania_Fountains => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Verdania_Fountains,
        SceneName = SceneNames.Clover_18,
        ObjectName = "Fountains Group/Plaque/Up/Inspect Region",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true }
            ]
    };

    public static Location Lore_Tablet__Blasted_Steps_Start => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Blasted_Steps_Start,
        SceneName = SceneNames.Coral_19,
        ObjectName = "Black Thread States Thread Only Variant/Normal World/Inspect Region",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true },
            new DisableObjectOnCheckTag() { ObjectPath = "Black Thread States Thread Only Variant/Normal World/inspect plink" },
            ]
    }.ToAct3DualLocation(297.66f, 35.57f);

    public static Location Lore_Tablet__Karak_Entrance => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Karak_Entrance,
        SceneName = SceneNames.Coral_25,
        ObjectName = "Coral Crust Lore Tablet/Interact",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true }
            ]
    };

    public static Location Lore_Tablet__Blasted_Steps_Nursery => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Blasted_Steps_Nursery,
        SceneName = SceneNames.Coral_36,
        ObjectName = "Inspect Region",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true },
            new DisableObjectOnCheckTag() { ObjectPath = "inspect plink" },
            ]
    };

    public static Location Lore_Tablet__Coral_Tower => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Coral_Tower,
        SceneName = SceneNames.Coral_Tower_01,
        ObjectName = "Coral Crust Lore Tablet/Interact",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true }
            ]
    };

    public static Location Lore_Tablet__Cradle_Cage_1 => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Cradle_Cage_1,
        SceneName = SceneNames.Cradle_02b,
        ObjectName = "Inspect Region (1)",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true },
            new DisableObjectOnCheckTag() { ObjectPath = "inspect plink" },
            ]
    };

    public static Location Lore_Tablet__Cradle_Cage_2 => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Cradle_Cage_2,
        SceneName = SceneNames.Cradle_02b,
        ObjectName = "Inspect Region",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true },
            new DisableObjectOnCheckTag() { ObjectPath = "inspect plink (1)" },
            ]
    };

    public static Location Lore_Tablet__Cradle_Cage_3 => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Cradle_Cage_3,
        SceneName = SceneNames.Cradle_02b,
        ObjectName = "Inspect Region (2)",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true },
            new DisableObjectOnCheckTag() { ObjectPath = "inspect plink (2)" },
            ]
    };

    public static Location Lore_Tablet__Greymoor_Orders_Above_Home => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Greymoor_Orders_Above_Home,
        SceneName = SceneNames.Greymoor_03,
        ObjectName = "Inspect Region",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true },
            new DisableObjectOnCheckTag() { ObjectPath = "inspect plink" },
            new DisableObjectOnCheckTag() { ObjectPath = "GameObject/inspect plink (1)" },
            ]
    };

    public static Location Lore_Tablet__Greymoor_Bottom => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Greymoor_Bottom,
        SceneName = SceneNames.Greymoor_16,
        ObjectName = "Inspect Region",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true },
            new DisableObjectOnCheckTag() { ObjectPath = "inspect plink" },
            ]
    };

    /*
     * Note - datamining found a nuu scroll with the following parameters:
     * Location name: Lore_Tablet__Nuu_Scroll
     * SceneName: Halfway_01
     * ObjectName: "_NPCs/Hunter Fan Control/Nuu_Scrolls/Inspect Region"
     * Language: ("Wanderers", "HUNTER_FAN_SCROLL_INSPECT")
     * 
     * I don't believe it's viewable in-game so I'm not putting it in the location list
     */

    public static Location Lore_Tablet__Trobbio_Sign => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Trobbio_Sign,
        SceneName = SceneNames.Library_13,
        ObjectName = "Grand Stage Scene/First_Stage/Post_defeat_set/trobbio_signpost/Inspect Region",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true },
            new DisableObjectOnCheckTag() { ObjectPath = "Grand Stage Scene/First_Stage/Post_defeat_set/trobbio_signpost/inspect plink" },
            ]
    };

    public static Location Lore_Tablet__Trobbio_Notes => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Trobbio_Notes,
        SceneName = SceneNames.Library_13b,
        ObjectName = "Desk Inspect and Quill/Group/desk_inspect/Inspect Region Act 2",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true },
            ]
    };

    public static Location Lore_Tablet__Tormented_Trobbio_Notes => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Tormented_Trobbio_Notes,
        SceneName = SceneNames.Library_13b,
        ObjectName = "Desk Inspect and Quill/Group/desk_inspect/Inspect Region Act 3",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true }
            ]
    };

    public static Location Lore_Tablet__Mosshome_Below_Silkspear => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Mosshome_Below_Silkspear,
        SceneName = SceneNames.Mosstown_02,
        ObjectName = "lore_tablet/moss_bone_plaque/Inspect Region",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true },
            new EnableMossTabletRevisitTag()
            ]
    };

    public static Location Lore_Tablet__Mosshome_Below_Silkspear_Harp => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Mosshome_Below_Silkspear_Harp,
        SceneName = SceneNames.Mosstown_02,
        ObjectName = "lore_tablet/weaver_harp_sign/Inspect Region",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true }
            ]
    };

    public static Location Lore_Tablet__Mount_Fay_Bottom => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Mount_Fay_Bottom,
        SceneName = SceneNames.Peak_10,
        ObjectName = "Inspect Region (2)",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true }
            ]
    };

    public static Location Lore_Tablet__Deep_Docks_Forge => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Deep_Docks_Forge,
        SceneName = SceneNames.Room_Forge,
        ObjectName = "Inspect Region",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true },
            new DisableObjectOnCheckTag() { ObjectPath = "inspect plink" },
            ]
    };

    public static Location Lore_Tablet__Bilewater_Shortcut => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Bilewater_Shortcut,
        SceneName = SceneNames.Shadow_08,
        ObjectName = "Inspect Region",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true },
            new DisableObjectOnCheckTag() { ObjectPath = "inspect plink" },
            ]
    };

    public static Location Lore_Tablet__Bilewater_Above_Groal => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Bilewater_Above_Groal,
        SceneName = SceneNames.Shadow_18,
        ObjectName = "Inspect Region",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true },
            new DisableObjectOnCheckTag() { ObjectPath = "inspect plink" },
            ]
    };

    public static Location Lore_Tablet__Weavenest_Murglin => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Weavenest_Murglin,
        SceneName = SceneNames.Shadow_Weavehome,
        ObjectName = "Inspect Region (1)",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true }
            ]
    };

    public static Location Lore_Tablet__Shellwood_West => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Shellwood_West,
        SceneName = SceneNames.Shellgrave,
        ObjectName = "Black Thread States Thread Only Variant/Normal World/Inspect Region",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true },
            new DisableObjectOnCheckTag() { ObjectPath = "Black Thread States Thread Only Variant/Normal World/inspect plink" },
            ]
    }.ToAct3DualLocation(91.15f, 4.57f);

    public static Location Lore_Tablet__Shellwood_Harp => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Shellwood_Harp,
        SceneName = SceneNames.Shellwood_10,
        ObjectName = "weaver_harp_sign/Inspect Region",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true }
            ]
    };

    public static Location Lore_Tablet__Nyleth => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Nyleth,
        SceneName = SceneNames.Shellwood_11b,
        ObjectName = "Inspect Region",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true },
            new DisableObjectOnCheckTag() { ObjectPath = "inspect plink" },
            ]
    };

    public static Location Lore_Tablet__Slab_Orders_1 => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Slab_Orders_1,
        SceneName = SceneNames.Slab_08,
        ObjectName = "dock_b__0051_lore_sign_hang/Inspect Region",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true },
            new DisableObjectOnCheckTag() { ObjectPath = "dock_b__0051_lore_sign_hang/inspect plink" },
            ]
    };

    public static Location Lore_Tablet__Slab_Orders_2 => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Slab_Orders_2,
        SceneName = SceneNames.Slab_08,
        ObjectName = "dock_b__0051_lore_sign_hang (1)/Inspect Region",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true },
            new DisableObjectOnCheckTag() { ObjectPath = "dock_b__0051_lore_sign_hang (1)/inspect plink" },
            ]
    };

    public static Location Lore_Tablet__First_Sinner => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__First_Sinner,
        SceneName = SceneNames.Slab_10c,
        ObjectName = "Break Gate Group/Group/gate/Inspect Region",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true },
            new SlabTabletShinyTag()
            ]
    };

    public static Location Lore_Tablet__Ventrica_Hub => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Ventrica_Hub,
        SceneName = SceneNames.Tube_Hub,
        ObjectName = "Black Thread States/Normal World/Inspect Region",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true },
            new DisableObjectOnCheckTag() { ObjectPath = "Black Thread States/Normal World/dock_b__0051_lore_sign_hang/inspect plink" },
            ]
    }.ToAct3DualLocation(118.67f, 39.57f);

    public static Location Lore_Tablet__Moss_Grotto_Chapel_Entrance => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Moss_Grotto_Chapel_Entrance,
        SceneName = SceneNames.Tut_04,
        ObjectName = "Inspect Region",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true },
            new DisableObjectOnCheckTag() { ObjectPath = "inspect plink" },
            ]
    };

    public static Location Lore_Tablet__Moss_Grotto_Chapel_Inner => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Moss_Grotto_Chapel_Inner,
        SceneName = SceneNames.Tut_05,
        ObjectName = "Inspect Region (1)",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true }
            ]
    };

    public static Location Lore_Tablet__Whiteward_Oath => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Whiteward_Oath,
        SceneName = SceneNames.Ward_07,
        ObjectName = "Group/Inspect Region",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true },
            new DisableObjectOnCheckTag() { ObjectPath = "Group/inspect plink" },
            ]
    };

    public static Location Lore_Tablet__Weavenest_Atla_East => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Weavenest_Atla_East,
        SceneName = SceneNames.Weave_08,
        ObjectName = "Group/Inspect Region (1)",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true }
            ]
    };

    // Static Journal entries/Materium

    public static Location Journal_Entry__Void_Tendrils => new ObjectLocation()
    {
        Name = LocationNames.Journal_Entry__Void_Tendrils,
        SceneName = SceneNames.Abyss_08,
        ObjectName = "Group (5)/Inspect Region - Void Tendrils",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true },
            new DisableObjectOnCheckTag() { ObjectPath = "inspect plink" },
        ]
    };

    public static Location Materium__Flintstone => new ObjectLocation()
    {
        Name = LocationNames.Materium__Flintstone,
        SceneName = SceneNames.Dock_02,
        ObjectName = "Inspect Region",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true },
            new DisableObjectOnCheckTag() { ObjectPath = "inspect plink" },
        ]
    };

    public static Location Materium__Magnetite => new ObjectLocation()
    {
        Name = LocationNames.Materium__Magnetite,
        SceneName = SceneNames.Peak_05e,
        ObjectName = "Inspect Region",
        Correction = default,
        Tags = [
        new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true },
        new DisableObjectOnCheckTag() { ObjectPath = "float rocks/peak_magnetite_01 (19)/inspect plink" },
        ]
    };

    public static Location Materium__Voltridian => new ObjectLocation()
    {
        Name = LocationNames.Materium__Voltridian,
        SceneName = SceneNames.Coral_29,
        ObjectName = "Inspect Region",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true },
        ]
    };

    /*
     * Roach guts exists but is never active in vanilla, it seems.
     * We could consider adding it as a location, with the following data:
     * Location name: Materium__Roach_Guts
     * SceneName: Dust_05
     * ObjectName: "Black Thread States Thread Only Variant/Black Thread World/Roach Guts Materium Inspect/Inspect Region"
     * Plink: "Black Thread States Thread Only Variant/Black Thread World/Roach Guts Materium Inspect/inspect plink"
     */

    private static DualLocation ToAct3DualLocation(this ObjectLocation orig, float X, float Y)
    {
        return new DualLocation()
        {
            Name = orig.Name,
            FalseLocation = orig,
            TrueLocation = new CoordinateLocation()
            {
                Name = orig.Name,
                SceneName = orig.SceneName,
                Managed = false,
                X = X,
                Y = Y,
                Z = 0,
                ForceDefaultContainer = true,
            },
            Test = new PDBool(nameof(PlayerData.blackThreadWorld))
        };
    }
}
