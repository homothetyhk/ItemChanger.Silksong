using BepInEx;
using Silksong.DataManager;
using System.Collections;
using System.Reflection;

namespace ItemChanger.Silksong;

// TODO: Support this upstream, make DataManager better support hot reloads.
internal static class DataManagerHotReload
{
    private static readonly PropertyInfo dataManagerInstanceProperty = typeof(DataManagerPlugin).GetProperty("Instance", BindingFlags.NonPublic | BindingFlags.Static);
    private static DataManagerPlugin DataManagerInstance => (DataManagerPlugin)dataManagerInstanceProperty.GetValue(null);

    private static readonly FieldInfo managedModsField = typeof(DataManagerPlugin).GetField("ManagedMods", BindingFlags.NonPublic | BindingFlags.Instance);
    private static IDictionary ManagedMods => (IDictionary)managedModsField.GetValue(DataManagerInstance);

    private static readonly MethodInfo tryCreateMethod = typeof(DataManagerPlugin).Assembly.GetType("Silksong.DataManager.ManagedMod").GetMethod("TryCreate", BindingFlags.NonPublic | BindingFlags.Static);

    private static bool CreateManagedMod(BaseUnityPlugin plugin, out object managedMod)
    {
        object?[] args = [plugin, null];
        bool success = (bool)tryCreateMethod.Invoke(null, args);

        managedMod = args[1]!;
        return success;
    }

    internal static void AddManagedMod(BaseUnityPlugin plugin)
    {
        if (CreateManagedMod(plugin, out var managedMod))
            ManagedMods[plugin.Info.Metadata.GUID] = managedMod;
    }

    internal static void RemoveManagedMod(BaseUnityPlugin plugin) => ManagedMods.Remove(plugin.Info.Metadata.GUID);
}
