using ItemChanger.Costs;
using ItemChanger.Locations;
using ItemChanger.Placements;
using ItemChanger.Silksong.Modules;
using ItemChanger.Silksong.Util;
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
        ActiveProfile!.Modules.GetOrAdd<MossDruidPreviewModule>().Add(Placement!);
    }

    protected override void DoUnload() {}

    private void HookDruid(PlayMakerFSM fsm)
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
            if (PlayerData.instance.GetInt(nameof(PlayerData.druidMossBerriesSold)) != 3)
            {
                return;
            }
            Cost? cost = ((ISingleCostPlacement)Placement!).Cost;
            if (cost == null)
            {
                fsm.SendEvent("ACCEPT");
                return;
            }
            CostDialogue.Prompt(
                    cost,
                    Placement!.GetUIName(),
                    () => fsm.SendEvent("ACCEPT"),
                    () => fsm.SendEvent("REFUSE"));
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
                if (Placement!.AllObtained())
                {
                    fsm.SendEvent("COMPLETE");
                }
            }
        });
    }
}
