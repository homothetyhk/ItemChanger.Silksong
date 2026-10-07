using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.ShinyTests;

internal class BellhomeUpgradesTest : AbstractShinyPlacementTest<BellhomeUpgradesTest>
{
    protected override string MenuName => "Bellhome Upgrades";

    protected override string MenuDescription => "Test shiny locations for Bellhome upgrades.";

    protected override int Revision => 2026100700;

    protected override void OnEnterGame()
    {
        BaseItemList.Crawbell.GiveImmediate(new());
        BaseItemList.Farsight.GiveImmediate(new());
        BaseItemList.Materium.GiveImmediate(new());
    }

    protected override IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> LocationTests =>
    [
        (new CoordinateStartDef { SceneName = SceneNames.Room_CrowCourt_02, X = 14.5f, Y = 46, RespawnFacingRight = true }, [LocationNames.Crawbell]),
        (new CoordinateStartDef { SceneName = SceneNames.Abyss_08, X = 153, Y = 92, RespawnFacingRight = true }, [LocationNames.Farsight]),
        (new CoordinateStartDef { SceneName = SceneNames.Arborium_07, X = 101, Y = 11.5f, RespawnFacingRight = true }, [LocationNames.Materium])
    ];
}
