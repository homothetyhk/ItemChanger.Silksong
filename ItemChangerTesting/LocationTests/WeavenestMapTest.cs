using ItemChanger.Silksong.RawData;
using PrepatcherPlugin;

namespace ItemChangerTesting.LocationTests;

internal class WeavenestMapTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Weavenest Map Location",
        MenuDescription = "Tests placing items at the Weavenest map tablets",
        Revision = 2026052000,
    };

    protected override void OnEnterGame()
    {
        PlayerDataAccess.HasAbyssMap = true;
        PlayerDataAccess.hasDash = true;
        PlayerDataAccess.hasWalljump = true;
        PlayerDataAccess.HasWeavehomeMap = true;
    }

    public override void Setup(TestArgs args)
    {
        StartAt(Benchwarp.Data.BaseBenchList.VoidTendrils);
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Map__Abyss)!.Wrap().Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!));
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Map__Weavenest_Atla)!.Wrap().Add(Finder.GetItem(ItemNames.White_Key)!));
    }
}
