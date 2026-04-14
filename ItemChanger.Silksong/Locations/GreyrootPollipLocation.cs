using ItemChanger.Locations;
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
        FsmState rewardState = fsm.MustGetState("Flower Quest Reward");
        rewardState.ReplaceFirstActionOfType<SetToolUnlocked>(new LambdaAction
        {
            Method = GiveAll
        });
    }
}