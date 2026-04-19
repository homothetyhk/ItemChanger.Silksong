using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Enums;
using ItemChanger.Items;
using ItemChanger.Modules;
using ItemChanger.Placements;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.RawData;
using QuestPlaymakerActions;
using Silksong.FsmUtil;

namespace ItemChanger.Silksong.Modules;

/// <summary>
/// Optional module for the Sprintmaster Swift race quest. When added to the profile alongside
/// the two <see cref="Locations.SprintmasterLocation"/> instances, inserts a yes/no skip prompt
/// at the start of the Sprintmaster conversation.
/// </summary>
/// <remarks>
/// Choosing yes skips to the final race directly and, upon quest completion, awards all
/// bypassed rewards together with the final race reward:
/// <list type="bullet">
/// <item>Race 1 vanilla reward (Rosary String) — captured at runtime and given at quest end</item>
/// <item>Race 2 IC placement (Beast Shard) — given at quest end if present and unobtained</item>
/// <item>Race 3 IC placement (Mask Shard) — given via <see cref="Locations.SprintmasterLocation"/> as normal</item>
/// </list>
/// The module advances the quest completion counter for the two skipped races so the FSM's
/// normal <c>Was Extra?</c> check correctly routes to <c>Quest End</c> after the final race win.
/// </remarks>
public class SprintmasterSkipModule : Module
{
    // skipActive: set when player chooses yes; cleared once Array Track? applies the jump.
    private bool skipActive;
    // skipApplied: set after the race jump is applied; cleared once End Dialogue 3 gives extras.
    private bool skipApplied;
    // Race 1 vanilla SavedItem (Rosary String), captured at Array Track? time.
    private SavedItem? race1Reward;

    protected override void DoLoad()
    {
        Using(new FsmEditGroup()
        {
            { new("Sprintmaster_Cave", "Sprintmaster Runner", "Behaviour"), HookSprintmaster }
        });
    }

    protected override void DoUnload() { }

    private void HookSprintmaster(PlayMakerFSM fsm)
    {
        HookSkipPrompt(fsm);
        HookArrayTrack(fsm);
        HookEndDialogue3(fsm);
    }

    private void HookSkipPrompt(PlayMakerFSM fsm)
    {
        // Insert the async yes/no prompt at the very start of the opening conversation state,
        // before anything else in Setup Convo Jog runs.
        // Read the same PD field and array that Array Track? uses so we can suppress the prompt
        // when the player is already on the final race (nothing left to skip).
        FsmState setupConvoJog = fsm.MustGetState("Setup Convo Jog");
        FsmState arrayTrackState = fsm.MustGetState("Array Track?");
        ArrayGet arrayGetAction = arrayTrackState.GetFirstActionOfType<ArrayGet>()!;
        GetPlayerDataVariable pdVarAction = arrayTrackState.GetFirstActionOfType<GetPlayerDataVariable>()!;

        setupConvoJog.InsertAction(0, new SprintmasterSkipPromptAction(
            onYes: () => { skipActive = true; },
            arrayGetAction: arrayGetAction,
            pdVarAction: pdVarAction
        ));
    }

    private void HookArrayTrack(PlayMakerFSM fsm)
    {
        // Array Track? reads the current race index from PlayerData via GetPlayerDataVariable,
        // then uses ArrayGet to fetch the corresponding track GameObject from the race array.
        // The separate Set Track state does the same independently, so we must write back to the
        // actual PlayerData field — not just override the local FsmInt — so both states pick up
        // the correct (final) race index without us having to hook Set Track separately.
        FsmState arrayTrackState = fsm.MustGetState("Array Track?");
        ArrayGet arrayGetAction = arrayTrackState.GetFirstActionOfType<ArrayGet>()!;
        GetPlayerDataVariable pdVarAction = arrayTrackState.GetFirstActionOfType<GetPlayerDataVariable>()!;

        // Locate the CheckQuestStateV2 action anywhere in the FSM to reach the quest object.
        // This is used to pre-increment the quest completion counter for the skipped races.
        CheckQuestStateV2? questCheckAction = fsm.FsmStates
            .SelectMany(static s => s.Actions ?? [])
            .OfType<CheckQuestStateV2>()
            .FirstOrDefault();

        // Insert at index 0, before GetPlayerDataVariable runs, so the PD field is already
        // set to lastRaceIdx when GetPlayerDataVariable reads it in both Array Track? and Set Track.
        arrayTrackState.InsertMethod(0, () =>
        {
            if (!skipActive) return;
            skipActive = false;
            skipApplied = true;

            int lastRaceIdx = arrayGetAction.array.Length - 1;

            // Capture Race 1's vanilla SavedItem (Rosary String) so we can give it at quest end.
            race1Reward ??= (arrayGetAction.array.Get(0) as GameObject)
                ?.GetComponent<SprintRaceController>()?.Reward;

            // Advance the quest completion counter for each race being skipped (all but the last).
            // This ensures the FSM's Was Extra? → CheckQuestStateV2 check sees CanComplete = true
            // after the player wins the final race.
            if (questCheckAction != null)
            {
                FullQuestBase? quest = questCheckAction.Quest.Value as FullQuestBase;
                if (quest != null)
                {
                    for (int i = 0; i < lastRaceIdx; i++)
                    {
                        quest.IncrementQuestCounter();
                    }
                }
            }

            // Write lastRaceIdx into the actual PlayerData field so that GetPlayerDataVariable
            // in Array Track? and the identical action in Set Track both naturally read it.
            // This causes Set Track to activate race 3's terrain colliders (not race 1's),
            // fixing the "terrain collider is inactive" coroutine error that prevented laps
            // from being counted.
            PlayerData.instance.SetInt(pdVarAction.VariableName.Value, lastRaceIdx);
        });
    }

