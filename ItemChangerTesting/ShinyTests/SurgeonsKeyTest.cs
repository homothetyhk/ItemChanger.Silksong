using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.StartDefs;
using PrepatcherPlugin;

namespace ItemChangerTesting.ShinyTests;

internal class SurgeonsKeyTest : AbstractShinyPlacementTest<SurgeonsKeyTest>
{
    protected override string MenuName => "Surgeon's Key";

    protected override string MenuDescription => "Test the Surgeon's Key location.";

    protected override int Revision => 2026100700;

    // We use Surgeon's Key as the default everywhere else, but we shouldn't do that here.
    protected override string DefaultShinyItem => ItemNames.White_Key;

    protected override void OnEnterGame()
    {
        PlayerDataAccess.hasHarpoonDash = true;
        PlayerDataAccess.silkRegenMax = 3;

        new SDBool(SceneNames.Ward_07, "ward_junk_pile_break 1").Value = true;
        BaseItemList.Surgeon_s_Key.GiveImmediate(new());
    }

    protected override IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> LocationTests =>
        [(new CoordinateStartDef { SceneName = SceneNames.Ward_07, X = 17, Y = 8, RespawnFacingRight = false }, [LocationNames.Surgeon_s_Key])];
}
