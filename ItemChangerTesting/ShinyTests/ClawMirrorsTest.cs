using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;
using PrepatcherPlugin;

namespace ItemChangerTesting.ShinyTests;

internal class ClawMirrorsTest : AbstractShinyPlacementTest<ClawMirrorsTest>
{
    protected override string MenuName => "Claw Mirrors";

    protected override string MenuDescription => "Tests drops from the Tormented Trobbio fight.";

    protected override int Revision => 2026093000;

    protected override void OnEnterGame()
    {
        base.OnEnterGame();

        StartAct3();
        PlayerDataAccess.defeatedTormentedTrobbio = false;
        QuestUtil.SetAccepted(Quests.Tormented_Trobbio);

        // TODO: Make progressive.
        BaseItemList.Claw_Mirror.GiveImmediate(new());
        BaseItemList.Claw_Mirrors.GiveImmediate(new());
    }

    protected override IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> LocationTests =>
    [
        (new CoordinateStartDef
        {
            SceneName = SceneNames.Library_13,
            X = 54.30f,
            Y = 14.57f,
            MapZone = GlobalEnums.MapZone.NONE
        }, [LocationNames.Claw_Mirrors]),
    ];
}
