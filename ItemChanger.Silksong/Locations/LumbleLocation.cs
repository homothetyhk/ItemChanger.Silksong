using ItemChanger.Locations;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Silksong.FsmUtil;

namespace ItemChanger.Silksong.Locations;

public class LumbleLocation : AutoLocation
{
    protected override void DoLoad()
    {
        Using(new FsmEditGroup()
        {
            { new(SceneName!, "Play Point", "Play Control"), HookLumble }
        });
    }

    protected override void DoUnload() { }

    private void HookLumble(PlayMakerFSM fsm)
    {
        FsmState giveMagnetiteDice = fsm.MustGetState("Get Reward");
        giveMagnetiteDice.RemoveActionsOfType<SetToolUnlocked>();
        giveMagnetiteDice.InsertLambdaMethod(0, GiveAll);
    }
}