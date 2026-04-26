namespace ItemChanger.Silksong.Extensions;

public static class QuestExtensions
{
    public delegate void CompletionModifier(ref QuestCompletionData.Completion c);

    extension(FullQuestBase self)
    {
        public void SetSeen() => self.ModifyCompletion((ref c) => c.HasBeenSeen = true);

        public void SetAccepted() => self.ModifyCompletion((ref c) => c.IsAccepted = true);

        public void ModifyCompletion(CompletionModifier modifier)
        {
            QuestCompletionData.Completion c = self.Completion;
            modifier(ref c);
            self.Completion = c;
        }

        public void ModifyTargetAmount(int newTargetAmount)
        {
            self.ModifyTargetAmount(0, newTargetAmount);
        }

        public void ModifyTargetAmount(int targetIndex, int newTargetAmount)
        {
            self.targets[targetIndex].Count = newTargetAmount;
        }
    }
}