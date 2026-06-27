using ItemChanger.Costs;
using ItemChanger.Silksong.Util;

namespace ItemChanger.Silksong.Costs;

/// <summary>
/// A cost that requires having at least a given amount of tool slots unlocked.
/// Unlike the vanilla Eva costs, this includes slots unlocked on the Hunter Crest
/// or its upgrades.
/// </summary>
public class ToolSlotCost : Cost
{
    /// <summary>
    /// The amount of slots required to fulfil the cost.
    /// </summary>
    public required int Amount { get; set; }

    private static int UnlockedToolSlots()
    {
        int n = 0;
        foreach (ToolCrest crest in ToolItemManager.Instance.crestList)
        {
            if (crest.IsUnlocked && !crest.IsHidden && !crest.IsUpgradedVersionUnlocked)
            {
                // Counting logic is based on the CountCrestUnlockPoints FSM action.
                List<ToolCrestsData.SlotData> slotData = crest.SaveData.Slots;
                for (int i = 0; i < crest.Slots.Length; i++)
                {
                    if (!crest.Slots[i].IsLocked || (slotData != null && i < slotData.Count && slotData[i].IsUnlocked))
                    {
                        n++;
                    }
                }
            }
        }
        return n;
    }

    /// <inheritdoc/>
    public override bool CanPay() => UnlockedToolSlots() >= Amount;

    /// <inheritdoc/>
    public override void OnPay() {}

    /// <inheritdoc/>
    public override bool IsFree => Amount == 0;

    /// <inheritdoc/>
    public override string GetCostText() => string.Format("FMT_PAY_TOOL_SLOTS".GetLanguageString(), Amount);

    /// <inheritdoc/>
    public override bool HasPayEffects() => false;
}
