using ItemChanger.Silksong.Modules;

namespace ItemChangerTesting.ModuleTests;

internal class LockedCrestPreviewTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.ModuleTests,
        MenuName = "Locked Crests Preview",
        MenuDescription = "Tests previewing locked crests.",
        Revision = 2026070600
    };

    public override void Setup(TestArgs args)
    {
        CommonLocations.StartInBonebottom();

        LockedCrestPreviewModule module = Profile.Modules.GetOrAdd(new LockedCrestPreviewModule());

        module.SetCrestVisible("Reaper");
        module.SetCrestVisible("Toolmaster");
    }
}
