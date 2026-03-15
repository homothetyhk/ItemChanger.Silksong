using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Modules;
using ItemChanger.Silksong.FsmStateActions;
using Silksong.FsmUtil;

namespace ItemChanger.Silksong.Modules.FastTravel;

/// <summary>
/// Module that automatically unlocks Ventrica spots for fast travel entry.
/// </summary>
[SingletonModule]
public sealed class VentricaAutoUnlockModule : Module
{
    protected override void DoLoad()
    {
        Using(new FsmEditGroup()
        {
            { new(SilksongHost.Wildcard, "tube_toll_machine", "Unlock Behaviour"), ModifyUnlockBehaviour },
            { new(SilksongHost.Wildcard, "City Travel Tube", "Tube Travel"), ModifyVentrica }
        });
    }

    protected override void DoUnload() { }

    private void ModifyUnlockBehaviour(PlayMakerFSM self)
    {
        FsmState inertState = self.GetState("Inert")!;
        inertState.RemoveActionsOfType<FsmStateAction>();
        inertState.AddMethod(static a => { a.fsm.Event("ACTIVATED"); });
    }

    private void ModifyVentrica(PlayMakerFSM self)
    {

        FsmState lockedState = self.GetState("Locked?")!;
        lockedState.ReplaceActionsOfType<PlayerDataBoolTest>(oldTest => new CustomCheckFsmStateAction(oldTest) { GetIsTrue = () => true });
    }
}
