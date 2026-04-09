using Benchwarp.Data;
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
        StartNear(SceneNames.Mosstown_02c, PrimitiveGateNames.left1);
        Profile.AddPlacement(
            Finder.GetLocation(LocationNames.Druid_s_Eye)!.Wrap()
                .Add(Finder.GetItem(ItemNames.Arcane_Egg)!));
    }
}