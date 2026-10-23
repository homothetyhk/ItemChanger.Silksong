using Benchwarp.Data;
using ItemChanger.Modules;
using ItemChanger.Silksong.RawData;
using PrepatcherPlugin;
using Silksong.UnityHelper.Extensions;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Modules;

/// <summary>
/// Prevents Second Sentinel from spawning in their arena if their quest has not yet been accepted
/// </summary>
[SingletonModule]
public class SecondSentinelRequireQuestModule : Module
{
    protected override void DoLoad()
    {
        Using(new SceneEditGroup { { SceneNames.Hang_17b, ModifyScene } });
    }
    protected override void DoUnload()
    {
    }
    private void ModifyScene(Scene scene)
    {
        GameObject? sentinel = scene.FindGameObject("Boss Scene - To Additive Load");
        if (sentinel == null)
        {
            LogWarn($"{GetType().Name} failed to find Second Sentintel boss scene.");
            return;
        }
        if (!QuestManager.GetQuest(Quests.Song_Knight).IsAccepted)
        {
            sentinel.SetActive(false);
        }
    }
}
