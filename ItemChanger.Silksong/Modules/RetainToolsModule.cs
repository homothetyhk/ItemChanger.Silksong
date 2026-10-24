using ItemChanger.Silksong.RawData;
using MonoDetour.DetourTypes;
using QuestPlaymakerActions;

namespace ItemChanger.Silksong.Modules;

/// <summary>
/// Prevents tools from being consumed by quest turn-ins
/// </summary>
public class RetainToolsModule : ItemChanger.Modules.Module
{
    protected override void DoLoad()
    {
        Using(Md.ToolItem.Consume.ControlFlowPrefix(ToolItemConsumePrefix));
        Using(Md.QuestPlaymakerActions.QuestConsumeTargetTake.DoAction.ControlFlowPrefix(QuestConsumeTargetTakeAction));
    }

    protected override void DoUnload() { }

    private static ReturnFlow ToolItemConsumePrefix(ToolItem self, ref int amount, ref bool showCounter) => ReturnFlow.SkipOriginal;

    private static ReturnFlow QuestConsumeTargetTakeAction(QuestConsumeTargetTake self, ref FullQuestBase quest)
    {
        if (quest.name == Quests.Extractor_Blue || quest.name == Quests.Extractor_Blue_Worms)
        {
            foreach ((FullQuestBase.QuestTarget questTarget, int amount) in quest.TargetsAndCountersNotHidden)
            {
                if (questTarget.Counter && questTarget.Counter is not ToolItem) // skip consuming needle phial
                {
                    questTarget.Counter.Consume(amount, showCounter: false);
                    CollectableUIMsg.ShowTakeMsg(questTarget.Counter, (TakeItemTypes)self.TakeDisplay.Value);
                }
            }
            return ReturnFlow.SkipOriginal;
        }
        return ReturnFlow.None;
    }
}
