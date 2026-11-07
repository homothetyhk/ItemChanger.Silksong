using ItemChanger.Locations;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Silksong.FsmUtil;

namespace ItemChanger.Silksong.Locations;

public class LoddieChallenge1Location : AutoLocation
{
    protected override void DoLoad()
    {
        Using(new FsmEditGroup()
        {
            { new(SceneName!, "Lady Bug Large", "Convo"), HookLoddieChallenge1 }
        });
    }

    protected override void DoUnload() { }

    private void HookLoddieChallenge1(PlayMakerFSM fsm)
    {
        FsmState reward1State = fsm.MustGetState("Reward 1");//tool pouch for challenge 1
        reward1State.RemoveActionsOfType<RunFSM>();
        reward1State.InsertLambdaMethod(1, GiveAll);
    }
}