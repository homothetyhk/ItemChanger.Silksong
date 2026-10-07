using Benchwarp.Data;
using ItemChanger;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.Modules;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

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

    public override IEnumerable<(string, Action)> TestMethods()
    {
        yield return ("Accept Sentinel Quest", () => QuestManager.GetQuest(Quests.Song_Knight).SetAccepted());
    }
}
