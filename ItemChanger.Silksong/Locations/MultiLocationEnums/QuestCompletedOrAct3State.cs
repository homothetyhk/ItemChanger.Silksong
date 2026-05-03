using ItemChanger.Serialization;
using Newtonsoft.Json;
using PrepatcherPlugin;

namespace ItemChanger.Silksong.Locations.MultiLocationEnums;

public enum QuestCompletedOrAct3State
{
    /// <summary>
    /// Quest has not been completed.
    /// </summary>
    QuestIncomplete,
    
    /// <summary>
    /// Quest has been completed.
    /// </summary>
    QuestComplete,
    
    /// <summary>
    /// The world is in Act 3 state; quest may or may not be completed.
    /// </summary>
    Act3
}

/// <summary>
/// A value provider indicating whether the world is in Act 3 and, if not, whether a quest has been completed.
/// </summary>
public class QuestCompletedOrAct3StateProvider : IValueProvider<QuestCompletedOrAct3State>
{
    /// <summary>
    /// The quest whose completion state should be inspected.
    /// </summary>
    public required string Quest { get; init; }

    [JsonIgnore]
    public QuestCompletedOrAct3State Value
    {
        get
        {
            if (PlayerDataAccess.blackThreadWorld)
                return QuestCompletedOrAct3State.Act3;

            if (QuestManager.GetQuest(Quest).IsCompleted)
                return QuestCompletedOrAct3State.QuestComplete;

            return QuestCompletedOrAct3State.QuestIncomplete;
        }
    }
}