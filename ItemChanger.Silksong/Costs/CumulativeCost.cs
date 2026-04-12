using ItemChanger.Costs;
using Newtonsoft.Json;

namespace ItemChanger.Silksong.Costs;

/// <summary>
/// A cost for a resource that is tracked cumulatively, rather than being consumed at will by the player.
/// 
/// A shop containing two items with cumulative costs 1, 2, and 5, will show the latter two items as costing 1 and 4 after the user has purchased the first item.
/// Likewise, after the user has purchased the second item, the final item will show as costing 3. In this way the only gating factor to purchasing any item
/// is the total amount of resources the player has ever collected, they do not need to choose what to purchase.
/// </summary>
public abstract class CumulativeCost : Cost
{
    /// <summary>
    /// The cumulative cost requirement for this Cost.
    /// </summary>
    public required int Value { get; set; }

    /// <summary>
    /// The module that defines this cost type. This should be retrieved by type at runtime, not serialized.
    /// </summary>
    [JsonIgnore]
    protected abstract ICumulativeCostModule Module { get; }

    public override bool IsFree => Module.TotalSpent >= Value;

    public override bool CanPay() => Module.TotalSpent + Module.CurrentlyAvailable >= Value;

    /// <summary>
    /// Returns a formatted string for the given cost amount, to be displayed in a shop context.
    /// </summary>
    protected abstract string GetCostText(int toSpend);

    public override string GetCostText() => GetCostText(Math.Max(0, Value - Module.TotalSpent));

    public override bool HasPayEffects() => true;

    public override void OnPay()
    {
        int toSpend = Value - Module.TotalSpent;
        if (toSpend > 0) Module.Spend(toSpend);
    }
}

/// <summary>
/// Module for implementing cumulative costs. Typically this is implemented as an ItemChanger.Module, but this is not required.
/// </summary>
public interface ICumulativeCostModule
{
    /// <summary>
    /// The count of the resource the player currently has available.
    /// </summary>
    int CurrentlyAvailable { get; }
    /// <summary>
    /// The total number of resources so far spent by the player for this module.
    /// </summary>
    int TotalSpent { get; }
    /// <summary>
    /// Mark `amount` resources as spent, generally by decrementing 'CurrentlyAvailable' and incrementing 'TotalSpent'.
    /// </summary>
    void Spend(int amount);
}
