using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;
using PrepatcherPlugin;

namespace ItemChangerTesting.ShinyTests;

internal class VoltFilamentTest : AbstractShinyPlacementTest<VoltFilamentTest>
{
    protected override string MenuName => "Volt Filament";

    protected override string MenuDescription => "Test drops from the Voltvyrm.";

    protected override int Revision => 2026093000;

    protected override void OnEnterGame()
    {
        base.OnEnterGame();
        PlayerDataAccess.defeatedZapCoreEnemy = false;
        BaseItemList.Volt_Filament.GiveImmediate(new());
    }

    protected override IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> LocationTests =>
        [(new CoordinateStartDef { SceneName = SceneNames.Coral_29, X = 190.00f, Y = 24.57f, MapZone = GlobalEnums.MapZone.CORAL_CAVERNS }, [LocationNames.Volt_Filament])];
}
