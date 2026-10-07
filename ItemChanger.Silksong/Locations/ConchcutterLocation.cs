using ItemChanger.Containers;
using ItemChanger.Extensions;
using ItemChanger.Locations;
using ItemChanger.Silksong.Components;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Locations;

/// <summary>
/// The conchcutter exists in two shinies, the pickup version and the fling version. This location modifies both.
/// </summary>
public class ConchcutterLocation : ContainerLocation
{
    /// <summary>
    /// The collectable pickup that is flung. Must be Managed.
    /// </summary>
    public required DelayedShinyLocation FlingLocation { get; init; }
    /// <summary>
    /// Offset to spawn new containers at.
    /// </summary>
    public required Vector3 Correction { get; init; }

    protected override void DoLoad()
    {
        Using(new SceneEditGroup { { SceneName!, ModifyScene } });
        FlingLocation.Placement = Placement;
        FlingLocation.LoadOnce();
    }

    protected override void DoUnload() => FlingLocation.UnloadOnce();

    private void ModifyScene(Scene scene)
    {
        GetContainer(scene, out Container container, out ContainerInfo info);

        GameObject root = scene.FindGameObject("Memory Group/Collectible Item Pickup Scene")!;
        GameObject stand = root.FindChild("Collectable Item Pickup Stand")!;
        if (container.Name == ContainerNames.Shiny && !Placement!.CheckVisitedAny(Enums.VisitState.ObtainedAnyItem))
        {
            // Modify both the stand and fling objects.
            UObject.Destroy(root.FindChild("Breaker")!.GetComponent<TestGameObjectActivator>());
            container.ModifyContainerInPlace(stand, info);
            GameObject fling = root.FindChild("Collectable Item Pickup Fling")!;
            FlingLocation.ApplyEdits(fling, container, info);
        }
        else
        {
            // Replace the stand entirely.
            GameObject surrogate = new();
            surrogate.transform.position = stand.transform.position;
            this.ReplaceContainer(container, info, surrogate, Correction);
            root.AddComponent<Deactivator>();
        }
    }
}
