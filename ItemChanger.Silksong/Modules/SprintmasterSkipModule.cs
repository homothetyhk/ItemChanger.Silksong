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
/// Race 2 Beast Shard (IC placement if registered, vanilla otherwise),
/// and Race 3 Mask Shard (via SprintmasterLocation).
/// If the Memento Race is available a second prompt offers to skip further;
/// choosing yes also bypasses Race 3 and gives all three bypassed rewards before the Memento Race starts.
/// </summary>
public class SprintmasterSkipModule : Module
{
    // skipActive: set on yes to prompt 1, cleared when Array Track? applies the jump.
    private bool skipActive;
    // skipApplied: set after the race-3 jump (or alongside skipMementoApplied when races 1&2 were also skipped),
    // cleared when the rewards for the bypassed races are given.
    private bool skipApplied;
    // skipMementoActive: set on yes to prompt 2, cleared when Array Track? jumps to Extra Track.
    private bool skipMementoActive;
    // skipMementoApplied: set after the Extra Track jump, cleared when bypassed rewards are given in Extra Track.
    private bool skipMementoApplied;

    // Vanilla rewards for bypassed races, captured when the skip is applied.
    private SavedItem? race1Reward;
    private SavedItem? race2Reward;
    private SavedItem? race3Reward;

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
        HookGiveReward(fsm);
    }

    private void HookSkipPrompt(PlayMakerFSM fsm)
    {
        FsmState setupConvoJog = fsm.MustGetState("Setup Convo Jog");
        FsmState arrayTrackState = fsm.MustGetState("Array Track?");
        ArrayGet arrayGetAction = arrayTrackState.GetFirstActionOfType<ArrayGet>()!;
        GetPlayerDataVariable pdVarAction = arrayTrackState.GetFirstActionOfType<GetPlayerDataVariable>()!;

        setupConvoJog.InsertAction(0, new SprintmasterSkipPromptAction(
            onYes: () => { skipActive = true; },
            onYesMemento: () => { skipMementoActive = true; },
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
            if (!skipActive && !skipMementoActive) return;

            int lastRaceIdx = arrayGetAction.array.Length - 1;
            int currentRaceIdx = PlayerData.instance.GetInt(pdVarAction.VariableName.Value);
            FullQuestBase? quest = questCheckAction?.Quest.Value as FullQuestBase;

            if (skipMementoActive)
            {
                bool alsoSkippedRaces = skipActive;
                skipActive = false;
                skipMementoActive = false;
                skipMementoApplied = true;
                skipApplied = alsoSkippedRaces;

                if (alsoSkippedRaces)
                {
                    race1Reward ??= (arrayGetAction.array.Get(0) as GameObject)
                        ?.GetComponent<SprintRaceController>()?.Reward;
                    race2Reward ??= arrayGetAction.array.Length > 1
                        ? (arrayGetAction.array.Get(1) as GameObject)?.GetComponent<SprintRaceController>()?.Reward
                        : null;
                }

                // Pre-increment for every bypassed race, including the final race we're skipping.
                if (quest != null)
                    for (int i = currentRaceIdx; i <= lastRaceIdx; i++)
                        quest.IncrementQuestCounter();

                PlayerData.instance.SetInt(pdVarAction.VariableName.Value, lastRaceIdx);
                fsm.SetState("Extra Track");
                return;
            }

            // skipActive only: jump to the final regular race.
            skipActive = false;
            skipApplied = true;

            race1Reward ??= (arrayGetAction.array.Get(0) as GameObject)
                ?.GetComponent<SprintRaceController>()?.Reward;
            race2Reward ??= arrayGetAction.array.Length > 1
                ? (arrayGetAction.array.Get(1) as GameObject)?.GetComponent<SprintRaceController>()?.Reward
                : null;

            // Pre-increment the quest counter for each skipped race so Was Extra? routes to Quest End.
            if (quest != null)
                for (int i = currentRaceIdx; i < lastRaceIdx; i++)
                    quest.IncrementQuestCounter();

            PlayerData.instance.SetInt(pdVarAction.VariableName.Value, lastRaceIdx);
        });
    }

    private void HookEndDialogue3(PlayMakerFSM fsm)
    {
        // Capture the vanilla Race 3 reward before SprintmasterLocation may replace the SavedItemGet.
        FsmState endDialogue3 = fsm.MustGetState("End Dialogue 3");
        race3Reward = endDialogue3.GetFirstActionOfType<SavedItemGet>()?.Item.Value as SavedItem;

        // Runs before SprintmasterLocation's hook (index 0) to give bypassed Race 1 and Race 2 rewards.
        endDialogue3.InsertMethod(0, () =>
        {
            if (!skipApplied) return;
            skipApplied = false;

            GiveInfo giveInfo = new()
            {
                Container = ContainerNames.Shiny,
                FlingType = FlingType.DirectDeposit,
                Transform = fsm.gameObject.transform,
                MessageType = MessageType.SmallPopup,
            };

            // Race 1: IC placement if registered, vanilla otherwise.
            if (ActiveProfile?.TryGetPlacement(LocationNames.Rosary_String__Sprintmaster_Race_1, out Placement? race1Placement) == true)
                race1Placement!.GiveAll(giveInfo);
            else
                race1Reward?.Get();

            // Race 2: IC placement if registered, vanilla otherwise.
            if (ActiveProfile?.TryGetPlacement(LocationNames.Beast_Shard__Sprintmaster_Race_2, out Placement? race2Placement) == true)
                race2Placement!.GiveAll(giveInfo);
            else
                race2Reward?.Get();
        });
    }

    private void HookGiveReward(PlayMakerFSM fsm)
    {
        // Give Reward fires after every race win, including the Extra Track race.
        // When the Memento skip was applied, deliver all bypassed rewards here (after the player wins).
        fsm.MustGetState("Give Reward").InsertMethod(0, () =>
        {
            if (!skipMementoApplied) return;
            skipMementoApplied = false;

            GiveInfo giveInfo = new()
            {
                Container = ContainerNames.Shiny,
                FlingType = FlingType.DirectDeposit,
                Transform = fsm.gameObject.transform,
                MessageType = MessageType.SmallPopup,
            };

            // Races 1 & 2: only bypassed when the player also said yes to the first skip prompt.
            if (skipApplied)
            {
                skipApplied = false;

                if (ActiveProfile?.TryGetPlacement(LocationNames.Rosary_String__Sprintmaster_Race_1, out Placement? p1) == true)
                    p1!.GiveAll(giveInfo);
                else
                    race1Reward?.Get();

                if (ActiveProfile?.TryGetPlacement(LocationNames.Beast_Shard__Sprintmaster_Race_2, out Placement? p2) == true)
                    p2!.GiveAll(giveInfo);
                else
                    race2Reward?.Get();
            }

            // Race 3 (Mask Shard): always bypassed when the Memento skip fires.
            if (ActiveProfile?.TryGetPlacement(LocationNames.Mask_Shard__Sprintmaster, out Placement? p3) == true)
                p3!.GiveAll(giveInfo);
            else
                race3Reward?.Get();
        });
    }

    /// <summary>
    /// Async FSM action that opens the skip yes/no dialogues.
    /// Prompt 1 (skip to final race) is suppressed if the player is already there.
    /// Prompt 2 (skip to Memento Race) is shown whenever the extra race is available,
    /// but only after the player has agreed to skip to race 3 (or was already there).
    /// </summary>
    private sealed class SprintmasterSkipPromptAction : FsmStateAction
    {
        private readonly Action onYes;
        private readonly Action onYesMemento;
        private readonly ArrayGet arrayGetAction;
        private readonly GetPlayerDataVariable pdVarAction;

        public SprintmasterSkipPromptAction(Action onYes, Action onYesMemento, ArrayGet arrayGetAction, GetPlayerDataVariable pdVarAction)
        {
            this.onYes = onYes;
            this.onYesMemento = onYesMemento;
            this.arrayGetAction = arrayGetAction;
            this.pdVarAction = pdVarAction;
        }

        public override void OnEnter()
        {
            int currentRaceIdx = PlayerData.instance.GetInt(pdVarAction.VariableName.Value);
            int lastRaceIdx = arrayGetAction.array.Length - 1;

            if (currentRaceIdx >= lastRaceIdx)
            {
                CheckMementoPrompt();
                return;
            }

            DialogueYesNoBox.Open(
                yes: () => { onYes(); CheckMementoPrompt(); },
                no: Finish,
                returnHud: true,
                text: ItemChangerLanguageStrings.SPRINTMASTER_SKIP_PROMPT.Value
            );
        }

        private void CheckMementoPrompt()
        {
            if (!PlayerData.instance.SprintMasterExtraRaceAvailable)
            {
                Finish();
                return;
            }

            DialogueYesNoBox.Open(
                yes: () => { onYesMemento(); Finish(); },
                no: Finish,
                returnHud: true,
                text: ItemChangerLanguageStrings.SPRINTMASTER_SKIP_MEMENTO_PROMPT.Value
            );
        }
    }
}
