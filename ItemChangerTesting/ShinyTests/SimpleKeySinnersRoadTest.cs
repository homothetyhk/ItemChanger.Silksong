using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.ShinyTests;

internal class SimpleKeySinnersRoadTest : AbstractShinyPlacementTest<SimpleKeySinnersRoadTest>
{
    protected override string MenuName => "Simple Key Sinner's Road";

    protected override string MenuDescription => "Simple Key drop from the Roachkeeper in Sinner's Road.";

    protected override int Revision => 2026093000;

    protected override void OnEnterGame()
    {
        for (int i = 0; i < 4; i++)
            BaseItemList.Simple_Key.GiveImmediate(new());
    }

    protected override IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> LocationTests =>
        [(new CoordinateStartDef { SceneName = SceneNames.Dust_06, X = 25, Y = 174, RespawnFacingRight = false, MapZone = GlobalEnums.MapZone.DUSTPENS }, [LocationNames.Simple_Key__Sinner_s_Road])];
}
