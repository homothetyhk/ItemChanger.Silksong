using ItemChanger.Modules;
using ItemChanger.Silksong.RawData;
using MonoDetour.DetourTypes;
using Newtonsoft.Json;
using UnityEngine;

namespace ItemChanger.Silksong.Modules;

/// <summary>
/// Allows viewing crests in the inventory before they are unlocked. The intended use case
/// is unlocking tool slots on a crest before unlocking the crest itself.
/// </summary>
public class LockedCrestPreviewModule : Module
{
    private const float LOCKED_CREST_OPACITY = 0.3f;
    private const float LOCKED_SLOT_OPACITY = 0.5f;

    public bool AlwaysDisplayAllCrests { get; set; }

    [JsonProperty] private HashSet<string> VisibleCrestIDs = [];

    protected override void DoLoad()
    {
        Using(Md.InventoryToolCrest.get_IsUnlocked.ControlFlowPrefix(OverrideCrestIsUnlocked));
        Using(Md.InventoryToolCrest.TransitionDisplayState.Prefix(OverrideCrestOpacity));
        Using(Md.InventoryToolCrestSlot.SetSlotColour.Prefix(OverrideCrestSlotOpacity));
        Using(Md.InventoryToolCrest.get_DisplayName.Postfix(OverrideCrestDisplayName));
        Using(Md.InventoryItemToolManager.IsAvailable.ControlFlowPrefix(ForceShowToolsPane));
    }

    protected override void DoUnload() { }

    public void SetCrestVisible(string crestID)
    {
        VisibleCrestIDs.Add(crestID);
    }

    private static bool ShouldDisplayAsLocked(InventoryToolCrest crest)
    {
        return crest && crest.manager && crest.manager.EquipState == InventoryItemToolManager.EquipStates.SwitchCrest
            && crest.CrestData && !crest.CrestData.IsUnlocked;
    }

    private ReturnFlow OverrideCrestIsUnlocked(InventoryToolCrest self, ref bool returnValue)
    {
        if (AlwaysDisplayAllCrests || VisibleCrestIDs.Contains(self.gameObject.name))
        {
            // Same as the original property, but we assume CrestData.IsUnlocked is true
            returnValue = self.CrestData && !self.CrestData.IsUpgradedVersionUnlocked
                && (!self.CrestData.IsHidden || self.CrestData.IsEquipped);

            return ReturnFlow.SkipOriginal;
        }

        return ReturnFlow.None;
    }

    private void OverrideCrestOpacity(InventoryToolCrest self, ref Color newColor, ref Vector3 newScale, ref bool isCurrentCrest, ref bool isInstant)
    {
        if (ShouldDisplayAsLocked(self))
        {
            newColor *= LOCKED_CREST_OPACITY;
            self.crestSilhouette.BaseColor = newColor;
        }
    }

    private void OverrideCrestSlotOpacity(InventoryToolCrestSlot self, ref Color color, ref float groupAlpha, ref bool fadeAlpha)
    {
        if (ShouldDisplayAsLocked(self.Crest))
        {
            color *= LOCKED_SLOT_OPACITY;
        }
    }

    private void OverrideCrestDisplayName(InventoryToolCrest self, ref string returnValue)
    {
        if (ShouldDisplayAsLocked(self))
        {
            returnValue = string.Format(ItemChangerLanguageStrings.FMT_LOCKED_CREST_PREVIEW.Value, returnValue);
        }
    }

    private ReturnFlow ForceShowToolsPane(InventoryItemToolManager self, ref bool returnValue)
    {
        // If there are crests to preview, show the tools pane in the inventory

        if (!CollectableItemManager.IsInHiddenMode() && ToolItemManager.GetAllCrests()
            .Count(crest => crest.IsVisible || VisibleCrestIDs.Contains(crest.name)) > 1)
        {
            returnValue = true;
            return ReturnFlow.SkipOriginal;
        }

        return ReturnFlow.None;
    }
}
