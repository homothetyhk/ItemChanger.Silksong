using ItemChanger;
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
        UIManager.instance.StartNewGame(false, false);
    }

    public static void StartTest(Test t)
    {
        Init();
        ItemChangerHost.Singleton.ActiveProfile!.Modules.Add(t);
        t.Setup(new());
        Run();
    }
}
