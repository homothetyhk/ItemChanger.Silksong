using ItemChanger.Enums;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Components;

namespace ItemChanger.Silksong.UIDefs;

public class LoreUIDef : CascadingUIDef
{
    public override MessageType RequiredMessageType => MessageType.Dialog;

    public required IValueProvider<string> Text { get; init; }

    public override void DoSendMessage(Action? callback)
    {
        DialogueBox.StartConversation(Text.Value, NPCControlProxy.Instance, false, DialogueBox.DisplayOptions.Default, callback, callback);
    }
}
