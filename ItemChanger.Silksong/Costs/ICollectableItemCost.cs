using ItemChanger.Costs;
using ItemChanger.Silksong.Extensions;

namespace ItemChanger.Silksong.Costs;

/// <summary>
/// A specific type of display cost which is associated with a CollectableItem.
/// 
/// Implementations may deduct resources directly, or implement a cumulative cost, or some other mechanism.
/// </summary>
public interface ICollectableItemCost : IDisplayCost
{
    public CollectableItem CollectableItem { get; }

    public static (CollectableItem item, int amount)? Get(Cost cost)
    {
        List<ICollectableItemCost> costs = [.. cost.GetCostsOfType<ICollectableItemCost>()];
        if (costs.Count == 0) return null;
        if (costs.Count > 1 && !costs.All(c => c.CollectableItem == costs[0].CollectableItem)) return null;

        return (costs[0].CollectableItem!, costs.Sum(p => p.Amount));
    }
}
