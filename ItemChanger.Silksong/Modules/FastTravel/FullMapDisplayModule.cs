using HarmonyLib;
using ItemChanger.Modules;
using MonoMod.RuntimeDetour;

namespace ItemChanger.Silksong.Modules.FastTravel;

// Note - this is being done like this for ease of implementation; the proper approach
// would split the set of locations into components (divided by the core piece) and show
// those with active locations in multiple components. But I don't think it's worth the effort to do this.

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
                if (self.GetComponentInParent<IFastTravelMap>() is FastTravelMapBase<TLocation>)
                {
                    return true;
                }

                return orig(self);
            });
    }

    protected override void DoUnload()
    {
        _hook?.Dispose();
        _hook = null;
    }
}
