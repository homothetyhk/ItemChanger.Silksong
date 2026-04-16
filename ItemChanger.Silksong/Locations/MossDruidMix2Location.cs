using ItemChanger.Locations;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Silksong.FsmUtil;
using Silksong.FsmUtil.Actions;

namespace ItemChanger.Silksong.Locations;

public class MossDruidMix2Location : MossDruidLocation
{
    protected override void HookDruid(PlayMakerFSM fsm)
    {
        FsmState offerAnswerState = fsm.MustGetState("Offer Answer");
        int i = offerAnswerState.IndexLastActionOfType<DialogueYesNoItemV4>();
        if (i == -1)
        {
            i = offerAnswerState.actions.Length;
        }
        else
        {
            offerAnswerState.RemoveAction(i);
        }
        offerAnswerState.InsertMethod(i, () =>
        {
            if (PlayerData.instance.GetInt(nameof(PlayerData.druidMossBerriesSold)) == 3)
            {
                PromptCost(fsm, "ACCEPT", "REFUSE");
            }
        });

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
                if (Checked())
                {
                    fsm.SendEvent("COMPLETE");
                }
            }
        });
    }
}
