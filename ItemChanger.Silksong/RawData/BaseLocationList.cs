using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Locations;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.RawData
{
    internal static class BaseLocationList
    {
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
