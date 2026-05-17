using HarmonyLib;
using ItemChanger.Modules;
using Newtonsoft.Json;
using UnityEngine;

namespace ItemChanger.Silksong.Modules;

/// <summary>
/// Centralized clock for tracking how much time has passed in the save file.
/// </summary>
public sealed class ClockModule : Module
{
    // We don't know what sort of sub-millisecond precision Unity gives us, so we use a float for short-range accuracy and a long for long-range stability.
    [JsonProperty] private long ElapsedMinutes;
    [JsonProperty] private float ElapsedSeconds;

    [JsonIgnore]
    public long Millis => ElapsedMinutes * 60_000 + Mathf.FloorToInt(ElapsedSeconds * 1000);

    private readonly Harmony harmony = new(typeof(ClockPatches).FullName);

    protected override void DoLoad()
    {
        harmony.PatchAll(typeof(ClockPatches));
        ClockPatches.OnUpdate += Update;
    }

    protected override void DoUnload()
    {
        ClockPatches.OnUpdate -= Update;
        harmony.UnpatchSelf();
    }

    private void Update()
    {
        ElapsedSeconds += Time.deltaTime;

        int minutes = Mathf.FloorToInt(ElapsedSeconds / 60);
        if (minutes > 0)
        {
            ElapsedMinutes += minutes;
            ElapsedSeconds -= minutes * 60;
        }
    }
}

[HarmonyPatch]
file static class ClockPatches
{
    internal static Action? OnUpdate;

    [HarmonyPatch(typeof(GameManager), nameof(GameManager.Update))]
#pragma warning disable IDE0060 // Remove unused parameter
    private static void Postfix(GameManager __instance) => OnUpdate?.Invoke();
#pragma warning restore IDE0060 // Remove unused parameter
}
