using ItemChanger.Locations;
using ItemChanger.Silksong.Extensions;
using Silksong.FsmUtil;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Newtonsoft.Json;

namespace ItemChanger.Silksong.Locations;

public class YarnabyLocation : AutoLocation
{
    [JsonIgnore]
    private FsmEditGroup? fsmEdits;    

    protected override void DoLoad()
    {
        fsmEdits = new()
        {
            {new(SceneName!, "Doctor Fly", "Dialogue"), HookYarnaby},
        };
    }

    protected override void DoUnload()
    {
        fsmEdits!.Dispose();
        fsmEdits = null;
    }

    private void HookYarnaby(PlayMakerFSM fsm)
    {
        void GotoConvoIfObtained()
        {
            if (Placement!.AllObtained())
            {
                fsm.SendEvent("FINISHED");
            }
        }

        void ModifyCursedCheckState(string stateName)
        {
            FsmState state = fsm.MustGetState(stateName);
            int i = state.IndexFirstActionOfType<PlayerDataVariableTest>();
            if (i == -1)
            {
                i = state.actions.Length;
            }
            state.InsertMethod(i, GotoConvoIfObtained);
        }

        ModifyCursedCheckState("Cursed? 3");
        ModifyCursedCheckState("Cursed? 2");

        FsmState comboBarState = fsm.MustGetState("Combo Bar Prompt");
        comboBarState.RemoveLastActionOfType<SetFsmString>();
        comboBarState.RemoveLastActionOfType<CreateObject>();
        comboBarState.RemoveTransition("GET ITEM MSG END");
        comboBarState.AddTransition("FINISHED", "Crest Get Antic");

        FsmState crestGetAnticState = fsm.MustGetState("Crest Get Antic");
        crestGetAnticState.RemoveLastActionOfType<Wait>();
        crestGetAnticState.RemoveLastActionOfType<HeroControllerMethods>();
        crestGetAnticState.RemoveLastActionMatching(act => 
            act is SendEventByName send
            && send.sendEvent.Value == "CREST CHANGE ANTIC");

        FsmState crestChangeState = fsm.MustGetState("Crest Change");
        crestChangeState.RemoveFirstActionOfType<UnlockCrest>();
        crestChangeState.RemoveFirstActionOfType<AutoEquipCrestV2>();
        crestChangeState.RemoveActionsOfType<CallMethodProper>();
        crestChangeState.InsertMethod(0, GiveAll);
    }
}