using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.ShinyTests;

internal class RuneHarpBurdenTest : AbstractShinyPlacementTest<RuneHarpBurdenTest>
{
    protected override string MenuName => "Rune Harp - Burden";

    protected override string MenuDescription => "Test the Rune Harp - Burden location in Act 3.";

    protected override int Revision => 2026100600;

    protected override void OnEnterGame()
    {
        StartAct3();
        BaseItemList.Rune_Harp__Burden.GiveImmediate(new());
    }

    protected override IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> LocationTests =>
        [(new CoordinateStartDef { SceneName = SceneNames.Hang_12, X = 22, Y = 6.5f, RespawnFacingRight = false }, [LocationNames.Rune_Harp__Burden])];
}
