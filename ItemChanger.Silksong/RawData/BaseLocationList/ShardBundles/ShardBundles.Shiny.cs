using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Tags;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.RawData
{
    //shard bundle locations based on shiny pickup items
    internal static partial class BaseLocationList
    {
        public static Location Shard_Bundle__Cogwork_Core => new ObjectLocation
        {
            Name = LocationNames.Shard_Bundle__Cogwork_Core,
            SceneName = SceneNames.Cog_10,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Shard_Bundle__Deep_Docks_Forge => new ObjectLocation
        {
            Name = LocationNames.Shard_Bundle__Deep_Docks_Forge,
            SceneName = SceneNames.Room_Forge,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Shard_Bundle__Deep_Docks_South => new ObjectLocation
        {
            Name = LocationNames.Shard_Bundle__Deep_Docks_South,
            SceneName = SceneNames.Dock_02,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Shard_Bundle__Greymoor_Lower => new ObjectLocation
        {
            Name = LocationNames.Shard_Bundle__Greymoor_Lower,
            SceneName = SceneNames.Greymoor_05,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Shard_Bundle__Greymoor_Upper => new ObjectLocation
        {
            Name = LocationNames.Shard_Bundle__Greymoor_Upper,
            SceneName = SceneNames.Greymoor_12,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Shard_Bundle__Memorium => new ObjectLocation
        {
            Name = LocationNames.Shard_Bundle__Memorium,
            SceneName = SceneNames.Arborium_11,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Shard_Bundle__Sinners_Road => new ObjectLocation
        {
            Name = LocationNames.Shard_Bundle__Sinners_Road,
            SceneName = SceneNames.Dust_06,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Shard_Bundle__Shellwood => new ObjectLocation
        {
            Name = LocationNames.Shard_Bundle__Shellwood,
            SceneName = SceneNames.Shellwood_13,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Shard_Bundle__The_Cauldron => new ObjectLocation
        {
            Name = LocationNames.Shard_Bundle__The_Cauldron,
            SceneName = SceneNames.Under_18,
            ObjectName = "Collectable Item Pickup (1)",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Shard_Bundle__The_Slab => new ObjectLocation
        {
            Name = LocationNames.Shard_Bundle__The_Slab,
            SceneName = SceneNames.Slab_04,
            ObjectName = "Slab Chain cage_small_break/lamp/Broken/Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Shard_Bundle__Underworks_Exhaust => new ObjectLocation
        {
            Name = LocationNames.Shard_Bundle__Underworks_Exhaust,
            SceneName = SceneNames.Library_12,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Shard_Bundle__Underworks_West => new ObjectLocation
        {
            Name = LocationNames.Shard_Bundle__Underworks_West,
            SceneName = SceneNames.Under_03,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };

    }
}
