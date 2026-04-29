using ItemChanger.Costs;
using UnityEngine.UI;

namespace ItemChanger.Silksong.Util;

/// <summary>
/// Contains utility methods for prompting for IC costs to be paid.
/// </summary>
public static class CostDialogue
{
    /// <summary>
    /// Displays a yes/no prompt describing the given cost and reward.
    /// The "Yes" button will be disabled if the cost cannot
    /// be paid.
    /// </summary>
    /// <param name="cost">The cost of the rewards.</param>
    /// <param name="rewardDescription">Text describing the rewards.</param>
    /// <param name="onAccept">Delegate called if the player answers "Yes".</param>
    /// <param name="onDecline">Delegate called if the player answers "No".</param>
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