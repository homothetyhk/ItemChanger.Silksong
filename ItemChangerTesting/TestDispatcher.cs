using ItemChanger;
using ItemChanger.Silksong;
using ItemChanger.Silksong.Modules;

namespace ItemChangerTesting;

internal static class TestDispatcher
{
    private static void Init()
    {
        GameManager.instance.profileID = ItemChangerTestingPlugin.Instance.cfgSaveSlot.Value;
        GameManager.instance.ClearSaveFile(ItemChangerTestingPlugin.Instance.cfgSaveSlot.Value, (b) => { });
        UIManager.instance.StartCoroutine(UIManager.instance.HideCurrentMenu());
        ItemChangerHost.Singleton.ActiveProfile?.Dispose();
        new ItemChangerProfile(host: ItemChangerHost.Singleton);
        ItemChangerHost.Singleton.ActiveProfile!.Modules.GetOrAdd<ConsistentRandomnessModule>().Seed = 12345;
    }

    private static void Run()
    {
        DisableSceneDataBehaviours();
        SceneData.instance.Reset();  // Normally invoked by QuitToMenu, but we skip that.
        UIManager.instance.StartNewGame(false, false);
    }

    public static void StartTest(Test t)
    {
        Init();
        ItemChangerHost.Singleton.ActiveProfile!.Modules.Add(t);
        t.Setup(new());
        Run();
    }

    // Immediately unsubscribe any behaviours that would write persistent data, to prevent it leaking into the next test case.
    // We must do this immediately because this otherwise occurs during scene unloading, which is after we have already started writing to the new save file.
    private static void DisableSceneDataBehaviours()
    {
        foreach (var component in UObject.FindObjectsByType<GeoRock>(UnityEngine.FindObjectsInactive.Include, UnityEngine.FindObjectsSortMode.None))
            component.OnDisable();
        foreach (var component in UObject.FindObjectsByType<PersistentBoolItem>(UnityEngine.FindObjectsInactive.Include, UnityEngine.FindObjectsSortMode.None))
            component.OnDestroy();
        foreach (var component in UObject.FindObjectsByType<PersistentIntItem>(UnityEngine.FindObjectsInactive.Include, UnityEngine.FindObjectsSortMode.None))
            component.OnDestroy();
    }
}
