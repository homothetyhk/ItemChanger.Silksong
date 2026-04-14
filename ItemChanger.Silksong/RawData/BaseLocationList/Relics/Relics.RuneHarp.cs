using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Tags;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.RawData
{
    //relic locations for rune harps
    //can also refer to this commit https://github.com/homothetyhk/ItemChanger.Silksong/commit/d4e181e93cfae405a354a171958c9f18db4d968e by kickafetus
    internal static partial class BaseLocationList
    {
        public static Location Rune_Harp__Burden => new ObjectLocation
        {
            Name = LocationNames.Rune_Harp__Burden,
            SceneName = SceneNames.Hang_12,
            ObjectName = "Black Thread States/Black Thread World/Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Rune_Harp__Escape => new ObjectLocation
        {
            Name = LocationNames.Rune_Harp__Escape,
            SceneName = SceneNames.Bone_East_Weavehome,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Rune_Harp__Eva => new ObjectLocation
        {
            Name = LocationNames.Rune_Harp__Eva,
            SceneName = SceneNames.Weave_08,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };

    }
}
