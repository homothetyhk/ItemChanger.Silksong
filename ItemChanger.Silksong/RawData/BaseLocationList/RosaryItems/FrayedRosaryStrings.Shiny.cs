using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Tags;
using ItemChanger.Tags;


namespace ItemChanger.Silksong.RawData
{
    //shiny locations for frayed rosary strings
    //NOTE: Frayed_Rosary__The_Slab_Choral_Entrance needs the flea container to properly handle replacing hanging cage locations
    internal static partial class BaseLocationList
    {

        public static Location Frayed_Rosary__Bilewater => new ObjectLocation
        {
            Name = LocationNames.Frayed_Rosary__Bilewater,
            SceneName = SceneNames.Shadow_02,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Frayed_Rosary__Blasted_Steps => new ObjectLocation
        {
            Name = LocationNames.Frayed_Rosary__Blasted_Steps,
            SceneName = SceneNames.Coral_03,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Frayed_Rosary__Deep_Docks => new ObjectLocation
        {
            Name = LocationNames.Frayed_Rosary__Deep_Docks,
            SceneName = SceneNames.Bone_East_04b,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Frayed_Rosary__Greymoor_Above_Yarnaby => new ObjectLocation
        {
            Name = LocationNames.Frayed_Rosary__Greymoor_Above_Yarnaby,
            SceneName = SceneNames.Wisp_03,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Frayed_Rosary__Greymoor_Craw_Lake_Ledge => new ObjectLocation
        {
            Name = LocationNames.Frayed_Rosary__Greymoor_Craw_Lake_Ledge,
            SceneName = SceneNames.Greymoor_15,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Frayed_Rosary__Greymoor_West_Craw_Lake => new ObjectLocation
        {
            Name = LocationNames.Frayed_Rosary__Greymoor_West_Craw_Lake,
            SceneName = SceneNames.Greymoor_15b,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Frayed_Rosary__High_Halls => new ObjectLocation
        {
            Name = LocationNames.Frayed_Rosary__High_Halls,
            SceneName = SceneNames.Hang_16,
            ObjectName = "laundry_basket (1)/Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Frayed_Rosary__Moss_Grotto_Above_Start => new ObjectLocation
        {
            Name = LocationNames.Frayed_Rosary__Moss_Grotto_Above_Start,
            SceneName = SceneNames.Tut_01,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Frayed_Rosary__Moss_Grotto_Silkspear => new ObjectLocation
        {
            Name = LocationNames.Frayed_Rosary__Moss_Grotto_Silkspear,
            SceneName = SceneNames.Mosstown_02,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Frayed_Rosary__Putrified_Ducts => new ObjectLocation
        {
            Name = LocationNames.Frayed_Rosary__Putrified_Ducts,
            SceneName = SceneNames.Aqueduct_01,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Frayed_Rosary__Shellwood => new ObjectLocation
        {
            Name = LocationNames.Frayed_Rosary__Shellwood,
            SceneName = SceneNames.Shellwood_01,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Frayed_Rosary__Sinners_Road => new ObjectLocation
        {
            Name = LocationNames.Frayed_Rosary__Sinners_Road,
            SceneName = SceneNames.Dust_01,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Frayed_Rosary__The_Marrow => new ObjectLocation
        {
            Name = LocationNames.Frayed_Rosary__The_Marrow,
            SceneName = SceneNames.Bone_10,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Frayed_Rosary__The_Slab_Choral_Entrance => new ObjectLocation //having a flea + item replacement makes a flea spawn when breaking the cage; items are still replaced
        {
            Name = LocationNames.Frayed_Rosary__The_Slab_Choral_Entrance,
            SceneName = SceneNames.Slab_02,
            ObjectName = "Slab Chain cage_small_break (2)/lamp/Broken/Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Frayed_Rosary__The_Slab_East => new ObjectLocation
        {
            Name = LocationNames.Frayed_Rosary__The_Slab_East,
            SceneName = SceneNames.Slab_18,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Frayed_Rosary__The_Slab_West => new ObjectLocation
        {
            Name = LocationNames.Frayed_Rosary__The_Slab_West,
            SceneName = SceneNames.Slab_22,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Frayed_Rosary__Underworks_Choral_Exit => new ObjectLocation
        {
            Name = LocationNames.Frayed_Rosary__Underworks_Choral_Exit,
            SceneName = SceneNames.Under_07c,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Frayed_Rosary__Underworks_Ventrica => new ObjectLocation
        {
            Name = LocationNames.Frayed_Rosary__Underworks_Ventrica,
            SceneName = SceneNames.Under_12,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Frayed_Rosary__Wormways => new ObjectLocation
        {
            Name = LocationNames.Frayed_Rosary__Wormways,
            SceneName = SceneNames.Crawl_02,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };

    }

}
