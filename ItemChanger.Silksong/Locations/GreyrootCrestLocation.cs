using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.Modules;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Silksong.FsmUtil;

namespace ItemChanger.Silksong.Locations;

public class GreyrootCrestLocation : AutoLocation
{
    protected override void DoLoad()
    {
        Using(new FsmEditGroup()
        {
            {new(UnsafeSceneName, "Wood Witch", "Dialogue"), HookWitch},
        });
        // We don't want to do the curse suppression directly in this location
        // because the DualLocation will disable it once the curse quest is complete,
        // which is before the Chapel of the Witch scene loads.
        // Furthermore, the curse suppression is also necessary upon reentering the Chapel
        // from a reusable warp.
        ActiveProfile!.Modules.GetOrAdd<CurseSuppressionModule>();
    }

    protected override void DoUnload() {}

    private void HookWitch(PlayMakerFSM fsm)
    {
        FsmState giveState = fsm.AddState("Give Crest Items");
        giveState.AddLambdaMethod(GiveAll);
        giveState.AddTransition("FINISHED", "Set Black");

        FsmState neckSnapState = fsm.MustGetState("Neck Snap");
        neckSnapState.ChangeTransition("FINISHED", giveState.Name);
    }
}
