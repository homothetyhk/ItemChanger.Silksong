using ItemChanger.Locations;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using QuestPlaymakerActions;
using Silksong.FsmUtil;

namespace ItemChanger.Silksong.Locations;

/// <summary>
/// Location for the Crafting Kit granted by Creige (HH Bartender) in Halfway_01
/// after Hornet completes the Crawbug Clearing wish. Strips the GetQuestReward
/// and SavedItemGet actions from the Dialogue FSM's "Quest Reward" state and
/// routes the grant through the IC placement. The preceding "Quest Complete
/// Prompt" state still runs EndQuest, so the wish closes normally.
/// </summary>
public class CreigeLocation : AutoLocation
{
    protected override void DoLoad()
    {
        Using(new FsmEditGroup()
        {
            { new(SceneName!, "HH Bartender", "Dialogue"), HookCreige },
        });
    }

    protected override void DoUnload() { }

    private void HookCreige(PlayMakerFSM fsm)
    {
        FsmState rewardState = fsm.MustGetState("Quest Reward");
        rewardState.RemoveActionsOfType<GetQuestReward>();
        rewardState.RemoveActionsOfType<SavedItemGet>();
        rewardState.InsertLambdaMethod(0, GiveAll);
    }
}
