using ItemChanger.Costs;
using ItemChanger.Silksong.Extensions;
using Newtonsoft.Json;
using PrepatcherPlugin;
using System.Diagnostics.CodeAnalysis;

namespace ItemChanger.Silksong.Costs;

[method: SetsRequiredMembers]
public class ShellShardCost(int amount) : Cost
{
    public required int Amount = amount;

    /// <summary>
    /// Amount after accounting for any discount rate.
    /// </summary>
    [JsonIgnore]
    public int ActualAmount => (int)(Amount * base.DiscountRate);

    public override bool CanPay() => PlayerDataAccess.ShellShards >= ActualAmount;

    public override string GetCostText() => RawData.ItemChangerLanguageStrings.CreatePayShellShardsString(ActualAmount.ToValueProvider()).Value;

    public override bool HasPayEffects() => true;

    public override void OnPay()
    {
        if (ActualAmount > 0) HeroController.instance.TakeShards(ActualAmount);
    }

    public override bool IsFree => ActualAmount <= 0;

    public static int GetShellShardCost(Cost cost) => cost.GetCostsOfType<ShellShardCost>().Select(c => c.ActualAmount).Sum();
}
