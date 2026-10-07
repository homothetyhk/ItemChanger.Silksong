using Benchwarp.Data;
using ItemChanger.Silksong;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;
using PrepatcherPlugin;
using UnityEngine.SceneManagement;

namespace ItemChangerTesting.ShinyTests;

internal class WispfireLanternTest : AbstractShinyPlacementTest<WispfireLanternTest>
{
    protected override string MenuName => "Wispfire Lantern";

    protected override string MenuDescription => "Test the Wispfire Lantern location.";

    protected override int Revision => 2026100500;

    protected override void OnEnterGame()
    {
        PlayerDataAccess.hasDash = true;
        PlayerDataAccess.hasDoubleJump = true;

        BaseItemList.Wispfire_Lantern.GiveImmediate(new());
        Using(new SceneEditGroup { { SceneNames.Belltown_08, WeakenBoss } });
    }

    private void WeakenBoss(Scene scene)
    {
        // Father of the Flame doesn't have a HealthManager so he needs custom treatment.
        foreach (var fsm in scene.GetRootGameObjects()
            .SelectMany(o => o.GetComponentsInChildren<PlayMakerFSM>(includeInactive: true))
            .Where(fsm => fsm.FsmName == "wisp_brazier_arm" || (fsm.name == "Wisp Pyre Effigy" && fsm.FsmName == "Take Damage")))
        {
            fsm.FsmVariables.GetFsmInt("HP").Value = 1;
        }
    }

    protected override IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> LocationTests =>
        [(new CoordinateStartDef { SceneName = SceneNames.Belltown_08, X = 63, Y = 12, RespawnFacingRight = false }, [LocationNames.Wispfire_Lantern])];
}
