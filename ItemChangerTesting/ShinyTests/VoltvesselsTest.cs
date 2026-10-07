using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.ShinyTests;

internal class VoltvesselsTest : AbstractShinyPlacementTest<VoltvesselsTest>
{
    protected override string MenuName => "Voltvessels";

    protected override string MenuDescription => "Test modifying Voltvessels and the Materium.";

    protected override int Revision => 2026100300;

    protected override void OnEnterGame() => BaseItemList.Voltvessels.GiveImmediate(new());

    protected override IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> LocationTests =>
        [(new CoordinateStartDef { SceneName = SceneNames.Arborium_07, X = 142, Y = 10, RespawnFacingRight = true }, [LocationNames.Voltvessels])];
}
