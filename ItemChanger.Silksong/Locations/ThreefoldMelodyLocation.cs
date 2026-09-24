using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Locations;
using ItemChanger.Silksong.Extensions;
using Silksong.FsmUtil;
using UnityEngine;

namespace ItemChanger.Silksong.Locations;

public abstract class ThreefoldMelodyLocation : AutoLocation
{

    protected void ReplaceMelody(RunFSM runFSMAction, Transform? transform = null, string? eventOnComplete = null) {
        Fsm melodyGetTemplateFsm = runFSMAction.fsmTemplateControl.runFsm;
        // Remove the BigUI popup for Conductor and Vaultkeeper
        // Architect does this in Give Item, so it doesn't have this state
        FsmState? uiState = melodyGetTemplateFsm.GetState("UI Msg");
        if (uiState != null) {
            uiState.actions = [];
        }
        else
        {
            // Architect uses a SavedItemGet in the state after Give Item. Of course.
            melodyGetTemplateFsm.MustGetState("Return Control").RemoveFirstActionOfType<SavedItemGet>();
        }

        // Replace the melody with the placement's items
        FsmState giveItemState = melodyGetTemplateFsm.MustGetState("Give Item");
        // Replace SavedItemGetV2 call with a GiveAll delegate
        // For Architect, remove BigUI and event register that listens for BigUIs closing, replace with "FINISHED" transition
        giveItemState.actions = [];
        // If provided, manually send eventOnComplete to progress the outer FSM
        if (eventOnComplete != null)
        {
            giveItemState.AddAction(new SendEventToRegister{eventName = eventOnComplete});
        }
        giveItemState.GetTransition(0).fsmEvent = FsmEvent.Finished;
        giveItemState.InsertLambdaMethod(0, this.CreateGiveAllDelegate(transform ? transform : melodyGetTemplateFsm.owner.transform));
    }
}
