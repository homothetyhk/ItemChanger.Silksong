using ItemChanger.Placements;
using ItemChanger.Serialization;
using Newtonsoft.Json;

namespace ItemChanger.Silksong.Serialization;

/// <summary>
/// Returns true if `ItemName` is not present and unobtained at `PlacementName`.
/// </summary>
public class IsItemMissingBool : IValueProvider<bool>
{
    public required string PlacementName { get; init; }

    public required string ItemName { get; init; }

    [JsonIgnore]
    public bool Value => SilksongHost.Instance.ActiveProfile?.TryGetPlacement(PlacementName, out Placement? pmt) is true && pmt.Items.All(i => i.IsObtained() || i.Name != ItemName);
}
