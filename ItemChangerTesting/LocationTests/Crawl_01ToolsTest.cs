using Benchwarp.Data;
using ItemChanger.Silksong.RawData;

namespace ItemChangerTesting.LocationTests;

internal class Crawl_01ToolsTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Crawl_01 Tools",
        MenuDescription = "Tests modifying Dead_Bug_s_Purse and Shell_Satchel in-place to give Surgeon's_Key.",
        Revision = 2026032200,
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Crawl_01, PrimitiveGateNames.right1);
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Dead_Bug_s_Purse)!
            .Wrap().Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!));
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Shell_Satchel)!//seems to not spawn when running test ingame; might require running the test with steel soul
            .Wrap().Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!));
    }
}
