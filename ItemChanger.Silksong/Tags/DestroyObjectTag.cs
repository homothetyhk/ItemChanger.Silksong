using ItemChanger.Extensions;
using ItemChanger.Tags;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Tags;

internal class DestroyObjectTag : Tag
{
    public required string SceneName { get; init; }

    public required string ObjectName { get; init; }

    protected override void DoLoad(TaggableObject parent)
    {
        ItemChangerHost.Singleton.GameEvents.AddSceneEdit(SceneName, DoDestroyObject);
    }

    protected override void DoUnload(TaggableObject parent)
    {
        ItemChangerHost.Singleton.GameEvents.RemoveSceneEdit(SceneName, DoDestroyObject);
    }

    private void DoDestroyObject(Scene scene)
    {
        if (scene.FindGameObject(ObjectName) is GameObject go)
        {
            UObject.Destroy(go);
        }
    }
}
