using ItemChanger.Containers;
using ItemChanger.Locations;
using ItemChanger.Tags;
using UnityEngine;

namespace ItemChanger.Silksong.Locations;

/// <summary>
/// Coordinate location that automatically stacks placements with the same coordinates into the same forced container.
/// </summary>
public sealed class CoalescingCoordinateLocation : CoordinateLocation
{
    required public string ContainerType { get; init; } = "Chest";

    /// <summary>
    /// List of container types known to support adding placements by repeatedly calling ModifyContainerInPlace
    /// </summary>
    private readonly IReadOnlyList<string> SupportedContainerTypes =
    [
        "Chest",
    ];

    public CoalescingCoordinateLocation() : base()
    {
        if (!SupportedContainerTypes.Contains(ContainerType)) throw new NotSupportedException();
        if (GetTag<OriginalContainerTag>() == null)
        {
            AddTag(new OriginalContainerTag()
                {
                    ContainerType = ContainerType,
                    Force = true,
                });
        }
    }

    public override void PlaceContainer(Container container, ContainerInfo info)
    {
        GameObject[] rootGameObjects = info.ContainingScene.GetRootGameObjects();
        // Iterate in reverse order to find IC-placed containers sooner
        for (int i = rootGameObjects.Length - 1; i >= 0; i--)
        {
            GameObject obj = rootGameObjects[i];
            ContainerInfo? objInfo = ContainerInfo.FindContainerInfo(obj);
            if (objInfo != null && objInfo.ContainerType == info.ContainerType)
            {
                // Check if the coordinates are equivalent; CoordinateLocations have no Correction,
                // so this should always work for containers that can't move on their own
                if (!Mathf.Approximately(obj.transform.position.x, X)) continue;
                if (!Mathf.Approximately(obj.transform.position.y, Y)) continue;
                // If the container is properly supported, this will add this location's Placement to the existing container
                // without removing or conflicting with its existing placement(s)
                container.ModifyContainerInPlace(obj, info);

                return;
            }
        }

        base.PlaceContainer(container, info);
    }
}
