using ItemChanger.Locations;
using ItemChanger.Placements;
using ItemChanger.Serialization;
using Newtonsoft.Json;

namespace ItemChanger.Silksong.Serialization;

/// <summary>
/// Returns true if the given placement uses a container other than the expected one.
/// </summary>
public class UnexpectedContainerBool : IValueProvider<bool>
{
    public required string PlacementName { get; init; }

    public required string ExpectedContainerType { get; init; }

    [JsonIgnore]
    public bool Value => SilksongHost.Instance.ActiveProfile?.TryGetPlacement(PlacementName, out Placement? pmt) is true
        && pmt is IPrimaryLocationPlacement plpmt
        && plpmt.Location is ContainerLocation cloc
        && cloc.ChooseBestContainerType() != ExpectedContainerType;
}
