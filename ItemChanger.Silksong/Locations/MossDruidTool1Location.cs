using ItemChanger.Locations;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Silksong.FsmUtil;
using Silksong.FsmUtil.Actions;

namespace ItemChanger.Silksong.Locations;

public class MossDruidTool1Location : MossDruidLocation
{
    protected override void HookDruid(PlayMakerFSM fsm)
    {
        FsmState givePromptState = fsm.MustGetState("Give Prompt");
        givePromptState.ReplaceFirstActionOfType<QuestCompleteYesNo>(new LambdaAction
        {
            Method = () =>
            {
                PromptCost(fsm, "SUCCESS", "FAIL");
            }
        });

        FsmState giveRewardState = fsm.MustGetState("Give Reward");
        giveRewardState.ReplaceFirstActionOfType<SetToolUnlocked>(new LambdaAction
        {
            Method = GiveAll,
        });
    }
}
