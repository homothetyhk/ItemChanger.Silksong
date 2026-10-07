using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.ShinyTests;

internal class ThreefoldPinTest : AbstractShinyPlacementTest<ThreefoldPinTest>
{
    protected override string MenuName => "Threefold Pin";

    protected override string MenuDescription => "Test mthe Threefold Pin location.";

    protected override int Revision => 2026100400;

    protected override void OnEnterGame() => BaseItemList.Threefold_Pin.GiveImmediate(new());

    protected override IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> LocationTests =>
        [(new CoordinateStartDef { SceneName = SceneNames.Greymoor_15b, X = 181.1f, Y = 100, RespawnFacingRight = true }, [LocationNames.Threefold_Pin])];
}
