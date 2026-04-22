using HutongGames.PlayMaker;
using ItemChanger.Modules;
using ItemChanger.Silksong;
using Silksong.FsmUtil;

namespace ItemChangerTesting.ModuleTests;

/// <summary>
/// Temporary debug module that logs every Sprintmaster Runner FSM state entry to the Unity log.
/// Use with <see cref="LocationTests.SprintmasterMementoLoggerTest"/> to identify the reward state
/// for the bonus (memento) race. Check BepInEx/LogOutput.log after completing the race.
/// </summary>
internal class SprintmasterMementoFsmLoggerModule : Module
{
    protected override void DoLoad()
    {
        Using(new FsmEditGroup()
        {
            { new("Sprintmaster_Cave", "Sprintmaster Runner", "Behaviour"), HookLogger }
        });
    }

    protected override void DoUnload() { }

    private static void HookLogger(PlayMakerFSM fsm)
    {
        foreach (FsmState state in fsm.FsmStates)
        {
            string name = state.Name;
            state.InsertMethod(0, () => UnityEngine.Debug.Log($"[Sprintmaster Memento FSM] → {name}"));
        }
    }
}
