using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.ShinyTests;

internal class KeyOfApostateTest : AbstractShinyPlacementTest<KeyOfApostateTest>
{
    protected override string MenuName => "Key of Apostate";

    protected override string MenuDescription => "Test the Key of Apostate location.";

    protected override int Revision => 2026100400;

    protected override void OnEnterGame() => BaseItemList.Key_of_Apostate.GiveImmediate(new());

    protected override IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> LocationTests =>
        [(new CoordinateStartDef { SceneName = SceneNames.Aqueduct_04, X = 13.5f, Y = 39, RespawnFacingRight = false }, [LocationNames.Key_of_Apostate])];
}
