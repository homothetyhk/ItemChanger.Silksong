using HarmonyLib;
using ItemChanger.Silksong.Extensions;
using System.Reflection;
using UnityEngine;

namespace ItemChanger.Silksong.Modules.CustomSkills;

/// <summary>
/// Novelty: Swift Step sprint while grounded only — no air sprint, no air dash, no shuttle-cock.
///
/// flibber (#209): trick the game so vanilla sprint anim/cState run.
///
/// Critical decomp (HeroController.FixedUpdate):
///   if (cState.jumping &amp;&amp; !cState.dashing &amp;&amp; !cState.isSprinting) Jump();
/// Jump() is the only place that applies JUMP_SPEED. If isSprinting stays true after a
/// sprint-jump (GS cancels shuttlecock but not the sprint flag in time), you get a
/// near-zero hop. CanJump() also requires !isSprinting, so sprint jump normally goes
/// through the shuttlecock FSM path — which we intentionally block.
/// </summary>
public class GroundedSprintModule : CustomSkillModule
{
#pragma warning disable IDE1006
    public bool hasGroundedSprint { get; set; }
#pragma warning restore IDE1006

    private static GroundedSprintModule? _activeInstance;
    private static readonly FieldInfo? SprintBufferStepsField =
        AccessTools.Field(typeof(HeroController), "sprintBufferSteps");
    private static readonly FieldInfo? HasDashField =
        AccessTools.Field(typeof(PlayerData), "hasDash")
        ?? AccessTools.DeclaredField(typeof(PlayerData), "hasDash");
    private static readonly FieldInfo? SprintSpeedAddFloatField =
        AccessTools.Field(typeof(HeroController), "sprintSpeedAddFloat");
    private static readonly FieldInfo? NoShuttlecockTimeField =
        AccessTools.Field(typeof(HeroController), "noShuttlecockTime");
    private static readonly FieldInfo? SyncBufferStepsField =
        AccessTools.Field(typeof(HeroController), "syncBufferSteps");

    private Harmony? _harmony;
    private static bool _wasOnGround = true;

    public override IEnumerable<string> GettableSkillBools() =>
    [
        nameof(hasGroundedSprint),
        nameof(PlayerData.hasDash),
    ];

