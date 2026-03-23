using Benchwarp.Data;
using ItemChanger;
using ItemChanger.Locations;
using ItemChanger.Silksong.RawData;

namespace ItemChangerTesting.LocationTests;

internal class MultiItemChestTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Multi-item chest",
        MenuDescription = "Tests giving various items from a spawned chest in Tut_02",
        Revision = 2026030400,
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
        Profile.AddPlacement(new CoordinateLocation
        {
            Name = "Multi-item chest test",
            SceneName = SceneNames.Tut_02,
            X = 133.6f,
            Y = 31.57f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
            ForceDefaultContainer = false,
        }.Wrap()
         .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!)
         .Add(Finder.GetItem(ItemNames.Everbloom)!)
         .Add(Finder.GetItem(ItemNames.Pale_Oil)!));
    }
}
