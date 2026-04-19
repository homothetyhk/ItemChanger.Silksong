using Benchwarp.Data;
using ItemChanger.Silksong.Modules;
using PrepatcherPlugin;

namespace ItemChangerTesting.ModuleTests;

internal class SlabCaptureTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.ModuleTests,
        MenuName = "Slab Capture",
        MenuDescription = "Tests kidnapping sequence and wardenfly availability",
        Revision = 2026041900
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Greymoor_05, PrimitiveGateNames.right2);

        Modules.Add(new SlabKidnappingModule());
    }

    protected override void OnEnterGame()
    {
        PlayerDataAccess.hasDash = true;
        PlayerDataAccess.hasBrolly = true;
    }
}