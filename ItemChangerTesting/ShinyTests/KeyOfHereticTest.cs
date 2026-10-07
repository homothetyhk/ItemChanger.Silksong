using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.ShinyTests;

internal class KeyOfHereticTest : AbstractShinyPlacementTest<KeyOfHereticTest>
{
    protected override string MenuName => "Key of Heretic";

    protected override string MenuDescription => "Test the Key of Heretic location.";

    protected override int Revision => 2026100400;

    protected override void OnEnterGame() => BaseItemList.Key_of_Heretic.GiveImmediate(new());

    internal static IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> CommonLocationTests =>
        [(new CoordinateStartDef { SceneName = SceneNames.Slab_16, X = 30.5f, Y = 32, RespawnFacingRight = false }, [LocationNames.Key_of_Heretic])];

    protected override IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> LocationTests => CommonLocationTests;
}
