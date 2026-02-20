using HarmonyLib;
using ItemChanger.Modules;
using MonoMod.RuntimeDetour;
using PrepatcherPlugin;

namespace ItemChanger.Silksong.Modules.FastTravel;

/// <summary>
/// Module modifying the fast travel map buttons so that they can be unlocked by items.
/// </summary>
[SingletonModule]
public sealed class CustomFastTravelLocationsModule<TLocation> : Module where TLocation : struct, IComparable
{
    /// <summary>
    /// Locations that have been unlocked.
    /// </summary>
    public HashSet<string> UnlockedCustomLocations { get; } = [];

    public static string BoolStringPrefix => $"{nameof(CustomFastTravelLocationsModule<>)}__{typeof(TLocation).Name}";

    public static string GetBoolStringForLocation(TLocation location)
    {
        return $"{BoolStringPrefix}__{location}";
    }

    public Hook? _hook;

    protected override void DoLoad()
    {
        PlayerDataVariableEvents.OnGetBool += CatchCustomBoolGet;
        PlayerDataVariableEvents.OnSetBool += CatchCustomBoolSet;
        _hook = new(
            AccessTools.Method(typeof(FastTravelMapButtonBase<TLocation>), nameof(FastTravelMapButtonBase<>.Awake)),
            SetPlayerDataBoolString
            );
    }

    private static void SetPlayerDataBoolString(Action<FastTravelMapButtonBase<TLocation>> orig, FastTravelMapButtonBase<TLocation> self)
    {
        if (string.IsNullOrEmpty(self.playerDataBool))
        {
            self.playerDataBool = GetBoolStringForLocation(self.targetLocation);
        }
        
        orig(self);
    }

    private bool CatchCustomBoolGet(PlayerData pd, string fieldName, bool current)
    {
        // current will be false for any fake bool so it is fine 
        return current || UnlockedCustomLocations.Contains(fieldName);
    }

    private bool CatchCustomBoolSet(PlayerData pd, string fieldName, bool current)
    {
        if (fieldName.StartsWith(BoolStringPrefix))
        {
            if (current)
            {
                UnlockedCustomLocations.Add(fieldName);
            }
            else
            {
                UnlockedCustomLocations.Remove(fieldName);
            }
        }

        return current;
    }


    protected override void DoUnload()
    {
        PlayerDataVariableEvents.OnGetBool -= CatchCustomBoolGet;
        PlayerDataVariableEvents.OnGetBool -= CatchCustomBoolSet;
        _hook?.Dispose();
        _hook = null;
    }
}

public static class CustomFastTravelLocations
{
    /// <summary>
    /// Convenience method to invoke <see cref="CustomFastTravelLocationsModule{TLocation}.GetBoolStringForLocation(TLocation)"/>
    /// with type inference.
    /// </summary>
    public static string GetBoolStringForLocation<TLocation>(TLocation location) where TLocation : struct, IComparable
        => CustomFastTravelLocationsModule<TLocation>.GetBoolStringForLocation(location);
}
