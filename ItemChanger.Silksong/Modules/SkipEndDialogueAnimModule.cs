using ItemChanger.Modules;
using ItemChanger.Silksong.Components;
using MonoDetour.DetourTypes;

namespace ItemChanger.Silksong.Modules;

/// <summary>
/// Module that will skip the non-virtual NPCControlBase.EndDialogue method on the
/// NPCControlProxy class, allowing the placement to control the hero animation.
/// </summary>
public class SkipEndDialogueAnimModule : Module
{
    protected override void DoLoad()
    {
        Using(Md.NPCControlBase.EndDialogue.ControlFlowPrefix(SkipEndDialogue));
    }

    protected override void DoUnload()
    {
        
    }

    private ReturnFlow SkipEndDialogue(NPCControlBase self)
    {
        return self is NPCControlProxy ? ReturnFlow.SkipOriginal : ReturnFlow.None;
    }
}
