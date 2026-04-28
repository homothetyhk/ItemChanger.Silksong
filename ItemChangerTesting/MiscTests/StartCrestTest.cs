using Benchwarp.Data;
using ItemChanger.Silksong.Modules;
using ItemChanger.Silksong.RawData;

namespace ItemChangerTesting.MiscTests;

internal class StartCrestTest : Test
{
    public override TestMetadata GetMetadata()
    {
        return new()
        {
            Folder = TestFolder.MiscTests,
            MenuName = "Starting Crest",
            MenuDescription = "Test starting with Beast Crest",
            Revision = 2026042501,
        };
    }

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Bonetown, PrimitiveGateNames.right1);
        Profile.ApplyStartCrest(ItemNames.Crest_of_Beast);
    }
}
