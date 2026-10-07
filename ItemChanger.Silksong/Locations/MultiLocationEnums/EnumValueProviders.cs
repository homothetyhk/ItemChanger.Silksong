using ItemChanger.Enums;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Serialization;

namespace ItemChanger.Silksong.Locations.MultiLocationEnums;

public static class EnumValueProviders
{
    public static IValueProvider<FleatopiaState> FleatopiaStateProvider =>
        new FleatopiaStateProvider();

    public static IValueProvider<SlabBattleState> SlabBattleStateProvider =>
        new SlabBattleStateProvider();

    /// <summary>
    /// Returns a value provider wrapping a <see cref="PlacementVisitStateBool"/> with an additional state for being in Act 3.
    /// </summary>
    /// <param name="placementName">Placement whose visit state should be inspected.</param>
    /// <param name="requiredFlags">Flags that must be present on the placement's visit state to result in the
    /// <see cref="PlacementVisitedOrAct3State.Visited"/> state.</param>
    /// <param name="missingPlacementTest">An optional test to use if the placement is not found.</param>
    public static IValueProvider<PlacementVisitedOrAct3State> PlacementVisitedOrAct3StateProvider(
        string placementName,
        VisitState requiredFlags = VisitState.ObtainedAnyItem,
        IValueProvider<bool>? missingPlacementTest = null
        ) =>
        new IfThenElseValueProvider<PlacementVisitedOrAct3State>(Test: new PDBool(nameof(PlayerData.blackThreadWorld)),
            TrueValue: PlacementVisitedOrAct3State.Act3,
            FalseValue: new IfThenElseValueProvider<PlacementVisitedOrAct3State>(Test: new PlacementVisitStateBool
            {
                PlacementName = placementName,
                RequiredFlags = requiredFlags,
                MissingPlacementTest = missingPlacementTest,
            }, TrueValue: PlacementVisitedOrAct3State.Visited, FalseValue: PlacementVisitedOrAct3State.NotVisited)
        );

    /// <summary>
    /// Returns a value provider indicating whether the world is in Act 3 and, if not, whether the specified quest has been completed.
    /// </summary>
    public static IValueProvider<QuestCompletedOrAct3State> QuestCompletedOrAct3StateProvider(string questName) =>
        new IfThenElseValueProvider<QuestCompletedOrAct3State>(Test: new PDBool(nameof(PlayerData.blackThreadWorld)),
            TrueValue: QuestCompletedOrAct3State.Act3,
            FalseValue: new IfThenElseValueProvider<QuestCompletedOrAct3State>(Test: new QuestCompletionBool(questName),
                TrueValue: QuestCompletedOrAct3State.QuestComplete, FalseValue: QuestCompletedOrAct3State.QuestIncomplete)
        );
}
