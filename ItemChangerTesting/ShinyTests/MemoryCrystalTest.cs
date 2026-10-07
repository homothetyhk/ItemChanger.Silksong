using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.ShinyTests;

internal class MemoryCrystalTest : AbstractShinyPlacementTest<MemoryCrystalTest>
{
    protected override string MenuName => "Memory Crystal";

    protected override string MenuDescription => "Test the Memory Crystal location.";

    protected override int Revision => 2026100500;

    protected override void OnEnterGame()
    {
        BaseItemList.Memory_Crystal.GiveImmediate(new());
        new SDBool(SceneNames.Bellway_Peak_02, "One Way Wall Crystal (2)").Value = true;
    }

    protected override IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> LocationTests =>
        [(new CoordinateStartDef { SceneName = SceneNames.Bellway_Peak_02, X = 86, Y = 6, RespawnFacingRight = true }, [LocationNames.Memory_Crystal])];
}
