using Benchwarp.Data;
using ItemChanger;
using ItemChanger.Locations;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.Tags;
using static ItemChanger.Silksong.Containers.ChestContainer;

namespace ItemChangerTesting.LocationTests;

internal class MultiItemChestTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Multi-item chest",
        MenuDescription = "Tests giving various items from a spawned chest in Tut_02",
        Revision = 2026032300,
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
        Profile.AddPlacement(new CoordinateLocation
        {
            Name = "Default Chest",
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

        Profile.AddPlacement(new CoordinateLocation
        {
            Name = "Bone Chest",
            SceneName = SceneNames.Tut_02,
            X = 129.6f,
            Y = 31.57f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
            ForceDefaultContainer = false,
        }.Wrap()
        .WithTag(new ChestControlTag { Info = new() { ChestType = ChestType.Bone } })
         .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!)
         .Add(Finder.GetItem(ItemNames.Everbloom)!)
         .Add(Finder.GetItem(ItemNames.Pale_Oil)!));

        Profile.AddPlacement(new CoordinateLocation
        {
            Name = "Mossy Bone Chest",
            SceneName = SceneNames.Tut_02,
            X = 125.6f,
            Y = 31.57f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
            ForceDefaultContainer = false,
        }.Wrap()
        .WithTag(new ChestControlTag { Info = new() { ChestType = ChestType.MossyBone } })
         .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!)
         .Add(Finder.GetItem(ItemNames.Everbloom)!)
         .Add(Finder.GetItem(ItemNames.Pale_Oil)!));

        Profile.AddPlacement(new CoordinateLocation
        {
            Name = "Ant Chest",
            SceneName = SceneNames.Tut_02,
            X = 121.6f,
            Y = 31.57f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
            ForceDefaultContainer = false,
        }.Wrap()
        .WithTag(new ChestControlTag { Info = new() { ChestType = ChestType.Ant } })
         .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!)
         .Add(Finder.GetItem(ItemNames.Everbloom)!)
         .Add(Finder.GetItem(ItemNames.Pale_Oil)!));

        Profile.AddPlacement(new CoordinateLocation
        {
            Name = "Docks Chest",
            SceneName = SceneNames.Tut_02,
            X = 117.6f,
            Y = 31.57f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
            ForceDefaultContainer = false,
        }.Wrap()
        .WithTag(new ChestControlTag { Info = new() { ChestType = ChestType.Docks } })
         .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!)
         .Add(Finder.GetItem(ItemNames.Everbloom)!)
         .Add(Finder.GetItem(ItemNames.Pale_Oil)!));

        Profile.AddPlacement(new CoordinateLocation
        {
            Name = "Pilgrim Chest",
            SceneName = SceneNames.Tut_02,
            X = 113.6f,
            Y = 31.57f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
            ForceDefaultContainer = false,
        }.Wrap()
        .WithTag(new ChestControlTag { Info = new() { ChestType = ChestType.Pilgrim } })
         .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!)
         .Add(Finder.GetItem(ItemNames.Everbloom)!)
         .Add(Finder.GetItem(ItemNames.Pale_Oil)!));
    }
}
