using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;
using PrepatcherPlugin;

namespace ItemChangerTesting.ShinyTests;

internal class ClawMirrorTest : AbstractShinyPlacementTest<ClawMirrorTest>
{
    protected override string MenuName => "Claw Mirror";

    protected override string MenuDescription => "Test drops from the first Trobbio fight.";

    protected override int Revision => 2026093000;

    protected override void OnEnterGame()
    {
        base.OnEnterGame();
        PlayerDataAccess.encounteredTrobbio = true;
        PlayerDataAccess.defeatedTrobbio = false;
        BaseItemList.Claw_Mirror.GiveImmediate(new());
    }

    protected override IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> LocationTests =>
        [(new CoordinateStartDef() { SceneName = SceneNames.Library_13, X = 54.30f, Y = 14.57f, MapZone = GlobalEnums.MapZone.NONE }, [LocationNames.Claw_Mirror])];
}
