using ItemChanger.Locations;
using ItemChanger.Enums;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Silksong.FsmUtil;
using Silksong.FsmUtil.Actions;

namespace ItemChanger.Silksong.Locations;

public class GreyrootPollipLocation : AutoLocation
{
    protected override void DoLoad()
    {
        Using(new FsmEditGroup()
        {
            {new(UnsafeSceneName, "Wood Witch", "Dialogue"), HookWitch},
        });
    }

    protected override void DoUnload() {}

    private void HookWitch(PlayMakerFSM fsm)
    {
        FsmState rewardQueryState = fsm.MustGetState("Pollip Reward?");
        int i = rewardQueryState.IndexFirstActionOfType<CheckIfToolUnlocked>();
        rewardQueryState.RemoveAction(i);
        rewardQueryState.InsertLambdaMethod(i, (finish) =>
        {
            if (Placement!.CheckVisitedAny(VisitState.ObtainedAnyItem))
            {
                Placement!.GiveSome(Placement!.Items.Where(it => !it.IsObtained() && it.WasEverObtained()), GetGiveInfo(), () =>
                {
                    fsm.SendEvent("FINISHED");
                    finish();
                });
            }
            else
            {
                fsm.SendEvent("POLLIP REWARD");
                finish();
            }
        });

        FsmState rewardState = fsm.MustGetState("Flower Quest Reward");
        i = rewardState.IndexFirstActionOfType<SetToolUnlocked>();
        rewardState.RemoveAction(i);
        rewardState.InsertLambdaMethod(i, GiveAll);
    }
}