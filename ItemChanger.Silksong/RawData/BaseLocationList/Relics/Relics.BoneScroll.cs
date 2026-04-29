using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Tags;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.RawData
{
    //relic locations for bone scrolls
    //can also refer to this commit https://github.com/homothetyhk/ItemChanger.Silksong/commit/d4e181e93cfae405a354a171958c9f18db4d968e by kickafetus
    internal static partial class BaseLocationList
    {
        public static Location Bone_Scroll__Burning_Bug => new ObjectLocation
        {
            Name = LocationNames.Bone_Scroll__Burning_Bug,
            SceneName = SceneNames.Wisp_08,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Bone_Scroll__Lost_Pilgrim => new ObjectLocation
        {
            Name = LocationNames.Bone_Scroll__Lost_Pilgrim,
            SceneName = SceneNames.Greymoor_21,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Bone_Scroll__Singed_Pilgrim => new ObjectLocation
        {
            Name = LocationNames.Bone_Scroll__Singed_Pilgrim,
            SceneName = SceneNames.Bone_East_14,
            ObjectName = "Collectable Item Pickup (1)",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Bone_Scroll__Underworker => new ObjectLocation
        {
            Name = LocationNames.Bone_Scroll__Underworker,
            SceneName = SceneNames.Under_16,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };

    }
}
