using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.Tags;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.RawData
{
    //tool locations based on shiny pickup items
    //TO DO: wait for flea container progress to support voltvessels and volt filament and claw mirror drop locations
    internal static partial class BaseLocationList
    {
        //red tools
        public static Location Conchcutter => new ObjectLocation
        {
            Name = LocationNames.Conchcutter,
            SceneName = SceneNames.Coral_Tower_01,
            ObjectName = "Memory Group/Collectible Item Pickup Scene/Collectable Item Pickup Stand",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Curvesickle => new ObjectLocation
        {
            Name = LocationNames.Curvesickle,
            SceneName = SceneNames.Bone_East_22,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
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
        public static Location Flintslate => new ObjectLocation
        {
            Name = LocationNames.Flintslate,
            SceneName = SceneNames.Dock_02b,
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
        public static Location Rosary_Cannon => new ObjectLocation
        {
            Name = LocationNames.Rosary_Cannon,
            SceneName = SceneNames.Hang_06_bank,
            ObjectName = "rosary_cannon/Art/Rosary Cannon Scene/Rosary Cannon Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
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
        public static Location Voltvessels => new ObjectLocation //note: flea item does not work in this location; regular items do work though
        {
            Name = LocationNames.Voltvessels,
            SceneName = SceneNames.Arborium_07,
            ObjectName = "Battle Scene/End Scene/Collectable Item Pickup Battle",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny, Force = true }]
        };

        //blue tools
        public static Location Claw_Mirror => new DualLocation
        {
            Name = LocationNames.Claw_Mirror,
            Test = new PDBool(nameof(PlayerData.defeatedTrobbio)),//defeated trobbio
            TrueLocation = new ObjectLocation //defeated trobbio but left arena
            {
                Name = LocationNames.Claw_Mirror,
                SceneName = SceneNames.Library_13,
                ObjectName = "Grand Stage Scene/Re-Entry Pickup",//version that is a shiny placed on the floor
                FlingType = Enums.FlingType.Everywhere,
                Correction = default,
                Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
            },
            FalseLocation = new ObjectLocation //has not defeated trobbio
            {
                Name = LocationNames.Claw_Mirror,
                SceneName = SceneNames.Library_13,
                ObjectName = "Grand Stage Scene/Boss Scene Trobbio/Collectable Item Pickup",//version that is dropped from trobbio
                FlingType = Enums.FlingType.Everywhere,
                Correction = default,
                Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny, Force = true }] //note: flea item does not work in this location; regular items do work though
            }
        };
        public static Location Claw_Mirrors => new DualLocation
        {
            Name = LocationNames.Claw_Mirrors,
            Test = new PDBool(nameof(PlayerData.defeatedTormentedTrobbio)),//defeated tormented trobbio
            TrueLocation = new ObjectLocation //defeated tormented trobbio but left arena
            {
                Name = LocationNames.Claw_Mirrors,
                SceneName = SceneNames.Library_13,
                ObjectName = "Grand Stage Scene/Re-Entry Pickup Upgrade",//version that is a shiny placed on the floor
                FlingType = Enums.FlingType.Everywhere,
                Correction = default,
                Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
            },
            FalseLocation = new ObjectLocation //has not defeated tormented trobbio
            {
                Name = LocationNames.Claw_Mirrors,
                SceneName = SceneNames.Library_13,
                ObjectName = "Grand Stage Scene/Boss Scene TormentedTrobbio/Item Spawn/Collectable Item Pickup",//version that is dropped from tormented trobbio
                FlingType = Enums.FlingType.Everywhere,
                Correction = default,
                Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny, Force = true }] //note: flea item does not work in this location; regular items do work though
            }
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
        public static Location Memory_Crystal => new ObjectLocation
        {
            Name = LocationNames.Memory_Crystal,
            SceneName = SceneNames.Bellway_Peak_02,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
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

        public static Location Volt_Filament => new DualLocation
        {
            Name = LocationNames.Volt_Filament,
            Test = new PDBool(nameof(PlayerData.defeatedZapCoreEnemy)),//defeated voltvyrm
            TrueLocation = new ObjectLocation //defeated voltvyrm but left arena
            {
                Name = LocationNames.Volt_Filament,
                SceneName = SceneNames.Coral_29,
                ObjectName = "Boss Scene/Collectable Item Pickup",//version that is a shiny placed on the floor
                FlingType = Enums.FlingType.Everywhere,
                Correction = default,
                Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
            },
            FalseLocation = new ObjectLocation //has not defeated voltvyrm
            {
                Name = LocationNames.Volt_Filament,
                SceneName = SceneNames.Coral_29,
                ObjectName = "Boss Scene/Zap Core Enemy/Collectable Item Pickup",//version that is dropped from voltvyrm
                FlingType = Enums.FlingType.Everywhere,
                Correction = default,
                Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny, Force = true }] //note: flea item does not work in this location; regular items do work though
            }
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
        public static Location Wreath_of_Purity => new ObjectLocation
        {
            Name = LocationNames.Wreath_of_Purity,
            SceneName = SceneNames.Aqueduct_06,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };

        //yellow tools
        public static Location Barbed_Bracelet => new ObjectLocation
        {
            Name = LocationNames.Barbed_Bracelet,
            SceneName = SceneNames.Dust_Barb,
            ObjectName = "pontoon/Art/Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Dead_Bug_s_Purse => new ObjectLocation
        {
            Name = LocationNames.Dead_Bug_s_Purse,
            SceneName = SceneNames.Crawl_01,
            ObjectName = "Tool Conditions/Collectable Item Pickup - Purse",
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
        public static Location Shell_Satchel => new ObjectLocation
        {
            Name = LocationNames.Shell_Satchel,
            SceneName = SceneNames.Crawl_01,
            ObjectName = "Tool Conditions/Collectable Item Pickup - Satchel",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };

        //misc
        public static Location Ruined_Tool => new ObjectLocation
        {
            Name = LocationNames.Ruined_Tool,
            SceneName = SceneNames.Shadow_Weavehome,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };

    }
}
