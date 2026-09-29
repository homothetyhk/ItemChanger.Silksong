using Benchwarp.Data;
using ItemChanger;
using ItemChanger.Locations;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Items;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.Tags;
using ItemChanger.Tags;
using static ItemChanger.Silksong.Containers.ChestContainer;

namespace ItemChangerTesting.LocationTests;

internal class MultiPlacementChestTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Multi-placement chest",
        MenuDescription = "Tests giving various placements from a single spawned chest in Tut_02",
        Revision = 2026092900,
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);

        Profile.AddPlacement(new CoalescingCoordinateLocation
        {
            Name = "Default Chest",
            SceneName = SceneNames.Tut_02,
            X = 133.6f,
            Y = 31.57f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
            ForceDefaultContainer = false,
            ContainerType = "Chest",
        }.Wrap()
         .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!)
         .Add(Finder.GetItem(ItemNames.Everbloom)!)
         .Add(Finder.GetItem(ItemNames.Pale_Oil)!));

        Profile.AddPlacement(new CoalescingCoordinateLocation
        {
            Name = "Default Chest Persistent",
            SceneName = SceneNames.Tut_02,
            X = 133.6f,
            Y = 31.57f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
            ForceDefaultContainer = false,
            ContainerType = "Chest",
        }.Wrap()
         .Add(RosariesItem.MakeRosariesItem(200))
         .WithAllPersistent());

        Profile.AddPlacement(new CoalescingCoordinateLocation
        {
            Name = "Default Chest Flea",
            SceneName = SceneNames.Tut_02,
            X = 133.6f,
            Y = 31.57f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
            ForceDefaultContainer = false,
            ContainerType = "Chest",
        }.Wrap()
         .Add(Finder.GetItem(ItemNames.Flea)!));

        // Won't stack because the coordinates aren't identical
        Profile.AddPlacement(new CoalescingCoordinateLocation
        {
            Name = "Default Chest Off By One",
            SceneName = SceneNames.Tut_02,
            X = 132.6f,
            Y = 31.57f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
            ForceDefaultContainer = false,
            ContainerType = "Chest",
        }.Wrap()
         .Add(Finder.GetItem(ItemNames.Mask_Shard)!));
    }
}
