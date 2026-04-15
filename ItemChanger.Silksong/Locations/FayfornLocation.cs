using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Locations;
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
        // Add FINISHED → Fade Back Pause since we suppress GET ITEM MSG END.
        FsmState msgState = fsm.MustGetState("Msg");
        msgState.Actions = msgState.Actions
            .Where(a => a.GetType().Name is not "SetPlayerDataVariable"
                                          and not "CreateUIMsgGetItem"
                                          and not "SetFsmString"
                                          and not "SendMessage"
                                          and not "Comment")
            .ToArray();
        msgState.InsertLambdaMethod(0, GiveAll);
        // After GiveAll completes, explicitly restore disablePause. UIMsgProxy.SetIsInMsg saves
        // and restores the prior value, so if the cutscene set disablePause=true before the IC
        // popup ran, SetIsInMsg(false) would restore it to true. Reset it here so menus work.
        msgState.InsertAction(1, new LambdaAction { Method = () =>
            PlayerData.instance.disablePause = false
        });
        fsm.AddTransition("Msg", "FINISHED", "Fade Back Pause");
    }
}
