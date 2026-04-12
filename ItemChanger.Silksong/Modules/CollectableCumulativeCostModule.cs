using ItemChanger.Modules;
using ItemChanger.Silksong.Costs;
using Newtonsoft.Json;

namespace ItemChanger.Silksong.Modules;

/// <summary>
/// An implementation of ICumulativeCostModule for items
/// that are represented internally by CollectableItem.
/// </summary>
public abstract class CollectableCumulativeCostModule : Module, ICumulativeCostModule
{
    protected override void DoLoad() {}

    protected override void DoUnload() {}

    /// <summary>
    /// The name of the item tracked by this module.
    /// </summary>
    [JsonIgnore]
    public abstract string ItemId { get; }

    /// <inheritdoc/>
    public int TotalSpent { get; set; } = 0;

    /// <inheritdoc/>
    [JsonIgnore]
    public int CurrentlyAvailable => Item.CollectedAmount;

    /// <inheritdoc/>
    public void Spend(int amount)
    {
        Item.Consume(amount, showCounter: true);
        TotalSpent += amount;
    }

    [JsonIgnore]
    private CollectableItem Item { get => field ??= CollectableItemManager.GetItemByName(ItemId); }
}
