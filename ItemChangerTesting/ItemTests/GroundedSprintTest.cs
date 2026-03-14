using Benchwarp.Data;
using ItemChanger;
using ItemChanger.Locations;
using ItemChanger.Silksong.RawData;

namespace ItemChangerTesting.ItemTests;

internal class GroundedSprintTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.ItemTests,
        MenuName = "Grounded Sprint",
        MenuDescription = "Tests Grounded Sprint → Swift Step chain progression from coordinate shinies.",
        Revision = 2026030900,
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
        Profile.AddPlacement(new CoordinateLocation
        {
            Name = "Grounded Sprint (1st pickup)",
            SceneName = SceneNames.Tut_02,
            X = 130.6f,
            Y = 31.57f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
        }.Wrap().Add(Finder.GetItem(ItemNames.Grounded_Sprint)!));
        Profile.AddPlacement(new CoordinateLocation
        {
            Name = "Swift Step via chain (2nd pickup)",
            SceneName = SceneNames.Tut_02,
            X = 136.6f,
            Y = 31.57f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
        }.Wrap().Add(Finder.GetItem(ItemNames.Grounded_Sprint)!));
    }
}
