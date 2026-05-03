using ItemChanger.Enums;
using ItemChanger.Placements;
using ItemChanger.Serialization;
using Newtonsoft.Json;
using PrepatcherPlugin;

namespace ItemChanger.Silksong.Locations.MultiLocationEnums;

public enum PlacementObtainedOrAct3State
{
    /// <summary>
    /// Placement has had no items obtained.
    /// </summary>
    Unobtained,

    /// <summary>
    /// Placement has had some or all items obtained or visited, configurable in
    /// <see cref="PlacementObtainedOrAct3StateProvider"/>.
    /// </summary>
    Obtained,

    /// <summary>
    /// The world is in Act 3 state; placement may or may not be obtained.
    /// </summary>
    Act3
}

/// <summary>
/// A value provider wrapping a <see cref="PlacementVisitStateBool"/> with an additional state for being in Act 3.
/// </summary>
/// <param name="placementName">Placement whose visit state should be inspected.</param>
/// <param name="requiredFlags">Flags that must be present on the placement's visit state to result in the
/// <see cref="PlacementObtainedOrAct3State.Obtained"/> state.</param>
/// <param name="missingPlacementTest">An optional test to use if the placement is not found.</param>
public class PlacementObtainedOrAct3StateProvider(
    string placementName,
    VisitState requiredFlags = VisitState.ObtainedAnyItem,
    IValueProvider<bool>? missingPlacementTest = null) : IValueProvider<PlacementObtainedOrAct3State>
{
    public string PlacementName => placementName;

    public VisitState RequiredFlags => requiredFlags;

    public IValueProvider<bool>? MissingPlacementTest => missingPlacementTest;

    private PlacementVisitStateBool VisitStateBool
    {
        get
        {
            field ??= new PlacementVisitStateBool()
            {
                PlacementName = PlacementName,
                RequiredFlags = RequiredFlags,
                MissingPlacementTest = MissingPlacementTest
            };
            return field;
        }
    }

    [JsonIgnore]
    public PlacementObtainedOrAct3State Value
    {
        get
        {
            if (PlayerDataAccess.blackThreadWorld)
                return PlacementObtainedOrAct3State.Act3;

            return VisitStateBool.Value
                ? PlacementObtainedOrAct3State.Obtained
                : PlacementObtainedOrAct3State.Unobtained;
        }
    }
}