using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.ShinyTests;

internal class WeavelightTest : AbstractShinyPlacementTest<WeavelightTest>
{
    protected override string MenuName => "Weavelight";

    protected override string MenuDescription => "Test the Weavelight location.";

    protected override int Revision => 2026100600;

    protected override void OnEnterGame() => BaseItemList.Weavelight.GiveImmediate(new());

    protected override IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> LocationTests =>
        [(new CoordinateStartDef { SceneName = SceneNames.Weave_03, X = 32, Y = 21, RespawnFacingRight = false }, [LocationNames.Weavelight])];
}
