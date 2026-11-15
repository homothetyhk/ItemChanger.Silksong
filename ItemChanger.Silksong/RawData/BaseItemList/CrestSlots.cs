using ItemChanger.Items;
using ItemChanger.Silksong.Items;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseItemList
{
    /*
     * Hunter - start: 1 red, 1 blue, 1 yellow, 1 silk. unlock: 1 red, 1 blue, 1 yellow, 0 silk.
     * Reaper - start: 1 red, 1 blue, 1 yellow, 1 silk. unlock: 1 red, 1 blue, 1 yellow, 0 silk.
     * Wanderer - start: 1 red, 0 blue, 2 yellow, 1 silk. unlock: 0 red, 2 blue, 1 yellow, 0 silk.
     * Beast - start: 2 red, 0 blue, 0 yellow, 1 silk. unlock: 0 red, 0 blue, 2 yellow, 0 silk.
     * Witch - start: 1 red, 1 blue, 0 yellow, 1 silk. unlock: 1 red, 2 blue, 0 yellow, 0 silk.
     * Architect - start: 3 red, 0 blue, 0 yellow, 0 silk. unlock: 0 red, 2 blue, 2 yellow, 0 silk.
     * Shaman - start: 0 red, 0 blue, 0 yellow, 3 silk. unlock: 0 red, 2 blue, 0 yellow, 0 silk.
    */
    public static Item Crest_Slot__Hunter__Red_Tool => CrestSlotUnlockItem.Create(
        name: ItemNames.Crest_Slot__Hunter__Red_Tool,
        crestID: "Hunter", toolType: ToolItemType.Red);

    public static Item Crest_Slot__Hunter__Blue_Tool => CrestSlotUnlockItem.Create(
        name: ItemNames.Crest_Slot__Hunter__Blue_Tool,
        crestID: "Hunter", toolType: ToolItemType.Blue);

    public static Item Crest_Slot__Hunter__Yellow_Tool => CrestSlotUnlockItem.Create(
        name: ItemNames.Crest_Slot__Hunter__Yellow_Tool,
        crestID: "Hunter", toolType: ToolItemType.Yellow);

    public static Item Crest_Slot__Hunter__Silk_Skill => CrestSlotUnlockItem.Create(
        name: ItemNames.Crest_Slot__Hunter__Silk_Skill,
        crestID: "Hunter", toolType: ToolItemType.Skill);

    public static Item Crest_Slot__Reaper__Red_Tool => CrestSlotUnlockItem.Create(
        name: ItemNames.Crest_Slot__Reaper__Red_Tool,
        crestID: "Reaper", toolType: ToolItemType.Red);

    public static Item Crest_Slot__Reaper__Blue_Tool => CrestSlotUnlockItem.Create(
        name: ItemNames.Crest_Slot__Reaper__Blue_Tool,
        crestID: "Reaper", toolType: ToolItemType.Blue);

    public static Item Crest_Slot__Reaper__Yellow_Tool => CrestSlotUnlockItem.Create(
        name: ItemNames.Crest_Slot__Reaper__Yellow_Tool,
        crestID: "Reaper", toolType: ToolItemType.Yellow);

    public static Item Crest_Slot__Reaper__Silk_Skill => CrestSlotUnlockItem.Create(
        name: ItemNames.Crest_Slot__Reaper__Silk_Skill,
        crestID: "Reaper", toolType: ToolItemType.Skill);

    public static Item Crest_Slot__Wanderer__Red_Tool => CrestSlotUnlockItem.Create(
        name: ItemNames.Crest_Slot__Wanderer__Red_Tool,
        crestID: "Wanderer", toolType: ToolItemType.Red);

    public static Item Crest_Slot__Wanderer__Blue_Tool => CrestSlotUnlockItem.Create(
        name: ItemNames.Crest_Slot__Wanderer__Blue_Tool,
        crestID: "Wanderer", toolType: ToolItemType.Blue);

    public static Item Crest_Slot__Wanderer__Yellow_Tool => CrestSlotUnlockItem.Create(
        name: ItemNames.Crest_Slot__Wanderer__Yellow_Tool,
        crestID: "Wanderer", toolType: ToolItemType.Yellow);

    public static Item Crest_Slot__Wanderer__Silk_Skill => CrestSlotUnlockItem.Create(
        name: ItemNames.Crest_Slot__Wanderer__Silk_Skill,
        crestID: "Wanderer", toolType: ToolItemType.Skill);

    public static Item Crest_Slot__Beast__Red_Tool => CrestSlotUnlockItem.Create(
        name: ItemNames.Crest_Slot__Beast__Red_Tool,
        crestID: "Warrior", toolType: ToolItemType.Red);

    public static Item Crest_Slot__Beast__Yellow_Tool => CrestSlotUnlockItem.Create(
        name: ItemNames.Crest_Slot__Beast__Yellow_Tool,
        crestID: "Warrior", toolType: ToolItemType.Yellow);

    public static Item Crest_Slot__Beast__Silk_Skill => CrestSlotUnlockItem.Create(
        name: ItemNames.Crest_Slot__Beast__Silk_Skill,
        crestID: "Warrior", toolType: ToolItemType.Skill);

    public static Item Crest_Slot__Witch__Red_Tool => CrestSlotUnlockItem.Create(
        name: ItemNames.Crest_Slot__Witch__Red_Tool,
        crestID: "Witch", toolType: ToolItemType.Red);

    public static Item Crest_Slot__Witch__Blue_Tool => CrestSlotUnlockItem.Create(
        name: ItemNames.Crest_Slot__Witch__Blue_Tool,
        crestID: "Witch", toolType: ToolItemType.Blue);

    public static Item Crest_Slot__Witch__Silk_Skill => CrestSlotUnlockItem.Create(
        name: ItemNames.Crest_Slot__Witch__Silk_Skill,
        crestID: "Witch", toolType: ToolItemType.Skill);

    public static Item Crest_Slot__Architect__Red_Tool => CrestSlotUnlockItem.Create(
        name: ItemNames.Crest_Slot__Architect__Red_Tool,
        crestID: "Toolmaster", toolType: ToolItemType.Red);

    public static Item Crest_Slot__Architect__Blue_Tool => CrestSlotUnlockItem.Create(
        name: ItemNames.Crest_Slot__Architect__Blue_Tool,
        crestID: "Toolmaster", toolType: ToolItemType.Blue);

    public static Item Crest_Slot__Architect__Yellow_Tool => CrestSlotUnlockItem.Create(
        name: ItemNames.Crest_Slot__Architect__Yellow_Tool,
        crestID: "Toolmaster", toolType: ToolItemType.Yellow);

    public static Item Crest_Slot__Shaman__Blue_Tool => CrestSlotUnlockItem.Create(
        name: ItemNames.Crest_Slot__Shaman__Blue_Tool,
        crestID: "Spell", toolType: ToolItemType.Blue);

    public static Item Crest_Slot__Shaman__Silk_Skill => CrestSlotUnlockItem.Create(
        name: ItemNames.Crest_Slot__Shaman__Silk_Skill,
        crestID: "Spell", toolType: ToolItemType.Skill);
}
