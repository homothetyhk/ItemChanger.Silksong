using ItemChanger.Extensions;
using ItemChanger.Serialization;
using ItemChanger.Tags;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Tags;

internal class RemoveComponentTag<T> : Tag where T : Component
{
    public required string SceneName { get; init; }

    public required string ObjectName { get; init; }

    public IValueProvider<bool> Test { get; init; } = new BoxedBool { Value = true };

    protected override void DoLoad(TaggableObject parent)
    {
        ItemChangerHost.Singleton.GameEvents.AddSceneEdit(SceneName, DoRemoveComponent);
    }

    protected override void DoUnload(TaggableObject parent)
    {
        ItemChangerHost.Singleton.GameEvents.RemoveSceneEdit(SceneName, DoRemoveComponent);
    }

    private void DoRemoveComponent(Scene scene)
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

        foreach (T component in go.GetComponents<T>())
        {
            UObject.Destroy(component);
        }
    }
}
