using ItemChanger.Modules;
using ItemChanger.Silksong.Costs;
using Newtonsoft.Json;

namespace ItemChanger.Silksong.Modules;

public abstract class CollectableCumulativeCostModule : Module, ICumulativeCostModule
{
    protected override void DoLoad() {}

    protected override void DoUnload() {}

    [JsonIgnore]
    public abstract string ItemId { get; }

    public int TotalSpent { get; set; } = 0;

    [JsonIgnore]
    public int CurrentlyAvailable => Item.CollectedAmount;

    public void Spend(int amount)
    {
        Item.Consume(amount, showCounter: true);
        TotalSpent += amount;
    }

    [JsonIgnore]
    private CollectableItem Item { get => field ??= CollectableItemManager.GetItemByName(ItemId); }
}
