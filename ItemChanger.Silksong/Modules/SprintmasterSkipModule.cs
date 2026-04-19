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
/// Inserts a yes/no skip prompt at the start of the Sprintmaster conversation.
/// Choosing yes jumps to the final race and awards all bypassed rewards on quest completion:
/// Race 1 Rosary String (IC placement if registered, vanilla otherwise),
/// Race 2 Beast Shard (IC placement if registered), and Race 3 Mask Shard (via SprintmasterLocation).
/// </summary>
public class SprintmasterSkipModule : Module
{
    // skipActive: set on yes, cleared when Array Track? applies the jump.
    private bool skipActive;
    // skipApplied: set after the jump, cleared when End Dialogue 3 gives bypassed rewards.
    private bool skipApplied;
    // Race 1 vanilla reward (Rosary String), captured when the skip is applied.
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
        // Insert at index 0 (before GetPlayerDataVariable) and write the target race index to
        // PlayerData directly, so both Array Track? and Set Track naturally read the final race.
        FsmState arrayTrackState = fsm.MustGetState("Array Track?");
        ArrayGet arrayGetAction = arrayTrackState.GetFirstActionOfType<ArrayGet>()!;
        GetPlayerDataVariable pdVarAction = arrayTrackState.GetFirstActionOfType<GetPlayerDataVariable>()!;

        CheckQuestStateV2? questCheckAction = fsm.FsmStates
            .SelectMany(static s => s.Actions ?? [])
            .OfType<CheckQuestStateV2>()
            .FirstOrDefault();

        arrayTrackState.InsertMethod(0, () =>
        {
            if (!skipActive) return;
            skipActive = false;
            skipApplied = true;

            int lastRaceIdx = arrayGetAction.array.Length - 1;

            race1Reward ??= (arrayGetAction.array.Get(0) as GameObject)
                ?.GetComponent<SprintRaceController>()?.Reward;

            // Pre-increment the quest counter for each skipped race so Was Extra? routes to Quest End.
            FullQuestBase? quest = questCheckAction?.Quest.Value as FullQuestBase;
            if (quest != null)
            {
                for (int i = 0; i < lastRaceIdx; i++)
                    quest.IncrementQuestCounter();
            }

            PlayerData.instance.SetInt(pdVarAction.VariableName.Value, lastRaceIdx);
        });
    }

    private void HookEndDialogue3(PlayMakerFSM fsm)
    {
        // Runs before SprintmasterLocation's hook (index 0) to give bypassed Race 1 and Race 2 rewards.
        FsmState endDialogue3 = fsm.MustGetState("End Dialogue 3");
        endDialogue3.InsertMethod(0, () =>
        {
            if (!skipApplied) return;
            skipApplied = false;

            // Race 1: IC placement if registered, vanilla otherwise.
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

            // Race 2: IC placement if registered.
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
    /// Async FSM action that opens the skip yes/no dialogue.
    /// Suppressed if the player is already on the final race.
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
        }
    }
}
