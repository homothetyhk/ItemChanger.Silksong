using HarmonyLib;
using System.Collections;

namespace ItemChanger.Silksong;

/// <summary>
/// Object to manage applying and unapplying a set of HarmonyPatches, organized by type.
/// </summary>
public sealed class HarmonyPatchGroup : IDisposable, IEnumerable<Type>
{
    private readonly List<Type> types = [];
    private Harmony? harmony;

    public void Add(Type type)
    {
        types.Add(type);

        harmony ??= new(type.FullName);
        harmony.PatchAll(type);
    }

    /// <summary>
    /// Removes all patches associated with the group.
    /// </summary>
    public void Dispose() => harmony?.UnpatchSelf();

    public IEnumerator<Type> GetEnumerator() => types.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => types.GetEnumerator();
}
