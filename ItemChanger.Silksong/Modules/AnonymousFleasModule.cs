using HarmonyLib;
using ItemChanger.Modules;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using Newtonsoft.Json;
using TeamCherry.SharedUtils;

namespace ItemChanger.Silksong.Modules;

/// <summary>
/// Module recording the number of fleas saved that are not associated to a particular scene.
/// </summary>
[SingletonModule]
public sealed class AnonymousFleasModule : Module
{
    /// <summary>
    /// The number of anonymous fleas available in the file.
    /// </summary>
    [JsonIgnore]  // Ignore from json because it's set by the flea items as they load
    public int AnonymousFleas { get; set; } = 0;
    
    /// <summary>
    /// The number of anonymous fleas which have been saved.
    /// </summary>
    public int AnonymousFleasSaved { get; set; } = 0;

    /// <summary>
    /// Vanilla flea locations which have been replaced in the file,
    /// and should be excluded from the total.
    /// </summary>
    [JsonIgnore]  // Ignore from json because it's set by the location tags as they load
    public int RemovedVanillaFleas { get; set; } = 0;

    private readonly List<IDisposable> _hooks = [];

    protected override void DoLoad()
    {
        _hooks.Add(
            new Hook(
                AccessTools.PropertyGetter(typeof(PlayerData), nameof(PlayerData.SavedFleasCount)),
                (Func<PlayerData, int> orig, PlayerData self) => orig(self) + AnonymousFleasSaved
            ));
        _hooks.Add(
            new ILHook(
                AccessTools.Method(typeof(SavedFleaActivator), nameof(SavedFleaActivator.Start)),
                ModifySavedFleaActivator
                ));
        _hooks.Add(
            new Hook(
                AccessTools.Method(typeof(QuestTargetPlayerDataBools), nameof(QuestTargetPlayerDataBools.GetCounts)),
                AddAnonymousFleasToCounts
            ));
    }

    private delegate void On_QuestTargetPlayerDataBools_orig_GetCounts(QuestTargetPlayerDataBools self, out int completed, out int total);

    private void AddAnonymousFleasToCounts(On_QuestTargetPlayerDataBools_orig_GetCounts orig, QuestTargetPlayerDataBools self, out int completed, out int total)
    {
        orig(self, out completed, out total);
        if (self.pdFieldTemplate != "SavedFlea_")
        {
            return;
        }

        completed += AnonymousFleasSaved;
        total += AnonymousFleas;
        total -= RemovedVanillaFleas;
    }

    private void ModifySavedFleaActivator(ILContext il)
    {
        ILCursor cursor = new(il);
        cursor.GotoNext(i => i.MatchCall(typeof(VariableExtensions), nameof(VariableExtensions.GetVariables)));
        cursor.GotoNext(MoveType.After, i => i.MatchCall(typeof(Enumerable), nameof(Enumerable.Count)));
        cursor.Emit(OpCodes.Ldarg_0);
        cursor.EmitDelegate<Func<int, SavedFleaActivator, int>>((x, sfa) =>
        {
            if (sfa.pdBoolTemplate == "SavedFlea_")
            {
                return x + AnonymousFleasSaved;
            }
            return x;
        });
    }

    protected override void DoUnload()
    {
        foreach (IDisposable hook in _hooks)
        {
            hook?.Dispose();
        }
        _hooks.Clear();
    }
}
