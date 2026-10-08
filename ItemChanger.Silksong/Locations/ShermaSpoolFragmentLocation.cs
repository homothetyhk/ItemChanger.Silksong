using ItemChanger.Locations;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Silksong.FsmUtil;
using ItemChanger.Silksong.Extensions;

namespace ItemChanger.Silksong.Locations;

public class ShermaSpoolFragmentLocation : AutoLocation
{
    protected override void DoLoad()
    {
        Using(new FsmEditGroup()
        {
            {new(UnsafeSceneName, "Sherma Rescue NPC", "Conversation Control"), HookSherma},
        });
    }

    protected override void DoUnload() {}

    private void HookSherma(PlayMakerFSM fsm)
    {
        FsmState rewardState = fsm.MustGetState("Get Spool Piece");
        rewardState.isSequence = true;
        int i = rewardState.IndexFirstActionOfType<CreateObject>();
        FsmGameObject hornet = ((CreateObject)rewardState.actions[i]).spawnPoint;
        rewardState.RemoveAction(i);
        rewardState.InsertLambdaMethod(i, finish => {
            this.CreateGiveAllDelegate(fsm.transform).Invoke(() =>
            {
                // Sherma's FSM expects the spool fragment collection animation to handle returning Hornet to normal
                HeroController.instance.RegainControl();
                HeroController.instance.StartAnimationControl();
                finish();
            });
        });
    }
}