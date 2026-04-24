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
            {new(SceneName!, "Wood Witch", "Dialogue"), HookWitch},
        });
        ActiveProfile!.Modules.GetOrAdd<CurseSuppressionModule>();
    }

    protected override void DoUnload() {}

    private void HookWitch(PlayMakerFSM fsm)
    {
        FsmState neckSnapState = fsm.MustGetState("Neck Snap");
        neckSnapState.AddMethod(GiveAll);
    }
}
