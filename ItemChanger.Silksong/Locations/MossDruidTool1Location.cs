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

public class MossDruidTool1Location : AutoLocation
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
        FsmState givePromptState = fsm.MustGetState("Give Prompt");
        givePromptState.ReplaceFirstActionOfType<QuestCompleteYesNo>(new LambdaAction
        {
            Method = () =>
            {
                Cost? cost = ((ISingleCostPlacement)Placement!).Cost;
                if (cost == null)
                {
                    fsm.SendEvent("SUCCESS");
                    return;
                }
                CostDialogue.Prompt(
                    cost,
                    () => fsm.SendEvent("SUCCESS"),
                    () => fsm.SendEvent("FAIL"));
            }
        });

        FsmState giveRewardState = fsm.MustGetState("Give Reward");
        giveRewardState.ReplaceFirstActionOfType<SetToolUnlocked>(new LambdaAction
        {
            Method = GiveAll,
        });
    }
}
