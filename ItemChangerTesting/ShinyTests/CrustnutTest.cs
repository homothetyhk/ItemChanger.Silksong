using Benchwarp.Data;
using HutongGames.PlayMaker;
using ItemChanger.Silksong;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.StartDefs;
using Silksong.FsmUtil;
using UnityEngine.SceneManagement;

namespace ItemChangerTesting.ShinyTests;

internal class CrustnutTest : AbstractShinyPlacementTest<CrustnutTest>
{
    protected override string MenuName => "Crustnut";

    protected override string MenuDescription => "Test the Crustnut location.";

    protected override int Revision => 2026100700;

    protected override void OnEnterGame()
    {
        BaseItemList.Crustnut.GiveImmediate(new());
        Using(new SceneEditGroup { { SceneNames.Coral_41, BreakCoralWalls } });
    }

    private void BreakCoralWalls(Scene scene)
    {
        foreach (IsCoralCrustWall wall in scene.GetRootGameObjects().SelectMany(o => o.GetComponentsInChildren<IsCoralCrustWall>(includeInactive: true)))
        {
            FsmState initState = wall.gameObject.LocateMyFSM("Control").MustGetState("Init");
            initState.RemoveTransition("FINISHED");
            initState.AddTransition("FINISHED", "Set Broken");
        }
    }

    protected override IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> LocationTests =>
        [(new CoordinateStartDef { SceneName = SceneNames.Coral_41, X = 13, Y = 36.5f, RespawnFacingRight = false }, [LocationNames.Crustnut])];
}
