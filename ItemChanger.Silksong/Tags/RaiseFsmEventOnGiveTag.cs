using ItemChanger.Enums;
using ItemChanger.Events.Args;
using ItemChanger.Extensions;
using ItemChanger.Locations;
using ItemChanger.Placements;
using ItemChanger.Tags;
using ItemChanger.Tags.Constraints;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Tags;

/// <summary>
/// Tag which raises a FSM event when a location's item is given.
/// </summary>
[LocationTag]
public class RaiseFsmEventOnGiveTag : Tag, IActionOnContainerReplaceTag
{
    public required string SceneName { get; init; }
    public required string ObjectPath { get; init; }
    
    /// <summary>
    /// Name of the PlayMakerFSM component on the tagged GameObject to execute the event on. If null, selects the
    /// first available such component.
    /// </summary>
    public string? FsmName { get; init; } = null;
    
    /// <summary>
    /// Name of the FSM event to invoke.
    /// </summary>
    public required string Event { get; init; }
    
    /// <summary>
    /// Only raise the FSM event if the location was replaced with a container.
    /// </summary>
    public bool OnlyIfContainerReplaced { get; init; } = false;

    private PlayMakerFSM? _fsm;
    private bool _wasReplaced = false;

    protected override void DoLoad(TaggableObject parent)
    {
        Placement? placement = (parent as Location)?.Placement;
        if (placement != null)
        {
            placement.OnVisited += OnVisited;
        }
        else
        {
            LogInfo($"Not a valid location with placement: {parent}");
        }

        ItemChangerHost.Singleton.GameEvents.AddSceneEdit(SceneName, FindFsm);
    }

    protected override void DoUnload(TaggableObject parent)
    {
        Placement? placement = (parent as Location)?.Placement;
        if (placement != null)
        {
            placement.OnVisited -= OnVisited;
        }

        ItemChangerHost.Singleton.GameEvents.RemoveSceneEdit(SceneName, FindFsm);
        _fsm = null;
    }

    public void OnReplace(Scene scene, GameObject newContainer)
    {
        _wasReplaced = true;
    }
    
    private void FindFsm(Scene scene)
    {
        GameObject? fsmGameObject = scene.FindGameObject(ObjectPath);
        if (fsmGameObject == null)
        {
            LogWarn($"FSM game object {ObjectPath} not found.");
            return;
        }

        _fsm = FsmName == null ? fsmGameObject.GetComponent<PlayMakerFSM>() : fsmGameObject.LocateMyFSM(FsmName);

        if (_fsm == null)
        {
            LogWarn($"FSM component on game object {ObjectPath} not found.");
        }
    }

    private void OnVisited(PlacementVisitedEventArgs args)
    {
        if (OnlyIfContainerReplaced && !_wasReplaced)
            return;
        if ((args.ProposedNewFlags & VisitState.ObtainedAnyItem) == 0)
            return;
        if (_fsm == null)
            return;
        _fsm.SendEvent(Event);
    }
}