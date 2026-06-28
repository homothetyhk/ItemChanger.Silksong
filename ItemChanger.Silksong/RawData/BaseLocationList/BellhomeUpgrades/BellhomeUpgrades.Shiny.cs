using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Tags;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.RawData
{
    //bellhome upgrade locations based on shiny pickup items
    internal static partial class BaseLocationList
    {
        public static Location Crawbell => new ObjectLocation
        {
            Name = LocationNames.Crawbell,
            SceneName = SceneNames.Room_CrowCourt_02,
            ObjectName = "Collectable Item Pickup Crawbell",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Farsight => new ObjectLocation
        {
            Name = LocationNames.Farsight,
            SceneName = SceneNames.Abyss_08,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Materium => new ObjectLocation
        {
            Name = LocationNames.Materium,
            SceneName = SceneNames.Arborium_07,
            ObjectName = "Collectable Item Pickup Materium",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };

    }
}
