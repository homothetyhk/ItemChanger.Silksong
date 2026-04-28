using ItemChanger.Enums;
using UnityEngine;

namespace ItemChanger.Silksong.UIDefs;

/// <summary>
/// UIDef that takes an action if the required message type is satisfied and control is currently relinquished,
/// and falls back to an inner uidef if not.
/// </summary>
public abstract class ControlRelinquishedUIDef : UIDef
{
    public abstract MessageType RequiredMessageType { get; }

    public abstract void DoSendMessage(Action? callback);

    public required UIDef Fallback { get; init; }
    
    protected bool ControlRelinquished()
    {
        return InteractManager.BlockingInteractable != null;
    }

    public sealed override void SendMessage(MessageType type, Action? callback = null)
    {
        if ((type & RequiredMessageType) == RequiredMessageType && ControlRelinquished())
        {
            DoSendMessage(callback);
        }
        else
        {
            Fallback.SendMessage(type, callback);
        }
    }

    public override string? GetLongDescription() => Fallback.GetLongDescription();
    public override string GetPostviewName() => Fallback.GetPostviewName();
    public override string GetPreviewName() => Fallback.GetPreviewName();
    public override Sprite GetSprite() => Fallback.GetSprite();
}
