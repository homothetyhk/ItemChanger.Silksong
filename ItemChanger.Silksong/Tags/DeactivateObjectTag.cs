using ItemChanger.Tags;
using ItemChanger.Tags.Constraints;
using ItemChanger.Extensions;
using UnityEngine.SceneManagement;
using ItemChanger.Locations;
using ItemChanger.Silksong.Components;
using ItemChanger.Serialization;

namespace ItemChanger.Silksong.Tags;

/// <summary>
/// Deactivate the specified object, subject to an optional condition.
/// </summary>
[LocationTag]
internal class DeactivateObjectTag : Tag
{
    public required string SceneName { get; init; }

    public required string ObjectName { get; init; }

    public IValueProvider<bool> Test { get; init; } = new BoxedBool { Value = true };

    protected override void DoLoad(TaggableObject parent) => Using(new SceneEditGroup { { SceneName, DoDeactivate } });

    private void DoDeactivate(Scene scene)
    {
        if (!Test.Value)
        {
            return;
        }

        GameObject? go = scene.FindGameObject(ObjectName);
        if (go == null)
        {
            return;
        }

        go.AddComponent<Deactivator>();
    }
}
