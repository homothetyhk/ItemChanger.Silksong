using ItemChanger.Locations;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Silksong.FsmUtil;

namespace ItemChanger.Silksong.Locations;

public class LoddieChallenge2Location : AutoLocation
{
    protected override void DoLoad()
    {
        Using(new FsmEditGroup()
        {
            { new(SceneName!, "Lady Bug Large", "Convo"), HookLoddieChallenge2 }
        });
    }

    protected override void DoUnload() { }

    private void HookLoddieChallenge2(PlayMakerFSM fsm)
    {
        FsmState reward2State = fsm.MustGetState("Reward 2");//heavy rosary necklace for challenge 2
        reward2State.RemoveActionsOfType<CollectableItemCollect>();
        reward2State.InsertLambdaMethod(0, GiveAll);
    }
}