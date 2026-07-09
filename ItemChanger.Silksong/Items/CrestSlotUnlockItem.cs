using ItemChanger.Items;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Modules;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.UIDefs;

namespace ItemChanger.Silksong.Items;

/// <summary>
/// When given, unlocks a crest slot for a specific crest and color. This replaces memory lockets
/// and better allows for tool skips, since it allows logic to guarantee the needed slots are
/// unlocked even if not every memory locket has been found yet.
/// </summary>
public class CrestSlotUnlockItem : Item
{
    public static CrestSlotUnlockItem Create(string name, string crestID, ToolItemType toolType)
    {
        BoxedString crestName = new BoxedString
        {
            Value = ToolItemManager.GetCrestByName(crestID).DisplayName
        };

        LanguageString slotType = toolType switch
        {
            ToolItemType.Red => ItemChangerLanguageStrings.SLOT_TYPE_RED,
            ToolItemType.Blue => ItemChangerLanguageStrings.SLOT_TYPE_BLUE,
            ToolItemType.Yellow => ItemChangerLanguageStrings.SLOT_TYPE_YELLOW,
            ToolItemType.Skill => ItemChangerLanguageStrings.SLOT_TYPE_SKILL,
            _ => throw new NotSupportedException()
        };

        // These sprites exist in the base game files, but they are grayscale.
        // The colors are stored separately in a global settings bundle.
        // For convenience, we use our own sprites with the colors pre-applied.
        ICSilksongSprite sprite = toolType switch
        {
            ToolItemType.Red => new ICSilksongSprite("Images.tool_slot_red"),
            ToolItemType.Blue => new ICSilksongSprite("Images.tool_slot_blue"),
            ToolItemType.Yellow => new ICSilksongSprite("Images.tool_slot_yellow"),
            ToolItemType.Skill => new ICSilksongSprite("Images.tool_slot_skill"),
            _ => throw new NotSupportedException()
        };

        return new()
        {
            Name = name,
            CrestID = crestID,
            ToolType = toolType,
            UIDef = new MsgUIDef
            {
                Name = CompositeString.Create(
                    ItemChangerLanguageStrings.FMT_CREST_SLOT_UNLOCK_ITEM,
                    new Dictionary<string, IValueProvider<object>>
                    {
                        { "CREST_NAME", crestName },
                        { "SLOT_TYPE", slotType }
                    }),
                Sprite = sprite
            },
        };
    }

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

            // This isn't saved to the file, but it needs to be kept in sync in case the crest is collected later
            ToolCrest crest = ToolItemManager.GetCrestByName(CrestID);
            ToolCrest.SlotInfo slotInfo = crest.Slots[slotIndex.Value];
            slotInfo.IsLocked = false;
            crest.Slots[slotIndex.Value] = slotInfo;

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
