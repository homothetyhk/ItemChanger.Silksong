using Benchwarp.Data;
using ItemChanger.Silksong.RawData;

namespace ItemChangerTesting.LocationTests;

internal class ShardBundleTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Modified Shard_Bundle__Deep_Docks_South",
        MenuDescription = "Tests modifying Shard_Bundle__Deep_Docks_South in-place to give Surgeon's_Key.",
        Revision = 2026040400,
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Dock_02, PrimitiveGateNames.left1);
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Shard_Bundle__Deep_Docks_South)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!));
    }
}
