using ItemChanger.Costs;
using UnityEngine.UI;

namespace ItemChanger.Silksong.Util;

public static class CostDialogue
{
    public static void Prompt(Cost cost, string rewardDescription, Action onAccept, Action onDecline)
    {
        UISelectionListItem yesButton = DialogueYesNoBox._instance.yesButton;
        Func<string> origCondition = yesButton.InactiveConditionText;
        
        void Accept()
        {
            try
            {
                if (cost.CanPay())
                {
                    cost.Pay();
                    onAccept();
                }
                else // if somehow the cost is no longer payable
                {
                    onDecline();
                }
            }
            finally
            {
                yesButton.InactiveConditionText = origCondition;
            }
        }

        void Decline()
        {
            try
            {
                onDecline();
            }
            finally
            {
                yesButton.InactiveConditionText = origCondition;
            }
        }

        yesButton.InactiveConditionText = () => cost.CanPay() ? "" : DialogueYesNoBox._instance.notEnoughText;
        DialogueYesNoBox.Open(Accept, Decline, false, $"{cost.GetCostText()}: {rewardDescription}");
    }
}