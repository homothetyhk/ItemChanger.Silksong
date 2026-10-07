using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Serialization;

/// <summary>
/// An writable value provider which requires a Scene context to render its value.
/// The scene must be explicitly provided because the name is not sufficient in cases where two scenes of the same name are being unloaded/loaded.
/// </summary>
public interface ISceneScopedWritableValueProvider<T> : ISceneScopedValueProvider<T>
{
    void Set(Scene scene, T value);
}
