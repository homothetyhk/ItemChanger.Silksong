using ItemChanger.Serialization;
using Newtonsoft.Json;
using PrepatcherPlugin;

namespace ItemChanger.Silksong.Locations.MultiLocationEnums;

public enum FleatopiaState
{
    /// <summary>
    /// The caravan has not arrived at Fleatopia
    /// </summary>
    PreCaravan,
    /// <summary>
    /// The caravan has arrived at Fleatopia
    /// </summary>
    Caravan,
    /// <summary>
    /// All fleas have been saved, and the caravan is at fleatopia
    /// </summary>
    Festival,
}

public class FleatopiaStateProvider : IValueProvider<FleatopiaState>
{
    [JsonIgnore]
    public FleatopiaState Value
    {
        get
        {
            // The state is controlled by the Caravan States - Conditions FSM
            if (PlayerDataAccess.CaravanTroupeLocation != GlobalEnums.CaravanTroupeLocations.Aqueduct)
            {
                return FleatopiaState.PreCaravan;
            }

            if (PlayerDataAccess.FleaGamesStarted || PlayerDataAccess.FleaGamesCanStart)
            {
                // Festival and PreFestival correspond to the same location, and we apply the fsm
                // edits for both in this case
                return FleatopiaState.Festival;
            }

            if (PlayerDataAccess.CaravanTroupeLocation == GlobalEnums.CaravanTroupeLocations.Aqueduct)
            {
                return FleatopiaState.Caravan;
            }

            return FleatopiaState.PreCaravan;
        }
    }
}
