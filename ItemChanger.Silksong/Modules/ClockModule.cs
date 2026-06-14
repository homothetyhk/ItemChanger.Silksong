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

    private ClockBehaviour? behaviour;

    protected override void DoLoad()
    {
        GameObject obj = new("ClockModule");
        UObject.DontDestroyOnLoad(obj);
        behaviour = obj.AddComponent<ClockBehaviour>();
        behaviour.Module = this;
    }

    protected override void DoUnload()
    {
        if (behaviour != null && behaviour.gameObject != null)
            UObject.Destroy(behaviour.gameObject);
    }

    private class ClockBehaviour : MonoBehaviour
    {
        public required ClockModule? Module;

        private void Update()
        {
            if (Module == null) return;

            Module.ElapsedSeconds += Time.deltaTime;

            int minutes = Mathf.FloorToInt(Module.ElapsedSeconds / 60);
            if (minutes > 0)
            {
                Module.ElapsedMinutes += minutes;
                Module.ElapsedSeconds -= minutes * 60;
            }
        }
    }
}
