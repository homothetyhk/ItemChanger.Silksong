using Benchwarp.Data;
using ItemChanger;
using ItemChanger.Items;
using ItemChanger.Silksong.RawData;
using ItemChanger.Tags;

namespace ItemChangerTesting.LocationTests;

internal class TabletLocationTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Tablet Location",
        MenuDescription = "Tests putting a debug item at each tablet location",
        Revision = 2026040900,
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Cradle_02b, PrimitiveGateNames.right1);

        foreach (string loc in Finder.LocationNames.Where(x => x.StartsWith("Lore_Tablet-")))
        {
            if (loc.Contains("Lore_Tablet-Cradle_Cage"))
            {
                continue;
            }

            Profile.AddPlacement(
                Finder
                .GetLocation(loc)!
                .Wrap()
                .WithDebugItem()
                );
        }

        Profile.AddPlacement(
            Finder.GetLocation(LocationNames.Lore_Tablet__Cradle_Cage_1)!
            .Wrap()
            .Add(new DebugItem()
            {
                Name = "Debug-Cradle1-1",
                UIDef = Finder.GetItem(ItemNames.Clawline)!.UIDef,
            }.WithTag(new PersistentItemTag() { Persistence = ItemChanger.Enums.Persistence.Persistent }))
            .Add(new DebugItem()
            {
                Name = "Debug-Cradle1-2",
                UIDef = Finder.GetItem(ItemNames.Sylphsong)!.UIDef,
            }.WithTag(new PersistentItemTag() { Persistence = ItemChanger.Enums.Persistence.Persistent }))
            );

        Profile.AddPlacement(
            Finder.GetLocation(LocationNames.Lore_Tablet__Cradle_Cage_2)!
            .Wrap()
            .Add(new DebugItem()
            {
                Name = "Debug-Cradle2-1",
                UIDef = Finder.GetItem(ItemNames.Cling_Grip)!.UIDef,
            }.WithTag(new PersistentItemTag() { Persistence = ItemChanger.Enums.Persistence.Persistent }))
            .Add(Finder.GetItem(ItemNames.Lore_Tablet__Abyss_Bottom_Left)!)
            );

        Profile.AddPlacement(
            Finder.GetLocation(LocationNames.Lore_Tablet__Cradle_Cage_3)!
            .Wrap()
            .WithDebugItem()
            .WithDebugItem(text: "Debug C3 Item 2")
            .Add(new DebugItem()
            {
                Name = "Debug-Cradle3-3",
                UIDef = Finder.GetItem(ItemNames.Cling_Grip)!.UIDef,
            })
            );
    }
}
