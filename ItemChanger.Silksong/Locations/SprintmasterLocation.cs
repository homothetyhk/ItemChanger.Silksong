using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Locations;
using Silksong.FsmUtil;
using Silksong.FsmUtil.Actions;

namespace ItemChanger.Silksong.Locations;

/// <summary>
/// Location for Sprintmaster Swift race rewards in Sprintmaster_Cave.
/// </summary>
/// <remarks>
/// Two instances of this class are needed for the two randomized locations:
/// <list type="bullet">
/// <item><see cref="IsQuestCompletion"/> = false: Race 2 Beast Shard reward
/// (<see cref="RawData.LocationNames.Beast_Shard__Sprintmaster_Race_2"/>).
/// Hooks the <c>Give Reward</c> FSM state; identified at runtime by the SavedItem name
/// <c>"Great Shard"</c>. Race 1 (Rosary String) passes through the same state unchanged.</item>
/// <item><see cref="IsQuestCompletion"/> = true: Final quest Mask Shard reward
/// (<see cref="RawData.LocationNames.Mask_Shard__Sprintmaster"/>).
/// Hooks the <c>End Dialogue 3</c> FSM state, which only carries a non-null item on the
/// quest-completion path (via <c>Quest End</c> → <c>GetQuestReward</c>).</item>
/// </list>
/// Both instances hook the <c>Sprintmaster Runner / Behaviour</c> FSM but modify different
/// states, so they coexist without conflict.
/// </remarks>
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
        {
            HookQuestCompletion(fsm);
        }
        else
        {
            HookRace2BeastShard(fsm);
        }
    }

    private void HookQuestCompletion(PlayMakerFSM fsm)
    {
        // Final race path:
        //   Quest End (GetQuestReward stores quest.RewardItem into FSM var)
        //   → Win After Wish
        //   → End Dialogue 3 (SavedItemGet reads that FSM var and gives the Mask Shard)
        //
        // On non-quest-completion paths the FSM var is null, so SavedItemGet does nothing.
        // Capture the original action before replacing it to retain its FsmObject binding.
        FsmState endDialogue3 = fsm.MustGetState("End Dialogue 3");
        SavedItemGet questRewardAction = endDialogue3.GetFirstActionOfType<SavedItemGet>()!;

        endDialogue3.ReplaceFirstActionOfType<SavedItemGet>(new LambdaAction
        {
            Method = () =>
            {
                SavedItem? rewardItem = questRewardAction.Item.Value as SavedItem;
                if (rewardItem != null && !Placement!.AllObtained())
                {
                    GiveAll();
                }
                // rewardItem null  → non-quest-completion path; do nothing.
                // AllObtained true → IC item already given; suppress vanilla to avoid double-give.
            }
        });
    }

    private void HookRace2BeastShard(PlayMakerFSM fsm)
    {
        // Normal race wins go through Give Reward → SavedItemGet.
        //   Race 1 gives a Rosary String.
        //   Race 2 gives the Beast Shard (SavedItem name "Great Shard").
        // The current race track's reward is written to the FSM variable by SetCurrentRaceTrack
        // before the race starts, and SavedItemGet reads it.
        // We identify Race 2 by checking the SavedItem's Unity object name.
        const string BeastShardSavedItemName = "Great Shard";

        FsmState giveRewardState = fsm.MustGetState("Give Reward");
        SavedItemGet raceRewardAction = giveRewardState.GetFirstActionOfType<SavedItemGet>()!;

        giveRewardState.ReplaceFirstActionOfType<SavedItemGet>(new LambdaAction
        {
            Method = () =>
            {
                SavedItem? rewardItem = raceRewardAction.Item.Value as SavedItem;
                if (rewardItem == null)
                {
                    return;
                }

                if (rewardItem.name == BeastShardSavedItemName)
                {
                    // Race 2: IC intercepts. Give the IC item if not yet obtained;
                    // suppress the vanilla Beast Shard in all cases.
                    if (!Placement!.AllObtained())
                    {
                        GiveAll();
                    }
                }
                else
                {
                    // Race 1 or any other non-IC race: give vanilla item unchanged.
                    rewardItem.Get();
                }
            }
        });
    }
}
