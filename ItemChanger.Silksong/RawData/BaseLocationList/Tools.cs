using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Costs;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.Tags;
using ItemChanger.Tags;
using UnityEngine;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
    public static Location Barbed_Bracelet => new ObjectLocation
    {
        Name = LocationNames.Barbed_Bracelet,
        SceneName = SceneNames.Dust_Barb,
        ObjectName = "pontoon/Art/Collectable Item Pickup",
        FlingType = Enums.FlingType.StraightUp,
        Correction = new(0, -1.1f),
        Tags = [new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true }, SmallFleaPrefabs]
    };

    public static Location Claw_Mirror => new DualLocation
    {
        Name = LocationNames.Claw_Mirror,
        Test = new PDBool(nameof(PlayerData.defeatedTrobbio)),
        TrueLocation = new ObjectLocation
        {
            Name = LocationNames.Claw_Mirror,
            SceneName = SceneNames.Library_13,
            ObjectName = "Grand Stage Scene/Re-Entry Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = new(0, -0.5f),
            Tags = [new OriginalContainerTag { ContainerType = ContainerNames.Shiny }]
        },
        FalseLocation = new DelayedShinyLocation
        {
            Name = LocationNames.Claw_Mirror,
            SceneName = SceneNames.Library_13,
            ObjectName = "Grand Stage Scene/Boss Scene Trobbio/Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = new(0, -0.5f),
            GiveEarly = new BoxedBool { Value = true }
        }
    };

    public static Location Claw_Mirrors => new DualLocation
    {
        Name = LocationNames.Claw_Mirrors,
        Test = new PDBool(nameof(PlayerData.defeatedTormentedTrobbio)),
        TrueLocation = new ObjectLocation
        {
            Name = LocationNames.Claw_Mirrors,
            SceneName = SceneNames.Library_13,
            ObjectName = "Grand Stage Scene/Re-Entry Pickup Upgrade",
            FlingType = Enums.FlingType.Everywhere,
            Correction = new(0, -0.5f),
            Tags = [new OriginalContainerTag { ContainerType = ContainerNames.Shiny }]
        },
        FalseLocation = new DelayedShinyLocation
        {
            Name = LocationNames.Claw_Mirrors,
            SceneName = SceneNames.Library_13,
            ObjectName = "Grand Stage Scene/Boss Scene TormentedTrobbio/Item Spawn/Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = new(0, -1f),
            GiveEarly = new BoxedBool { Value = true }
        }
    };

    public static Location Conchcutter => new ConchcutterLocation
    {
        Name = LocationNames.Conchcutter,
        SceneName = SceneNames.Coral_Tower_01,
        FlingType = Enums.FlingType.Everywhere,
        Correction = new(0, -0.6f),
        FlingLocation = new DelayedShinyLocation
        {
            Name = LocationNames.Conchcutter,
            SceneName = SceneNames.Coral_Tower_01,
            ObjectName = "Memory Group/Collectible Item Pickup Scene/Collectable Item Pickup Fling",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            GiveEarly = new BoxedBool { Value = true },
            Managed = true
        },
        Tags = [new OriginalContainerTag { ContainerType = ContainerNames.Shiny }]
    };

    // TODO: Handle Curvesickle/Curveclaw progression.
    //public static Location Curvesickle => new ObjectLocation
    //{
    //    Name = LocationNames.Curvesickle,
    //    SceneName = SceneNames.Bone_East_22,
    //    ObjectName = "Collectable Item Pickup",
    //    FlingType = Enums.FlingType.Everywhere,
    //    Correction = default,
    //    Tags = [new OriginalContainerTag { ContainerType = ContainerNames.Shiny }]
    //};

    private static Location DeadBugsPurseOrShellSatchel(string locationName, string objectName, string fieldName) => new ObjectLocation()
    {
        Name = locationName,
        SceneName = SceneNames.Crawl_01,
        ObjectName = objectName,
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [
            new OriginalContainerTag { ContainerType = ContainerNames.Shiny },
            new ShinyControlTag { Info = new() { ShinyFling = ShinyContainer.ShinyFling.FloatInPlace } },
            new DeactivateObjectTag {
                SceneName = SceneNames.Crawl_01,
                ObjectName = "corpses_pilgrim_sand_0001_1 (2)",
                Test = new UnexpectedContainerBool
                {
                    PlacementName = locationName,
                    ExpectedContainerType = ContainerNames.Shiny
                }
            },
            new ReplaceGameObjectReferenceTag {
                Reference = new ComponentClassFieldOption<TestGameObjectActivator, GameObject>(SceneNames.Crawl_01, "Tool Conditions", fieldName)
            }
        ]
    };

    public static Location Dead_Bug_s_Purse => DeadBugsPurseOrShellSatchel(LocationNames.Dead_Bug_s_Purse, "Tool Conditions/Collectable Item Pickup - Purse", nameof(TestGameObjectActivator.activateGameObject));

    public static Location Delver_s_Drill => new ObjectLocation
    {
        Name = LocationNames.Delver_s_Drill,
        SceneName = SceneNames.Under_14,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [
            new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true },
            new AdjustPositionTag  // Desk overlaps chest lid when opened.
            {
                SceneName = SceneNames.Under_14,
                ObjectName = "dock_b__0049_table_wide",
                Adjustment = new(0, 0, 0.01f),
                Test = new UnexpectedContainerBool { PlacementName = LocationNames.Delver_s_Drill, ExpectedContainerType = ContainerNames.Shiny }
            },
            new DeactivateObjectTag
            {
                SceneName = SceneNames.Under_14,
                ObjectName = "Collectable Item Pickup/Item Display Folder",
                Test = new IsItemMissingBool { PlacementName = LocationNames.Delver_s_Drill, ItemName = ItemNames.Delver_s_Drill }
            }
        ]
    };

    public static Location Druid_s_Eye => new MossDruidMix1Location
    {
        Name = LocationNames.Druid_s_Eye,
        SceneName = SceneNames.Mosstown_02c,
        FlingType = Enums.FlingType.DirectDeposit,
        PreviewIndex = 0,
    }.WithTag(new DefaultCostTag { Cost = new MossberryCost { Value = 3 } });

    public static Location Druid_s_Eyes => new MossDruidMix2Location
    {
        Name = LocationNames.Druid_s_Eyes,
        SceneName = SceneNames.Mosstown_02c,
        FlingType = Enums.FlingType.DirectDeposit,
        PreviewIndex = 4,
    }.WithTag(new DefaultCostTag { Cost = new MossberryCost { Value = 7 } });

    public static Location Flintslate => new ObjectLocation
    {
        Name = LocationNames.Flintslate,
        SceneName = SceneNames.Dock_02b,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = new(0, -0.2f),
        Tags = [new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true }]
    };

    public static Location Injector_Band => new ObjectLocation
    {
        Name = LocationNames.Injector_Band,
        SceneName = SceneNames.Ward_03,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true }]
    };

    public static Location Longpin => new ObjectLocation
    {
        Name = LocationNames.Longpin,
        SceneName = SceneNames.Belltown_Room_shellwood,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = new(0, -0.75f),
        Tags = [
            new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true },
            new DeactivateObjectTag
            {
                SceneName = SceneNames.Belltown_Room_shellwood,
                ObjectName = "Collectable Item Pickup/fisher_room_0002_1",
                Test = new IsItemMissingBool { PlacementName = LocationNames.Longpin, ItemName = ItemNames.Longpin }
            },
            SmallFleaPrefabs
        ]
    };

    public static Location Memory_Crystal => new DelayedShinyLocation
    {
        Name = LocationNames.Memory_Crystal,
        SceneName = SceneNames.Bellway_Peak_02,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        GiveEarly = new Negation { Bool = new SDBool(SceneNames.Bellway_Peak_02, "charm_break_wall") }
    };

    public static Location Pin_Badge => new PinBadgeLocation
    {
        SceneName = SceneNames.Peak_07,
        Name = LocationNames.Pin_Badge,
    };

    public static Location Pollip_Pouch => new DualLocation
    {
        SceneName = SceneNames.Room_Witch,
        Name = LocationNames.Pollip_Pouch,
        Test = new QuestCompletionBool(Quests.Wood_Witch_Curse),
        TrueLocation = new CoordinateLocation
        {
            SceneName = SceneNames.Room_Witch,
            Name = LocationNames.Pollip_Pouch,
            X = 17.0f,
            Y = 6.57f,
            Managed = false,
            ForceDefaultContainer = true,
        },
        FalseLocation = new GreyrootPollipLocation
        {
            Name = LocationNames.Pollip_Pouch,
            SceneName = SceneNames.Room_Witch,
        },
    };

    public static Location Quick_Sling => new ObjectLocation
    {
        Name = LocationNames.Quick_Sling,
        SceneName = SceneNames.Shadow_11,
        ObjectName = "Collectable Item Pickup (1)",
        FlingType = Enums.FlingType.Everywhere,
        Correction = new(0, -1.6f),
        Tags = [new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true }, SmallFleaPrefabs]
    };

    public static Location Rosary_Cannon => new DelayedShinyLocation
    {
        Name = LocationNames.Rosary_Cannon,
        SceneName = SceneNames.Hang_06_bank,
        ObjectName = "rosary_cannon/Art/Rosary Cannon Scene/Rosary Cannon Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        ForceDefaultContainer = true,
        GiveEarly = new Negation { Bool = new PDBool(nameof(PlayerData.destroyedRosaryCannonMachine)) }
    };

    public static Location Ruined_Tool => new ObjectLocation
    {
        Name = LocationNames.Ruined_Tool,
        SceneName = SceneNames.Shadow_Weavehome,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true }]
    };

    public static Location Shard_Pendant => new ObjectLocation
    {
        Name = LocationNames.Shard_Pendant,
        SceneName = SceneNames.Bone_17,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = new(0, -0.3f),
        Tags = [new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true }]
    };

    public static Location Shell_Satchel => DeadBugsPurseOrShellSatchel(LocationNames.Shell_Satchel, "Tool Conditions/Collectable Item Pickup - Satchel", nameof(TestGameObjectActivator.deactivateGameObject));

    private const string SILKSPEED_ANKLETS_PATH = "Weaver Speed Challenge/Weaver Challenge Reward/stand/holder/Collectable Item Pickup";
    public static Location Silkspeed_Anklets => new DualLocation
    {
        Name = LocationNames.Silkspeed_Anklets,
        Test = new PDBool(nameof(PlayerData.CompletedWeaveSprintChallengeMax)),
        FalseLocation = new SilkspeedAnkletsLocation
        {
            Name = LocationNames.Silkspeed_Anklets,
            SceneName = SceneNames.Bone_East_Weavehome,
            ObjectName = SILKSPEED_ANKLETS_PATH,
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag { ContainerType = ContainerNames.Shiny, Force = true }]
        },
        // Spawn to the left if the stand is opened.
        TrueLocation = new CoordinateLocation
        {
            Name = LocationNames.Silkspeed_Anklets,
            SceneName = SceneNames.Bone_East_Weavehome,
            X = 121,
            Y = 91,
            Managed = false,
            Tags = [new DeactivateObjectTag { SceneName = SceneNames.Bone_East_Weavehome, ObjectName = SILKSPEED_ANKLETS_PATH }]
        }
    };

    public static Location Snare_Setter => new ObjectLocation
    {
        Name = LocationNames.Snare_Setter,
        SceneName = SceneNames.Weave_14,
        ObjectName = "Collectable Item Pickup (1)",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true }]
    };

    public static Location Straight_Pin => new ObjectLocation
    {
        Name = LocationNames.Straight_Pin,
        SceneName = SceneNames.Bone_12,
        ObjectName = "Collectable Item Pickup Pin",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [
            new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true },
            new DeactivateObjectTag
            {
                SceneName = SceneNames.Bone_12,
                ObjectName = "Collectable Item Pickup Pin/Tool_straight_pin0006 (1)",
                Test = new IsItemMissingBool { PlacementName = LocationNames.Straight_Pin, ItemName = ItemNames.Straight_Pin }
            },
            new AdjustPositionTag  // Desk overlaps chest lid when opened.
            {
                SceneName = SceneNames.Bone_12,
                ObjectName = "jail_extra_0001_table (3)",
                Adjustment = new(0, 0, 0.01f),
                Test = new UnexpectedContainerBool { PlacementName = LocationNames.Straight_Pin, ExpectedContainerType = ContainerNames.Shiny }
            },
            SmallFleaPrefabs
        ]
    };

    public static Location Tacks => new DualLocation
    {
        Name = LocationNames.Tacks,
        SceneName = SceneNames.Dust_Shack,
        Test = new PDBool(nameof(PlayerData.blackThreadWorld)),
        FalseLocation = new BenjinAndCrullTacksLocation() 
        {
            Name = LocationNames.Tacks,
            SceneName = SceneNames.Dust_Shack,
        },
        TrueLocation = new ObjectLocation()
        {
            Name = LocationNames.Tacks,
            SceneName = SceneNames.Dust_Shack,
            ObjectName = "Collectable Item Dustpilo",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag { ContainerType = ContainerNames.Shiny }]
        },
    };

    // Greymoor_15b has multiple 'Group' objects so StrictObjectLocation is needed.
    public static Location Threefold_Pin => new StrictObjectLocation
    {
        Name = LocationNames.Threefold_Pin,
        SceneName = SceneNames.Greymoor_15b,
        ObjectName = "Group/Collectable Item Pickup (1)",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [
            new OriginalContainerTag { ContainerType = ContainerNames.Shiny, Force = true },
            new RemoveComponentTag<SpriteRenderer>  // Remove the threefold pin visual if it's not there.
            {
                SceneName = SceneNames.Greymoor_15b,
                ObjectName = "rosary_string_empty - Tri Pin/rosary_string/string_cap",
                Test = new IsItemMissingBool { PlacementName = LocationNames.Threefold_Pin, ItemName = ItemNames.Threefold_Pin }
            }
        ]
    };

    // TODO: The Volt_Filament shiny has a unique graphical effect.
    // Similar to the Twisted Bud, it should be removed here, and added to the actual item where possible.
    public static Location Volt_Filament => new DualLocation
    {
        Name = LocationNames.Volt_Filament,
        Test = new PDBool(nameof(PlayerData.defeatedZapCoreEnemy)),
        TrueLocation = new ObjectLocation
        {
            Name = LocationNames.Volt_Filament,
            SceneName = SceneNames.Coral_29,
            ObjectName = "Boss Scene/Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag { ContainerType = ContainerNames.Shiny, Force = true }]
        },
        FalseLocation = new DelayedShinyLocation
        {
            Name = LocationNames.Volt_Filament,
            SceneName = SceneNames.Coral_29,
            ObjectName = "Boss Scene/Zap Core Enemy/Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            GiveEarly = new BoxedBool { Value = true }
        }
    };

    public static Location Voltvessels => new DelayedShinyLocation
    {
        Name = LocationNames.Voltvessels,
        SceneName = SceneNames.Arborium_07,
        ObjectName = "Battle Scene/End Scene/Collectable Item Pickup Battle",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        GiveEarly = new Negation { Bool = new SDBool(SceneNames.Arborium_07, "Battle Scene") },
        Tags = [new ShinyControlTag() { Info = new() { ShinyFling = ShinyContainer.ShinyFling.FloatInPlace } }]
    };

    public static Location Warding_Bell => new ObjectLocation
    {
        Name = LocationNames.Warding_Bell,
        SceneName = SceneNames.Dock_03b,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = new(0, -1.4f),
        Tags = [new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true }, SmallFleaPrefabs]
    };

    public static Location Weavelight => new DualLocation
    {
        Name = LocationNames.Weavelight,
        Test = new SDBool(SceneNames.Weave_03, "Boss Scene"),
        FalseLocation = new DelayedShinyLocation
        {
            Name = LocationNames.Weavelight,
            SceneName = SceneNames.Weave_03,
            ObjectName = "Boss Scene/Battle End/Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            GiveEarly = new BoxedBool { Value = true }
        },
        TrueLocation = new ObjectLocation
        {
            Name = LocationNames.Weavelight,
            SceneName = SceneNames.Weave_03,
            ObjectName = "Boss Scene/Active After Battle/Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag { ContainerType = ContainerNames.Shiny }]
        }
    };

    public static Location Wispfire_Lantern => new DelayedShinyLocation
    {
        Name = LocationNames.Wispfire_Lantern,
        SceneName = SceneNames.Belltown_08,
        ObjectName = "Boss Scene/Lantern Item",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        GiveEarly = new BoxedBool { Value = false }
    };

    public static Location Wreath_of_Purity => new ObjectLocation
    {
        Name = LocationNames.Wreath_of_Purity,
        SceneName = SceneNames.Aqueduct_06,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = new(0, -1.4f),
        Tags = [new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true }]
    };
}
