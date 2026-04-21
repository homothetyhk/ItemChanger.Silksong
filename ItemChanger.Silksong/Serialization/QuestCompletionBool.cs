using ItemChanger.Serialization;
using Newtonsoft.Json;

namespace ItemChanger.Silksong.Serialization;

public enum QuestCompletion
{
    HasBeenSeen,
    IsAccepted,
    IsCompleted,
    WasEverCompleted
}

public record QuestCompletionBool(string QuestName, QuestCompletion RequiredCompletion) : IValueProvider<bool>
{
    [JsonIgnore]
    public bool Value
    {
        get
        {
            if (!QuestManager.TryGetFullQuestBase(QuestName, out var quest))
            {
                LogWarn($"Unable to locate quest '{quest}'.");
                return false;
            }

            return RequiredCompletion switch
            {
                QuestCompletion.HasBeenSeen => quest.Completion.HasBeenSeen,
                QuestCompletion.IsAccepted => quest.Completion.IsAccepted,
                QuestCompletion.IsCompleted => quest.Completion.IsCompleted,
                QuestCompletion.WasEverCompleted => quest.Completion.WasEverCompleted,
                _ => throw new ArgumentOutOfRangeException(nameof(RequiredCompletion), RequiredCompletion,
                    "Unexpected quest completion value.")
            };
        }
    }
}