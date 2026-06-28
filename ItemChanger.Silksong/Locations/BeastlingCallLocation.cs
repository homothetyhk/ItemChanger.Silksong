using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Locations;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Serialization;
using Silksong.FsmUtil;

namespace ItemChanger.Silksong.Locations;

public class BeastlingCallLocation : AutoLocation
{
    /// <summary>
    /// Requirements to activate the dialogue with Bell Beast after Bell Eater is defeated. Defaults to having
    /// obtained the Needolin.
    /// </summary>
    public IValueProvider<bool> InteractRequirements { get; init; } = new PDBool(nameof(PlayerData.hasNeedolin));

    protected override void DoLoad()
    {
        Using(new FsmEditGroup()
        {
            { new(SceneName!, "Bell Beast DefeatedCentipede NPC", "Control"), HookBellEaterDefeat },
        });
    }

    protected override void DoUnload()
    {
    }

    private void HookBellEaterDefeat(PlayMakerFSM fsm)
    {
        // Only allow interaction with Bell Beast if the requirements are met
        FsmState startState = fsm.MustGetState("Start NPC");

        FsmState enableInteractState = fsm.AddState("Enable Interact");
        startState.AddTransition("ENABLE INTERACT", "Enable Interact");
        enableInteractState.AddTransition("FINISHED", "Idle L");

        enableInteractState.AddAction(startState.GetFirstActionOfType<ActivateInteractible>()!);
        startState.RemoveFirstActionOfType<ActivateInteractible>();

        startState.AddMethod(() => fsm.SendEvent(InteractRequirements.Value ? "ENABLE INTERACT" : "FINISHED"));

        // Also shortcut ActivateInteractible events on tink response states
        FsmState lTinkState = fsm.MustGetState("Tink React L");
        lTinkState.AddTransition("NO INTERACT", "Idle L");
        lTinkState.AddMethod(() => fsm.SendEvent(InteractRequirements.Value ? "FINISHED" : "NO INTERACT"));

        FsmState rTinkState = fsm.MustGetState("Tink React R");
        rTinkState.AddTransition("NO INTERACT", "Idle R");
        rTinkState.AddMethod(() => fsm.SendEvent(InteractRequirements.Value ? "FINISHED" : "NO INTERACT"));

        // Remove Beastling Call UI popup
        FsmState pickupMessageState = fsm.MustGetState("Get Item Msg");
        pickupMessageState.RemoveFirstActionOfType<CreateUIMsgGetItem>();

        // Give placement - given slightly early so that UI popup doesn't get eaten by the scene transition
        pickupMessageState.InsertMethod(3, () => GiveAll(() => fsm.SendEvent("GET ITEM MSG END")));

        // Remove granting Beastling Call
        FsmState timePassesState = fsm.MustGetState("Time Passes");
        timePassesState.RemoveFirstActionOfType<SetPlayerDataVariable>();
    }
}