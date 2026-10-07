using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.ShinyTests;

internal class DeadBugsPurseTest : AbstractShinyPlacementTest<DeadBugsPurseTest>
{
    protected override string MenuName => $"Dead Bug's Purse";

    protected override string MenuDescription => $"Test the Dead Bug's Purse location.";

    protected override int Revision => 2026100300;

    protected override void OnEnterGame() => BaseItemList.Dead_Bug_s_Purse.GiveImmediate(new());

    internal static CoordinateStartDef StartDef => new()
    {
        SceneName = SceneNames.Crawl_01,
        X = 57.32f,
        Y = 85.57f,
        MapZone = GlobalEnums.MapZone.CRAWLSPACE
    };

    protected override IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> LocationTests => [(StartDef, [LocationNames.Dead_Bug_s_Purse])];
}
