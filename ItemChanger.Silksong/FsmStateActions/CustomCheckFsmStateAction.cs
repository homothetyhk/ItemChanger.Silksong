namespace ItemChanger.Silksong.FsmStateActions;

public class CustomCheckFsmStateAction : FSMUtility.CheckFsmStateAction
{
    public required Func<bool> GetIsTrue;

    public override bool IsTrue => GetIsTrue();

    public CustomCheckFsmStateAction() { }

    public CustomCheckFsmStateAction(FSMUtility.CheckFsmStateAction orig)
    {
        trueEvent = orig.trueEvent;
        falseEvent = orig.falseEvent;
        storeValue = orig.storeValue;
    }
}
