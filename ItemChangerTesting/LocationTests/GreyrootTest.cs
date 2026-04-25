using Benchwarp.Data;
using ItemChanger;
using ItemChanger.Placements;
using ItemChanger.Enums;
using ItemChanger.Tags;
using ItemChanger.Items;
using ItemChanger.Silksong.RawData;

namespace ItemChangerTesting.LocationTests;

internal class GreyrootTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Greyroot",
        MenuDescription = "Tests giving items from each Greyroot location",
        Revision = 2026041400,
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Room_Witch, PrimitiveGateNames.left1);
        Placement start = Finder.GetLocation(LocationNames.Start)!.Wrap();
        start.Add(Finder.GetItem(ItemNames.Twisted_Bud)!);
        for (int i = 0; i < 6; i++)
        {
            start.Add(Finder.GetItem(ItemNames.Pollip_Heart)!);
        }
        Profile.AddPlacement(start);
        Profile.AddPlacement(
            Finder.GetLocation(LocationNames.Pollip_Pouch)!.Wrap()
                .Add(Finder.GetItem(ItemNames.Simple_Key)!
                    .WithTag(new PersistentItemTag { Persistence = Persistence.Persistent })));
        Profile.AddPlacement(
            Finder.GetLocation(LocationNames.Crest_of_Cursed_Witch)!.Wrap()
                .Add(Finder.GetItem(ItemNames.Twisted_Bud)!
                    .WithTag(new PersistentItemTag { Persistence = Persistence.Persistent })));
    }

    protected override void OnEnterGame()
    {
        base.OnEnterGame();
        QuestUtil.SetReadyToComplete(Quests.Shell_Flowers);
    }
}