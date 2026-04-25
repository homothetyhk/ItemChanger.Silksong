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
            {new(SceneName!, "Wood Witch", "Dialogue"), HookWitch},
        });
    }

    protected override void DoUnload() {}

    private void HookWitch(PlayMakerFSM fsm)
    {
        FsmState rewardQueryState = fsm.MustGetState("Pollip Reward?");
        rewardQueryState.ReplaceFirstActionOfType<CheckIfToolUnlocked>(new LambdaAction
        {
            Method = () =>
            {
                if (Placement!.CheckVisitedAny(VisitState.ObtainedAnyItem))
                {
                    Placement!.GiveSome(Placement!.Items.Where(it => !it.IsObtained() && it.WasEverObtained()), GetGiveInfo());
                    fsm.SendEvent("FINISHED");
                }
                else
                {
                    fsm.SendEvent("POLLIP REWARD");
                }
            }
        });

        FsmState rewardState = fsm.MustGetState("Flower Quest Reward");
        rewardState.ReplaceFirstActionOfType<SetToolUnlocked>(new LambdaAction
        {
            Method = GiveAll
        });
    }
}