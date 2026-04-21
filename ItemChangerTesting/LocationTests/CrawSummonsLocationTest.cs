using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using PrepatcherPlugin;

namespace ItemChangerTesting.LocationTests;

internal class CrawSummonsLocationTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Craw Summons",
        MenuDescription = "Tests giving items from Craw Summons location",
        Revision = 2026042000,
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Shellwood_08c, PrimitiveGateNames.left1);
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Craw_Summons)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Flea)!));
    }

    protected override void OnEnterGame()
    {
        base.OnEnterGame();
        
        // Act 3
        PlayerDataAccess.blackThreadWorld = true;
        PlayerDataAccess.act3_enclaveWakeSceneCompleted = true;
        PlayerDataAccess.act3_wokeUp = true;
        
        // Craw summons precondition
        PlayerDataAccess.hitCrowCourtSwitch = true;
        QuestUtil.SetCompleted(Quests.Black_Thread_Pt1_Shamans);
        PlayerData.instance.AddGeo(2000);
    }
}