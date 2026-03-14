using HarmonyLib;
using UnityEngine;

namespace ItemChanger.Silksong.Modules.CustomSkills;

public class GroundedSprintModule : CustomSkillModule
{
    public static GroundedSprintModule Instance { get; private set; }

    private HarmonyLib.Harmony _harmony;

#pragma warning disable IDE1006
    public bool hasGroundedSprint { get; set; }
#pragma warning restore IDE1006

    public override IEnumerable<string> GettableSkillBools() => [nameof(hasGroundedSprint)];
    public override bool GetBool(string boolName) => boolName switch
    {
        nameof(hasGroundedSprint) => hasGroundedSprint,
        _ => throw UnsupportedBoolName(boolName),
    };
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
        Instance = this;
        _harmony = new HarmonyLib.Harmony("ItemChanger.Silksong.GroundedSprint");
        _harmony.Patch(
            AccessTools.Method(typeof(HeroController), nameof(HeroController.Move)),
            postfix: new HarmonyMethod(typeof(GroundedSprintModule), nameof(ApplyGroundSprint))
        );
    }

    protected override void DoUnload()
    {
        base.DoUnload();
        _harmony?.UnpatchSelf();
        _harmony = null;
        if (Instance == this) Instance = null;
    }

    private static void ApplyGroundSprint(HeroController __instance)
    {
        if (Instance == null || !Instance.hasGroundedSprint) return;
        if (PlayerData.instance.hasDash) return;
        if (!__instance.cState.onGround) return;
        if (!InputHandler.Instance.inputActions.Dash.IsPressed) return;

        Rigidbody2D rb = __instance.GetComponent<Rigidbody2D>();
        if (rb == null || rb.bodyType == RigidbodyType2D.Static) return;

        float dir = __instance.cState.facingRight ? 1f : -1f;
        rb.velocity = new Vector2(dir * __instance.DASH_SPEED, rb.velocity.y);
    }
}
