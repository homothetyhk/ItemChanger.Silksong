using ItemChanger.Extensions;
using ItemChanger.Locations;
using ItemChanger.Silksong.Components;
using ItemChanger.Tags;
using ItemChanger.Tags.Constraints;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Tags;

/// <summary>
/// Deactivate the listed object if the location's container does not match the expected container.
/// </summary>
[TagConstrainedTo<ContainerLocation>]
public class DeactivateIfUnexpectedContainerTag : Tag
{
    public required string SceneName { get; init; }

    public required string ObjectName { get; init; }

    public required string ExpectedContainerType { get; init; }

    private ContainerLocation? _location;

    protected override void DoLoad(TaggableObject parent)
    {
        _location = parent as ContainerLocation;
        ItemChangerHost.Singleton.GameEvents.AddSceneEdit(SceneName, DoDeactivate);
    }

    protected override void DoUnload(TaggableObject parent)
    {
        _location = null;
        ItemChangerHost.Singleton.GameEvents.RemoveSceneEdit(SceneName, DoDeactivate);
    }

    private void DoDeactivate(Scene scene)
    {
        GameObject? go = scene.FindGameObject(ObjectName);
        if (go == null)
        {
            return;
        }

        if (_location!.ChooseBestContainerType() != ExpectedContainerType)
        {
            go.AddComponent<Deactivator>();
        }
    }
}
