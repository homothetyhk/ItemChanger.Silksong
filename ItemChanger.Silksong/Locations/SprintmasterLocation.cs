using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Locations;
using Silksong.FsmUtil;
using Silksong.FsmUtil.Actions;

namespace ItemChanger.Silksong.Locations;

/// <summary>
/// Location for Sprintmaster Swift race rewards in Sprintmaster_Cave.
/// Two instances are needed:
/// <list type="bullet">
/// <item><see cref="IsQuestCompletion"/> = false — Race 2 Beast Shard (<c>Give Reward</c> state, identified by SavedItem name <c>"Great Shard"</c>).</item>
/// <item><see cref="IsQuestCompletion"/> = true — final Mask Shard (<c>End Dialogue 3</c> state, quest-completion path only).</item>
/// </list>
/// Both instances hook the same FSM but different states, so they coexist without conflict.
/// </summary>
public class SprintmasterLocation : AutoLocation
{
    /// <summary>
    /// When true, hooks the quest completion reward path (Mask Shard, <c>End Dialogue 3</c> state).
    /// When false, hooks the Race 2 Beast Shard path (<c>Give Reward</c> state).
    /// </summary>
    public bool IsQuestCompletion { get; set; }

    protected override void DoLoad()
    {
        Using(new FsmEditGroup()
        {
            {new(SceneName!, "Sprintmaster Runner", "Behaviour"), HookSprintmaster},
        });
    }

    protected override void DoUnload() { }

    private void HookSprintmaster(PlayMakerFSM fsm)
    {
        if (IsQuestCompletion)
            HookQuestCompletion(fsm);
        else
            HookRace2BeastShard(fsm);
    }

    private void HookQuestCompletion(PlayMakerFSM fsm)
    {
        // Quest End → Win After Wish → End Dialogue 3 (SavedItemGet gives Mask Shard).
        // FSM var is null on non-quest-completion paths, so SavedItemGet does nothing there.
        FsmState endDialogue3 = fsm.MustGetState("End Dialogue 3");
        SavedItemGet questRewardAction = endDialogue3.GetFirstActionOfType<SavedItemGet>()!;

        endDialogue3.ReplaceFirstActionOfType<SavedItemGet>(new LambdaAction
        {
            Method = () =>
            {
                SavedItem? rewardItem = questRewardAction.Item.Value as SavedItem;
                if (rewardItem != null && !Placement!.AllObtained())
                    GiveAll();
            }
        });
    }

    private void HookRace2BeastShard(PlayMakerFSM fsm)
    {
        // Give Reward → SavedItemGet gives the current race's reward.
        // Race 2 is identified by SavedItem name "Great Shard"; Race 1 (Rosary String) passes through.
        const string BeastShardSavedItemName = "Great Shard";

        FsmState giveRewardState = fsm.MustGetState("Give Reward");
        SavedItemGet raceRewardAction = giveRewardState.GetFirstActionOfType<SavedItemGet>()!;

        giveRewardState.ReplaceFirstActionOfType<SavedItemGet>(new LambdaAction
        {
            Method = () =>
            {
                SavedItem? rewardItem = raceRewardAction.Item.Value as SavedItem;
                if (rewardItem == null)
                    return;

                if (rewardItem.name == BeastShardSavedItemName)
                {
                    if (!Placement!.AllObtained())
                        GiveAll();
                }
                else
                {
                    rewardItem.Get();
                }
            }
        });
    }
}
