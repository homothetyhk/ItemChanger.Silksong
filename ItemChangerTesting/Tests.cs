using ItemChanger;
using ItemChanger.Silksong.Modules;
using ItemChanger.Silksong.StartDefs;
using Benchwarp.Data;
using System.ComponentModel;

namespace ItemChangerTesting;

public enum Tests
{
    [Description("Tests a TransitionOffsetStartDef at Tut_02, right1")]
    StartInTut_02
}

public static class TestDispatcher
{
    private static void Init()
    {
        GameManager.instance.profileID = ItemChangerTestingPlugin.Instance.cfgSaveSlot.Value;
        GameManager.instance.ClearSaveFile(ItemChangerTestingPlugin.Instance.cfgSaveSlot.Value, (b) => { });
        UIManager.instance.StartCoroutine(UIManager.instance.HideCurrentMenu());
        ItemChangerHost.Singleton.ActiveProfile?.Dispose();
        new ItemChangerProfile(host: ItemChangerHost.Singleton);
    }

    private static void StartNear(string scene, string gate)
    {
        ItemChangerHost.Singleton.ActiveProfile!.Modules.Remove<StartDefModule>();
        ItemChangerHost.Singleton.ActiveProfile!.Modules.Add(new StartDefModule
        {
            StartDef = new TransitionOffsetStartDef { SceneName = scene, GateName = gate, }
        });
    }

    private static void Run()
    {
        UIManager.instance.StartNewGame(false, false);
    }

    public static void StartTest()
    {
        Init();
        ItemChangerProfile prof = ItemChangerHost.Singleton.ActiveProfile!;
        switch (ItemChangerTestingPlugin.Instance.cfgTest.Value)
        {
            case Tests.StartInTut_02:
                StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
                break;
        }
        Run();
    }
}
