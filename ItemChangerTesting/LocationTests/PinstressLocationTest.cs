using Benchwarp.Data;
using ItemChanger;
using ItemChanger.Silksong.RawData;

namespace ItemChangerTesting.LocationTests;

internal class PinstressLocationTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Pinstress Location",
        MenuDescription = "Tests giving various items from the Pin Badge slot. Defeat Pinstress, then talk to her in Peak_07 to claim the reward.",
        Revision = 2026042900
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Peak_07, PrimitiveGateNames.left1);
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Pin_Badge)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!)
            .Add(Finder.GetItem(ItemNames.Flea)!));
    }
}
