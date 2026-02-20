using HarmonyLib;
using ItemChanger.Modules;
using MonoMod.RuntimeDetour;

namespace ItemChanger.Silksong.Modules.FastTravel;

// Note - this is being done like this for ease of implementation; the proper approach
// would split the set of locations into components (divided by the core piece) and show
// those with active locations in multiple components. But I don't think it's worth the effort to do this.
// Note - for the most part the background is part of the MapPiece sprite rather than the MapCorePiece sprite,
// so it's hard to actually show the full map. But it doesn't matter...
// Perhaps it would be better to kill this module entirely...

/// <summary>
/// Module to display the full map when opening a fast travel map.
/// </summary>
[SingletonModule]
public sealed class FullMapDisplayModule<TLocation> : Module where TLocation : struct, IComparable
{
    private Hook? _hook;

    protected override void DoLoad()
    {
        _hook = new(
            AccessTools.PropertyGetter(typeof(FastTravelMapCorePiece), nameof(FastTravelMapCorePiece.IsVisible)),
            static (Func<FastTravelMapCorePiece, bool> orig, FastTravelMapCorePiece self) =>
            {
                ItemChangerPlugin.Instance.Logger.LogInfo($"Checking {self.gameObject.name} as {typeof(TLocation).Name}");

                if (self.GetComponentInParent<IFastTravelMap>() is FastTravelMapBase<TLocation>)
                {
                    ItemChangerPlugin.Instance.Logger.LogInfo($"Marked as true");
                    return true;
                }

                ItemChangerPlugin.Instance.Logger.LogInfo($"Skipped");
                return orig(self);
            });
    }

    protected override void DoUnload()
    {
        _hook?.Dispose();
        _hook = null;
    }
}
