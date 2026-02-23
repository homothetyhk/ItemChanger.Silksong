using HutongGames.PlayMaker;
using Silksong.FsmUtil;

namespace ItemChanger.Silksong.Extensions;

internal static class FsmExtensions
{
    public static FsmState MustGetState(this PlayMakerFSM fsm, string name)
    {
        var state = fsm.GetState(name);
        if (state == null)
        {
            throw new InvalidOperationException($"FSM {fsm.FsmName} does not have a state {name}");
        }
        return state;
    }

    private static int IndexFirstActionMatching(this FsmState state, Func<FsmStateAction, bool> predicate)
    {
        var n = state.actions.Length;
        for (var i = 0; i < n; i++)
        {
            if (predicate(state.actions[i]))
            {
                return i;
            }
        }
        return -1;
    }

    public static int IndexFirstActionOfType<T>(this FsmState state)
        where T : FsmStateAction
        => state.IndexFirstActionMatching(act => act is T);

    public static void RemoveLastActionMatching(this FsmState state, Func<FsmStateAction, bool> predicate)
    {
        var n = state.actions.Length;
        for (var i = n - 1; i >= 0; i--)
        {
            if (predicate(state.actions[i]))
            {
                state.RemoveAction(i);
                return;
            }
        }
    }
}