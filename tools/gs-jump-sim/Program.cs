// Pure offline physics-gate simulation for Grounded Sprint jump.
// Mirrors HeroController decomp (no Unity): Jump() only runs when
//   jumping && !dashing && !isSprinting
// and applies JUMP_SPEED each FixedUpdate step until jump_steps > JUMP_STEPS.
//
// Run:  dotnet run --project tools/gs-jump-sim

const float JumpSpeed = 16.5f;       // placeholder; game uses HeroController.JUMP_SPEED
const float Gravity = 60f;           // approximate; only for trajectory shape
const float FixedDt = 1f / 50f;      // Silksong-ish fixed step
const int JumpSteps = 12;            // JUMP_STEPS placeholder
const int JumpStepsMin = 4;

Console.WriteLine("=== Grounded Sprint Jump Gate Sim (HeroController decomp) ===\n");
Console.WriteLine("Gate: if (jumping && !dashing && !isSprinting) Jump();");
Console.WriteLine("Jump(): vy = JUMP_SPEED for jump_steps <= JUMP_STEPS\n");

var scenarios = new (string Name, bool IsSprintingDuringJump, bool ClearSprintBeforeFixedUpdate)[]
{
    ("A: normal ground jump (no sprint)", false, false),
    ("B: BUG — sprint jump, isSprinting stays true (old GS)", true, false),
    ("C: FIX — sprint jump, clear isSprinting before FixedUpdate (new GS)", true, true),
};

foreach (var s in scenarios)
{
    var r = Simulate(s.IsSprintingDuringJump, s.ClearSprintBeforeFixedUpdate);
    Console.WriteLine($"--- {s.Name} ---");
    Console.WriteLine($"  peakY={r.PeakY:F3}  maxVy={r.MaxVy:F3}  framesWithJumpApply={r.JumpApplyFrames}  airTime={r.AirFrames}");
    Console.WriteLine($"  first5 vy: {string.Join(", ", r.VyTrace.Take(5).Select(v => v.ToString("F2")))}");
    Console.WriteLine(r.PeakY < 0.5f ? "  RESULT: WEAK HOP (matches playtest)" : "  RESULT: normal jump height");
    Console.WriteLine();
}

// Downslash gate notes (not a full physics body, just documented conditions)
Console.WriteLine("=== Downslash / Downspike notes (from decomp) ===");
Console.WriteLine("CanAttackAction: blocks if hard_landing / !CanInput / downSpikeRecovery");
Console.WriteLine("Downspike FixedUpdate: if (cState.downSpiking) Downspike() sets vy = -DownspikeSpeed");
Console.WriteLine("If isSprinting holds controlReqlinquished, CanInput/CanAttack may fail.");
Console.WriteLine("GS fix: FixedUpdate prefix clears isSprinting in air + skips X-clamp during downspike.");

static SimResult Simulate(bool startSprinting, bool clearSprintBeforePhysics)
{
    bool jumping = true;
    bool dashing = false;
    bool isSprinting = startSprinting;
    int jumpSteps = 0;
    float y = 0f;
    float vy = 0f;
    float peakY = 0f;
    float maxVy = 0f;
    int jumpApply = 0;
    int air = 0;
    var vyTrace = new List<float>();

    // HeroJump: sets jumping=true, does NOT set vy. BecomeAirborne zeros negative y vel only.
    for (int i = 0; i < 80; i++)
    {
        // GS FixedUpdate PREFIX
        if (clearSprintBeforePhysics && (jumping || y > 0.001f))
            isSprinting = false;

        // FixedUpdate jump gate (exact decomp structure)
        if (jumping && !dashing && !isSprinting)
        {
            if (jumpSteps <= JumpSteps)
            {
                vy = JumpSpeed;
                jumpSteps++;
                jumpApply++;
            }
            else
            {
                jumping = false;
            }
        }

        // crude gravity when not applying jump
        if (!(jumping && !dashing && !isSprinting && jumpSteps <= JumpSteps + 1))
            vy -= Gravity * FixedDt;

        y += vy * FixedDt;
        if (y < 0f) { y = 0f; vy = 0f; break; }
        if (y > peakY) peakY = y;
        if (vy > maxVy) maxVy = vy;
        if (i < 20) vyTrace.Add(vy);
        air++;
    }

    return new SimResult(peakY, maxVy, jumpApply, air, vyTrace);
}

readonly record struct SimResult(float PeakY, float MaxVy, int JumpApplyFrames, int AirFrames, List<float> VyTrace);
