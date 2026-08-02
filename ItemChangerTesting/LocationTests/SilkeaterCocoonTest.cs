using Benchwarp.Data;
using ItemChanger.Silksong.RawData;

namespace ItemChangerTesting.LocationTests;

internal class SilkeaterCocoonTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Silkeater Cocoon Location",
        MenuDescription = "Tests giving items at Silkeater Cocoons",
        Revision = 2026072400
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Coral_37, PrimitiveGateNames.left1);
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Silkeater__Blasted_Steps)!.Wrap().Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!));
    }
}
