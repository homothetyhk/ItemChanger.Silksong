using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Tags;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.RawData
{
    //relic locations for unique relics
    //can also refer to this commit https://github.com/homothetyhk/ItemChanger.Silksong/commit/d4e181e93cfae405a354a171958c9f18db4d968e by kickafetus
    internal static partial class BaseLocationList
    {
        public static Location Arcane_Egg => new ObjectLocation
        {
            Name = LocationNames.Arcane_Egg,
            SceneName = SceneNames.Abyss_04,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Sacred_Cylinder => new ObjectLocation
        {
            Name = LocationNames.Sacred_Cylinder,
            SceneName = SceneNames.Library_10,
            ObjectName = "Collectable Item Pickup - Melody",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };

    }
}
