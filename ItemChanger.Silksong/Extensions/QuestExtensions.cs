using ItemChanger.Enums;
using ItemChanger.Items;
using ItemChanger.Placements;
using ItemChanger.Silksong.Placements;
using UnityEngine;

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

        public void ModifyReward(Placement placement)
        {
            var savedItem = ScriptableObject.CreateInstance<PlacementSavedItem>();
            savedItem.Placement = placement;
            savedItem.GiveInfo = new GiveInfo()
            {
                FlingType = FlingType.DirectDeposit,
                MessageType = MessageType.Any,
            };

            self.ModifyReward(savedItem);
        }

        public void ModifyReward(SavedItem reward)
        {
            self.rewardCount = 1;
            self.rewardCountAct3 = 1;
            self.rewardItem = reward;
            // Set reward icon to null so that the game fetches the icon from the item
            self.rewardIcon = null;
            self.rewardIconType = FullQuestBase.IconTypes.Image;
        }
    }
}