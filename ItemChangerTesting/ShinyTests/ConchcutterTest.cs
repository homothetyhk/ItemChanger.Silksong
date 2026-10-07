using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.ShinyTests;

internal class ConchcutterTest : AbstractShinyPlacementTest<ConchcutterTest>
{
    protected override string MenuName => "Conchcutter";

    protected override string MenuDescription => "Test the Conchcutter location.";

    protected override int Revision => 2026100600;

    protected override void OnEnterGame() => BaseItemList.Conchcutter.GiveImmediate(new());

    protected override IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> LocationTests =>
        [(new CoordinateStartDef { SceneName = SceneNames.Coral_Tower_01, X = 77, Y = 8, RespawnFacingRight = true }, [LocationNames.Conchcutter])];
}
