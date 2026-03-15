using GlobalEnums;
using HarmonyLib;
using ItemChanger.Modules;
using Mono.Cecil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using Silksong.UnityHelper.Extensions;  // Transitive reference from ModMenu

namespace ItemChanger.Silksong.Modules.FastTravel;

/// <summary>
/// Module modifying the fast travel map to allow for travel from locked source locations.
/// </summary>
/// <typeparam name="TLocation">Enumeration of fast travel locations.</typeparam>
/// <typeparam name="TLockerComponent">Non-generic subclass of <see cref="LocationLocker{TLocation}"/>.</typeparam>
public sealed class FastTravelSourceModule<TLocation, TLockerComponent> : FastTravelSourceModule<TLocation>
    where TLocation : struct, IComparable
    where TLockerComponent : LocationLocker<TLocation>
{
    protected override void DoLoad()
    {
        Using(new ILHook(
            AccessTools.Method(typeof(FastTravelMapButtonBase<TLocation>), nameof(FastTravelMapButtonBase<>.IsCurrentLocation)),
            RemoveCircularReference
            ));

        Using(new Hook(
            AccessTools.Method(typeof(FastTravelMapButtonBase<TLocation>), nameof(FastTravelMapButtonBase<>.IsUnlocked)),
            AutoUnlockCurrentLocation
            ));

        Using(new Hook(
            AccessTools.Method(typeof(FastTravelMapButtonBase<TLocation>), nameof(FastTravelMapButtonBase<>.Awake)),
            LockLockedLocationButton
            ));
    }

    protected override void DoUnload() { }

    private static void LockLockedLocationButton(
        Action<FastTravelMapButtonBase<TLocation>> orig, FastTravelMapButtonBase<TLocation> self)
    {
        orig(self);
        self.gameObject.GetOrAddComponent<TLockerComponent>();
    }

    private static void RemoveCircularReference(ILContext il)
    {
        ILCursor cursor = new(il);

        while (cursor.TryGotoNext(
            MoveType.Before,
            // TODO - match on type as well? Requires fiddling with the generics
            i => i.MatchCallOrCallvirt(out MethodReference mref) && mref.Name == nameof(FastTravelMapButtonBase<>.IsUnlocked)
        ))
        {
            cursor.Remove();
            // Ignore checks for IsUnlocked in this function by replacing with "true"
            cursor.EmitDelegate<Func<FastTravelMapButtonBase<TLocation>, bool>>(self => true);
        }
    }

    private static bool AutoUnlockCurrentLocation(
        Func<FastTravelMapButtonBase<TLocation>, bool> orig, FastTravelMapButtonBase<TLocation> self)
    {
        return orig(self) || self.IsCurrentLocation();
    }
}

// Singleton per TLocation, regardless of any other type parameters
[SingletonModule]
public abstract class FastTravelSourceModule<TLocation> : Module where TLocation : struct, IComparable { }

public static class FastTravelSourceModule
{
    // Unity doesn't let us add generic components so we need concrete subclasses
    private class BellwayLocker : LocationLocker<FastTravelLocations> { }
    private class VentricaLocker : LocationLocker<TubeTravelLocations> { }

    public static Type BellwayType => typeof(FastTravelSourceModule<FastTravelLocations, BellwayLocker>);
    public static Type VentricaType => typeof(FastTravelSourceModule<TubeTravelLocations, VentricaLocker>);
}
