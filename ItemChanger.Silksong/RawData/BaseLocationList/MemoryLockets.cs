using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.Tags;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
    public static Location Memory_Locket__Bellhart_BellhomeCiel => new ObjectLocation
    {
        Name = LocationNames.Memory_Locket__Bellhart_BellhomeCiel,
        SceneName = SceneNames.Belltown,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = new(0, -1.7f),
        Tags = [
            new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true },
            new DeactivateObjectTag
            {
                SceneName = SceneNames.Belltown,
                ObjectName = "pilgrim_corpse_0002_2",
                Test = new UnexpectedContainerBool { PlacementName = LocationNames.Memory_Locket__Bellhart_BellhomeCiel, ExpectedContainerType = ContainerNames.Shiny }
            },
            SmallFleaPrefabs
        ]
    };

    public static Location Memory_Locket__Bilewater_Cocoon_Corpse => new BilewaterCorpseSackLocation
    {
        Name = LocationNames.Memory_Locket__Bilewater_Cocoon_Corpse,
        SceneName = SceneNames.Shadow_27,
        SackObjectName = "Breakable Hang Sack Memory Locket/Active/Sack",
        ShinyObjectName = "Breakable Hang Sack Memory Locket/Corpse Pilgrim 03/Sack Corpse Pickup",
        FlingType = Enums.FlingType.Everywhere
    };

    public static Location Memory_Locket__Bilewater_Hidden_Room_West_Bench => new ObjectLocation
    {
        Name = LocationNames.Memory_Locket__Bilewater_Hidden_Room_West_Bench,
        SceneName = SceneNames.Shadow_20,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.StraightUp,
        Correction = new(0, -0.6f),
        Tags = [
            new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true },
            new DeactivateObjectTag
            {
                SceneName = SceneNames.Shadow_20,
                ObjectName = "bone_deep_0127_n (36)",
                Test = new UnexpectedContainerBool { PlacementName = LocationNames.Memory_Locket__Bilewater_Hidden_Room_West_Bench, ExpectedContainerType = ContainerNames.Shiny }
            },
            SmallFleaPrefabs
        ]
    };

    public static Location Memory_Locket__Blasted_Steps => new ObjectLocation
    {
        Name = LocationNames.Memory_Locket__Blasted_Steps,
        SceneName = SceneNames.Coral_02,
        ObjectName = "Collectable Item Pickup (1)",
        FlingType = Enums.FlingType.DirectDeposit,
        Correction = new(0, -1.5f),
        Tags = [
            new OriginalContainerTag { ContainerType = ContainerNames.Shiny },
            new DeactivateObjectTag
            {
                SceneName = SceneNames.Coral_02,
                ObjectName = "pilgrim_corpse_0001_3 (10)",
                Test = new UnexpectedContainerBool { PlacementName = LocationNames.Memory_Locket__Blasted_Steps, ExpectedContainerType = ContainerNames.Shiny }
            },
            SmallFleaPrefabs
        ]
    };

    public static Location Memory_Locket__Choral_Chambers => new ObjectLocation
    {
        Name = LocationNames.Memory_Locket__Choral_Chambers,
        SceneName = SceneNames.Bellway_City,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = new(0, -1.5f),
        Tags = [
            new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true },
            new DeactivateObjectTag
            {
                SceneName = SceneNames.Bellway_City,
                ObjectName = "sleep_set_small/SP_sleep_state_type_01 (7)",
                Test = new UnexpectedContainerBool { PlacementName = LocationNames.Memory_Locket__Choral_Chambers, ExpectedContainerType = ContainerNames.Shiny }
            }
        ]
    };

    public static Location Memory_Locket__Deep_Docks => new ObjectLocation
    {
        Name = LocationNames.Memory_Locket__Deep_Docks,
        SceneName = SceneNames.Dock_13,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.DirectDeposit,
        Correction = new(0, -1.1f),
        Tags = [new OriginalContainerTag { ContainerType = ContainerNames.Shiny }]
    };

    public static Location Memory_Locket__Far_Fields_Secret => new ObjectLocation
    {
        Name = LocationNames.Memory_Locket__Far_Fields_Secret,
        SceneName = SceneNames.Bone_East_25,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = new(0, -1.5f),
        Tags = [
            new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true },
            new DeactivateObjectTag
            {
                SceneName = SceneNames.Bone_East_25,
                ObjectName = "Black_Thread_Core__0017_pilgrim",
                Test = new UnexpectedContainerBool { PlacementName = LocationNames.Memory_Locket__Far_Fields_Secret, ExpectedContainerType = ContainerNames.Shiny }
            }
        ]
    };

    public static Location Memory_Locket__Greymoor_HH => new ObjectLocation
    {
        Name = LocationNames.Memory_Locket__Greymoor_HH,
        SceneName = SceneNames.Halfway_01,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = new(0, -1.4f),
        Tags = [new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true }, SmallFleaPrefabs]
    };

    public static Location Memory_Locket__Greymoor_Sewer => new ObjectLocation
    {
        Name = LocationNames.Memory_Locket__Greymoor_Sewer,
        SceneName = SceneNames.Greymoor_16,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = new(0, -1.5f),
        Tags = [
            new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true },
            new DeactivateObjectTag
            {
                SceneName = SceneNames.Greymoor_16,
                ObjectName = "catcher_corpses_0002_1",
                Test = new UnexpectedContainerBool { PlacementName = LocationNames.Memory_Locket__Greymoor_Sewer, ExpectedContainerType = ContainerNames.Shiny }
            },
            SmallFleaPrefabs
        ]
    };

    public static Location Memory_Locket__Hunters_March => new DualLocation
    {
        Name = LocationNames.Memory_Locket__Hunters_March,
        Test = new SDBool(SceneNames.Ant_20, "Enemy Break Cage (2)"),
        FalseLocation = new DelayedShinyLocation
        {
            SceneName = SceneNames.Ant_20,
            Name = LocationNames.Memory_Locket__Hunters_March,
            ObjectName = "Enemy Break Cage (2)/Corpse/Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            GiveEarly = new BoxedBool { Value = true }
        },
        TrueLocation = new CoordinateLocation
        {
            SceneName = SceneNames.Ant_20,
            Name = LocationNames.Memory_Locket__Hunters_March,
            X = 147.39f,
            Y = 13.79f,
            Managed = false,
            Tags = [new UnsupportedContainerTag { ContainerType = ContainerNames.Chest }]
        }
    };

    public static Location Memory_Locket__Memorium => new ObjectLocation
    {
        Name = LocationNames.Memory_Locket__Memorium,
        SceneName = SceneNames.Arborium_05,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.StraightUp,
        Correction = new(0, -1.5f),
        Tags = [
            new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true },
            new DeactivateObjectTag
            {
                SceneName = SceneNames.Arborium_05,
                ObjectName = "SM_death_shellwood",
                Test = new UnexpectedContainerBool { PlacementName = LocationNames.Memory_Locket__Memorium, ExpectedContainerType = ContainerNames.Shiny }
            },
            SmallFleaPrefabs
        ]
    };

    public static Location Memory_Locket__Sands_of_Karak => new ObjectLocation
    {
        Name = LocationNames.Memory_Locket__Sands_of_Karak,
        SceneName = SceneNames.Coral_23,
        ObjectName = "GameObject (3)/Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = new(0, -1.3f),
        Tags = [
            new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true },
            new DeactivateObjectTag
            {
                SceneName = SceneNames.Coral_23,
                ObjectName = "corpses_pilgrim_sand_0003_1",
                Test = new UnexpectedContainerBool { PlacementName = LocationNames.Memory_Locket__Sands_of_Karak, ExpectedContainerType = ContainerNames.Shiny }
            },
            SmallFleaPrefabs
        ]
    };

    public static Location Memory_Locket__The_Marrow => new DualLocation
    {
        Name = LocationNames.Memory_Locket__The_Marrow,
        Test = new PDBool(nameof(PlayerData.blackThreadWorld)),
        FalseLocation = new ObjectLocation
        {
            SceneName = SceneNames.Bone_18,
            Name = LocationNames.Memory_Locket__The_Marrow,
            ObjectName = "Quest Scene/Final Encounter/Battle Scene/Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [
                new OriginalContainerTag { ContainerType = ContainerNames.Shiny, Force = true },
                new RemoveComponentTag<DeactivateIfPlayerdataFalse>  // Scene turns off if the player doesn't have claw.
                {
                    SceneName = SceneNames.Bone_18,
                    ObjectName = "Quest Scene/Final Encounter/Battle Scene"
                }
            ]
        },
        TrueLocation = new ObjectLocation
        {
            SceneName = SceneNames.Bone_18,
            Name = LocationNames.Memory_Locket__The_Marrow,
            ObjectName = "Black Thread States/Black Thread World/Battle Scene/Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag { ContainerType = ContainerNames.Shiny, Force = true }]
        }
    };

    public static Location Memory_Locket__The_Slab => new ObjectLocation
    {
        Name = LocationNames.Memory_Locket__The_Slab,
        SceneName = SceneNames.Slab_Cell_Quiet,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = new(0, -1.4f),
        Tags = [new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true }]
    };

    public static Location Memory_Locket__Underworks_Confessional => new ObjectLocation
    {
        Name = LocationNames.Memory_Locket__Underworks_Confessional,
        SceneName = SceneNames.Under_08,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = new(-0.6f, -1.25f),
        Tags = [
            new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true },
            new DeactivateObjectTag
            {
                SceneName = SceneNames.Under_08,
                ObjectName = "US_death0005",
                Test = new UnexpectedContainerBool { PlacementName = LocationNames.Memory_Locket__Underworks_Confessional, ExpectedContainerType = ContainerNames.Shiny }
            },
            SmallFleaPrefabs
        ]
    };

    public static Location Memory_Locket__Whispering_Vaults => new ObjectLocation
    {
        Name = LocationNames.Memory_Locket__Whispering_Vaults,
        SceneName = SceneNames.Library_08,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.DirectDeposit,
        Correction = default,
        Tags = [new OriginalContainerTag { ContainerType = ContainerNames.Shiny, Force = true }]
    };

    public static Location Memory_Locket__Wormways => new ObjectLocation
    {
        Name = LocationNames.Memory_Locket__Wormways,
        SceneName = SceneNames.Crawl_09,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = new(0, -1.5f),
        Tags = [
            new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true },
            new DeactivateObjectTag
            {
                SceneName = SceneNames.Crawl_09,
                ObjectName = "corpses_pilgrim_sand_0000_1",
                Test = new UnexpectedContainerBool { PlacementName = LocationNames.Memory_Locket__Wormways, ExpectedContainerType = ContainerNames.Shiny }
            },
            SmallFleaPrefabs
        ]
    };
}
