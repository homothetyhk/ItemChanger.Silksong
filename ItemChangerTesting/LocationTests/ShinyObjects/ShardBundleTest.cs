using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.LocationTests;

internal class ShardBundleTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Modified Shard_Bundle__Deep_Docks_South",
        MenuDescription = "Tests modifying Shard_Bundle__Deep_Docks_South in-place to give Surgeon's_Key and Flea.",
        Revision = 2026040400,
    };

    public override void Setup(TestArgs args)
    {
        StartAt(new CoordinateStartDef()
        {
            SceneName = SceneNames.Dock_02,
            X = 61.54f,
            Y = 6.65f,
            MapZone = GlobalEnums.MapZone.NONE
        });
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Shard_Bundle__Deep_Docks_South)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!)
            .Add(Finder.GetItem(ItemNames.Flea)!));
    }
}
