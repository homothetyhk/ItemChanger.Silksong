using ItemChanger.Silksong.RawData;
using PrepatcherPlugin;

namespace ItemChangerTesting.LocationTests;

internal class MapMachineTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Map Machine Location",
        MenuDescription = "Tests putting items in a Citadel map machine.",
        Revision = 2026041700,
    };

    protected override void OnEnterGame()
    {
        PlayerDataAccess.geo += 1000;
        PlayerDataAccess.hasDash = true;
    }

    public override void Setup(TestArgs args)
    {
        StartAt(Benchwarp.Data.BaseBenchList.HighHallsVentrica);
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Map__High_Halls)!.Wrap().Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!));
    }
}
