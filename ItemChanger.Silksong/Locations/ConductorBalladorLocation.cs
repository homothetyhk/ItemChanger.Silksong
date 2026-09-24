using GenericVariableExtension;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Silksong.RawData;
using PrepatcherPlugin;
using QuestPlaymakerActions;
using Silksong.FsmUtil;
using Silksong.FsmUtil.Actions;

namespace ItemChanger.Silksong.Locations;

public class ConductorBalladorLocation : ThreefoldMelodyLocation
{
    protected override void DoLoad()
    {
        Using(new FsmEditGroup()
        {
            {new(SceneName!, "Last Conductor NPC", "Dialogue"), HookConductor},
        });
    }

    protected override void DoUnload() { }

    private void HookConductor(PlayMakerFSM fsm)
    {
        // Replace this hasMelodyConductor check with a placement-obtained check so
        // Ballador gives his other dialogue if the placement has no items to give.
        FsmState hasItemState = fsm.MustGetState("Has Item?");
        hasItemState.ReplaceFirstActionOfType<SavedItemCanGetMore>(
            new LambdaAction { Method = () =>
                {
                    if (Placement!.AllObtained()) fsm.SendEvent("FINISHED");
                }
            });
        // Ballador has another hasMelodyConductor check, but that one only determines whether Hornet's Needolin
        // will play the Conductor's Melody instead of the usual tune, so just leave it as is

        // The vanilla game doesn't enforce a Needolin requirement for this cutscene, so we need to make one ourselves
        // The base game already has alternate dialogue for getting here early, so just reuse that
        hasItemState.InsertMethod(4, () =>
            {
                if (!PlayerDataAccess.hasNeedolin) fsm.SetVariable("Is Melody Quest Active", false);
            });
        // Ballador checks the quest state twice instead of reusing the "Is Melody Quest Active" variable.
        FsmState questCheckState = fsm.MustGetState("Quest Active?");
        FsmEvent falseEvent = questCheckState.GetFirstActionOfType<CheckQuestStateV2>()!.NotTrackedEvent;
        questCheckState.InsertAction(new PlayerDataBoolTest() { boolName = "hasNeedolin", isFalse = falseEvent}, 0);
        
        ReplaceMelody(fsm.MustGetState("Run Melody Play Prompted").GetFirstActionOfType<RunFSM>()!);

        // Remove quest update notification (unless it's a Dearest)
        if (Placement?.Items.Any(i => i.Name == ItemNames.Conductor_s_Melody) != true)
        {
            fsm.MustGetState("End Dialogue").RemoveFirstActionOfType<ShowQuestUpdatedStandalone>();
        }
    }
}
