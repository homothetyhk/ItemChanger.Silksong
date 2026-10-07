using ItemChanger.Containers;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Locations;

/// <summary>
/// A shiny location which is only activated after a trigger of some sort, such as defeating a boss, or breaking a container.
/// For locations which have a distinct visible preview asset which is not a generic shiny, like a mask shard, use <see cref="BreakableWithDummyLocation"/> instead.
/// </summary>
public class DelayedShinyLocation : StrictObjectLocation
{
    /// <summary>
    /// Evaluated at scene load time to determine whether this shiny should give items early, like silk refills or rosaries.
    /// </summary>
    public required IValueProvider<bool> GiveEarly { get; init; }

    /// <summary>
    /// If true, something else is responsible for applying location edits.
    /// </summary>
    public bool Managed { get; init; } = false;

    /// <summary>
    /// Do not invoke directly unless this is a Managed location.
    /// </summary>
    public void ApplyEdits(GameObject target, Container container, ContainerInfo info)
    {
        // Replace the container lazily when the shiny becomes active, instead of immediately.
        var wasActive = target.activeSelf;
        target.SetActive(false);
        target.AddComponent<ReplaceContainerWhenActive>().Init(this, container, info, GiveEarly.Value);
        target.SetActive(wasActive);
    }

    protected override void OnSceneLoaded(Scene scene)
    {
        if (!Managed)
        {
            GetContainer(scene, out Container container, out ContainerInfo info);
            GameObject target = StrictFindObject(scene, ObjectName);
            ApplyEdits(target, container, info);
        }
    }

    /// <summary>
    /// Under certain condiitions, fling items from the specified position rather than leaving them in a single shiny.
    /// </summary>
    public static bool MaybeFlingItems(Vector3 position, Container container, ContainerInfo info)
    {
        if (container.Name != ContainerNames.Chest && container.Name != ContainerNames.Shiny)
            return false;
        if (info.CostInfo != null)
            return false;
        if (!info.GiveInfo.Items.Any(i => !i.IsObtained() && i.GiveEarly(ContainerNames.Chest)))
            return false;

        GameObject surrogate = new();
        surrogate.transform.position = position;
        info.OpenAndFlingItems(surrogate.transform, ContainerNames.Chest);
        return true;
    }
}

file class ReplaceContainerWhenActive : MonoBehaviour
{
    private DelayedShinyLocation? location;
    private Container? container;
    private ContainerInfo? info;
    private bool giveEarly;

    internal void Init(DelayedShinyLocation location, Container container, ContainerInfo info, bool giveEarly)
    {
        this.location = location;
        this.container = container;
        this.info = info;
        this.giveEarly = giveEarly;
    }

    private void OnEnable()
    {
        if (location == null || container == null || info == null)
            return;

        if (giveEarly && DelayedShinyLocation.MaybeFlingItems(transform.position, container, info))
            Destroy(gameObject);
        // We never spawn chests on delay so it doesn't make sense to materialize them later.  Force chests into shinies.
        else if (container.Name == ContainerNames.Shiny || container.Name == ContainerNames.Chest)
        {
            container.ModifyContainerInPlace(gameObject, info);
            Destroy(this);
        }
        else
            location.ReplaceWithContainer(gameObject.scene, container, info);
    }
}
