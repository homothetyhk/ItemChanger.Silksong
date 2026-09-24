using System.Collections;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Extensions;
using PrepatcherPlugin;
using Silksong.FsmUtil;
using Silksong.FsmUtil.Actions;
using UnityEngine;

namespace ItemChanger.Silksong.Locations;

public class ArchitectPuzzleLocation : ThreefoldMelodyLocation
{
    private static WaitForSeconds _waitForSeconds5 = new(5f);

    protected override void DoLoad()
    {
        Using(new FsmEditGroup()
        {
            {new(SceneName!, "puzzle cylinders", "Cylinder States"), HookCylinders},
            {new(SceneName!, "puzzle cylinders", "Prompt Control"), HookNeedolinPrompt},
        });
    }

    protected override void DoUnload() { }

    private void HookCylinders(PlayMakerFSM fsm)
    {
        // Replace the hasMelodyArchitect check with a placement-obtained check so
        // the scene shows as "Completed" if the placement has no items to give.
        fsm.MustGetState("Wait For Notify").ReplaceFirstActionOfType<PlayerDataVariableTest>(
            new LambdaAction { Method = () =>
                {
                    if (Placement!.AllObtained()) fsm.SendEvent("CANCEL");
                }
            });

        // The vanilla game doesn't enforce a Needolin requirement for this cutscene, so we need to make one ourselves
        FsmState promptState = fsm.MustGetState("Needolin Prompt");
        fsm.fsm.events = [ ..fsm.fsm.events, NoNeedolin ];
        promptState.InsertAction(new PlayerDataBoolTest() { boolName = "hasNeedolin", isFalse = NoNeedolin}, 4);

        // Create a new state for the fsm that does the following:
        // - Enable inventory
        // - Remove camera lock
        // - Restore HUD
        // - Prompt Needolin (to remind the player that they don't have it)
        // - Wait 5 seconds
        // - Hide prompt
        // - Return to "Pressure Plate Raise" state to wait for Hornet to stand on it again
        FsmState hintState = fsm.AddState(new FsmState(fsm.Fsm)
        {
            name = "Needolin Hint",
            // functionality implemented as below as LambdaMethod
            transitions = 
            [
                new()
                {
                    fsmEvent = FsmEvent.Finished,
                    toFsmState = fsm.MustGetState("Pressure Plate Raise"),
                    toState = "Pressure Plate Raise"
                }
            ],
        });
        IEnumerator Run(Action finish)
        {
            PlayerDataAccess.disableInventory = false;
            // Unlock the camera
            fsm.MustGetState("Singing End").GetFirstActionOfType<ActivateGameObject>()!.gameObject.gameObject.value.SetActive(false);
            fsm.fsm.Event(fsm.MustGetState("Start Lock").GetFirstActionOfType<SendEventByNameV2>()!.eventTarget, "IN");  // Something like this
            EventRegister.SendEvent("REMINDER NEEDOLIN");
            yield return _waitForSeconds5;
            EventRegister.SendEvent("REMINDER NEEDOLIN END");
            finish();
        }
        hintState.AddLambdaMethod(cb => fsm.StartCoroutine(Run(cb)));
        promptState.AddTransition("NO NEEDOLIN", "Needolin Hint");
    }

    private void HookNeedolinPrompt(PlayMakerFSM fsm) {
        ReplaceMelody
        (
            fsm.MustGetState("Get Melody").GetFirstActionOfType<RunFSM>()!,
            fsm.gameObject.FindChild("Hornet_pressure_plate")!.transform,
            "GET ITEM MSG COVERED"
        );
    }

    protected static readonly FsmEvent NoNeedolin = new("NO NEEDOLIN");
}
