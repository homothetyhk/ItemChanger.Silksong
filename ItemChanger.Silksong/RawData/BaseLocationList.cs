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

        // Shared tag for non-CollectableItemPickup interactables (Heart Piece, Silk Spool, Silk Grub, etc.)
        // IC replaces the object with a new shiny. ShinyType.Instant uses the contact-pickup prefab
        // (same mechanic as mossberry drops), bypassing the interactEvents system which does not
        // reliably detect the player for runtime-spawned shinies. FloatInPlace keeps the shiny
        // stationary at the original object's position rather than letting it fall.
        internal static ShinyControlTag FloatShiny => new()
        {
            Info = new ShinyContainer.ShinyControlInfo
            {
                ShinyType = ShinyContainer.ShinyType.Instant,
                ShinyFling = ShinyContainer.ShinyFling.FloatInPlace,
            }
        };

        public static Dictionary<string, Location> GetBaseLocations()
        {
            return typeof(BaseLocationList).GetProperties().Select(p => (Location)p.GetValue(null)).ToDictionary(l => l.Name);
        }
    }
}
