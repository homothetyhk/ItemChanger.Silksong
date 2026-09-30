using Benchwarp.Data;
using ItemChanger;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;
using PrepatcherPlugin;

namespace ItemChangerTesting.LocationTests;

internal class TrailsEndLocationTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Trail's End Location",
        MenuDescription = "Tests giving various items in place of Throwing Ring.",
        Revision = 2026093000
    };

    public override void Setup(TestArgs args)
    {
        StartAt(new CoordinateStartDef()
        {
            SceneName = SceneNames.Shadow_24,
            X = 277.66f,
            Y = 8.57f,
            MapZone = GlobalEnums.MapZone.SWAMP
        });
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Throwing_Ring)!.Wrap()
            .WithVariousItems().WithAllPersistent());
    }

    protected override void OnEnterGame()
    {
        base.OnEnterGame();

        PlayerDataAccess.hasNeedolin = true;
        QuestManager.GetQuest(Quests.Shakra_Final_Quest).SetAccepted();
    }
}
