using ItemChanger.Silksong.RawData;

namespace ItemChangerTesting.LocationTests;

internal class DriftersCloakLocationTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Drifter's Cloak Location",
        MenuDescription = "Tests giving items from the Seamstress in Far Fields.",
        Revision = 2026031400,
    };

    public override void Setup(TestArgs args)
    {
        StartAt(Benchwarp.Data.BaseBenchList.Seamstress);
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Drifter_s_Cloak)!.Wrap().Add(
            Finder.GetItem(ItemNames.Surgeon_s_Key)!));
    }

    public override IEnumerable<(string, Action)> TestMethods()
    {
        yield return ("Collect Spines", () => QuestUtil.SetReadyToComplete(Quests.Brolly_Get));
        yield return ("Mark Completed", () => QuestUtil.SetCompleted(Quests.Brolly_Get));
    }
}
