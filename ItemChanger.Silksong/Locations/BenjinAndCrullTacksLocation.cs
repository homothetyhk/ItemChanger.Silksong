using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Locations;
using ItemChanger.Silksong.RawData;
using QuestPlaymakerActions;
using Silksong.FsmUtil;

namespace ItemChanger.Silksong.Locations;

public class BenjinAndCrullTacksLocation : AutoLocation
{
    protected override void DoLoad()
    {
        Using(new FsmEditGroup()
        {
            { new(SceneName!, "Dust Traders", "Dialogue"), HookDustTraders }
        });
    }

    protected override void DoUnload() {}

    private void HookDustTraders(PlayMakerFSM fsm)
    {
        // Replace CanEndQuest state - to support persistent items
        FsmState dialogTreeCheckState = fsm.MustGetState("State?");
        dialogTreeCheckState.RemoveFirstActionOfType<CanEndQuestV2>();
        dialogTreeCheckState.InsertLambdaMethod(0, _ =>
        {
            FullQuestBase quest = QuestManager.GetQuest(Quests.Roach_Killing);
            if (quest == null || !quest.IsAccepted)
                return;

            if (quest.CanComplete && !quest.IsCompleted)
            {
                fsm.SendEvent("CAN END");
                return;
            }
        });
        
        // Replace granting tacks with obtaining the placement
        FsmState rewardState = fsm.MustGetState("Reward");
        rewardState.RemoveActionsOfType<GetQuestReward>();
        rewardState.RemoveActionsOfType<SavedItemGet>();
        rewardState.AddLambdaMethod(GiveAll);
    }
}