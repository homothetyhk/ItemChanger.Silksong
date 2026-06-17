using ItemChanger.Locations;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Silksong.FsmUtil;

namespace ItemChanger.Silksong.Locations;

public class LoddieAct3DeskLocation : AutoLocation
{
    protected override void DoLoad()
    {
        Using(new FsmEditGroup()
        {
            { new(SceneName!, "Ladybug Craft Pickup", "FSM"), HookLoddieAct3Desk }
        });
    }

    protected override void DoUnload() { }

    private void HookLoddieAct3Desk(PlayMakerFSM fsm)//location is the desk that has the alternative placement for the tool pouch upgrade
    {
        FsmState toolPouchState = fsm.MustGetState("Get Item");
        toolPouchState.RemoveActionsOfType<SavedItemGet>();
        toolPouchState.InsertLambdaMethod(1, GiveAll);

        //skip tool pouch yes/no prompt
        fsm.RemoveTransitionsTo("Idle", "Inspect");
        fsm.AddTransition("Idle", "INTERACT", "Wait");//bypass Idle -> Inspect path to Idle -> Wait
        //skip tool pouch creation cutscene/animation with fade to black
        fsm.MustGetState("Wait").RemoveActionsOfType<Wait>();//removing 0.3s of waiting
        fsm.MustGetState("Hero Face Table").RemoveActionsOfType<ScreenFader>();
        fsm.MustGetState("Hero Face Table").RemoveActionsOfType<Wait>();//removing 1.5s of waiting
        fsm.RemoveTransitionsTo("Hero Face Table", "Craft");
        fsm.AddTransition("Hero Face Table", "FINISHED", "Turn Back");//bypass Hero Face Table -> Craft path to Hero Face Table -> Turn Back
        fsm.MustGetState("Turn Back").RemoveActionsOfType<ScreenFader>();
        fsm.MustGetState("Turn Back").RemoveActionsOfType<Wait>();//remove 0.5s of waiting
        
    }
}