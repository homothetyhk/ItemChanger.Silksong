using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Tags;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.RawData
{
    // This is a partial class containing a large number of Location properties. The properties are arranged in the files in the BaseLocationList folder.

    internal static partial class BaseLocationList
    {
        // Convenience tag: configures a shiny to float in place rather than fall.
        // Used by locations whose objects are not CollectableItemPickup and need FloatInPlace fling.
        private static ShinyControlTag FloatShiny => new() { Info = new() { ShinyFling = ShinyContainer.ShinyFling.FloatInPlace } };

        public static Location Start => new StartLocation
        {
            Name = LocationNames.Start,
            FlingType = Enums.FlingType.DirectDeposit,
            MessageType = Enums.MessageType.SmallPopup,
        };

        public static Location Pale_Oil__Whispering_Vaults => new ObjectLocation
        {
            Name = LocationNames.Pale_Oil__Whispering_Vaults,
            SceneName = SceneNames.Library_03,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };

        public static Dictionary<string, Location> GetBaseLocations()
        {
            return typeof(BaseLocationList).GetProperties().Select(p => (Location)p.GetValue(null)).ToDictionary(l => l.Name);
        }
    }
}
