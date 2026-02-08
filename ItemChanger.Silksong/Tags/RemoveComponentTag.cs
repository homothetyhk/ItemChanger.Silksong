using ItemChanger.Extensions;
using ItemChanger.Tags;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Tags;

internal class RemoveComponentTag<T> : Tag where T : Component
{
    public required string SceneName { get; init; }

    public required string ObjectName { get; init; }

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