    public override bool GetBool(string boolName) => boolName switch
    {
        nameof(hasGroundedSprint) => hasGroundedSprint,
        nameof(PlayerData.hasDash) => ComputeEffectiveHasDash(),
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

    private bool ComputeEffectiveHasDash()
    {
        if (ReadRawHasDash()) return true;
        if (!hasGroundedSprint) return false;
        // Strict ground only — false the instant we leave ground so FSM gates flip.
        HeroController? hc = HeroController.SilentInstance;
        return hc != null && IsEffectivelyGrounded(hc);
    }

    private static bool ReadRawHasDash()
    {
        PlayerData? pd = PlayerData.instance;
        if (pd == null) return false;
        if (HasDashField != null)
            return (bool)HasDashField.GetValue(pd)!;
        return false;
    }

    private static bool OnlyGroundedSprintKit()
    {
        GroundedSprintModule? module = _activeInstance;
        if (module == null || !module.hasGroundedSprint) return false;
        return !ReadRawHasDash();
    }

    /// <summary>
    /// Grounded for GS purposes: onGround flag OR still touching floor this frame.
    /// Leaving ledge often has a frame where velocity is high but flag lags — treat
    /// !CheckTouchingGround as air for cancel/clamp.
    /// </summary>
    private static bool IsEffectivelyGrounded(HeroController hc)
    {
        if (hc.cState.onGround) return true;
        if (hc.cState.wallSliding || hc.cState.wallClinging || hc.cState.wallScrambling)
            return false;
        try
        {
            return hc.CheckTouchingGround();
        }
        catch
        {
            return false;
        }
    }

    protected override void DoLoad()
    {
        base.DoLoad();
        _activeInstance = this;
        _wasOnGround = true;

        Using(Md.InventoryItemConditional.Evaluate.Prefix(OverrideInventoryDisplayTest));

        _harmony = new Harmony("itemchanger.silksong.groundedsprint");

        Patch(typeof(HeroController), nameof(HeroController.CanDash), postfix: nameof(CanDashPostfix));
        Patch(typeof(HeroController), nameof(HeroController.CanJump), postfix: nameof(CanJumpPostfix));

        var heroDashPressed = AccessTools.Method(typeof(HeroController), "HeroDashPressed");
        if (heroDashPressed != null)
        {
            _harmony.Patch(heroDashPressed,
                prefix: new HarmonyMethod(typeof(GroundedSprintModule), nameof(HeroDashPressedPrefix)));
        }
        var heroDash = AccessTools.Method(typeof(HeroController), "HeroDash", [typeof(bool)]);
        if (heroDash != null)
        {
            _harmony.Patch(heroDash,
                prefix: new HarmonyMethod(typeof(GroundedSprintModule), nameof(HeroDashPrefix)));
        }

        // PREFIX FixedUpdate so isSprinting is cleared before Jump() gate.
        Patch(typeof(HeroController), "Update",
            prefix: nameof(HeroUpdatePrefix),
            postfix: nameof(HeroUpdatePostfix));
        Patch(typeof(HeroController), "FixedUpdate",
            prefix: nameof(HeroFixedUpdatePrefix),
            postfix: nameof(HeroFixedUpdatePostfix));

        var leftGround = AccessTools.Method(typeof(HeroController), "LeftGround", [typeof(bool)]);
        if (leftGround != null)
        {
            _harmony.Patch(leftGround,
                prefix: new HarmonyMethod(typeof(GroundedSprintModule), nameof(LeftGroundPrefix)),
                postfix: new HarmonyMethod(typeof(GroundedSprintModule), nameof(LeftGroundPostfix)));
        }

        var heroJumpBool = AccessTools.Method(typeof(HeroController), "HeroJump", [typeof(bool)]);
        if (heroJumpBool != null)
        {
            _harmony.Patch(heroJumpBool,
                prefix: new HarmonyMethod(typeof(GroundedSprintModule), nameof(HeroJumpBoolPrefix)));
        }

        var shuttle = AccessTools.Method(typeof(HeroController), "OnShuttleCockJump");
        if (shuttle != null)
        {
            _harmony.Patch(shuttle,
                prefix: new HarmonyMethod(typeof(GroundedSprintModule), nameof(OnShuttleCockJumpPrefix)));
        }

        ItemChangerPlugin.Instance.Logger.LogInfo(
            "[GroundedSprint] loaded: Jump-gate isSprinting clear (FixedUpdate prefix), CanJump while GS sprint, no shuttlecock.");
    }

    private void Patch(Type type, string name, string? prefix = null, string? postfix = null)
    {
        MethodInfo? m = AccessTools.Method(type, name);
        if (m == null)
        {
            ItemChangerPlugin.Instance.Logger.LogWarning($"[GroundedSprint] method not found: {type.Name}.{name}");
            return;
        }
        _harmony!.Patch(m,
            prefix: prefix != null ? new HarmonyMethod(typeof(GroundedSprintModule), prefix) : null,
            postfix: postfix != null ? new HarmonyMethod(typeof(GroundedSprintModule), postfix) : null);
    }

    protected override void DoUnload()
    {
        _harmony?.UnpatchSelf();
        _harmony = null;
        if (_activeInstance == this) _activeInstance = null;
        base.DoUnload();
    }

    private void OverrideInventoryDisplayTest(InventoryItemConditional self)
    {
        if (!hasGroundedSprint || ReadRawHasDash()) return;
        if (self.Test.IsSingleTest(out PlayerDataTest.Test t) && t.FieldName == nameof(PlayerData.hasDash))
        {
            self.Test.Modify(test =>
            {
                test.FieldName = nameof(hasGroundedSprint);
                return test;
            });
        }
    }

    // ---- Harmony ----

    private static void CanDashPostfix(HeroController __instance, ref bool __result)
    {
        if (!OnlyGroundedSprintKit()) return;
        __result = false;
    }

    /// <summary>
    /// Vanilla CanJump requires !isSprinting (sprint jump is shuttlecock via FSM).
    /// GS blocks shuttlecock, so allow a normal grounded jump while GS-sprinting.
    /// </summary>
    private static void CanJumpPostfix(HeroController __instance, ref bool __result)
    {
        if (__result || !OnlyGroundedSprintKit()) return;
        if (!__instance.cState.isSprinting && !__instance.cState.isBackSprinting) return;
        // Mirror CanJump's safe grounded branch without ActorStates (not public in all refs).
        if (__instance.cState.onGround
            && !__instance.cState.dashing
            && !__instance.cState.jumping
            && !__instance.cState.hazardDeath
            && !__instance.cState.hazardRespawning
            && !__instance.cState.dead)
        {
            __result = true;
        }
    }

    private static bool HeroDashPressedPrefix(HeroController __instance)
    {
        if (!OnlyGroundedSprintKit()) return true;
        if (__instance.cState.onGround && !__instance.cState.jumping && __instance.CanSprint())
            __instance.sprintFSM?.SendEvent("TRY SPRINT");
        return false;
    }

    private static bool HeroDashPrefix(HeroController __instance, bool startAlreadyDashing)
    {
        if (!OnlyGroundedSprintKit()) return true;
        if (__instance.cState.onGround && !__instance.cState.jumping && __instance.CanSprint())
            __instance.sprintFSM?.SendEvent("TRY SPRINT");
        return false;
    }

    private static bool OnShuttleCockJumpPrefix() => !OnlyGroundedSprintKit();

    private static void HeroJumpBoolPrefix(HeroController __instance, ref bool checkSprint)
    {
        if (!OnlyGroundedSprintKit()) return;
        checkSprint = false;
        __instance.PreventShuttlecock();
        SprintBufferStepsField?.SetValue(__instance, 0);
        SyncBufferStepsField?.SetValue(__instance, false);
        NoShuttlecockTimeField?.SetValue(__instance, Time.timeAsDouble + 5.0);
        // Must clear isSprinting here so Jump() can apply JUMP_SPEED this physics step.
        SoftCancelSprint(__instance, sendFsmEvent: true, clampAirSpeed: false);
        __instance.cState.shuttleCock = false;
    }

    private static void LeftGroundPrefix(HeroController __instance)
    {
        if (!OnlyGroundedSprintKit()) return;
        __instance.cState.isSprinting = false;
        __instance.cState.isBackSprinting = false;
        SprintBufferStepsField?.SetValue(__instance, 0);
        SyncBufferStepsField?.SetValue(__instance, false);
    }

    private static void LeftGroundPostfix(HeroController __instance)
    {
        if (!OnlyGroundedSprintKit()) return;
        SoftCancelSprint(__instance, sendFsmEvent: true, clampAirSpeed: true);
    }

    /// <summary>
    /// Before Update input: if jump pressed while GS-sprinting, cancel sprint so
    /// CanJump/HeroJump see a clean non-sprint state.
    /// </summary>
    private static void HeroUpdatePrefix(HeroController __instance)
    {
        if (!OnlyGroundedSprintKit()) return;

        InputHandler? ih = InputHandler.Instance;
        if (ih != null
            && ih.inputActions.Jump.WasPressed
            && (__instance.cState.isSprinting || __instance.cState.isBackSprinting)
            && __instance.cState.onGround)
        {
            SoftCancelSprint(__instance, sendFsmEvent: true, clampAirSpeed: false);
            __instance.cState.shuttleCock = false;
            __instance.PreventShuttlecock();
        }
    }

    private static void HeroUpdatePostfix(HeroController __instance) => TickGuards(__instance, physics: false);

    /// <summary>
    /// BEFORE FixedUpdate body: Jump() is gated on !isSprinting. Clear sprint flags
    /// for any airborne / jumping frame so JUMP_SPEED applies and downspike isn't
    /// fighting sprint control.
    /// </summary>
    private static void HeroFixedUpdatePrefix(HeroController __instance)
    {
        if (!OnlyGroundedSprintKit()) return;

        bool airborne = !IsEffectivelyGrounded(__instance)
            || __instance.cState.jumping
            || __instance.cState.doubleJumping
            || __instance.cState.downSpiking
            || __instance.cState.downSpikeAntic
            || __instance.cState.shuttleCock;

        if (!airborne) return;

        // Hard-clear every physics frame while air/jumping — FSM can re-set isSprinting.
        __instance.cState.isSprinting = false;
        __instance.cState.isBackSprinting = false;
        __instance.cState.shuttleCock = false;
        SprintBufferStepsField?.SetValue(__instance, 0);
        SyncBufferStepsField?.SetValue(__instance, false);
        ClearSprintSpeedAdd(__instance);

        if (__instance.sprintFSM != null)
        {
            var isSprint = __instance.sprintFSM.FsmVariables.GetFsmBool("Is Sprinting");
            if (isSprint != null && isSprint.Value)
            {
                isSprint.Value = false;
                __instance.sprintFSM.SendEvent("CANCEL SPRINT");
            }
        }
    }

    private static void HeroFixedUpdatePostfix(HeroController __instance) => TickGuards(__instance, physics: true);

    private static void TickGuards(HeroController hc, bool physics)
    {
        if (!OnlyGroundedSprintKit())
        {
            _wasOnGround = hc.cState.onGround;
            return;
        }

        hc.PreventShuttlecock();
        NoShuttlecockTimeField?.SetValue(hc, Time.timeAsDouble + 1.0);

        bool grounded = IsEffectivelyGrounded(hc);
        bool busyAirAction = hc.cState.jumping
            || hc.cState.doubleJumping
            || hc.cState.downSpiking
            || hc.cState.downSpikeAntic
            || hc.cState.attacking;

        if (_wasOnGround && !grounded)
            SoftCancelSprint(hc, sendFsmEvent: true, clampAirSpeed: true);

        _wasOnGround = grounded;

        if (!grounded || busyAirAction)
        {
            if (hc.cState.isSprinting || hc.cState.isBackSprinting)
                SoftCancelSprint(hc, sendFsmEvent: true, clampAirSpeed: !busyAirAction);
            else
                ClearSprintSpeedAdd(hc);

            // Don't clamp horizontal during downspike thrust (sets intentional X vel).
            if (physics && !grounded && !hc.cState.downSpiking && !hc.cState.downSpikeAntic)
                ClampAirHorizontalSpeed(hc);
            return;
        }

        // Ground only: hold dash → sprint. Never while jumping.
        if (InputHandler.Instance != null
            && InputHandler.Instance.inputActions.Dash.IsPressed
            && !hc.cState.dashing
            && !hc.cState.jumping
            && !hc.cState.hazardDeath
            && hc.CanSprint())
        {
            hc.sprintFSM?.SendEvent("TRY SPRINT");
        }
    }

    private static void SoftCancelSprint(HeroController hc, bool sendFsmEvent, bool clampAirSpeed)
    {
        SprintBufferStepsField?.SetValue(hc, 0);
        SyncBufferStepsField?.SetValue(hc, false);

        if (sendFsmEvent)
            hc.sprintFSM?.SendEvent("CANCEL SPRINT");

        hc.cState.isSprinting = false;
        hc.cState.isBackSprinting = false;
        hc.cState.shuttleCock = false;

        if (hc.sprintFSM != null)
        {
            var isSprint = hc.sprintFSM.FsmVariables.GetFsmBool("Is Sprinting");
            if (isSprint != null) isSprint.Value = false;
        }

        ClearSprintSpeedAdd(hc);

        if (clampAirSpeed && !IsEffectivelyGrounded(hc) && !hc.cState.downSpiking && !hc.cState.downSpikeAntic)
            ClampAirHorizontalSpeed(hc);
    }

    private static void ClearSprintSpeedAdd(HeroController hc)
    {
        if (SprintSpeedAddFloatField?.GetValue(hc) is HutongGames.PlayMaker.FsmFloat add)
            add.Value = 0f;
    }

    /// <summary>
    /// Cap midair |vx| to run speed (not walk). Vanilla Move() uses GetRunSpeed() in air
    /// (~8.25); walk (~5) was killing normal jump/fall air control. Sprint is faster than
    /// run, so this still strips ledge-sprint carry without nerfing ordinary air mobility.
    /// </summary>
    private static void ClampAirHorizontalSpeed(HeroController hc)
    {
        Rigidbody2D rb = hc.rb2d;
        if (rb == null) return;

        float max = Mathf.Abs(hc.GetRunSpeed());
        if (max < 0.01f) max = 8.25f;

        Vector2 v = rb.linearVelocity;
        if (Mathf.Abs(v.x) > max)
        {
            v.x = Mathf.Sign(v.x) * max;
            rb.linearVelocity = v;
        }
    }
}
