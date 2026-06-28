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
    protected override void DoLoad()
    {
        PlayerDataVariableEvents.OnGetBool += SetBellwayUnlocked;
        Using(new FsmEditGroup()
        {
            { new(SilksongHost.Wildcard, SilksongHost.Wildcard, "Unlock Behaviour"), ModifyUnlockBehaviour },
            { new(SilksongHost.Wildcard, "Bone Beast NPC", "Interaction"), ModifyBellbeast }
        });
    }

    protected override void DoUnload()
    {
        PlayerDataVariableEvents.OnGetBool -= SetBellwayUnlocked;
    }

    private bool SetBellwayUnlocked(PlayerData pd, string fieldName, bool current)
    {
        return current || fieldName == nameof(PlayerData.UnlockedFastTravel);
    }

    private void ModifyUnlockBehaviour(PlayMakerFSM self)
    {
        if (!self.gameObject.name.StartsWith("Bellway Toll Machine"))
        {
            return;
        }

        FsmState inertState = self.GetState("Inert")!;
        inertState.RemoveActionsOfType<FsmStateAction>();
        inertState.AddMethod(static a => { a.fsm.Event("ACTIVATED"); });
    }

    private void ModifyBellbeast(PlayMakerFSM self)
    {
        // Bellway is always usable
        self.GetState("Is Unlocked?")!.ReplaceActionsOfType<PlayerDataBoolTest>(oldTest =>
            new CustomCheckFsmStateAction(oldTest) { GetIsTrue = () => true });
        self.GetState("Can Appear")!.ReplaceActionsOfType<PlayerDataBoolTest>(oldTest =>
            new CustomCheckFsmStateAction(oldTest) { GetIsTrue = () => true });
    }
}