    private void HookEndDialogue3(PlayMakerFSM fsm)
    {
        // End Dialogue 3 fires on the quest-completion path (Quest End → Win After Wish → End Dialogue 3).
        // SprintmasterLocation (IsQuestCompletion = true) hooks the SavedItemGet here to give the Mask Shard IC item.
        // Our inserted method runs first (index 0) to give the two bypassed rewards before that.
        FsmState endDialogue3 = fsm.MustGetState("End Dialogue 3");
        endDialogue3.InsertMethod(0, () =>
        {
            if (!skipApplied) return;
            skipApplied = false;

            // Give the Race 1 Rosary String: IC placement if present in profile, vanilla otherwise.
            if (ActiveProfile != null
                && ActiveProfile.TryGetPlacement(LocationNames.Rosary_String__Sprintmaster_Race_1, out Placement? race1Placement)
                && !race1Placement!.AllObtained())
            {
                race1Placement.GiveAll(new GiveInfo
                {
                    Container = ContainerNames.Shiny,
                    FlingType = FlingType.DirectDeposit,
                    Transform = fsm.gameObject.transform,
                    MessageType = MessageType.SmallPopup,
                });
            }
            else
            {
                race1Reward?.Get();
            }

            // Give the Race 2 IC placement (Beast Shard) if it is in the profile and unobtained.
            if (ActiveProfile != null
                && ActiveProfile.TryGetPlacement(LocationNames.Beast_Shard__Sprintmaster_Race_2, out Placement? race2Placement)
                && !race2Placement!.AllObtained())
            {
                race2Placement.GiveAll(new GiveInfo
                {
                    Container = ContainerNames.Shiny,
                    FlingType = FlingType.DirectDeposit,
                    Transform = fsm.gameObject.transform,
                    MessageType = MessageType.SmallPopup,
                });
            }
        });
    }

    /// <summary>
    /// Custom async FSM action that opens the skip yes/no dialogue without blocking.
    /// Does not call <see cref="FsmStateAction.Finish"/> until the player responds.
    /// Suppresses the prompt entirely if the player is already on the final race.
    /// </summary>
    private sealed class SprintmasterSkipPromptAction : FsmStateAction
    {
        private readonly Action onYes;
        private readonly ArrayGet arrayGetAction;
        private readonly GetPlayerDataVariable pdVarAction;

        public SprintmasterSkipPromptAction(Action onYes, ArrayGet arrayGetAction, GetPlayerDataVariable pdVarAction)
        {
            this.onYes = onYes;
            this.arrayGetAction = arrayGetAction;
            this.pdVarAction = pdVarAction;
        }

        public override void OnEnter()
        {
            // Suppress the prompt if the player is already at or past the final race index.
            int currentRaceIdx = PlayerData.instance.GetInt(pdVarAction.VariableName.Value);
            int lastRaceIdx = arrayGetAction.array.Length - 1;
            if (currentRaceIdx >= lastRaceIdx)
            {
                Finish();
                return;
            }

            DialogueYesNoBox.Open(
                yes: () => { onYes(); Finish(); },
                no: () => Finish(),
                returnHud: true,
                text: "Skip to the final race and collect all Sprintmaster rewards?"
            );
            // Intentionally do NOT call Finish() here — the dialogue callback does it asynchronously.
        }
    }
}
