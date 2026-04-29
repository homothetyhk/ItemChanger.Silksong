using ItemChanger.Silksong.RawData;
using PrepatcherPlugin;

namespace ItemChangerTesting.LocationTests;

internal class NeedleStrikeLocationTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Needle Strike",
        MenuDescription = "Tests placing items at Pinstress in the Blasted Steps.",
        Revision = 2026041500,
    };

    public override void Setup(TestArgs args)
    {
        StartAt(Benchwarp.Data.BaseBenchList.Pinstress);
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Needle_Strike)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!));
        // TODO: Test persistent items when Pinstress goes away.
    }

    public override IEnumerable<(string, Action)> TestMethods() => [("Pinstress Leave", () => PlayerDataAccess.pinstressQuestReady = true)];
}
