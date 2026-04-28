using HutongGames.PlayMaker.Actions;
using Silksong.FsmUtil;

namespace ItemChanger.Silksong.FsmStateActions;

/// <summary>
/// CheckFsmStateAction which sends an event based on the value of a predicate,
/// and stores the result of the predicate in an FSM bool variable.
/// 
/// The bool variable must not be null for this to function, but the events
/// may be null (and will not be sent in that case).
/// </summary>
public class CustomCheckFsmStateAction : FSMUtility.CheckFsmStateAction
{
    public required Func<bool> GetIsTrue { get; init; }

    public override bool IsTrue => GetIsTrue();

    public CustomCheckFsmStateAction() { }

    public CustomCheckFsmStateAction(FSMUtility.CheckFsmStateAction orig)
    {
        trueEvent = orig.trueEvent;
        falseEvent = orig.falseEvent;
        storeValue = orig.storeValue;
    }

    public CustomCheckFsmStateAction(PlayerDataBoolTest orig)
    {
        trueEvent = orig.isTrue;
        falseEvent = orig.isFalse;
        storeValue = orig.fsm.GetBoolVariable("ITEMCHANGER DUMMY BOOL");
    }

    public CustomCheckFsmStateAction(BoolTest orig)
    {
        trueEvent = orig.isTrue;
        falseEvent = orig.isFalse;
        storeValue = orig.fsm.GetBoolVariable("ITEMCHANGER DUMMY BOOL");
    }

    public CustomCheckFsmStateAction(PlayerDataVariableTest orig)
    {
        trueEvent = orig.IsExpectedEvent;
        falseEvent = orig.IsNotExpectedEvent;
        storeValue = orig.fsm.GetBoolVariable("ITEMCHANGER DUMMY BOOL");
    }
}
