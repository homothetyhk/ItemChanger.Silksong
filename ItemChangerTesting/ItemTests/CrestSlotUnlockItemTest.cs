using ItemChanger.Silksong.Modules;

namespace ItemChangerTesting.ItemTests;

internal class CrestSlotUnlockItemTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.ItemTests,
        MenuName = "Crest Slot Unlock Items",
        MenuDescription = "Tests various crest slot unlock items and previewing locked crests.",
        Revision = 2026070800
    };

    public override void Setup(TestArgs args)
    {
        CommonLocations.StartInBonebottom();

        LockedCrestPreviewModule module = Profile.Modules.GetOrAdd(new LockedCrestPreviewModule());

        module.SetCrestVisible("Reaper");
        module.SetCrestVisible("Toolmaster");
    }
}
