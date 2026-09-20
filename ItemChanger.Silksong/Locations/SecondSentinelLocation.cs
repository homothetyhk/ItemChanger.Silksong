using ItemChanger.Locations;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using QuestPlaymakerActions;
using Silksong.FsmUtil;
using ItemChanger.Silksong.Extensions;

namespace ItemChanger.Silksong.Locations;

public class SecondSentinelLocation : AutoLocation
{
    protected override void DoLoad()
    {
        Using(new FsmEditGroup()
        {
            { new(UnsafeSceneName, "Corpse Song Knight(Clone)", "Death"), Hook2ndSentinelDefeated },
        });
    }

    protected override void DoUnload() { }

    private void Hook2ndSentinelDefeated(PlayMakerFSM fsm)
    {
        FsmState awardState = fsm.MustGetState("Award Item");
        awardState.RemoveActionsOfType<SavedItemGet>();
        awardState.InsertLambdaMethod(3, finish =>
        {
            this.CreateGiveAllDelegate(fsm.transform).Invoke(finish);
        });
    }
}
