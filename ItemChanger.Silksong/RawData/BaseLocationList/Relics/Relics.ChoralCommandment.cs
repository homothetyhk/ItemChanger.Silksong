using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Tags;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.RawData
{
    //relic locations for choral commandments
    //can also refer to this commit https://github.com/homothetyhk/ItemChanger.Silksong/commit/d4e181e93cfae405a354a171958c9f18db4d968e by kickafetus
    internal static partial class BaseLocationList
    {

        //Choral_Commandment__Eternity: sold by jubilana [https://github.com/homothetyhk/ItemChanger.Silksong/issues/85]

        public static Location Choral_Commandment__Light => new ObjectLocation
        {
            Name = LocationNames.Choral_Commandment__Light,
            SceneName = SceneNames.Ward_05,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Choral_Commandment__Surgeon => new ObjectLocation
        {
            Name = LocationNames.Choral_Commandment__Surgeon, //test
            SceneName = SceneNames.Ward_02b,
            ObjectName = "Husk Item Ambush/Ward Bed (1)/corpse/Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Choral_Commandment__White_Wyrm => new ObjectLocation
        {
            Name = LocationNames.Choral_Commandment__White_Wyrm,
            SceneName = SceneNames.Aspid_01,
            ObjectName = "Collectable Item Pickup (2)",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };

    }
}
