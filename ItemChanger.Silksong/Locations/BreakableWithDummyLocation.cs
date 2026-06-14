using GlobalSettings;
using ItemChanger.Containers;
using ItemChanger.Enums;
using ItemChanger.Extensions;
using ItemChanger.Locations;
using ItemChanger.Placements;
using ItemChanger.Silksong.Containers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Locations;

/// <summary>
/// Location for items held inside a breakable container, where a dummy object is displayed before the container is broken.
/// The IC location replaces both the item and its dummy by shiny items.
/// </summary>
public class BreakableWithDummyLocation : AutoLocation
{
    /// <summary>
    /// Name of a GameObject stored in the remnantParts array of the breakable, to be replaced by a shiny item.
    /// </summary>
    public required string ObjectName { get; init; }
    /// <summary>
    /// Path to a breakable object in the scene.
    /// </summary>
    public required string BreakablePath { get; init; }
    /// <summary>
    /// Path to the dummy item in the scene, to be replaced by a dummy shiny.
    /// </summary>
    public required string DummyPath { get; init; }
    /// <summary>
    /// True if the original dummy is in the wholeParts array of the breakable. False if it is in the hierarchy of a whole part.
    /// </summary>
    public required bool ReplaceWholePart { get; init; }

    protected override void DoLoad()
    {
        ItemChangerHost.Singleton.GameEvents.AddSceneEdit(SceneName!, SetupContainer);
    }

    protected override void DoUnload()
    {
        ItemChangerHost.Singleton.GameEvents.RemoveSceneEdit(SceneName!, SetupContainer);
    }

    private void SetupContainer(Scene scene)
    {
        Breakable? breakable = scene.FindGameObject(BreakablePath)?.GetComponent<Breakable>();
        if (breakable == null)
        {
            LogWarn($"{nameof(BreakableWithDummyLocation)} Could not find {BreakablePath} in {scene.name}");
            return;
        }

        int remnantIndex = Array.FindIndex(breakable.remnantParts, 0, o => o.name == ObjectName);
        if (remnantIndex == -1)
        {
            LogWarn($"[{nameof(BreakableWithDummyLocation)}] Child {ObjectName} is not a remnant part of {BreakablePath} in {scene.name}");
            return;
        }
        GameObject child = breakable.remnantParts[remnantIndex];

        GameObject? origDummy = scene.FindGameObject(DummyPath);
        if (origDummy == null)
        {
            LogWarn($"{nameof(BreakableWithDummyLocation)} Could not find {DummyPath} in {scene.name}");
            return;
        }

        ContainerInfo info = ContainerInfo.FromPlacement(Placement!, scene, ContainerNames.Shiny, FlingType, (Placement as ISingleCostPlacement)?.Cost);
        GameObject shiny = ShinyContainer.Instance.GetNewContainer(info);
        ShinyContainer.Instance.ApplyTargetContext(shiny, child, Vector3.zero);

        breakable.remnantParts[remnantIndex] = shiny;
        UObject.Destroy(child);

        GameObject dummy = scene.Instantiate(Gameplay.CollectableItemPickupInstantPrefab.gameObject);
        dummy.name = "IC Dummy Shiny - " + shiny.name;
        ShinyContainer.Instance.ApplyTargetContext(dummy, origDummy, Vector3.zero);
        UObject.Destroy(dummy.GetComponent<CollectableItemPickup>());
        dummy.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        if (ReplaceWholePart)
        {
            int i = Array.FindIndex(breakable.wholeParts, o => o == origDummy);
            if (i != -1)
            {
                breakable.wholeParts[i] = dummy;
            }
            else
            {
                LogWarn($"[{nameof(BreakableWithDummyLocation)}] {origDummy.name} is not a whole part of {BreakablePath} in {scene.name}");
            }
        }
        UObject.Destroy(origDummy);
    }
}
