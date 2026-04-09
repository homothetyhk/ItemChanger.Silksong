using ItemChanger.Costs;
using ItemChanger.Silksong.Util;
using Newtonsoft.Json;

namespace ItemChanger.Silksong.Costs;

public class CollectableItemCost : Cost
{
    public required string ItemId { get; init; }

    public required string CostLanguageKey { get; init; }

    public required int Amount { get; init; }

    public static CollectableItemCost Mossberries(int n) => new()
    {
        ItemId = "Mossberry",
        CostLanguageKey = "FMT_PAY_MOSSBERRY",
        Amount = n,
    };

    public override void OnPay()
    {
        Item.Take(Amount, false);
    }

    public override string GetCostText() => string.Format(CostLanguageKey.GetLanguageString(), Amount);

    public override bool CanPay() => Item.SaveData.Amount >= Amount;

    public override bool IsFree => Amount == 0;

    public override bool HasPayEffects() => true;

    [JsonIgnore]
    private CollectableItem Item { get => field ??= CollectableItemManager.GetItemByName(ItemId); }
}
