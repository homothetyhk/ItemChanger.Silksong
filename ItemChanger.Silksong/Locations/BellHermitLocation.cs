using ItemChanger.Locations;
using Newtonsoft.Json;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Silksong.FsmUtil;

namespace ItemChanger.Silksong.Locations;

public class BellHermitLocation : AutoLocation
{
    [JsonIgnore] private FsmEditGroup? fsmEdits;

    protected override void DoLoad()
    {
        fsmEdits = new()
        {
            { new(SceneName!, "Bell Hermit", "Dialogue"), HookBellHermit }
        };
    }

    protected override void DoUnload()
    {
        fsmEdits!.Dispose();
        fsmEdits = null;
    }

    private void HookBellHermit(PlayMakerFSM fsm)
    {
        FsmState doSnareConvoState = fsm.MustGetState("Do Snare Convo?");
        doSnareConvoState.InsertMethod(3, () =>
        {
            if (Placement!.AllObtained())
            {
                fsm.SendEvent("FALSE");
            }
        });
        
        FsmState giveSoulState = fsm.MustGetState("Give Soul");
        giveSoulState.RemoveActionsOfType<SavedItemGetV2>();
        giveSoulState.InsertMethod(4, GiveAll);
    }
}