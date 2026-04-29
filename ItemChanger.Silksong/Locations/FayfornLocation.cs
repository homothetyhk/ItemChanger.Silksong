using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Locations;
using PrepatcherPlugin;
using Silksong.FsmUtil;
using Silksong.FsmUtil.Actions;

namespace ItemChanger.Silksong.Locations;

public class FayfornLocation : AutoLocation
{
    protected override void DoLoad()
    {
        Using(new FsmEditGroup()
        {
            {new(SceneName!, "DJ Get Sequence", "DJ Get Sequence"), HookDJGetSequence},
        });
    }

    protected override void DoUnload() { }

    private void HookDJGetSequence(PlayMakerFSM fsm)
    {
        // Replace the hasDoubleJump check with a placement-obtained check so
        // the scene shows as "Completed" iff the item has already been given.
        fsm.MustGetState("Has DJ?").ReplaceFirstActionOfType<PlayerDataVariableTest>(
            new LambdaAction { Method = () =>
                fsm.SendEvent(Placement!.AllObtained() ? "TRUE" : "FALSE")
            });

        // Remove only the hasDoubleJump ability grant from "Fade Back" — preserve any other
        // SetPlayerDataVariable actions (e.g. the disablePause reset) so control restoration works.
        fsm.MustGetState("Fade Back").Actions = fsm.MustGetState("Fade Back").Actions
            .Where(a => !(a is SetPlayerDataVariable spd && spd.VariableName.Value == "hasDoubleJump"))
            .ToArray();

        // Remove ability grant and skill popup from Msg, keep ActivateGameObject/ScreenFader
        // since they manage UI state (including any pause blockers). Add GiveAll in their place.
        FsmState msgState = fsm.MustGetState("Msg");
        msgState.Actions = msgState.Actions
            .Where(a => a is not SetPlayerDataVariable
                          and not CreateUIMsgGetItem
                          and not SetFsmString
                          and not SendMessage
                          and not Comment)
            .ToArray();
        // Pass disablePause reset and FSM advance as the GiveAll callback so both run after all
        // items are given. UIMsgProxy.SetIsInMsg restores the prior value of disablePause, so if
        // the cutscene set it to true, the callback resets it so menus work. Reusing the original
        // "GET ITEM MSG END" event means no transition edits are needed.
        msgState.InsertMethod(0, () => GiveAll(() =>
        {
            PlayerDataAccess.disablePause = false;
            fsm.SendEvent("GET ITEM MSG END");
        }));
    }
}
