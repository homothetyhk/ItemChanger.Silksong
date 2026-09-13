using System.Collections;
using System.IO;
using System.Reflection;
using System.Text;
using HarmonyLib;
using ItemChanger;
using ItemChanger.Silksong.Modules.CustomSkills;
using UnityEngine;

namespace ItemChangerTesting.ModuleTests;

/// <summary>
/// In-engine physics probe for Grounded Sprint. Samples real HeroController rb2d
/// velocity across jump / sprint-jump scenarios and writes a report under
/// Application.persistentDataPath/gs_physics_probe.txt (+ BepInEx log).
/// </summary>
internal static class GroundedSprintPhysicsProbe
{
    private static readonly MethodInfo? HeroJumpBool =
        AccessTools.Method(typeof(HeroController), "HeroJump", [typeof(bool)]);
    private static readonly MethodInfo? DownAttackMethod =
        AccessTools.Method(typeof(HeroController), "DoAttack");

    private static readonly string ReportPath = Path.Combine(
        Application.persistentDataPath, "gs_physics_probe.txt");

    public static IEnumerator Run()
    {
        var log = new StringBuilder();
        void L(string s)
        {
            log.AppendLine(s);
            ItemChangerTestingPlugin.Instance.Logger.LogInfo("[GS-Probe] " + s);
        }

        L("=== GS Physics Probe start ===");
        L($"time={System.DateTime.UtcNow:o}");

        HeroController? hc = HeroController.instance;
        if (hc == null)
        {
            L("FAIL: no HeroController");
            Write(log);
            yield break;
        }

        Rigidbody2D rb = hc.rb2d;
        GroundedSprintModule? gs = ItemChangerHost.Singleton.ActiveProfile?.Modules
            .Get<GroundedSprintModule>();
        L($"module={gs != null} hasGroundedSprint={gs?.hasGroundedSprint}");
        L($"onGround={hc.cState.onGround} isSprinting={hc.cState.isSprinting} CanJump={hc.CanJump()}");
        L($"JUMP_SPEED={hc.JUMP_SPEED} walk={hc.GetWalkSpeed()}");

        // Scenario 1: baseline jump
        yield return WaitGrounded(hc);
        ForceClearSprint(hc);
        yield return SampleForcedJump(hc, rb, "baseline_jump", sprintFirst: false, log);

        // Scenario 2: force isSprinting then HeroJump (old-bug reproduction path)
        yield return WaitGrounded(hc);
        ForceClearSprint(hc);
        yield return SampleForcedJump(hc, rb, "sprint_jump_gs", sprintFirst: true, log);

        // Scenario 3: jump then force DownAttack midair
        yield return WaitGrounded(hc);
        ForceClearSprint(hc);
        hc.cState.isSprinting = true;
        hc.sprintFSM?.SendEvent("TRY SPRINT");
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        HeroJumpBool?.Invoke(hc, [true]);

        float maxVy = float.MinValue;
        float minVy = float.MaxValue;
        bool sawDownspike = false;
        bool canAttackAtAir = false;
        for (int i = 0; i < 50; i++)
        {
            yield return new WaitForFixedUpdate();
            Vector2 v = rb.linearVelocity;
            if (v.y > maxVy) maxVy = v.y;
            if (v.y < minVy) minVy = v.y;

            if (i == 8 && !hc.cState.onGround)
            {
                canAttackAtAir = hc.CanAttack();
                L($"midair CanAttack={canAttackAtAir} isSprinting={hc.cState.isSprinting} acceptingInput={hc.acceptingInput} controlReq={hc.controlReqlinquished}");
                if (canAttackAtAir)
                    DownAttackMethod?.Invoke(hc, null);
            }

            if (hc.cState.downSpiking || hc.cState.downSpikeAntic || hc.cState.downAttacking)
                sawDownspike = true;
        }
        L($"downspike_after_sprint_jump: maxVy={maxVy:F3} minVy={minVy:F3} sawDownspike={sawDownspike}");

        L("=== GS Physics Probe done ===");
        L($"report: {ReportPath}");
        Write(log);
    }

    private static IEnumerator SampleForcedJump(
        HeroController hc, Rigidbody2D rb, string name, bool sprintFirst, StringBuilder log)
    {
        if (sprintFirst)
        {
            hc.cState.isSprinting = true;
            hc.sprintFSM?.SendEvent("TRY SPRINT");
            // let FSM tick a couple frames while grounded
            for (int i = 0; i < 8; i++)
                yield return new WaitForFixedUpdate();
        }

        log.AppendLine($"pre {name}: isSprinting={hc.cState.isSprinting} CanJump={hc.CanJump()} onGround={hc.cState.onGround}");

        Vector3 start = hc.transform.position;
        float maxY = start.y;
        float maxVy = float.MinValue;
        int jumpApplyHints = 0;
        int isSprintTrueFrames = 0;
        var trace = new StringBuilder();

        // Direct HeroJump(true) — exercises GS prefix + Jump() gate without input edges.
        HeroJumpBool?.Invoke(hc, [true]);

        for (int i = 0; i < 60; i++)
        {
            yield return new WaitForFixedUpdate();
            Vector2 v = rb.linearVelocity;
            float y = hc.transform.position.y;
            if (y > maxY) maxY = y;
            if (v.y > maxVy) maxVy = v.y;
            if (hc.cState.isSprinting) isSprintTrueFrames++;
            if (Mathf.Abs(v.y - hc.JUMP_SPEED) < 0.75f) jumpApplyHints++;
            if (i < 12)
                trace.Append($"[{i} dy={y - start.y:F2} vy={v.y:F2} sprint={hc.cState.isSprinting} jump={hc.cState.jumping}] ");
            if (hc.cState.onGround && i > 8 && v.y <= 0.05f)
                break;
        }

        float height = maxY - start.y;
        string verdict = height < 0.8f ? "WEAK_HOP" : "OK_HEIGHT";
        log.AppendLine($"scenario={name} height={height:F3} maxVy={maxVy:F3} jumpApply~={jumpApplyHints} sprintTrueFrames={isSprintTrueFrames} verdict={verdict}");
        log.AppendLine($"  trace: {trace}");
        ItemChangerTestingPlugin.Instance.Logger.LogInfo($"[GS-Probe] {name} height={height:F3} {verdict}");
    }

    private static IEnumerator WaitGrounded(HeroController hc)
    {
        float t0 = Time.time;
        while ((!hc.cState.onGround || hc.cState.jumping) && Time.time - t0 < 4f)
            yield return null;
        for (int i = 0; i < 12; i++)
            yield return new WaitForFixedUpdate();
        ForceClearSprint(hc);
    }

    private static void ForceClearSprint(HeroController hc)
    {
        hc.cState.isSprinting = false;
        hc.cState.isBackSprinting = false;
        hc.cState.shuttleCock = false;
        hc.sprintFSM?.SendEvent("CANCEL SPRINT");
    }

    private static void Write(StringBuilder log)
    {
        try
        {
            File.WriteAllText(ReportPath, log.ToString());
        }
        catch (System.Exception e)
        {
            ItemChangerTestingPlugin.Instance.Logger.LogError("[GS-Probe] write failed: " + e.Message);
        }
    }
}
