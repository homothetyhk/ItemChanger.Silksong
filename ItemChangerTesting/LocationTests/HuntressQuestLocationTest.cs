using ItemChanger.Silksong.RawData;

namespace ItemChangerTesting.LocationTests;

internal class HuntressQuestLocationTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Longclaw Location",
        MenuDescription = "Tests giving items from the Huntress and Runt in Putrified Ducts.",
        Revision = 2026092600,
    };

    public override void Setup(TestArgs args)
    {
        StartAt(Benchwarp.Data.BaseBenchList.Huntress);
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Longclaw)!.Wrap()
            .WithVariousItems().WithAllPersistent());
    }

    public override IEnumerable<(string, Action)> TestMethods()
    {
        yield return ("Start Act 3", StartAct3);
        yield return ("Collect Organs", () => QuestUtil.SetReadyToComplete(Quests.Huntress_Quest));
        yield return ("Mark Completed", () => QuestUtil.SetCompleted(Quests.Huntress_Quest));
    }
}
