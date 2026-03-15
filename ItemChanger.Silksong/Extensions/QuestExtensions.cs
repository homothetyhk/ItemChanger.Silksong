namespace ItemChanger.Silksong.Extensions;

public static class QuestExtensions
{
    extension(FullQuestBase quest)
    {
        public void SetSeen()
        {
            QuestCompletionData.Completion c = quest.Completion;
            c.HasBeenSeen = true;
            quest.Completion = c;
        }

        public void SetAccepted()
        {
            QuestCompletionData.Completion c = quest.Completion;
            c.HasBeenSeen = true;
            quest.Completion = c;
        }

        public void ModifyCompletion(Action<QuestCompletionData.Completion> action)
        {
            QuestCompletionData.Completion c = quest.Completion;
            action(c);
            quest.Completion = c;
        }
    }
}
