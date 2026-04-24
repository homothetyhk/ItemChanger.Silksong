using ItemChanger.Serialization;
using Newtonsoft.Json;

namespace ItemChanger.Silksong.Serialization;

public class QuestCompletedBool : IValueProvider<bool>
{
    public required string QuestName { get; set; }

    [JsonIgnore]
    private FullQuestBase Quest => field ??= QuestManager.GetQuest(QuestName);

    [JsonIgnore]
    public bool Value => Quest.IsCompleted;
}
