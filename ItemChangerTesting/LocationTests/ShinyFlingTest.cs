using Benchwarp.Data;
using ItemChanger;
using ItemChanger.Locations;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Tags;

namespace ItemChangerTesting.LocationTests;

internal class ShinyFlingTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Shiny Fling",
        MenuDescription = "Tests configuring shiny fling.",
        Revision = 2026032500,
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
        Profile.AddPlacement(new CoordinateLocation
        {
            Name = "Float",
            SceneName = SceneNames.Tut_02,
            X = 133.6f,
            Y = 32f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
        }.Wrap().WithDebugItem()
        .WithTag(new ShinyControlTag 
        { 
            Info = new ShinyContainer.ShinyControlInfo
            {
                ShinyFling = ShinyContainer.ShinyFling.FloatInPlace,
                ShinyType = ShinyContainer.ShinyType.Normal,
            } 
        }));
        Profile.AddPlacement(new CoordinateLocation
        {
            Name = "Drop",
            SceneName = SceneNames.Tut_02,
            X = 133.6f,
            Y = 51.57f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
        }.Wrap().WithDebugItem()
        .WithTag(new ShinyControlTag
        {
            Info = new ShinyContainer.ShinyControlInfo
            {
                ShinyFling = ShinyContainer.ShinyFling.Drop,
                ShinyType = ShinyContainer.ShinyType.Instant,
            }
        }));
    }
}
