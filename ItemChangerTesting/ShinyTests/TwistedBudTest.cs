using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.ShinyTests;

internal class TwistedBudTest : AbstractShinyPlacementTest<TwistedBudTest>
{
    protected override string MenuName => "Twisted Bud";

    protected override string MenuDescription => "Test the Twisted Bud location.";

    protected override int Revision => 2026100700;

    protected override void OnEnterGame()
    {
        new SDBool(SceneNames.Shadow_20, "Witch_Cluster_Vine").Value = true;
        new SDBool(SceneNames.Shadow_20, "Witch_Cluster_Vine (1)").Value = true;

        BaseItemList.Twisted_Bud.GiveImmediate(new());
    }

    protected override IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> LocationTests =>
        [(new CoordinateStartDef { SceneName = SceneNames.Shadow_20, X = 103, Y = 49, RespawnFacingRight = true }, [LocationNames.Twisted_Bud])];
}
