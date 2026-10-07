using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.Tags;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
    public static Location Crawbell => new ObjectLocation
    {
        Name = LocationNames.Crawbell,
        SceneName = SceneNames.Room_CrowCourt_02,
        ObjectName = "Collectable Item Pickup Crawbell",
        FlingType = Enums.FlingType.Everywhere,
        Correction = new(0, -1.5f),
        Tags = [
            new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true },
            SmallFleaPrefabs
        ]
    };

    public static Location Farsight => new ObjectLocation
    {
        Name = LocationNames.Farsight,
        SceneName = SceneNames.Abyss_08,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = new(2.5f, -1.4f),
        Tags = [
            new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true },
            new DeactivateObjectTag
            {
                SceneName = SceneNames.Abyss_08,
                ObjectName = "weaver_heat_lamp_0005_1_plinth",
                Test = new UnexpectedContainerBool { PlacementName = LocationNames.Farsight, ExpectedContainerType = ContainerNames.Shiny }
            },
            SmallFleaPrefabs
        ]
    };

    public static Location Materium => new ObjectLocation
    {
        Name = LocationNames.Materium,
        SceneName = SceneNames.Arborium_07,
        ObjectName = "Collectable Item Pickup Materium",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [
            new OriginalContainerTag { ContainerType = ContainerNames.Shiny },
            new DeactivateObjectTag  // Remove the Materium graphic if it's not present.
            {
                SceneName = SceneNames.Arborium_07,
                ObjectName = "Collectable Item Pickup Materium/Item Display Folder",
                Test = new IsItemMissingBool { PlacementName = LocationNames.Materium, ItemName = ItemNames.Materium }
            }
        ]
    };
}
