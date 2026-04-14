using Benchwarp.Data;
using ItemChanger.Placements;
using ItemChanger.Silksong.RawData;

namespace ItemChangerTesting.LocationTests;

internal class GreyrootTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Greyroot",
        MenuDescription = "Tests giving items from each Greyroot location",
        Revision = 2026041400,
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Room_Witch, PrimitiveGateNames.left1);
        Placement start = Finder.GetLocation(LocationNames.Start)!.Wrap();
        for (int i = 0; i < 6; i++)
        {
            start.Add(Finder.GetItem(ItemNames.Pollip_Heart)!);
        }
        Profile.AddPlacement(start);
        Profile.AddPlacement(
            Finder.GetLocation(LocationNames.Pollip_Pouch)!.Wrap()
                .Add(Finder.GetItem(ItemNames.Simple_Key)!));
    }
}