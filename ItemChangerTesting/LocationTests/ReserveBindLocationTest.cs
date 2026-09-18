using Benchwarp.Data;
using ItemChanger;
using ItemChanger.Extensions;
using ItemChanger.Silksong;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.Modules;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;
using UnityEngine.SceneManagement;

namespace ItemChangerTesting.LocationTests;

internal class ReserveBindLocationTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Second Sentinel Location",
        MenuDescription = "Tests giving various items from the Reserve Bind slot.",
        Revision = 2026091700
    };

    public override void Setup(TestArgs args)
    {
        StartAt(new CoordinateStartDef()
        {
            SceneName = SceneNames.Hang_17b,
            X = 41.30f,
            Y = 4.57f,
            MapZone = GlobalEnums.MapZone.NONE
        });
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Reserve_Bind)!.Wrap()
            .WithVariousItems().WithAllPersistent());

        Modules.Add(new SecondSentinelRequireQuestModule());
    }

    protected override void DoLoad()
    {
        base.DoLoad();
        Using(new SceneEditGroup { { SceneNames.Hang_17b, WeakenBoss } });
    }

    protected override void OnEnterGame()
    {
        base.OnEnterGame();

        PlayerData pd = PlayerData.instance;
        if (pd == null) return;

        // Also test module to remove Second Sentinel from Hang_17b before the quest is accepted
        // QuestManager.GetQuest(Quests.Song_Knight).SetAccepted();
    }

    private static void WeakenBoss(Scene scene)
    {
        GameObject? sentinel = scene.FindGameObject("Boss Scene - To Additive Load/Song Knight");
        if (sentinel == null)
        {
            ItemChangerTestingPlugin.Instance.Logger.LogWarning("Failed to locate Second Sentinel boss");
            return;
        }
        sentinel.GetComponent<HealthManager>().hp = 1;
    }
    public override IEnumerable<(string, Action)> TestMethods()
    {
        yield return ("Accept Sentinel Quest", () => QuestManager.GetQuest(Quests.Song_Knight).SetAccepted());
    }
}
