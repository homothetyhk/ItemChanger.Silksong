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
    }
}