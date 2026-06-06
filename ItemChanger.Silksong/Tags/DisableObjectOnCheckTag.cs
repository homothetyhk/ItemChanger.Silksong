using ItemChanger.Events.Args;
using ItemChanger.Extensions;
using ItemChanger.Locations;
using ItemChanger.Serialization;
using ItemChanger.Tags;
using ItemChanger.Tags.Constraints;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Tags;

/// <summary>
/// Tag that disables a game object when the given location is checked.
/// </summary>
[LocationTag]
public class DisableObjectOnCheckTag : Tag
{
    [JsonIgnore] private Location? _location;

    public required string ObjectPath { get; init; }

    public IValueProvider<bool>? ShouldExecute { get; init; }

    protected override void DoLoad(TaggableObject parent)
    {
        _location = (parent as Location)!;
        Using(new SceneEditGroup() { { _location.UnsafeSceneName, DisableObjectOnSceneLoad } });
        _location.Placement!.OnVisited += DisableObjectOnCheckLocation;
    }

    protected override void DoUnload(TaggableObject parent)
    {
        _location!.Placement!.OnVisited -= DisableObjectOnCheckLocation;
        _location = null;
    }

    private void DisableObjectOnSceneLoad(Scene scene)
    {
        if (ShouldExecute?.Value == false) return;

        GameObject? go = scene.FindGameObject(ObjectPath);
        if (go == null)
        {
            return;
        }

        if (_location!.Placement!.AllObtained())
        {
            go.SetActive(false);
        }
    }

    private void DisableObjectOnCheckLocation(PlacementVisitedEventArgs args)
    {
        if (ShouldExecute?.Value == false) return;

        if (args.ProposedNewFlags.HasFlag(Enums.VisitState.ObtainedAnyItem))
        {
            GameObject? go = UnityEngine.SceneManagement.SceneManager.GetActiveScene().FindGameObject(ObjectPath);
            if (go != null)
            {
                go.SetActive(false);
            }
        }
    }
}
