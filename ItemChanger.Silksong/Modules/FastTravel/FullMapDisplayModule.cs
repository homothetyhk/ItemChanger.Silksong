using HarmonyLib;
using ItemChanger.Modules;
using MonoMod.RuntimeDetour;
using TeamCherry.NestedFadeGroup;
using UnityEngine;

namespace ItemChanger.Silksong.Modules.FastTravel;

// Note - this is being done like this for ease of implementation; the proper approach
// would split the set of locations into components (divided by the core piece) and show
// those with active locations in multiple components. But I don't think it's worth the effort to do this.
// Note - for the most part the background is part of the MapPiece sprite rather than the MapCorePiece sprite,
// so both kinds of piece need to be forced visible to actually show the full map.

/// <summary>
/// Module to display the full map when opening a fast travel map.
/// Pieces of the map belonging to locations which are not unlocked are dimmed.
/// </summary>
[SingletonModule]
public sealed class FullMapDisplayModule<TLocation> : Module where TLocation : struct, IComparable
{
    /// <summary>
    /// Opacity of map pieces whose location is not unlocked.
    /// </summary>
    private const float LockedPieceAlpha = 0.4f;

    protected override void DoLoad()
    {
        Using(new Hook(
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
            }));
        Using(new Hook(
            AccessTools.Method(typeof(FastTravelMapBase<TLocation>), nameof(FastTravelMapBase<>.Open)),
            ShowAllMapPieces
            ));
    }

    protected override void DoUnload() { }

    private static void ShowAllMapPieces(Action<FastTravelMapBase<TLocation>> orig, FastTravelMapBase<TLocation> self)
    {
        orig(self);

        // Each MapPiece is deactivated during Opened unless its paired button is unlocked, so reactivate them afterwards.
        // The indicator for the current location is still only placed by the vanilla handler.
        foreach (IFastTravelMapPiece piece in self.GetComponentsInChildren<IFastTravelMapPiece>(includeInactive: true))
        {
            if (piece is not Component component || piece is FastTravelMapCorePiece)
            {
                continue;
            }

            component.gameObject.SetActive(true);

            // For a MapPiece, IsVisible is whether its paired button is unlocked. The map is reused between openings,
            // so unlocked pieces must be reset in case they were dimmed previously.
            if (component.GetComponent<NestedFadeGroupSpriteRenderer>() is NestedFadeGroupSpriteRenderer fade)
            {
                fade.AlphaSelf = piece.IsVisible ? 1f : LockedPieceAlpha;
            }
        }
    }
}
