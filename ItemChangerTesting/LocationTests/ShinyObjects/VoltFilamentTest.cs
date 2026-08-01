using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.LocationTests;

internal class VoltFilamentTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Modified Volt_Filament",
        MenuDescription = "Tests modifying Volt_Filament in-place to give Surgeon's_Key and Flea. (requires fighting voltvyrm)",
        Revision = 2026041300,
    };

    public override void Setup(TestArgs args)
    {
        StartAt(new CoordinateStartDef()
        {
            SceneName = SceneNames.Coral_29,
            X = 190.00f,
            Y = 24.57f,
            MapZone = GlobalEnums.MapZone.NONE
        });
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Volt_Filament)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!)
            .Add(Finder.GetItem(ItemNames.Flea)!));
    }

    protected override void OnEnterGame()
    {
        base.OnEnterGame();
        PlayerData.instance.defeatedZapCoreEnemy = false;
    }
}
