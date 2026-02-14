using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Modules;
using ItemChanger.Silksong.FsmStateActions;
using PrepatcherPlugin;
using Silksong.FsmUtil;

namespace ItemChanger.Silksong.Modules.FastTravel;

/// <summary>
/// Module that automatically unlocks Bellway spots for fast travel entry.
/// </summary>
[SingletonModule]
public sealed class BellwayAutoUnlockModule : Module
{
    // TODO - verify this works. If this is true, then there should also be a module that automatically unlocks
    // the bell eater fight, either somewhere or anywhere
    /// <summary>
    /// If this is true, then the bellway can be used in act 3 prior to defeating bell eater.
    /// </summary>
    public bool BypassCentipede { get; set; } = false;

    protected override void DoLoad()
    {
        // TODO - rewrite with something like SilksongHost.Instance.SilksongEvents.AddFsmEdit
        On.PlayMakerFSM.Start += ModifyBellbeast;
        On.PlayMakerFSM.Start += ModifyUnlockBehaviour;
        PlayerDataVariableEvents.OnGetBool += SetBellwayUnlocked;
    }

    private bool SetBellwayUnlocked(PlayerData pd, string fieldName, bool current)
    {
        return current || fieldName == nameof(PlayerData.UnlockedFastTravel);
    }

    private void ModifyUnlockBehaviour(On.PlayMakerFSM.orig_Start orig, PlayMakerFSM self)
    {
        if (!self.gameObject.name.StartsWith("Bellway Toll Machine") || self.FsmName != "Unlock Behaviour")
        {
            orig(self);
            return;
        }

        FsmState inertState = self.GetState("Inert")!;
        inertState.RemoveActionsOfType<FsmStateAction>();
        inertState.AddMethod(static a => { a.fsm.Event("ACTIVATED"); });

        orig(self);
    }

    private void ModifyBellbeast(On.PlayMakerFSM.orig_Start orig, PlayMakerFSM self)
    {
        if (self.gameObject.name != "Bone Beast NPC" || self.FsmName != "Interaction")
        {
            orig(self);
            return;
        }

        self.GetState("Is Unlocked?")!.ReplaceActionsOfType<PlayerDataBoolTest>(oldTest => new CustomCheckFsmStateAction(oldTest) { GetIsTrue = () => true });
        self.GetState("Can Appear")!.ReplaceActionsOfType<PlayerDataBoolTest>(oldTest => new CustomCheckFsmStateAction(oldTest) { GetIsTrue = () => true });

        if (BypassCentipede)
        {
            self.GetState("Centipede")!.ReplaceActionsOfType<PlayerDataVariableTest>(oldTest => new CustomCheckFsmStateAction(oldTest) { GetIsTrue = () => false });
            self.GetState("Appear Delay")!.ReplaceActionsOfType<PlayerDataVariableTest>(oldTest => new CustomCheckFsmStateAction(oldTest) { GetIsTrue = () => false });
        }

        orig(self);
    }

    protected override void DoUnload()
    {
        On.PlayMakerFSM.Start -= ModifyBellbeast;
        On.PlayMakerFSM.Start -= ModifyUnlockBehaviour;
        PlayerDataVariableEvents.OnGetBool -= SetBellwayUnlocked;
    }
}
