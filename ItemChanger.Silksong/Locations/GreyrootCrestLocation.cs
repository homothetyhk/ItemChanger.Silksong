using Benchwarp.Data;
using ItemChanger.Locations;
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
            {new(SceneNames.Shellwood_25b, "door_curseSequenceEnd", "Curse Sequence"), RemoveVanillaReward},
        });
    }

    protected override void DoUnload() {}

    private void HookWitch(PlayMakerFSM fsm)
    {
        FsmState neckSnapState = fsm.MustGetState("Neck Snap");
        neckSnapState.AddMethod(GiveAll);
    }

    private void RemoveVanillaReward(PlayMakerFSM fsm)
    {
        FsmState setHeroPosState = fsm.MustGetState("Set Hero Pos");
        setHeroPosState.RemoveFirstActionOfType<TakeSilk>();
        setHeroPosState.RemoveFirstActionMatching(act => act is CallMethodProper call && call.methodName.Value == "SetSilkRegenBlocked");

        FsmState setCursedState = fsm.MustGetState("Set Cursed");
        setCursedState.RemoveLastActionMatching(act => act is CallMethodProper call && call.methodName.Value == "UpdateSilkCursed");
        setCursedState.RemoveLastActionOfType<AutoEquipCrest>();
    }
}
