using ItemChanger.Serialization;
using Newtonsoft.Json;

namespace ItemChanger.Silksong.Serialization;

/// <summary>
/// A boolean value provider that reports whether a quest is marked as complete.
/// </summary>
public class QuestCompletedBool : IValueProvider<bool>
{
    /// <summary>
    /// The internal name of the quest, as accepted by QuestManager.GetQuest.
    /// </summary>
    public required string QuestName { get; set; }

    [JsonIgnore]
    private FullQuestBase Quest => field ??= QuestManager.GetQuest(QuestName);

    /// <inheritdoc/>
    [JsonIgnore]
    public bool Value => Quest.IsCompleted;
}
