using ItemChanger.Costs;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.RawData;
using System.Diagnostics.CodeAnalysis;

namespace ItemChanger.Silksong.Costs;

/// <summary>
/// Requires the player to own at least N crest tools in order to purchase this item.
/// </summary>
[method: SetsRequiredMembers]
public class ToolCountCost(int amount) : Cost
{
    public required int Amount = amount;

    public override bool IsFree => Amount <= 0;

    public override bool CanPay() => Amount <= ToolItemManager.GetOwnedToolsCount(ToolItemManager.OwnToolsCheckFlags.AllTools);

    public override string GetCostText() => CanPay() ? "" : ItemChangerLanguageStrings.FMT_PAY_TOOLS.Format(Amount - ToolItemManager.GetOwnedToolsCount(ToolItemManager.OwnToolsCheckFlags.Red));

    public override bool HasPayEffects() => false;

    public override void OnPay() { }

    public static int GetToolCountCost(Cost cost) => cost.GetCostsOfType<ToolCountCost>().Select(c => c.Amount).DefaultIfEmpty().Max();
}
