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
    // TODO - verify BypassCentipede works. If this is true, then there should also be a module that automatically unlocks
    // the bell eater fight, either somewhere or anywhere

    /// <summary>
    /// If this is true, then the bellway can be used in act 3 prior to defeating bell eater.
    /// </summary>
    public bool BypassCentipede { get; set; } = false;

    private FsmEditGroup? _fsmEdits;

    protected override void DoLoad()
    {
        PlayerDataVariableEvents.OnGetBool += SetBellwayUnlocked;
        _fsmEdits = new()
        {
            { new(SilksongHost.Wildcard, SilksongHost.Wildcard, "Unlock Behaviour"), ModifyUnlockBehaviour },
            { new(SilksongHost.Wildcard, "Bone Beast NPC", "Interaction"), ModifyBellbeast }
        };
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
        self.GetState("Is Unlocked?")!.ReplaceActionsOfType<PlayerDataBoolTest>(oldTest => new CustomCheckFsmStateAction(oldTest) { GetIsTrue = () => true });
        self.GetState("Can Appear")!.ReplaceActionsOfType<PlayerDataBoolTest>(oldTest => new CustomCheckFsmStateAction(oldTest) { GetIsTrue = () => true });

        if (BypassCentipede)
        {
            self.GetState("Centipede")!.ReplaceActionsOfType<PlayerDataVariableTest>(oldTest => new CustomCheckFsmStateAction(oldTest) { GetIsTrue = () => false });
            self.GetState("Appear Delay")!.ReplaceActionsOfType<PlayerDataVariableTest>(oldTest => new CustomCheckFsmStateAction(oldTest) { GetIsTrue = () => false });
        }
    }

    protected override void DoUnload()
    {
        PlayerDataVariableEvents.OnGetBool -= SetBellwayUnlocked;
        _fsmEdits?.Dispose();
        _fsmEdits = null;
    }
}
