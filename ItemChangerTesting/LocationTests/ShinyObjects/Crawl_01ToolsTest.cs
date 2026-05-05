using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.LocationTests;

internal class Crawl_01ToolsTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Modify Crawl_01 Tools",
        MenuDescription = "Tests modifying Dead_Bug_s_Purse and Shell_Satchel in-place to give Surgeon's_Key and Flea.",
        Revision = 2026032200,
    };

    public override void Setup(TestArgs args)
    {
        StartAt(new CoordinateStartDef()
        {
            SceneName = SceneNames.Crawl_01,
            X = 57.32f,
            Y = 85.57f,
            MapZone = GlobalEnums.MapZone.NONE
        });
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Dead_Bug_s_Purse)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!)
            .Add(Finder.GetItem(ItemNames.Flea)!));
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Shell_Satchel)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!)
            .Add(Finder.GetItem(ItemNames.Flea)!));
    }
}
