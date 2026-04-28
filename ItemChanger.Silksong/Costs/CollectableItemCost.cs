using ItemChanger.Costs;
using ItemChanger.Silksong.Extensions;
using Newtonsoft.Json;

namespace ItemChanger.Silksong.Costs;

public class CollectableItemCost : Cost
{
    public required int Amount;
    public required string CollectableItemId
    {
        get => field;
        set
        {
            field = value;
            CollectableItem = CollectableItemManager.GetItemByName(value);
        }
    }

    [JsonIgnore]
    public CollectableItem? CollectableItem { get; private set; }

    [JsonIgnore]
    public override bool IsFree => Amount <= 0;

    public override bool CanPay() => CollectableItem == null || CollectableItem.CollectedAmount >= Amount;

    public override string GetCostText()
    {
        var item = CollectableItem;
        var displayName = item != null ? item.GetDisplayName(CollectableItem.ReadSource.Shop) : "???";
        return $"{Amount} {displayName}";
    }

    public override bool HasPayEffects() => Amount > 0;

    public override void OnPay()
    {
        var item = CollectableItem;
        if (item != null) item.Consume(Amount, showCounter: true);
    }

    public static (CollectableItem item, int amount)? GetCollectableItemCost(Cost cost)
    {
        List<CollectableItemCost> costs = [.. cost.GetCostsOfType<CollectableItemCost>()];
        if (costs.Count == 0) return null;
        if (costs.Count > 1 && !costs.All(c => c.CollectableItem == costs[0].CollectableItem)) return null;

        return (costs[0].CollectableItem!, costs.Select(p => p.Amount).Sum());
    }
}
