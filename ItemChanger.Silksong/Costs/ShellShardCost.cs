using ItemChanger.Costs;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.RawData;
using Newtonsoft.Json;
using PrepatcherPlugin;
using UnityEngine;

namespace ItemChanger.Silksong.Costs;

public class ShellShardCost(int amount) : Cost, ICurrencyCost, IDisplayCost
{
    public int Amount { get; init; } = amount;

    /// <summary>
    /// Amount after accounting for any discount rate.
    /// </summary>
    [JsonIgnore]
    public int ActualAmount => (int)(Amount * base.DiscountRate);

    public override bool CanPay() => PlayerDataAccess.ShellShards >= ActualAmount;

    public override string GetCostText() => ItemChangerLanguageStrings.CreatePayShellShardsString(ActualAmount.ToValueProvider()).Value;

    public override bool HasPayEffects() => true;

    public override void OnPay()
    {
        if (ActualAmount > 0) HeroController.instance.TakeShards(ActualAmount);
    }

    public override bool IsFree => ActualAmount <= 0;

    int ICurrencyCost.Amount => ActualAmount;

    CurrencyType ICurrencyCost.CurrencyType => CurrencyType.Shard;

    int IDisplayCost.Amount => ActualAmount;

    Sprite IDisplayCost.DisplaySprite => BaseAtlasSprites.ShellShards.Value;

    public static int GetShellShardCost(Cost cost) => cost.GetCostsOfType<ShellShardCost>().Select(c => c.ActualAmount).Sum();
}
