using Benchwarp.Data;
using ItemChanger.Modules;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Silksong.FsmUtil;

namespace ItemChanger.Silksong.Modules;

[SingletonModule]
public class CurseSuppressionModule : Module
{
    protected override void DoLoad()
    {
        Using(new FsmEditGroup()
        {
            {new(SceneNames.Shellwood_25b, "door_curseSequenceEnd", "Curse Sequence"), SuppressCurse},
        });
    }

    protected override void DoUnload() {}

    private static void SuppressCurse(PlayMakerFSM fsm)
    {
        FsmState setHeroPosState = fsm.MustGetState("Set Hero Pos");
        setHeroPosState.RemoveFirstActionOfType<TakeSilk>();
        setHeroPosState.RemoveFirstActionMatching(act => act is CallMethodProper call && call.methodName.Value == "SetSilkRegenBlocked");

        FsmState setCursedState = fsm.MustGetState("Set Cursed");
        setCursedState.RemoveLastActionMatching(act => act is CallMethodProper call && call.methodName.Value == "UpdateSilkCursed");
        setCursedState.RemoveLastActionOfType<AutoEquipCrest>();
    }
}