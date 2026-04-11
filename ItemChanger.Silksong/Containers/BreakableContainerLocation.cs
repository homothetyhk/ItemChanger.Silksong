using ItemChanger.Containers;
using ItemChanger.Enums;
using ItemChanger.Extensions;
using ItemChanger.Locations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Containers;

/// <summary>
/// Location for items held inside a breakable container. The child object named
/// <see cref="ObjectName"/> is used to locate the parent <see cref="PersistentBoolItem"/> in the scene.
/// An IC shiny is pre-created at that position and revealed when the container is broken
/// (i.e. when the container's <c>PersistentBoolItem</c> transitions to <c>true</c>).
/// </summary>
public class BreakableContainerLocation : AutoLocation
{
    /// <summary>
    /// Name of a child object inside the breakable container, used to locate it in the scene.
    /// </summary>
    public required string ObjectName { get; init; }

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
        GameObject? child = scene.FindGameObjectByName(ObjectName);
        if (child == null)
        {
            UnityEngine.Debug.LogWarning($"[BreakableContainerLocation] {Name}: could not find '{ObjectName}' in {scene.name}");
            return;
        }

        PersistentBoolItem? persistentBool = child.GetComponentInParent<PersistentBoolItem>();
        if (persistentBool == null)
        {
            UnityEngine.Debug.LogWarning($"[BreakableContainerLocation] {Name}: no PersistentBoolItem found on parent of '{ObjectName}' in {scene.name}");
            return;
        }

        Vector3 spawnPos = child.transform.position;

        // Remove the original child so it doesn't interfere with the IC shiny.
        UObject.Destroy(child);

        if (Placement!.AllObtained()) return;

        ContainerInfo info = ContainerInfo.FromPlacement(
            Placement!,
            scene,
            ContainerNames.Shiny,
            FlingType.DirectDeposit
        );

        GameObject shiny = ShinyContainer.Instance.GetNewContainer(info);
        ShinyContainer.Instance.ApplyTargetContext(shiny, spawnPos, Vector3.zero);
        shiny.SetActive(false); // hidden until the container is broken

        // PersistentBoolItem.OnSetSaveState only fires at save time, not at the moment
        // the FSM sets Activated=true during gameplay. Poll GetCurrentValue() each frame
        // via a watcher component so the shiny appears the instant the container breaks.
        ContainerBreakWatcher watcher = persistentBool.gameObject.AddComponent<ContainerBreakWatcher>();
        watcher.Target = persistentBool;
        watcher.OnBroken = () => { if (shiny != null) shiny.SetActive(true); };
    }

    private class ContainerBreakWatcher : MonoBehaviour
    {
        public PersistentBoolItem Target = null!;
        public System.Action OnBroken = null!;

        private void Update()
        {
            if (Target != null && Target.GetCurrentValue())
            {
                OnBroken?.Invoke();
                Destroy(this);
            }
        }
    }
}
