using HutongGames.PlayMaker.Actions;
using ItemChanger.Locations;
using ItemChanger.Silksong.RawData;
using Silksong.FsmUtil;

namespace ItemChanger.Silksong.Locations;

// Rune Rage is granted from a 'weaver corpse' but the fsm has significant differences from other weaver corpses so it gets a custom location.
public class RuneRageLocation : AutoLocation
{
    protected override void DoLoad()
    {
        Using(new FsmEditGroup()
        {
            { new(SceneName!, "Shrine First Weaver NPC", "Inspection"), ModifyFsm },
        });
    }

    protected override void DoUnload() { }

    private void ModifyFsm(PlayMakerFSM fsm)
    {
        var initState = fsm.MustGetState("Init");
        initState.GetFirstActionOfType<PlayerDataBoolTest>()?.enabled = false;
        initState.InsertMethod(0, _ =>
        {
            if (Placement?.AllObtained() ?? false) fsm.SendEvent("COMPLETE");
        });

        var collectedState = fsm.MustGetState("Collected Check");
        collectedState.GetFirstActionOfType<PlayerDataBoolTest>()?.enabled = false;
        collectedState.AddMethod(_ =>
        {
            if (Placement?.AllObtained() ?? false) fsm.SendEvent("COLLECTED");
        });

        bool DearestRuneRage() => fsm.FsmVariables.GetFsmBool("Is Rune Bomb").Value && (Placement?.Items.Any(i => i.Name == ItemNames.Rune_Rage) ?? false);
        var runeBombFxState = fsm.MustGetState("Rune Bomb FX");
        runeBombFxState.GetFirstActionOfType<BoolTest>()?.enabled = false;
        runeBombFxState.InsertMethod(0, _ =>
        {
            if (!DearestRuneRage()) fsm.SendEvent("FINISHED");
        });

        // Skip memories.
        var toMemoryState = fsm.MustGetState("To Memory?");
        toMemoryState.RemoveTransition("FINISHED");
        toMemoryState.AddTransition("FINISHED", "Heal");
        toMemoryState.InsertMethod(0, _ => fsm.SendEvent("FINISHED"));

        // Give items, allowing big UI defs.
        var giveState = fsm.MustGetState("Heal");
        giveState.AddLambdaMethod(GiveAll);

        var skipState = fsm.AddState("Reload Scene");
        giveState.RemoveTransition("FINISHED");
        giveState.AddTransition("FINISHED", "Reload Scene");

        skipState.AddAction(new BeginSceneTransition()
        {
            sceneName = SceneName,
            entryGateName = "door_wakeOnGround",
            entryDelay = 0,
            visualization = GameManager.SceneLoadVisualizations.Default,
            preventCameraFadeOut = false,
        });
        skipState.AddAction(new Wait() { time = 999 });  // Don't terminate the FSM before the scene loads.

        var endState = fsm.MustGetState("End");
        foreach (var set in endState.GetActionsOfType<HutongGames.PlayMaker.Actions.SetPlayerDataBool>()) set.enabled = false;
        endState.GetFirstActionOfType<CallStaticMethod>()?.enabled = false;

        fsm.MustGetState("Harpoon Dash Reminder").GetFirstActionOfType<SendEventToRegisterDelay>()?.enabled = false;
    }
}
