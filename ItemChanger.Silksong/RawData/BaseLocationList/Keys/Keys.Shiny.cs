using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Tags;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.RawData
{
    //key locations based on shiny pickup items
    internal static partial class BaseLocationList
    {
        public static Location Simple_Key__Sands_of_Karak => new ObjectLocation
        {
            Name = LocationNames.Simple_Key__Sands_of_Karak,
            SceneName = SceneNames.Bellshrine_Coral,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        

    }
}
