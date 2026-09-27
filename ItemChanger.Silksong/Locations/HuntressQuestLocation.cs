using ItemChanger.Locations;
using ItemChanger.Enums;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Silksong.FsmUtil;
using ItemChanger.Silksong.Extensions;
using Silksong.FsmUtil.Actions;

namespace ItemChanger.Silksong.Locations;

public class HuntressQuestLocation : AutoLocation
{
    protected override void DoLoad()
    {
        Using(new FsmEditGroup()
        {
            {new(UnsafeSceneName, "Huntress", "Dialogue"), HookHuntressOrRunt},
            // Runt uses a functionally identical FSM to handle the quest
            {new(UnsafeSceneName, "Huntress Runt", "Dialogue"), HookHuntressOrRunt},
        });
    }

    protected override void DoUnload() {}

    private void HookHuntressOrRunt(PlayMakerFSM fsm)
    {
        Fsm questTemplate = fsm.MustGetState("Dlg Common").GetFirstActionOfType<RunFSM>()!.runFsm;
        FsmState rewardState = questTemplate.MustGetState("Give Reward");
        rewardState.RemoveFirstActionOfType<SavedItemGet>();
        rewardState.AddLambdaMethod(this.CreateGiveAllDelegate(fsm.transform));
        // Quest complete dialogue; give persistent items
        questTemplate.MustGetState("Repeat").ChangeTransition("CONVO_END", "Give Reward");
    }
}