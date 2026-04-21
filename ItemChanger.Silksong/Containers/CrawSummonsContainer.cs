using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Containers;
using Silksong.FsmUtil;
using UnityEngine;

namespace ItemChanger.Silksong.Containers;

/// <summary>
/// Container representing the Craw Summons pin.
/// </summary>
public class CrawSummonsContainer : Container
{
    public override string Name => ContainerNames.CrawSummons;
    public override uint SupportedCapabilities => ContainerCapabilities.None;
    public override bool SupportsInstantiate => false;
    public override bool SupportsModifyInPlace => true;

    protected override void DoLoad() { }

    protected override void DoUnload() { }

    public override void ModifyContainerInPlace(GameObject obj, ContainerInfo info)
    {
        PlayMakerFSM fsm = obj.LocateMyFSM("FSM");

        // Remove the summons cloth from the pin iff the placement is obtained
        FsmState emptyState = fsm.MustGetState("Empty?");
        emptyState.Actions = [];
        emptyState.AddLambdaMethod(_ =>
        {
            if (info.GiveInfo.Placement.AllObtained())
                fsm.SendEvent("TRUE");
            else
                fsm.SendEvent("FINISHED");
        });

        // Replace the craw summons item
        FsmState setPickupState = fsm.MustGetState("Set Pickup");
        SavedContainerItem icItem = ScriptableObject.CreateInstance<SavedContainerItem>();
        icItem.ContainerInfo = info;
        icItem.ContainerTransform = obj.transform;
        setPickupState.GetFirstActionOfType<SetCollectablePickupItem>()!.Item = icItem;
    }
}