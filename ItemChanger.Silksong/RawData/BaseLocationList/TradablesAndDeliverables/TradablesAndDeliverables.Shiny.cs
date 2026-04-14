using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Tags;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.RawData
{
    //shiny locations for tradables and deliverables
    internal static partial class BaseLocationList
    {
        public static Location Crustnut => new ObjectLocation
        {
            Name = LocationNames.Crustnut,
            SceneName = SceneNames.Coral_41,
            ObjectName = "Group/Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Grass_Doll => new ObjectLocation
        {
            Name = LocationNames.Grass_Doll,
            SceneName = SceneNames.Bone_East_18b,
            ObjectName = "ant_item_string/Item Holder/Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Twisted_Bud => new ObjectLocation
        {
            Name = LocationNames.Twisted_Bud,
            SceneName = SceneNames.Shadow_20,
            ObjectName = "Collectable Mandrake Scene/Collectable Item Pickup (1)",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        
        
    }
}
