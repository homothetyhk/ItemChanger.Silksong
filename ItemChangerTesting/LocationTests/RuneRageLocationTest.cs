using Benchwarp.Data;
using ItemChanger;
using ItemChanger.Silksong.RawData;

namespace ItemChangerTesting.LocationTests;

internal class RuneRageLocationTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Rune Rage",
        MenuDescription = "Test items at First Sinner",
        Revision = 2026041100
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Slab_10b, "left1");
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Rune_Rage)!.Wrap().Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!));
    }
}
