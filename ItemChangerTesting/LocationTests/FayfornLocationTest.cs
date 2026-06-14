using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.LocationTests;

internal class FayfornLocationTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Fayforn (Faydown Cloak location)",
        MenuDescription = "Tests giving Faydown_Cloak from the Fayforn encounter in Peak_08b",
        Revision = 2026041400,
    };

    public override void Setup(TestArgs args)
    {
        StartAt(new CoordinateStartDef() { SceneName = "Peak_08b", X = 278.94f, Y = 102.58f, MapZone = GlobalEnums.MapZone.NONE });
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Faydown_Cloak)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!));
    }

    protected override void OnEnterGame()
    {
        base.OnEnterGame();

        PlayerData.instance.hasDash = true;
        PlayerData.instance.hasWalljump = true;
        PlayerData.instance.hasNeedolin = true;
        PlayerData.instance.HasPeakMap = true;
    }
}
