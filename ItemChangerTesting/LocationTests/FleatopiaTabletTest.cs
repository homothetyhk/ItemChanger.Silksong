using Benchwarp.Data;
using ItemChanger;
using ItemChanger.Items;
using ItemChanger.Locations;
using ItemChanger.Serialization;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.Tags;
using ItemChanger.Silksong.UIDefs;
using ItemChanger.Tags;
using PrepatcherPlugin;

namespace ItemChangerTesting.LocationTests;

internal class FleatopiaTabletTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Fleatopia tablet",
        MenuDescription = "Tests the fleatopia tablet location, with shinies available to toggle the state",
        Revision = 2026041200,
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Aqueduct_05, PrimitiveGateNames.left1);

        Profile.AddPlacement(Finder.GetLocation(LocationNames.Lore_Tablet__Fleatopia_Weaver_Harp)!.Wrap()
            .WithDebugItem(persistence: ItemChanger.Enums.Persistence.Persistent));

        Profile.AddPlacement(Finder.GetLocation(LocationNames.Start)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Swift_Step)!)
            .Add(Finder.GetItem(ItemNames.Needolin)!)
            .Add(Finder.GetItem(ItemNames.Cling_Grip)!)
            .Add(Finder.GetItem(ItemNames.Clawline)!)
            .Add(Finder.GetItem(ItemNames.Faydown_Cloak)!)
            );

        for (int i = 0; i < 4; i++)
        {
            Profile.AddPlacement(new CoordinateLocation()
            {
                SceneName = SceneNames.Aqueduct_03,
                X = 240 + 5 * i,
                Y = 14.83f,
                Name = $"Fleatopia control state {i}",
                Managed = false
            }.Wrap()
            .WithTag(new PlacementItemsHintBoxTag())
            .Add(new SetFleatopiaStateItem()
            {
                Name = $"Fleatopia state {i}",
                Num = i,
                UIDef = new MsgUIDef() { Name = new BoxedString{ Value = $"Fleatopia state {i}" }, Sprite = new EmptySprite() }
            }.WithTag(new PersistentItemTag() { Persistence = ItemChanger.Enums.Persistence.Persistent })));
        }
    }

    private class SetFleatopiaStateItem : Item
    {
        public int Num { get; set; }

        public override void GiveImmediate(GiveInfo info)
        {
            PlayerDataAccess.CaravanTroupeLocation = Num == 0 ? GlobalEnums.CaravanTroupeLocations.Greymoor : GlobalEnums.CaravanTroupeLocations.Aqueduct;
            PlayerDataAccess.FleaGamesCanStart = Num >= 2;
            PlayerDataAccess.FleaGamesStarted = Num == 3;
        }
    }
}
