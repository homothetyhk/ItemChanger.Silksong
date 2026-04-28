using ItemChanger.Costs;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.RawData;
using Newtonsoft.Json;
using PrepatcherPlugin;
using UnityEngine;

namespace ItemChanger.Silksong.Costs;

public class RosaryCost(int amount) : Cost, ICurrencyCost, IDisplayCost
{
    public int Amount { get; init; } = amount;

    /// <summary>
    /// Amount after accounting for any discount rate.
    /// </summary>
    [JsonIgnore]
    public int ActualAmount => (int)(Amount * base.DiscountRate);

    public override bool CanPay() => PlayerDataAccess.geo >= ActualAmount;

    public override string GetCostText() => RawData.ItemChangerLanguageStrings.CreatePayRosariesString(ActualAmount.ToValueProvider()).Value;

    public override bool HasPayEffects() => true;

    public override void OnPay()
    {
        if (ActualAmount > 0) HeroController.instance.TakeGeo(ActualAmount);
    }

    public override bool IsFree => ActualAmount <= 0;

    int ICurrencyCost.Amount => ActualAmount;

    CurrencyType ICurrencyCost.CurrencyType => CurrencyType.Money;
    
    int IDisplayCost.Amount => ActualAmount;

    Sprite IDisplayCost.DisplaySprite => BaseAtlasSprites.Rosaries.Value;

    public static int GetRosaryCost(Cost cost) => cost.GetCostsOfType<RosaryCost>().Select(c => c.ActualAmount).Sum();
}
