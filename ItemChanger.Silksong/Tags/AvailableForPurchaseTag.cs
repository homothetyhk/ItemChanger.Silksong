using ItemChanger.Serialization;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.Tags;

/// <summary>
/// Tag to indicate that a specific item may not be available for purchase in a shop until preconditions are met.
/// This is independent of the Cost of the item, which the user may or may not be able to afford.
/// 
/// This can be placed on a specific item, an entire location or entire placement.
/// If multiple tags affect a specific item, the conjunction applies.
/// If no tags affect a specific shop item, it is assumed to be available for purchase.
/// </summary>
public class AvailableForPurchaseTag : Tag
{
    public required IValueProvider<bool> Available;
}
