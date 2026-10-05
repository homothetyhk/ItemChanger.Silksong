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
        // First-time reward path (confirmed via UnityPy dump of
        // scenes_scenes_scenes/halfway_01.bundle → HH Bartender Dialogue FSM):
        //   Can Complete Dialogue → Quest Complete Yes No → accept →
        //   Quest Complete Dialogue 1 → Quest Complete Prompt (EndQuest) →
        //   Quest Complete Dialogue 2 → Quest Reward → Quest Complete Dialogue 3
        //
        // Quest Reward holds GetQuestReward + SavedItemGet. EndConversation
        // before GiveAll so CrestUIDef does not stack on the NPC text box
        // (qwint review on #212 / #208).
        FsmState rewardState = fsm.MustGetState("Quest Reward");
        rewardState.RemoveActionsOfType<GetQuestReward>();
        rewardState.RemoveActionsOfType<SavedItemGet>();
        rewardState.InsertLambdaMethod(0, finish =>
        {
            DialogueBox.EndConversation(true);
            GiveAll(finish);
        });

        // Persistence / re-give path only after the wish is already complete.
        // Quest State routes CheckQuestState COMPLETE → General Convos and
        // never re-enters Quest Reward. Hook General Convos — NOT Quest State
        // itself — because Quest State also runs for NONE / ACTIVE /
        // Can Complete? and granting there lets the player take items before
        // turning in Ragpelt (qwint retest 2026-07-19).
        FsmState generalConvos = fsm.MustGetState("General Convos");
        generalConvos.InsertLambdaMethod(0, finish =>
        {
            if (!Placement!.AllObtained())
            {
                DialogueBox.EndConversation(true);
                GiveAll(finish);
            }
            else
            {
                finish();
            }
        });
    }
}
