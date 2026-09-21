using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Costs;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.Tags;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
    public static Location Barbed_Bracelet => new ObjectLocation
    {
        Name = LocationNames.Barbed_Bracelet,
        SceneName = SceneNames.Dust_Barb,
        ObjectName = "pontoon/Art/Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny, Force = true }]
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
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny, Force = true }]
        },
        FalseLocation = new DelayedShinyLocation
        {
            Name = LocationNames.Claw_Mirror,
            SceneName = SceneNames.Library_13,
            ObjectName = "Grand Stage Scene/Boss Scene Trobbio/Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
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
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny, Force = true }]
        },
        FalseLocation = new DelayedShinyLocation
        {
            Name = LocationNames.Claw_Mirrors,
            SceneName = SceneNames.Library_13,
            ObjectName = "Grand Stage Scene/Boss Scene TormentedTrobbio/Item Spawn/Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            GiveEarly = new BoxedBool { Value = true }
        }
    };

    public static Location Conchcutter => new ObjectLocation
    {
        Name = LocationNames.Conchcutter,
        SceneName = SceneNames.Coral_Tower_01,
        ObjectName = "Memory Group/Collectible Item Pickup Scene/Collectable Item Pickup Stand",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Shiny, Force = true },
            new ShinyControlTag() { Info = new() { ShinyFling = ShinyContainer.ShinyFling.FloatInPlace } }
        ]
    };

    // TODO: Handle Curvesickle/Curveclaw progression.
    //public static Location Curvesickle => new ObjectLocation
    //{
    //    Name = LocationNames.Curvesickle,
    //    SceneName = SceneNames.Bone_East_22,
    //    ObjectName = "Collectable Item Pickup",
    //    FlingType = Enums.FlingType.Everywhere,
    //    Correction = default,
    //    Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
    //};

    public static Location Dead_Bug_s_Purse => new DualLocation()
    {
        Name = LocationNames.Dead_Bug_s_Purse,
        Test = new IntComparisonBool { ToCompare = new PDInt(nameof(PlayerData.permadeathMode)), Amount = 1 },
        TrueLocation = new CoordinateLocation()
        {
            Name = LocationNames.Dead_Bug_s_Purse,
            SceneName = SceneNames.Crawl_01,
            X = 52.50f,
            Y = 85.20f,
            Managed = false,
        },
        FalseLocation = new ObjectLocation()
        {
            Name = LocationNames.Dead_Bug_s_Purse,
            SceneName = SceneNames.Crawl_01,
            ObjectName = "Tool Conditions/Collectable Item Pickup - Purse",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [
                new OriginalContainerTag() { ContainerType = ContainerNames.Shiny, Force = true },
                new ShinyControlTag() { Info = new() { ShinyFling = ShinyContainer.ShinyFling.FloatInPlace } }
            ]
        }
    };

    public static Location Delver_s_Drill => new ObjectLocation
    {
        Name = LocationNames.Delver_s_Drill,
        SceneName = SceneNames.Under_14,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
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
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
    };

    public static Location Injector_Band => new ObjectLocation
    {
        Name = LocationNames.Injector_Band,
        SceneName = SceneNames.Ward_03,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
    };

    public static Location Longpin => new ObjectLocation
    {
        Name = LocationNames.Longpin,
        SceneName = SceneNames.Belltown_Room_shellwood,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
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
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
    };

    public static Location Rosary_Cannon => new DelayedShinyLocation
    {
        Name = LocationNames.Rosary_Cannon,
        SceneName = SceneNames.Hang_06_bank,
        ObjectName = "rosary_cannon/Art/Rosary Cannon Scene/Rosary Cannon Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        GiveEarly = new Negation { Bool = new PDBool(nameof(PlayerData.destroyedRosaryCannonMachine)) }
    };

    public static Location Ruined_Tool => new ObjectLocation
    {
        Name = LocationNames.Ruined_Tool,
        SceneName = SceneNames.Shadow_Weavehome,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
    };

    public static Location Shard_Pendant => new ObjectLocation
    {
        Name = LocationNames.Shard_Pendant,
        SceneName = SceneNames.Bone_17,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
    };

    public static Location Shell_Satchel => new DualLocation()
    {
        Name = LocationNames.Shell_Satchel,
        Test = new IntComparisonBool { ToCompare = new PDInt(nameof(PlayerData.permadeathMode)), Amount = 1 },
        TrueLocation = new ObjectLocation()
        {
            Name = LocationNames.Shell_Satchel,
            SceneName = SceneNames.Crawl_01,
            ObjectName = "Tool Conditions/Collectable Item Pickup - Satchel",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [
                new OriginalContainerTag() { ContainerType = ContainerNames.Shiny, Force = true },
                new ShinyControlTag() { Info = new() { ShinyFling = ShinyContainer.ShinyFling.FloatInPlace } }
            ]
        },
        FalseLocation = new CoordinateLocation()
        {
            Name = LocationNames.Shell_Satchel,
            SceneName = SceneNames.Crawl_01,
            X = 52.50f,
            Y = 85.2f,
            Managed = false,
        }
    };

    public static Location Silkspeed_Anklets => new DelayedShinyLocation
    {
        Name = LocationNames.Silkspeed_Anklets,
        SceneName = SceneNames.Bone_East_Weavehome,
        ObjectName = "Weaver Speed Challenge/Weaver Challenge Reward/stand/holder/Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        GiveEarly = new BoxedBool { Value = false }
    };

    public static Location Snare_Setter => new ObjectLocation
    {
        Name = LocationNames.Snare_Setter,
        SceneName = SceneNames.Weave_14,
        ObjectName = "Collectable Item Pickup (1)",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
    };

    public static Location Straight_Pin => new ObjectLocation
    {
        Name = LocationNames.Straight_Pin,
        SceneName = SceneNames.Bone_12,
        ObjectName = "Collectable Item Pickup Pin",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
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
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        },
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
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny, Force = true }]
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

    public static Location Voltvessels => new ObjectLocation
    {
        Name = LocationNames.Voltvessels,
        SceneName = SceneNames.Arborium_07,
        ObjectName = "Battle Scene/End Scene/Collectable Item Pickup Battle",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Shiny, Force = true },
            new ShinyControlTag() { Info = new() { ShinyFling = ShinyContainer.ShinyFling.FloatInPlace } }
        ]
    };

    public static Location Warding_Bell => new ObjectLocation
    {
        Name = LocationNames.Warding_Bell,
        SceneName = SceneNames.Dock_03b,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
    };

    public static Location Weavelight => new DelayedShinyLocation
    {
        Name = LocationNames.Weavelight,
        SceneName = SceneNames.Weave_03,
        ObjectName = "Boss Scene/Active After Battle/Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        GiveEarly = new Negation { Bool = new SDBool(SceneNames.Weave_03, "Boss Scene") }
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
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
    };
}
