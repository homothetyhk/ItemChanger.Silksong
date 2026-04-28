using ItemChanger.Modules;
using TeamCherry.Localization;

namespace ItemChanger.Silksong.Modules;

/// <summary>
/// Module to make the crest UI def work for base game crests that don't usually use the crest UI def popup.
/// 
/// Any custom crests should instead properly set the fields on the ToolCrest instance.
/// </summary>
public class CrestUIMsgRepairModule : Module
{
    protected override void DoLoad()
    {
        Using(Md.ToolCrestUIMsg.Setup.Postfix(RepairCrestUISetup));
    }

    protected override void DoUnload()
    {
        
    }

    private void RepairCrestUISetup(ToolCrestUIMsg self, ref ToolCrest crest)
    {
        if (self.itemPrefixText)
        {
            // For evolved crests, a different prompt asset is used. We don't really want to bother
            // with that, because it's slower and more complexity.
            // The counterpoint is that we need to manually override several fields which are
            // set in the prompt FSM rather than on the ToolCrest.
            if (crest.name == "Hunter_v2" || crest.name == "Hunter_v3")
            {
                self.itemPrefixText.text = new LocalisedString("Prompts", "UI_MSG_HCOMBO_PREFIX");
            }

            // For base game crests, ItemNamePrefix is either UI_MSG_TITLE_CREST or something
            // that doesn't exist (e.g. Witch). In such cases, we set it to the default.

            else if (!crest.ItemNamePrefix.Exists)
            {
                self.itemPrefixText.text = new LocalisedString("Tools", "UI_MSG_TITLE_CREST");
            }
        }

        // For base game crests, GetPromptDesc is always either blank, or equal to Description.
        // In cases where it is blank (e.g. Hunter), we ensure it is equal to Description.
        // For the Hunter upgrades, this differs from the prompt description, but matches
        // the description in the crest UI.
        if (self.descText && string.IsNullOrEmpty(crest.GetPromptDesc.Sheet))
        {
            self.descText.text = crest.Description;
        }
    }
}
