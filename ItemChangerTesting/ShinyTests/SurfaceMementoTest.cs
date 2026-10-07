using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;
using PrepatcherPlugin;

namespace ItemChangerTesting.ShinyTests;

internal class SurfaceMementoTest : AbstractShinyPlacementTest<SurfaceMementoTest>
{
    protected override string MenuName => "Surface Memento";

    protected override string MenuDescription => "Tests items replacing the Surface Memento location.";

    protected override int Revision => 2026093000;

    protected override void OnEnterGame()
    {
        base.OnEnterGame();
        PlayerDataAccess.hasNeedolin = true;
        PlayerDataAccess.silkRegenMax = 3;
        BaseItemList.Surface_Memento.GiveImmediate(new());
    }

    protected override IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> LocationTests =>
        [(new CoordinateStartDef { SceneName = SceneNames.Abandoned_town, X = 348, Y = 8.5f, RespawnFacingRight = true }, [LocationNames.Surface_Memento])];
}
