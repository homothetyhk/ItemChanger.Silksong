using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.ShinyTests;

internal class RosaryCannonTest : AbstractShinyPlacementTest<RosaryCannonTest>
{
    protected override string MenuName => "Rosary Cannon";

    protected override string MenuDescription => "Test the Rosary Cannon location.";

    protected override int Revision => 2026100500;

    protected override void OnEnterGame() => BaseItemList.Rosary_Cannon.GiveImmediate(new());

    protected override IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> LocationTests =>
        [(new CoordinateStartDef { SceneName = SceneNames.Hang_06_bank, X = 45, Y = 14, RespawnFacingRight = false }, [LocationNames.Rosary_Cannon])];
}
