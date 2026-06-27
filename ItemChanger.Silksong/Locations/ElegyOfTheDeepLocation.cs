using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Locations;
using ItemChanger.Serialization;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.Serialization;
using Silksong.FsmUtil;
using Silksong.FsmUtil.Actions;

namespace ItemChanger.Silksong.Locations;

public class ElegyOfTheDeepLocation : AutoLocation
{
    /// <summary>
    /// Conditions for this placement to be available. Defaults to the player having visited the Abyss and
    /// obtained the Needolin.
    /// </summary>
    public IValueProvider<bool> LocationPreconditions { get; init; } = new Conjunction(
        new QuestAcceptedBool(Quests.Black_Thread_Pt4_Return),
        new PDBool(nameof(PlayerData.hasNeedolin))
    );

    /// <summary>
    /// Dialog to be displayed if when <see cref="LocationPreconditions"/> is not met.
    /// </summary>
    public LanguageString? PreconditionHint { get; init; } = ItemChangerLanguageStrings.SNAILS_ACT3_DESCEND_HINT;

    protected override void DoLoad()
    {
        Using(new FsmEditGroup()
        {
            { new(SceneName!, "Snail Shamans Set", "Dialogue"), HookSnailShamans }
        });
    }

    protected override void DoUnload()
    {
    }

    private void HookSnailShamans(PlayMakerFSM fsm)
    {
        // Overwrite dialog tree conditions. Location is obtainable as long as
        // the Abyss has been visited.
        FsmState dialogTreeState = fsm.MustGetState("Talk?");
        for (int i = 0; i < dialogTreeState.Actions.Length; i++)
        {
            FsmStateAction action = dialogTreeState.Actions[i];
            if (action is not BoolTestMulti { trueEvent.Name: "GET MELODY" }) continue;
            dialogTreeState.ReplaceAction(i, new LambdaAction { Method = () =>
                {
                    if (LocationPreconditions.Value)
                    {
                        fsm.SendEvent("GET MELODY");
                    }
                }
            });
        }
        FsmState melodyHintCheck = fsm.AddState("Melody Hint?");
        FsmState melodyHint = fsm.AddState("Melody Hint");
        dialogTreeState.ChangeTransition("FINISHED", melodyHintCheck.Name);
        melodyHintCheck.AddTransition("HINT", melodyHint.Name);
        melodyHintCheck.AddTransition("FINISHED", "Repeat");
        melodyHintCheck.AddMethod(() =>
        {
            if (PreconditionHint != null)
            {
                fsm.SendEvent("HINT");
            }
        });
        melodyHint.AddAction(new RunDialogue
        {
            Sheet = PreconditionHint!.Sheet,
            Key = PreconditionHint!.Key,
            PlayerVoiceTableOverride = new() { Value = null },
            PreventHeroAnimation = false,
            HideDecorators = false,
            TextAlignment = TMProOld.TextAlignmentOptions.TopLeft,
            OffsetY = 0f,
            OverrideContinue = false,
            Target = new() { OwnerOption = OwnerDefaultOption.UseOwner },
        });
        melodyHint.AddTransition("CONVO_END", "End Dialogue");

        // Skip the Elegy popup, grant the placement whilst the screen is black.
        FsmState popupState = fsm.MustGetState("Get Item Msg");
        popupState.Actions = [];
        popupState.InsertLambdaMethod(0, _ => GiveAll(() => fsm.SendEvent("GET ITEM MSG END")));

        // Avoid granting Elegy.
        FsmState updateQuestState = fsm.MustGetState("Update Quest");
        updateQuestState.GetFirstActionOfType<SetPlayerDataVariable>()!.Active = false;
    }
}