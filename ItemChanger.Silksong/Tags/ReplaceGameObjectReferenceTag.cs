using ItemChanger.Silksong.Serialization;
using ItemChanger.Tags;
using ItemChanger.Tags.Constraints;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Tags;

/// <summary>
/// Update a writable GameObject reference with the new container on replacement.
/// </summary>
[LocationTag]
public class ReplaceGameObjectReferenceTag : Tag, IActionOnContainerReplaceTag
{
    public required ISceneScopedWritableValueProvider<GameObject?> Reference { get; init; }

    public void OnReplace(Scene scene, GameObject newContainer) => Reference.Set(scene, newContainer);
}
