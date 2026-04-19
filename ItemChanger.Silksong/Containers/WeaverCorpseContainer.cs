using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Containers;
using ItemChanger.Enums;
using ItemChanger.Items;
using ItemChanger.Placements;
using Silksong.FsmUtil;

namespace ItemChanger.Silksong.Containers;

/// <summary>
/// Container representing a Weaver Corpse, usually found in a Weaver Burial Spire.
///
/// <remarks>
/// This container should be used to replace GameObject "Shrine Weaver Ability",
///  which contains the FSM functionality for binding the corpse. An additional GameObject
///  "Burst Deactivate" contains the corpse sprite, and should be destroyed
///  when the container is replaced.
/// </remarks>
/// </summary>
public class WeaverCorpseContainer : Container
{
    public override string Name => ContainerNames.WeaverCorpse;
    public override uint SupportedCapabilities => ContainerCapabilities.None;
    public override bool SupportsInstantiate => false;
    public override bool SupportsModifyInPlace => true;

    protected override void DoLoad() { }
    protected override void DoUnload() { }

    public override void ModifyContainerInPlace(GameObject shrineWeaverAbility, ContainerInfo info)
    {
        PlayMakerFSM fsm = shrineWeaverAbility.GetComponent<PlayMakerFSM>();
        Placement placement = info.GiveInfo.Placement;
        
        // Make shrine available depending on whether placement has any items
        FsmState collectedCheckState = fsm.MustGetState("Collected Check");
        collectedCheckState.RemoveActionsOfType<PlayerDataBoolTest>();
        collectedCheckState.AddMethod(() =>
        {
            if (placement.AllObtained())
            {
                fsm.SendEvent("COLLECTED");
            }
        });
        
        // Skip long fade-to-black, auto-equip logic, ability-get text
        FsmState fadeToBlackState = fsm.MustGetState("Fade To Black");
        fadeToBlackState.AddTransition("SKIP_MSG", "Heal");
        fadeToBlackState.RemoveActionsOfType<Wait>();
        fadeToBlackState.AddLambdaMethod(_ =>
        {
            fsm.SendEvent("SKIP_MSG");
        });
        
        // Give ability slightly early - so the pickup shows during the stand-up animation
        FsmState getUpState = fsm.MustGetState("Get Up Pause");
        getUpState.GetFirstActionOfType<Wait>()!.finishEvent = null;
        getUpState.AddLambdaMethod(cb => placement.GiveAll(new GiveInfo{
            Container = info.ContainerType,
            FlingType = info.GiveInfo.FlingType,
            MessageType = MessageType.Any
        }, cb));
        
        // Remove original ability gain
        FsmState animationFinishedState = fsm.MustGetState("End");
        animationFinishedState.RemoveActionsOfType<HutongGames.PlayMaker.Actions.SetPlayerDataBool>();
        animationFinishedState.RemoveActionsOfType<CallStaticMethod>();
    }
}