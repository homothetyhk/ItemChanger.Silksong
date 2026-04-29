using Benchwarp.Data;
using ItemChanger.Silksong.RawData;

namespace ItemChangerTesting.LocationTests;

internal class VoltvesselsTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Modify Voltvessels and Materium",
        MenuDescription = "Tests modifying Voltvessels and Materium in-place to give Surgeon's_Key and Flea.",
        Revision = 2026041000,
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Arborium_07, PrimitiveGateNames.top1);
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Voltvessels)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!)
            .Add(Finder.GetItem(ItemNames.Flea)!));
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Materium)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!)
            .Add(Finder.GetItem(ItemNames.Flea)!));

    }
}
