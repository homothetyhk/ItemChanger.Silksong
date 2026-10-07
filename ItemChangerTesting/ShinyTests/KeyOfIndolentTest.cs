using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.ShinyTests;

internal class KeyOfIndolentTest : AbstractShinyPlacementTest<KeyOfIndolentTest>
{
    protected override string MenuName => "Key of Indolent";

    protected override string MenuDescription => "Test the Key of Indolent location.";

    protected override int Revision => 2026100500;

    protected override void OnEnterGame() => BaseItemList.Key_of_Indolent.GiveImmediate(new());

    protected override IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> LocationTests =>
        [(new CoordinateStartDef { SceneName = SceneNames.Slab_14, X = 18, Y = 4, RespawnFacingRight = false }, [LocationNames.Key_of_Indolent])];
}
