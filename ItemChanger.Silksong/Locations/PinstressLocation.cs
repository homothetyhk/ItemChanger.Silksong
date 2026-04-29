using ItemChanger.Locations;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using QuestPlaymakerActions;
using Silksong.FsmUtil;

namespace ItemChanger.Silksong.Locations;

/// <summary>
/// Location for the Pin Badge granted by the Pinstress NPC in Peak_07
/// after the player defeats her in the arena. Same handoff shape as the
/// Bell Hermit dialogue: the NPC's "Reward" state runs GetQuestReward
/// + SavedItemGetV2 to give the badge; we strip those and route the
/// grant through the IC placement instead. EndDialogue and the quest
/// state writes (SetPlayerDataVariable / EndQuest in the next state)
/// stay intact so the conversation closes and the quest resolves.
/// </summary>
public class PinstressLocation : AutoLocation
{
    protected override void DoLoad()
    {
        Using(new FsmEditGroup()
        {
            { new(SceneName!, "NPC", "NPC Control"), HookPinstressNpc },
        });
    }

    protected override void DoUnload() { }

    private void HookPinstressNpc(PlayMakerFSM fsm)
    {
        FsmState rewardState = fsm.MustGetState("Reward");
        rewardState.RemoveActionsOfType<GetQuestReward>();
        rewardState.RemoveActionsOfType<SavedItemGetV2>();
        rewardState.InsertLambdaMethod(0, GiveAll);
    }
}
