using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.RawData;

namespace ItemChangerTesting.ModuleTests;

internal class GroundedSprintTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.ModuleTests,
        MenuName = "Grounded Sprint",
        MenuDescription = "Pickup grants Grounded Sprint. While the dash button is held, ground speed should match Swift Step's dash speed and air speed should fall to a walk. Picking up Swift Step (placed nearby) restores normal behavior.",
        Revision = 2026042900
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
        Profile.AddPlacement(new CoordinateLocation
        {
            Name = "GroundedSprintPickup",
            SceneName = SceneNames.Tut_02,
            X = 133.6f,
            Y = 31.57f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
        }.Wrap().Add(Finder.GetItem(ItemNames.Grounded_Sprint)!));
        Profile.AddPlacement(new CoordinateLocation
        {
            Name = "SwiftStepPickup",
            SceneName = SceneNames.Tut_02,
            X = 136.6f,
            Y = 31.57f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
        }.Wrap().Add(Finder.GetItem(ItemNames.Swift_Step)!));
    }
}
