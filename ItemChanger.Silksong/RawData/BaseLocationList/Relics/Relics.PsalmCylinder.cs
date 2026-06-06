using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Tags;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.RawData
{
    //relic locations for psalm cylinders
    //can also refer to this commit https://github.com/homothetyhk/ItemChanger.Silksong/commit/d4e181e93cfae405a354a171958c9f18db4d968e by kickafetus
    internal static partial class BaseLocationList
    {
        public static Location Psalm_Cylinder__Ascendence_Theme => new ObjectLocation
        {
            Name = LocationNames.Psalm_Cylinder__Ascendence_Theme,
            SceneName = SceneNames.Hang_10,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Psalm_Cylinder__Choir_Voices => new ObjectLocation
        {
            Name = LocationNames.Psalm_Cylinder__Choir_Voices,
            SceneName = SceneNames.Library_08,
            ObjectName = "Collectable Item Pickup Librarian",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };

        //Psalm_Cylinder__Salvation_Theme: sold by grindle [https://github.com/homothetyhk/ItemChanger.Silksong/issues/85]

        public static Location Psalm_Cylinder__Sermon => new ObjectLocation
        {
            Name = LocationNames.Psalm_Cylinder__Sermon,
            SceneName = SceneNames.Library_09,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Psalm_Cylinder__Surgery => new ObjectLocation
        {
            Name = LocationNames.Psalm_Cylinder__Surgery,
            SceneName = SceneNames.Under_08,
            ObjectName = "Collectable Item Pickup (1)",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };

    }
}
