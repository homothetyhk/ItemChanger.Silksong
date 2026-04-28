using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong;

public class DisposableSceneEdit : IDisposable
{
    private readonly string _sceneName;
    private readonly Action<Scene> _action;

    public DisposableSceneEdit(string sceneName, Action<Scene> action)
    {
        _sceneName = sceneName;
        _action = action;
        ItemChangerHost.Singleton.GameEvents.AddSceneEdit(_sceneName, _action);
    }

    public void Dispose()
    {
        ItemChangerHost.Singleton.GameEvents.RemoveSceneEdit(_sceneName, _action);
    }
}
