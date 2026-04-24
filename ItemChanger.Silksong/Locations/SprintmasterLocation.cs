using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Items;
using ItemChanger.Locations;
using ItemChanger.Placements;
using ItemChanger.Silksong.RawData;
using Silksong.FsmUtil;
using Silksong.FsmUtil.Actions;

namespace ItemChanger.Silksong.Locations;

/// <summary>
/// Location for Sprintmaster Swift race rewards in Sprintmaster_Cave.
/// Two instances are needed:
/// <list type="bullet">
/// <item><see cref="IsQuestCompletion"/> = false — Race 1 Rosary String, Race 2 Beast Shard, or bonus Memento (<c>Give Reward</c>
/// state, dispatched by race index: 0 = Race 1, 1 = Race 2; bonus race detected via <c>Extra Track</c> flag).</item>
/// <item><see cref="IsQuestCompletion"/> = true — final Mask Shard (<c>End Dialogue 3</c> state, quest-completion path only).</item>
/// </list>
/// Both hooks target different states and coexist without conflict. Multiple non-quest-completion instances share one
/// <c>Give Reward</c> hook that dispatches dynamically via the active profile.
/// </summary>
public class SprintmasterLocation : AutoLocation
{
    /// <summary>
    /// When true, hooks the quest completion reward path (Mask Shard, <c>End Dialogue 3</c> state).
    /// When false, hooks the race reward path (<c>Give Reward</c> state) for the race whose index maps to this location's name.
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
            HookRaceReward(fsm);
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

    private void HookRaceReward(PlayMakerFSM fsm)
    {
        // Give Reward → SavedItemGet gives the current race's reward.
        // The race index (0 = Race 1, 1 = Race 2) is read from PlayerData at reward time and mapped to a
        // location name. The bonus memento race is detected by a flag set in Extra Track (which runs before
        // Give Reward only on the bonus race path). Multiple SprintmasterLocation instances share this hook;
        // only the first to load replaces SavedItemGet — subsequent instances return early and rely on the
        // dynamic ActiveProfile lookup.
        FsmState arrayTrackState = fsm.MustGetState("Array Track?");
        GetPlayerDataVariable pdVarAction = arrayTrackState.GetFirstActionOfType<GetPlayerDataVariable>()!;
        string raceIndexVarName = pdVarAction.VariableName.Value;

        FsmState giveRewardState = fsm.MustGetState("Give Reward");
        SavedItemGet? raceRewardAction = giveRewardState.GetFirstActionOfType<SavedItemGet>();
        if (raceRewardAction == null) return; // Give Reward already hooked by another SprintmasterLocation instance.

        bool isExtraRace = false;
        fsm.MustGetState("Extra Track").InsertMethod(0, () => isExtraRace = true);

        giveRewardState.ReplaceFirstActionOfType<SavedItemGet>(new LambdaAction
        {
            Method = () =>
            {
                SavedItem? rewardItem = raceRewardAction.Item.Value as SavedItem;
                if (rewardItem == null) return;

                GiveInfo giveInfo = GetGiveInfo();

                if (isExtraRace)
                {
                    isExtraRace = false;
                    if (ActiveProfile?.TryGetPlacement(LocationNames.Sprintmaster_Memento, out Placement? p) == true && p != null)
                        p.GiveAll(giveInfo);
                    else
                        rewardItem.Get();
                    return;
                }

                int raceIdx = PlayerData.instance.GetInt(raceIndexVarName);
                string? locationName = raceIdx switch
                {
                    0 => LocationNames.Rosary_String__Sprintmaster_Race_1,
                    1 => LocationNames.Beast_Shard__Sprintmaster_Race_2,
                    _ => null
                };

                if (locationName != null
                    && ActiveProfile?.TryGetPlacement(locationName, out Placement? racePlacement) == true
                    && racePlacement != null)
                {
                    racePlacement.GiveAll(giveInfo);
                }
                else
                {
                    rewardItem.Get();
                }
            }
        });
    }
}
