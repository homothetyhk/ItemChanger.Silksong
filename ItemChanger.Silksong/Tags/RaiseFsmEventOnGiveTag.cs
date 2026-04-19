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
public class RaiseFsmEventOnGiveTag : Tag
{
    public required string SceneName { get; init; }
    public required string ObjectPath { get; init; }
    public required string Event { get; init; }

    private PlayMakerFSM? _fsm;

    protected override void DoLoad(TaggableObject parent)
    {
        Placement? placement = (parent as Location)?.Placement;
        if (placement != null)
        {
            placement.OnVisitStateChanged += OnVisitStateChanged;
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
            placement.OnVisitStateChanged -= OnVisitStateChanged;
        }
        
        ItemChangerHost.Singleton.GameEvents.RemoveSceneEdit(SceneName, FindFsm);
        _fsm = null;
    }

    private void OnVisitStateChanged(VisitStateChangedEventArgs obj)
    {
        if (
            obj.NewFlags.HasFlag(VisitState.ObtainedAnyItem)
            && !obj.Orig.HasFlag(VisitState.ObtainedAnyItem)
        )
        {
            RaiseEvent();
        }
    }

    private void FindFsm(Scene scene)
    {
        GameObject? fsmGameObject = scene.FindGameObject(ObjectPath);
        if (fsmGameObject == null)
        {
            LogWarn($"FSM game object {ObjectPath} not found.");
            return;
        }
        
        _fsm = fsmGameObject.GetComponent<PlayMakerFSM>();
        if (_fsm == null)
        {
            LogWarn($"FSM component on game object {ObjectPath} not found.");
        }
    }

    private void RaiseEvent()
    {
        if (_fsm == null)
            return;
        
        _fsm.SendEvent(Event);
    }
}