using ItemChanger.Costs;
using ItemChanger.Silksong.Extensions;
using Newtonsoft.Json;
using UnityEngine;

namespace ItemChanger.Silksong.Costs;

public class CollectableItemCost : Cost, ICollectableItemCost
{
    public required int Amount { get; init; }

    public required string CollectableItemId;

    [JsonIgnore]
    public CollectableItem CollectableItem => CollectableItemManager.GetItemByName(CollectableItemId);

    [JsonIgnore]
    public override bool IsFree => Amount <= 0;

    [JsonIgnore]
    public Sprite DisplaySprite => CollectableItem!.GetCollectionIcon();

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
}
