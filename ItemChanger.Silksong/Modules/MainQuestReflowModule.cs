using Benchwarp.Data;
using ItemChanger.Modules;
using ItemChanger.Silksong.RawData;
using QuestPlaymakerActions;
using Silksong.FsmUtil;

namespace ItemChanger.Silksong.Modules;

/// <summary>
/// Module which edits main quest triggers to improve robustness for nonvanilla progression. See remarks for detailed changes.
/// </summary>
/// <remarks>
/// Removes the trigger for Quests.Black_Thread_Pt4_Return from Dock_04[left1] transition.
/// Modifies the Lace Abyss conversation to trigger Quests.Black_Thread_Pt4_Return instead of Quests.Black_Thread_Pt3_Escape, 
/// effectively removing the Escape step of the quest.
/// </remarks>
public class MainQuestReflowModule : Module
{
    protected override void DoLoad()
    {
        Using(new FsmEditGroup
        {
            { new(SceneNames.Abyss_05, "Lace Abyss Ghost Cutscene", "Lace Cutscene"), ReplaceEscapeQuestTrigger },
            { new(SceneNames.Dock_04, "left1", "Advance Quest"), RemoveReturnQuestTrigger },
        });
    }

    protected override void DoUnload() { }

    private void ReplaceEscapeQuestTrigger(PlayMakerFSM fsm)
    {
        // replace trigger to begin pt3 by trigger to begin pt4
        fsm.MustGetState("Return Control").GetFirstActionOfType<BeginQuest>()!.Quest = QuestManager.GetQuest(Quests.Black_Thread_Pt4_Return);
    }

    private void RemoveReturnQuestTrigger(PlayMakerFSM fsm)
    {
        // remove trigger to begin pt4
        fsm.MustGetState("State 1").RemoveTransitions();
    }
}
