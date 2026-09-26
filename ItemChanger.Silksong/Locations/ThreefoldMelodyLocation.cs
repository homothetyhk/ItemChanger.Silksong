using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Locations;
using ItemChanger.Silksong.Extensions;
using Silksong.FsmUtil;
using UnityEngine;

namespace ItemChanger.Silksong.Locations;

public abstract class ThreefoldMelodyLocation : AutoLocation
{

    protected void ReplaceMelody(RunFSM runFSMAction, Transform? transform = null) {
        Fsm melodyGetTemplateFsm = runFSMAction.fsmTemplateControl.runFsm;
        FsmState giveItemState = melodyGetTemplateFsm.MustGetState("Give Item");
        // Replace SavedItemGetV2 call with a GiveAll delegate
        // For Architect, remove BigUI and event register that listens for BigUIs closing, replace with "FINISHED" transition
        giveItemState.actions = [];
        // Remove the BigUI popup for Conductor and Vaultkeeper
        FsmState? uiState = melodyGetTemplateFsm.GetState("UI Msg");
        if (uiState != null) {
            // The UI popup normally starts during the singing
            // Add a wait to prevent the state from moving on during the song
            uiState.actions = [ new Wait() { time = 3.5f} ];
            melodyGetTemplateFsm.MustGetState("Stop Needolin").GetTransition(0).fsmEvent = FsmEvent.Finished;
        }
        else
        {
            // Architect doesn't have a "UI Msg" state.
            // Instead, its UI popup is in its "Give Item" state, and it gives the item in the next state, "Return Control"
            uiState = giveItemState;
            melodyGetTemplateFsm.MustGetState("Return Control").RemoveFirstActionOfType<SavedItemGet>();
        }

        uiState.InsertAction(new SendEventToRegister(){ eventName = "GET ITEM MSG COVERED" }, 0);
        uiState.InsertLambdaMethod(1, this.CreateGiveAllDelegate(transform ? transform : melodyGetTemplateFsm.owner.transform));
        uiState.GetTransition(0).fsmEvent = FsmEvent.Finished;
    }
}
