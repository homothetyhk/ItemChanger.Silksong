using HutongGames.PlayMaker;
using ItemChanger.Containers;
using ItemChanger.Silksong.Containers;
using Silksong.FsmUtil;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Locations;

/// <summary>
/// Custom location that tweaks the wake-enemies FSM logic.
/// TODO: Currently this is always instantiated as a shiny container. When other containers support
/// falling, this location could support instantiating other containers directly in place of the corpse.
/// </summary>
public class SurgeonsKeyLocation : StrictObjectLocation
{
    protected override void ModifyContainerInPlace(Scene scene, Container container, ContainerInfo info)
    {
        base.ModifyContainerInPlace(scene, container, info);

        // Picking up the shiny triggers an enemy spawn which can cause unavoidable damage if this shiny happens to contain large popups or lore.
        // So we modify the fsm to wake the enemy *after* the pickup fully completes, instead of at the start of the interaction.
        const string SAFE_EVENT = "ITEM COLLECTED SAFE";
        PlayMakerFSM fsm = StrictFindObject(scene, "Group/Junk Hatch").LocateMyFSM("Control")!;
        FsmState endState = fsm.MustGetState("End");
        endState.RemoveTransition("ITEM COLLECTED");
        endState.AddTransition(SAFE_EVENT, "Wake Enemies");

        GameObject target = StrictFindObject(scene, ObjectName);
        ((SavedContainerItem)target.GetComponent<CollectableItemPickup>().Item).Callback += () => fsm.SendEvent(SAFE_EVENT);
    }
}
