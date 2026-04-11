using UnityEngine;

namespace ItemChanger.Silksong.Components;

/// <summary>
/// A component which performs an FSM edit exactly once, when OnEnable is first called.
/// <br/>Useful to run edits after FSMs Awake.
/// If a GameObject is inactive when a scene is loaded, its FSM has not yet run Awake, so edits can be erased by a subsequent template load.
/// </summary>
[DefaultExecutionOrder(5)]
internal class EditFsmOnEnable : MonoBehaviour
{
    private bool applied = false;
    /// <summary>
    /// The name of the fsm to edit, on the same GameObject as the EditFsmOnEnable component.
    /// </summary>
    public required string FsmName { get; set; }
    /// <summary>
    /// The action to run in OnEnable on the specified FSM.
    /// </summary>
    public required Action<PlayMakerFSM> Edit { get; set; }

    public void OnEnable()
    {
        if (applied) return;

        PlayMakerFSM fsm = gameObject.LocateMyFSM(FsmName);
        if (!fsm)
        {
            LogWarn($"Failed to find fsm {FsmName} on {gameObject.name} for deferred fsm edit!");
            return;
        }

        try
        {
            Edit(fsm);
            applied = true;
        }
        catch (Exception e)
        {
            LogError($"Error applying deferred fsm edit to fsm {FsmName} on {gameObject.name}:\n{e}");
        }
    }
}
