using ItemChanger;
using ItemChanger.Silksong.Modules;
using System.Collections;
using UnityEngine;

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

    private static IEnumerator StartAfterSQ(Test t)
    {
        yield return GameManager.instance.ReturnToMainMenu(true, forceMainMenu: true);
        yield return new WaitWhile(() =>
        {
            return GameManager.SilentInstance is not GameManager gm || gm == null || !gm.IsMenuScene();
        });
        StartTest(t);
    }

    public static void StartTest(Test t)
    {
        if (!GameManager.instance.IsMenuScene())
        {
            ItemChangerTestingPlugin.Instance.StartCoroutine(StartAfterSQ(t));
            return;
        }

        Init();
        ItemChangerHost.Singleton.ActiveProfile!.Modules.Add(t);
        t.Setup(new());
        Run();
    }
}
