using ItemChanger.Tags;
using ItemChanger.Tags.Constraints;
using ItemChanger.Extensions;
using UnityEngine.SceneManagement;
using ItemChanger.Locations;
using ItemChanger.Silksong.Components;

namespace ItemChanger.Silksong.Tags;

[LocationTag]
internal class DeactivateIfPlacementCheckedTag : Tag
{
    public required string SceneName { get; init; }

    public required string ObjectName { get; init; }

    private Location? _location;

    protected override void DoLoad(TaggableObject parent)
    {
        ItemChangerHost.Singleton.GameEvents.AddSceneEdit(SceneName, DoDeactivate);
        _location = (parent as Location);
    }

    protected override void DoUnload(TaggableObject parent)
    {
        ItemChangerHost.Singleton.GameEvents.RemoveSceneEdit(SceneName, DoDeactivate);
        _location = null;
    }

    private void DoDeactivate(Scene scene)
    {
        GameObject? go = scene.FindGameObject(ObjectName);
        if (go == null)
        {
            return;
        }

        if (_location!.Placement!.AllObtained())
        {
            go.AddComponent<Deactivator>();
        }
    }
}
