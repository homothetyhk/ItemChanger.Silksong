using HarmonyLib;
using ItemChanger.Modules;
using MonoMod.RuntimeDetour;
using Newtonsoft.Json;
using URandom = UnityEngine.Random;

namespace ItemChanger.Silksong.Modules;

/// <summary>
/// Replaces the Mist's per-entry random room/door selection with a deterministic draw
/// seeded by <see cref="ConsistentRandomnessModule"/>. Opt-in; not added by default.
/// Mist scenes are the Dust_Maze_* family; RNG is driven by <c>MazeController.LinkDoors</c>.
/// </summary>
[SingletonModule]
public sealed class SeededMistModule : Module
{
    /// <summary>
    /// Extra salt mixed into the RNG key. Lets rando partition the seed space
    /// without touching <see cref="ConsistentRandomnessModule.Seed"/>.
    /// </summary>
    public string KeySalt { get; set; } = "";

    /// <summary>
    /// Bumped every time <c>MazeController.LinkDoors</c> runs while this module is loaded.
    /// Persisted so save/reload mid-Mist keeps the same sequence.
    /// </summary>
    public int VisitCounter { get; set; } = 0;

    [JsonIgnore]
    private Hook? linkDoorsHook;

    [JsonIgnore]
    private int reentryDepth;

    protected override void DoLoad()
    {
        var method = AccessTools.Method(typeof(MazeController), "LinkDoors");
        if (method == null)
        {
            ItemChangerPlugin.Instance.Logger.LogWarning(
                "SeededMistModule: MazeController.LinkDoors not found; module inert.");
            return;
        }
        linkDoorsHook = new Hook(method, LinkDoorsDetour);
        Using(linkDoorsHook);
    }

    protected override void DoUnload() { }

    // Signature: void LinkDoors(IReadOnlyList<TransitionPoint> totalDoors)
    private delegate void LinkDoorsOrig(MazeController self, IReadOnlyList<TransitionPoint> totalDoors);

    private void LinkDoorsDetour(LinkDoorsOrig orig, MazeController self, IReadOnlyList<TransitionPoint> totalDoors)
    {
        // LinkDoors recurses on door-link failure; only seed the outermost call.
        if (reentryDepth > 0)
        {
            orig(self, totalDoors);
            return;
        }

        VisitCounter++;

        string sceneName = self.gameObject.scene.name;
        PlayerData pd = PlayerData.instance;
        string entryGate = $"{pd.MazeEntranceScene}->{pd.MazeEntranceDoor}";
        int draw = ChooseSeed($"{sceneName} // {entryGate} // visit={VisitCounter}");

        var prev = URandom.state;
        URandom.InitState(draw);
        reentryDepth++;
        try
        {
            orig(self, totalDoors);
        }
        finally
        {
            reentryDepth--;
            URandom.state = prev;
        }
    }

    /// <summary>
    /// Public for testing / connection mods that want to drive the same draw.
    /// Returns a deterministic 32-bit hash for the given room key.
    /// </summary>
    public int ChooseRoom(string entryGate, int roomCount)
    {
        return ItemChangerHost.Singleton.ActiveProfile!
            .Modules.GetOrAdd<ConsistentRandomnessModule>()
            .Choose($"Mist // {KeySalt} // {entryGate} // visit={VisitCounter}", 0, roomCount - 1);
    }

    private int ChooseSeed(string key)
    {
        // CRM.Choose computes (max - min + 1) which overflows to 0 at the full int range.
        // Cap to non-negative; UnityEngine.Random.InitState takes any int, so half range is fine.
        return ItemChangerHost.Singleton.ActiveProfile!
            .Modules.GetOrAdd<ConsistentRandomnessModule>()
            .Choose($"Mist // {KeySalt} // {key}", 0, int.MaxValue);
    }
}
