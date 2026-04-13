using ItemChanger.Modules;

namespace ItemChanger.Silksong.Modules.YNBox;

/// <summary>
/// Without this module, if Hornet takes damage while reading a YN dialog box at a shiny,
/// the box remains visible, both options selectable, but selecting yes causes the cost
/// to get paid without giving the item.
/// 
/// The modded behaviour is that taking damage causes No to be selected, as expected.
/// </summary>
[SingletonModule]
public sealed class YNBoxDamageControlModule : Module
{
    protected override void DoLoad()
    {
        Using(Md.HeroController.Start.Postfix(SubscribeToDamageEvent));
    }

    // This should never happen
    protected override void DoUnload()
    {
        if (HeroController.SilentInstance != null)
        {
            HeroController.SilentInstance.OnTakenDamage -= CloseYNDialogWhenDamaged;
        }
    }

    private void SubscribeToDamageEvent(HeroController self)
    {
        self.OnTakenDamage += CloseYNDialogWhenDamaged;
    }

    private void CloseYNDialogWhenDamaged()
    {
        DialogueYesNoBox box = DialogueYesNoBox._instance;

        if (box == null)
        {
            return;
        }

        if (box.uiList.isActive)
        {
            box.SelectNo();
        }
    }
}
