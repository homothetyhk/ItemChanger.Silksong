using ItemChanger.Locations;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Silksong.FsmUtil;
using Silksong.FsmUtil.Actions;

namespace ItemChanger.Silksong.Locations;

public class MossDruidMix1Location : MossDruidLocation
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
        int i = giveRewardState.IndexFirstActionOfType<SetToolUnlocked>();
        giveRewardState.RemoveAction(i);
        giveRewardState.InsertLambdaMethod(i, GiveAll);
    }
}
