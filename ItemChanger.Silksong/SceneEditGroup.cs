using System.Collections;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong;

/// <summary>
/// Object to manage hooking and unhooking a group of scene edits.
/// </summary>
public sealed class SceneEditGroup : IDisposable, IEnumerable<(string, Action<Scene>)>
{
    private readonly List<(string, Action<Scene>)> edits = [];

    public void Add(string sceneName, Action<Scene> edit)
    {
        edits.Add((sceneName, edit));
        SilksongHost.Instance.GameEvents.AddSceneEdit(sceneName, edit);
    }

    /// <summary>
    /// Removes all edits associated with the group.
    /// </summary>
    public void Dispose()
    {
        foreach ((var sceneName, var edit) in edits) SilksongHost.Instance.GameEvents.RemoveSceneEdit(sceneName, edit);
    }

    IEnumerator<(string, Action<Scene>)> IEnumerable<(string, Action<Scene>)>.GetEnumerator() => edits.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => edits.GetEnumerator();
}
