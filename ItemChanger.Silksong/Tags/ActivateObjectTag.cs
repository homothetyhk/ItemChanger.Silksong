using ItemChanger.Extensions;
using ItemChanger.Tags;
using ItemChanger.Tags.Constraints;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Tags;

[LocationTag]
public class ActivateObjectTag : Tag
{
    public required string SceneName { get; init; }

    public required string ObjectName { get; init; }

    protected override void DoLoad(TaggableObject parent) => Using(new SceneEditGroup { { SceneName, ModifyScene } });

    private void ModifyScene(Scene scene)
    {
        GameObject? go = scene.FindGameObject(ObjectName);
        if (go != null)
            go.SetActive(true);
    }
}
