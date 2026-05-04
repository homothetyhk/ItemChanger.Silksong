using Benchwarp.Data;
using ItemChanger.Placements;
using ItemChanger.Silksong.RawData;

namespace ItemChangerTesting.LocationTests;

internal class MossDruidTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Moss Druid",
        MenuDescription = "Tests giving items from each of the Moss Druid locations",
        Revision = 2026040900,
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Mosstown_02c, PrimitiveGateNames.left2);
        Placement start = Finder.GetLocation(LocationNames.Start)!.Wrap();
        for (int i = 0; i < 7; i++)
        {
            start.Add(Finder.GetItem(ItemNames.Mossberry)!);
        }
        Profile.AddPlacement(start);
        Profile.AddPlacement(
            Finder.GetLocation(LocationNames.Druid_s_Eye)!.Wrap()
                .Add(Finder.GetItem(ItemNames.Arcane_Egg)!));
        Profile.AddPlacement(
            Finder.GetLocation(LocationNames.Druid_s_Eyes)!.Wrap()
                .Add(Finder.GetItem(ItemNames.Simple_Key)!));
        Profile.AddPlacement(
            Finder.GetLocation(LocationNames.Moss_Druid_Payout_1)!.Wrap()
                .Add(Finder.GetItem(ItemNames.Frayed_Rosary_String)!));
        Profile.AddPlacement(
            Finder.GetLocation(LocationNames.Moss_Druid_Payout_2)!.Wrap()
                .Add(Finder.GetItem(ItemNames.Rosary_String)!));
        Profile.AddPlacement(
            Finder.GetLocation(LocationNames.Moss_Druid_Payout_3)!.Wrap()
                .Add(Finder.GetItem(ItemNames.Rosary_Necklace)!));
        Profile.AddPlacement(
            Finder.GetLocation(LocationNames.Mossberry_Stew)!.Wrap()
                .Add(Finder.GetItem(ItemNames.Heavy_Rosary_Necklace)!));
    }

    protected override void OnEnterGame()
    {
        base.OnEnterGame();
        QuestUtil.SetAccepted(Quests.Great_Gourmand);
    }
}