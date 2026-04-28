namespace ItemChangerTesting;

internal static class QuestUtil
{
    public static void SetAvailable(string questName)
    {
        if (QuestManager.TryGetFullQuestBase(questName, out var quest))
            quest.SetAvailable();
        else
            ItemChangerTestingPlugin.Instance.Logger.LogError($"Unable to locate quest '{quest}'.");
    }

    public static void SetReadyToComplete(string questName)
    {
        if (QuestManager.TryGetFullQuestBase(questName, out var quest))
            quest.SetReadyToComplete();
        else
            ItemChangerTestingPlugin.Instance.Logger.LogError($"Unable to locate quest '{quest}'.");
    }

    public static void SetCompleted(string questName)
    {
        if (QuestManager.TryGetFullQuestBase(questName, out var quest))
            quest.SetCompleted();
        else
            ItemChangerTestingPlugin.Instance.Logger.LogError($"Unable to locate quest '{quest}'.");
    }
}
