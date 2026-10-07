using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.ShinyTests;

internal class ChoralCommandmentSurgeonTest : AbstractShinyPlacementTest<ChoralCommandmentSurgeonTest>
{
    protected override string MenuName => "Choral Commandment - Surgeon";

    protected override string MenuDescription => "Tests the Choral Commandment - Surgeon location.";

    protected override int Revision => 2026100600;

    protected override void OnEnterGame() => BaseItemList.Choral_Commandment__Surgeon.GiveImmediate(new());

    protected override IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> LocationTests =>
        [(new CoordinateStartDef { SceneName = SceneNames.Ward_02b, X = 56, Y = 4, RespawnFacingRight = false }, [LocationNames.Choral_Commandment__Surgeon])];
}
