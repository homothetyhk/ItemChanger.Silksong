using GlobalEnums;
using HarmonyLib;
using ItemChanger.Modules;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using Silksong.UnityHelper.Extensions;  // Transitive reference from ModMenu
using TeamCherry.SharedUtils;
using UnityEngine;
using Mono.Cecil;

namespace ItemChanger.Silksong.Modules.FastTravel;

public class LocationLocker<TLocation> : MonoBehaviour where TLocation : struct, IComparable
{
    private FastTravelMapButtonBase<TLocation> _buttonComponent;
    private FastTravelMapBase<TLocation> _mapComponent;
    private UISelectionListItem _uiListItemComponent;
    private TMProOld.TextMeshPro _textComponent;

    void Awake()
    {
        _buttonComponent = GetComponent<FastTravelMapButtonBase<TLocation>>();

        if (_buttonComponent == null)
        {
            Destroy(this);
            return;
        }

        _mapComponent = GetComponentInParent<FastTravelMapBase<TLocation>>();
        _uiListItemComponent = GetComponent<UISelectionListItem>();
        _textComponent = gameObject.FindChild("Text")!.GetComponent<TMProOld.TextMeshPro>();

        _mapComponent.Opening += SetButtonLockedState;
    }

    private bool ShouldLockButton()
    {
        string pdBool = _buttonComponent.playerDataBool;

        // The original code for IsUnlocked, regardless of the value of IsCurrentLocation
        return !(string.IsNullOrEmpty(pdBool) || PlayerData.instance.GetVariable<bool>(pdBool));
    }

    private void SetButtonLockedState()
    {
        if (ShouldLockButton())
        {
            _textComponent.color = Color.gray;
            _uiListItemComponent.InactiveConditionText = () => "LOCKED";  // This text is never displayed, but needs to be nonempty
        }

        else
        {
            _textComponent.color = Color.white;
            _uiListItemComponent.InactiveConditionText = null;
        }
    }
}

// Singleton per TLocation, regardless of any other type parameters
// TODO - uncomment SingletonModule when IC.Core updates
// [SingletonModule]
public abstract class FastTravelSourceModule<TLocation> : Module where TLocation : struct, IComparable { }

/// <summary>
/// Module modifying the fast travel map to allow for travel from locked source locations.
/// </summary>
/// <typeparam name="TLocation">Enumeration of fast travel locations.</typeparam>
/// <typeparam name="TLockerComponent">Non-generic subclass of <see cref="LocationLocker{TLocation}"/>.</typeparam>
public sealed class FastTravelSourceModule<TLocation, TLockerComponent> : FastTravelSourceModule<TLocation>
    where TLocation : struct, IComparable
    where TLockerComponent : LocationLocker<TLocation>
{
    private readonly List<IDisposable> _hooks = [];

    protected override void DoLoad()
    {
        _hooks.Add(new ILHook(
            AccessTools.Method(typeof(FastTravelMapButtonBase<TLocation>), nameof(FastTravelMapButtonBase<>.IsCurrentLocation)),
            RemoveCircularReference
            ));

        _hooks.Add(new Hook(
            AccessTools.Method(typeof(FastTravelMapButtonBase<TLocation>), nameof(FastTravelMapButtonBase<>.IsUnlocked)),
            AutoUnlockCurrentLocation
            ));

        _hooks.Add(new Hook(
            AccessTools.Method(typeof(FastTravelMapButtonBase<TLocation>), nameof(FastTravelMapButtonBase<>.Awake)),
            LockLockedLocationButton
            ));
    }

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

    protected override void DoUnload()
    {
        foreach (IDisposable hook in _hooks)
        {
            hook.Dispose();
        }

        _hooks.Clear();
    }

}

public static class FastTravelSourceModule
{
    // Unity doesn't let us add generic components so we need concrete subclasses
    private class BellwayLocker : LocationLocker<FastTravelLocations> { }
    private class VentricaLocker : LocationLocker<TubeTravelLocations> { }

    public static Type BellwayType => typeof(FastTravelSourceModule<FastTravelLocations, BellwayLocker>);
    public static Type VentricaType => typeof(FastTravelSourceModule<TubeTravelLocations, VentricaLocker>);
}
