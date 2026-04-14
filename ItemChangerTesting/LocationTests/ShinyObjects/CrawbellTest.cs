using Benchwarp.Data;
using ItemChanger.Silksong.RawData;

namespace ItemChangerTesting.LocationTests;

internal class Crawbelltest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Modified Crawbell",
        MenuDescription = "Tests modifying Crawbell in-place to give Surgeon's_Key and Flea.",
        Revision = 2026041000,
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Room_CrowCourt_02, PrimitiveGateNames.top1);
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Crawbell)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!)
            .Add(Finder.GetItem(ItemNames.Flea)!));
    }
}
