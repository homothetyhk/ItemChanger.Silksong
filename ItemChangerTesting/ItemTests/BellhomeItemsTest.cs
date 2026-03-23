using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.RawData;

namespace ItemChangerTesting.ItemTests;

//tests PDIntItem for lacquers, PDBoolItem for desk, CollectableItem for crawbell
internal class BellhomeItemsTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.ItemTests,
        MenuName = "Bellhome Items",
        MenuDescription = "Tests various Bellhome items. (red/blue/chrome lacquers, desk, crawbell)",
        Revision = 2026031400,
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
        Profile.AddPlacement(new CoordinateLocation
        {
            Name = "Red Lacquer",
            SceneName = SceneNames.Tut_02,
            X = 130.6f,
            Y = 31.57f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
        }.Wrap().Add(Finder.GetItem(ItemNames.Bell_Lacquer__Red)!));
        Profile.AddPlacement(new CoordinateLocation
        {
            Name = "Blue Lacquer",
            SceneName = SceneNames.Tut_02,
            X = 131.6f,
            Y = 31.57f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
        }.Wrap().Add(Finder.GetItem(ItemNames.Bell_Lacquer__Blue)!));
        Profile.AddPlacement(new CoordinateLocation
        {
            Name = "Chrome Lacquer",
            SceneName = SceneNames.Tut_02,
            X = 132.6f,
            Y = 31.57f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
        }.Wrap().Add(Finder.GetItem(ItemNames.Bell_Lacquer__Chrome)!));
        Profile.AddPlacement(new CoordinateLocation
        {
            Name = "Desk",
            SceneName = SceneNames.Tut_02,
            X = 134.6f,
            Y = 31.57f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
        }.Wrap().Add(Finder.GetItem(ItemNames.Desk)!));
        Profile.AddPlacement(new CoordinateLocation
        {
            Name = "Crawbell",
            SceneName = SceneNames.Tut_02,
            X = 136.6f,
            Y = 31.57f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
        }.Wrap().Add(Finder.GetItem(ItemNames.Crawbell)!));
       
    }
}
