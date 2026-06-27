using ItemChanger.Serialization;
using Newtonsoft.Json;

namespace ItemChanger.Silksong.Serialization;

public record QuestAcceptedBool(string QuestName) : IValueProvider<bool>
{
    [JsonIgnore]
    public bool Value => QuestManager.TryGetFullQuestBase(QuestName, out var quest) && quest.Completion.IsAccepted;
}
