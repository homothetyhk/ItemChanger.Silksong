using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Locations;
using PrepatcherPlugin;
using Silksong.FsmUtil;

namespace ItemChanger.Silksong.Locations;

public class PhantomLocation : AutoLocation
{
    protected override void DoLoad()
    {
        Using(new FsmEditGroup()
        {
            {new(SceneName!, "Phantom", "Control"), HookPhantomControl},
            {new(SceneName!, "Phantom", "Set Cross Stitch Connected"), HookSetCrossStitchConnected},
        });
    }

    protected override void DoUnload() { }

    private void HookPhantomControl(PlayMakerFSM fsm)
    {
        // Replace all vanilla actions in "UI Msg" (SpawnSkillGetMsg, SetPlayerDataBool, etc.) with
        // GiveAll + setting defeatedPhantom. Add a FINISHED transition to "Get Control" since the
        // vanilla transition fires on a skill-screen event that we've suppressed.
        FsmState uiMsgState = fsm.MustGetState("UI Msg");
        uiMsgState.Actions = [];
        uiMsgState.InsertMethod(0, () => GiveAll(() =>
        {
            PlayerDataAccess.defeatedPhantom = true;
            fsm.SendEvent("FINISHED");
        }));
        fsm.AddTransition("UI Msg", "FINISHED", "Get Control");

        // Remove the ScreenFader from Get Control — it fades to black for the vanilla skill-get screen,
        // but we skip that screen so we never want the screen to go black.
        fsm.MustGetState("Get Control").RemoveActionsOfType<ScreenFader>();
        // We never faded to black, so skip End Pause (no fade-in needed) — go straight to Set Data.
        fsm.AddTransition("Get Control", "FINISHED", "Set Data");
        // Set Data fires FINISHED via SendEventByNameV2 — route to End to restore Hornet's sprite.
        fsm.AddTransition("Set Data", "FINISHED", "End");
        // End's vanilla FINISHED transition loops back to Set Data — remove it to prevent an infinite cycle.
        fsm.RemoveTransition("End", "FINISHED");

        // Skip the post-death cutscene: jump directly to "UI Msg" instead of "Fade To Black".
        fsm.RemoveTransitionsTo("Death Explode", "Fade To Black");
        fsm.AddTransition("Death Explode", "FINISHED", "UI Msg");
    }

    private static void HookSetCrossStitchConnected(PlayMakerFSM fsm)
    {
        foreach (FsmState state in fsm.FsmStates)
        {
            state.RemoveActionsOfType<SetFsmBool>();
        }
    }
}
