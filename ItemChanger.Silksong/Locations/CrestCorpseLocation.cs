using ItemChanger.Locations;
using ItemChanger.Silksong.Extensions;
using Silksong.FsmUtil;
using Silksong.FsmUtil.Actions;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Newtonsoft.Json;

namespace ItemChanger.Silksong.Locations;

public class CrestCorpseLocation : AutoLocation
{
    [JsonIgnore]
    private FsmEditGroup? fsmEdits;

    protected override void DoLoad()
    {
        fsmEdits = new()
        {
            {new(SceneName!, "Crest Get Shrine", "Control"), HookCorpse},
            {new(SceneName!, "Music Control", "Control"), HookMusic},
        };
    }

    protected override void DoUnload()
    {
        fsmEdits!.Dispose();
        fsmEdits = null;
    }

    private void HookCorpse(PlayMakerFSM fsm)
    {
        FsmState checkState = fsm.MustGetState("Check Unlocked");
        checkState.RemoveActionsOfType<GetIsCrestUnlocked>();
        checkState.InsertMethod(0, () =>
        {
            if (Placement!.AllObtained())
            {
                fsm.SendEvent("UNLOCKED");
            }
        });

        FsmState crestChangeState = fsm.MustGetState("Crest Change");
        crestChangeState.RemoveActionsOfType<UnlockCrest>();
        crestChangeState.RemoveActionsOfType<AutoEquipCrestV2>();
        crestChangeState.RemoveActionsOfType<ToolsActiveStateControlV2>();
        crestChangeState.InsertMethod(0, GiveAll);

        FsmState crestMsgState = fsm.MustGetState("Crest Msg");
        crestMsgState.RemoveActionsOfType<ShowToolCrestUIMsg>();

        FsmState crestGetAnticState = fsm.MustGetState("Crest Get Antic");
        crestGetAnticState.RemoveLastActionOfType<Wait>();
        crestGetAnticState.RemoveLastActionMatching(act => 
            act is SendEventByName send
            && send.sendEvent.Value == "CREST CHANGE ANTIC");
    }

    private void HookMusic(PlayMakerFSM fsm)
    {
        // Most chapels use Fade Up except for the Ruined Chapel, which uses State 1.
        FsmState? controlState = fsm.GetState("Fade Up") ?? fsm.GetState("State 1");
        // This is purely cosmetic so we can be graceful if it fails.
        if (controlState != null)
        {
            controlState.ReplaceFirstActionOfType<GetIsCrestUnlocked>(new LambdaAction { Method = () =>
            {
                if (Placement!.AllObtained())
                {
                    fsm.SendEvent("INACTIVE");
                }
            }});
        }
    }
}
