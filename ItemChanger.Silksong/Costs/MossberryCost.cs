using ItemChanger.Costs;
using ItemChanger.Silksong.Util;
using Newtonsoft.Json;

namespace ItemChanger.Silksong.Costs;

public class MossberryCost : Cost
{
    public required int Amount { get; init; }

    public override void OnPay()
    {
        Item.Take(Amount, false);
        PlayerData.instance.IntAdd(nameof(PlayerData.instance.druidMossBerriesSold), -Amount);
    }

    public override string GetCostText() => string.Format("FMT_PAY_MOSSBERRIES".GetLanguageString(), Amount);

    public override bool CanPay() => Item.SaveData.Amount >= Amount;

    public override bool IsFree => Amount == 0;

    public override bool HasPayEffects() => true;

    [JsonIgnore]
    private CollectableItem Item { get => field ??= CollectableItemManager.GetItemByName("Mossberry"); }
}