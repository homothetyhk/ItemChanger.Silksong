using System;
using System.Reflection;
using HutongGames.PlayMaker;

namespace ItemChanger.Silksong.Modules.CustomSkills;

public class GroundedSprintModule : CustomSkillModule
{
#pragma warning disable IDE1006
    public bool hasGroundedSprint { get; set; }
#pragma warning restore IDE1006

    private float? cachedBase;
    private float? cachedOne;
    private float? cachedBoth;

    public override IEnumerable<string> GettableSkillBools() => [nameof(hasGroundedSprint)];

    public override bool GetBool(string boolName)
    {
        switch (boolName)
        {
            case nameof(hasGroundedSprint): return hasGroundedSprint;
            default: throw UnsupportedBoolName(boolName);
        }
    }

    public override IEnumerable<string> SettableSkillBools() => [nameof(hasGroundedSprint)];

    public override void SetBool(string boolName, bool value)
    {
        switch (boolName)
        {
            case nameof(hasGroundedSprint):
                hasGroundedSprint = value;
                break;
            default:
                throw UnsupportedBoolName(boolName);
        }
    }

    protected override void DoLoad()
    {
        base.DoLoad();
        Using(Md.HeroController.GetRunSpeed.Postfix(OverrideRunSpeed));
    }

    private void OverrideRunSpeed(HeroController self, ref float returnValue)
    {
        if (!hasGroundedSprint) return;
        if (PlayerData.instance != null && PlayerData.instance.hasDash) return;
        if (InputHandler.Instance == null) return;
        if (!InputHandler.Instance.inputActions.Dash.IsPressed) return;

        if (self.cState.onGround)
        {
            returnValue = GetSprintSpeed(self);
        }
        else
        {
            returnValue = self.GetWalkSpeed();
        }
    }

    private float GetSprintSpeed(HeroController self)
    {
        EnsureCachedSpeeds(self);
        if (cachedBase == null) return self.RUN_SPEED;

        var boosts = 0;
        if (self.IsSprintMasterActive) boosts++;
        if (self.IsUsingQuickening) boosts++;

        if (boosts >= 2) return cachedBoth!.Value;
        if (boosts == 1) return cachedOne!.Value;
        return cachedBase!.Value;
    }

    private void EnsureCachedSpeeds(HeroController self)
    {
        if (cachedBase != null) return;
        if (self.sprintFSM == null) return;

        try
        {
            foreach (var state in self.sprintFSM.FsmStates)
            {
                foreach (var action in state.Actions)
                {
                    if (action.GetType().Name != "ConvertDoubleBoolToFloat") continue;

                    var t = action.GetType();
                    var bothFalse = t.GetField("bothFalseValue", BindingFlags.Public | BindingFlags.Instance)?.GetValue(action) as FsmFloat;
                    var oneTrue = t.GetField("oneTrueValue", BindingFlags.Public | BindingFlags.Instance)?.GetValue(action) as FsmFloat;
                    var bothTrue = t.GetField("bothTrueValue", BindingFlags.Public | BindingFlags.Instance)?.GetValue(action) as FsmFloat;
                    if (bothFalse == null || oneTrue == null || bothTrue == null) continue;

                    cachedBase = Math.Abs(bothFalse.Value);
                    cachedOne = Math.Abs(oneTrue.Value);
                    cachedBoth = Math.Abs(bothTrue.Value);
                    return;
                }
            }
        }
        catch { }
    }
}
