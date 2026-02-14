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
        // TODO - rewrite with something like SilksongHost.Instance.SilksongEvents.AddFsmEdit
        On.PlayMakerFSM.Start += ModifyVentrica;
        On.PlayMakerFSM.Start += ModifyUnlockBehaviour;
    }

    private void ModifyUnlockBehaviour(On.PlayMakerFSM.orig_Start orig, PlayMakerFSM self)
    {
        if (!self.gameObject.name.StartsWith("tube_toll_machine") || self.FsmName != "Unlock Behaviour")
        {
            orig(self);
            return;
        }

        FsmState inertState = self.GetState("Inert")!;
        inertState.RemoveActionsOfType<FsmStateAction>();
        inertState.AddMethod(static a => { a.fsm.Event("ACTIVATED"); });

        orig(self);

    }

    private void ModifyVentrica(On.PlayMakerFSM.orig_Start orig, PlayMakerFSM self)
    {
        if (self.gameObject.name != "City Travel Tube" || self.FsmName != "Tube Travel")
        {
            orig(self);
            return;
        }

        FsmState lockedState = self.GetState("Locked?")!;
        lockedState.ReplaceActionsOfType<PlayerDataBoolTest>(oldTest => new CustomCheckFsmStateAction(oldTest) { GetIsTrue = () => true });

        orig(self);
    }

    protected override void DoUnload()
    {
        On.PlayMakerFSM.Start -= ModifyVentrica;
        On.PlayMakerFSM.Start -= ModifyUnlockBehaviour;
    }
}
