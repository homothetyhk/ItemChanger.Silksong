using ItemChanger.Enums;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Components;
using TeamCherry.Localization;
using UnityEngine;

namespace ItemChanger.Silksong.UIDefs;

public class LoreUIDef : ControlRelinquishedUIDef
{
    public static DialogueBox.DisplayOptions DefaultTopCenter => new()
    {
        ShowDecorators = true,
        Alignment = TMProOld.TextAlignmentOptions.Top,
        OffsetY = 0,
        StopOffsetY = 0,
        TextColor = Color.white
    };

    public sealed override MessageType RequiredMessageType => MessageType.Dialog;

    public required IValueProvider<string> Text { get; init; }

    public DialogueBox.DisplayOptions DisplayOptions { get; init; } = DialogueBox.DisplayOptions.Default;

    public override void DoSendMessage(Action? callback)
    {
        void endConversation() => DialogueBox.EndConversation(true, callback);

        DialogueBox.StartConversation(
            LocalisedString.ReplaceTags(Text.Value), NPCControlProxy.Instance, false, DisplayOptions, endConversation);
    }

    /*
     * With no modifications, the flow here when closing is:
     * - Invoke the callback passed to StartConversation
     * - if AutoEnd is true on the NPCControlBase instigator, call DialogueBox.EndConversation with no callback
     * - DialogueBox.EndConversation starts the CloseAndEnd coroutine
     * - The CloseAndEnd coroutine:
     *   - Closes the dialogue box
     *   - Calls EndDialogue on the NPC (which animates the player and returns control)
     *   - Invokes the callback passed to EndConversation (which was null)
     * 
     * The callback passed to StartConversation runs too early in the case of chained UI Defs - in particular,
     * if the callback starts a second dialogue box, it will be started before the CloseAndEnd coroutine
     * from the previous dialogue box closes it shortly after.
     * 
     * The modifications we apply are:
     * - Delay running the callback passed to DoSendMessage until after the dialogue box is closed
     * - Skip the NPCControlBase.EndDialogue body (via the SkipEndDialogueAnimModule)
     */
}
