using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.RawData;

namespace ItemChangerTesting.ItemTests;

//tests QuillItem for the quills, PDBoolItem for cradle map and bench pin and citadel flea findings, BilewaterMapSprite for bilewater map, MarkerItem for bronze marker, 
internal class MapItemsTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.ItemTests,
        MenuName = "Map Items",
        MenuDescription = "Tests various map items. (quills, cradle map, bilewater map, bronze marker, bench pin, citadel flea findings)",
        Revision = 2026031400,
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
        Profile.AddPlacement(new CoordinateLocation
        {
            Name = "White Quill",
            SceneName = SceneNames.Tut_02,
            X = 130.6f,
            Y = 31.57f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
        }.Wrap().Add(Finder.GetItem(ItemNames.Quill__White)!));
        Profile.AddPlacement(new CoordinateLocation
        {
            Name = "Red Quill",
            SceneName = SceneNames.Tut_02,
            X = 131.6f,
            Y = 31.57f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
        }.Wrap().Add(Finder.GetItem(ItemNames.Quill__Red)!));
        Profile.AddPlacement(new CoordinateLocation
        {
            Name = "Purple Quill",
            SceneName = SceneNames.Tut_02,
            X = 132.6f,
            Y = 31.57f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
        }.Wrap().Add(Finder.GetItem(ItemNames.Quill__Purple)!));
        Profile.AddPlacement(new CoordinateLocation
        {
            Name = "Cradle Map",
            SceneName = SceneNames.Tut_02,
            X = 134.6f,
            Y = 31.57f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
        }.Wrap().Add(Finder.GetItem(ItemNames.Cradle_Map)!));
        Profile.AddPlacement(new CoordinateLocation
        {
            Name = "Bilewater Map",
            SceneName = SceneNames.Tut_02,
            X = 136.6f,
            Y = 31.57f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
        }.Wrap().Add(Finder.GetItem(ItemNames.Bilewater_Map)!));
        Profile.AddPlacement(new CoordinateLocation
        {
            Name = "Bronze Marker",
            SceneName = SceneNames.Tut_02,
            X = 138.6f,
            Y = 31.57f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
        }.Wrap().Add(Finder.GetItem(ItemNames.Bronze_Marker)!));
        Profile.AddPlacement(new CoordinateLocation
        {
            Name = "Bench Pin",
            SceneName = SceneNames.Tut_02,
            X = 140.6f,
            Y = 31.57f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
        }.Wrap().Add(Finder.GetItem(ItemNames.Bench_Pins)!));
        Profile.AddPlacement(new CoordinateLocation
        {
            Name = "Citadel Flea Findings",
            SceneName = SceneNames.Tut_02,
            X = 142.6f,
            Y = 31.57f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
        }.Wrap().Add(Finder.GetItem(ItemNames.Flea_Findings__The_Citadel)!));
    }
}
