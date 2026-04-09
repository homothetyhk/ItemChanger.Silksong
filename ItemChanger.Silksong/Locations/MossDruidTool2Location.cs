using ItemChanger.Locations;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Silksong.FsmUtil;
using Silksong.FsmUtil.Actions;

namespace ItemChanger.Silksong.Locations;

public class MossDruidTool2Location : AutoLocation
{
    protected override void DoLoad()
    {
        Using(new FsmEditGroup()
        {
            {new(SceneName!, "Moss Creep NPC", "Conversation Control"), HookDruid},
        });
    }

    protected override void DoUnload() {}

    private void HookDruid(PlayMakerFSM fsm)
    {
        FsmState giveRewardFState = fsm.MustGetState("Give Reward F");
        giveRewardFState.RemoveFirstActionOfType<SetToolLocked>();
        giveRewardFState.ReplaceFirstActionOfType<SetToolUnlocked>(new LambdaAction
        {
            Method = GiveAll,
        });

        FsmState tradeCheckState = fsm.MustGetState("Trade Check");
        tradeCheckState.ReplaceFirstActionOfType<CheckIfToolUnlocked>(new LambdaAction
        {
            Method = () =>
            {
                if (Placement!.AllObtained())
                {
                    fsm.SendEvent("COMPLETE");
                }
            }
        });
    }
}
