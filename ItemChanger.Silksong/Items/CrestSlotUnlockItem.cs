using ItemChanger.Items;
using ItemChanger.Silksong.Modules;

namespace ItemChanger.Silksong.Items;

/// <summary>
/// When given, unlocks a crest slot for a specific crest and color. This replaces memory lockets
/// and better allows for tool skips, since it allows logic to guarantee the needed slots are
/// unlocked even if not every memory locket has been found yet.
/// </summary>
public class CrestSlotUnlockItem : Item
{
    public required string CrestID { get; set; }
    public required ToolItemType ToolType { get; set; }

    protected override void DoLoad()
    {
        ActiveProfile!.Modules.GetOrAdd<LockedCrestPreviewModule>();
    }

    public override bool Redundant()
    {
        return FindNextLockedSlotIndex() != null;
    }

    public override void GiveImmediate(GiveInfo info)
    {
        int? slotIndex = FindNextLockedSlotIndex();

        if (slotIndex != null)
        {
            ToolCrestsData.Data crestData = PlayerData.instance.ToolEquips.GetData(CrestID);
            List<ToolCrestsData.SlotData> slots = crestData.Slots ?? (crestData.Slots = []);

            while (slots.Count <= slotIndex.Value)
            {
                slots.Add(default);
            }

            ToolCrestsData.SlotData slotData = slots[slotIndex.Value];
            slotData.IsUnlocked = true;
            slots[slotIndex.Value] = slotData;

            PlayerData.instance.ToolEquips.SetData(CrestID, crestData);

            LockedCrestPreviewModule module = ActiveProfile!.Modules.GetOrAdd<LockedCrestPreviewModule>();
            module.SetCrestVisible(CrestID);
        }
    }

    private int? FindNextLockedSlotIndex()
    {
        ToolCrest crest = ToolItemManager.GetCrestByName(CrestID);

        for (int i = 0; i < crest.Slots.Length; i++)
        {
            ToolCrest.SlotInfo slot = crest.Slots[i];

            if (slot.Type == ToolType && slot.IsLocked)
            {
                return i;
            }
        }

        return null;
    }
}
