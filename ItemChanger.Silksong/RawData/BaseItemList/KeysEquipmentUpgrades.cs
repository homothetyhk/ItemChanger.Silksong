using ItemChanger.Items;
using ItemChanger.Silksong.Items;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.UIDefs;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseItemList
{
    //items
    /* note: crafting kit and tool pouch refer to the same internal CollectableItem Tool Pouch&Kit Inv
     * unsure if PlayerData has specific bools for crafting kit and tool pouch; PlayerData values ToolPouchUpgrades and ToolKitUpgrades could control tool pouch and crafting kit
     */
    //TODO: extend ItemChangerCollectableItem class to support separate values for crafting kit and tool pouch

    public static Item Silk_Heart => new PDIntItem { Name = ItemNames.Silk_Heart, IntName = nameof(PlayerData.silkRegenMax), Amount = 1, Increment = true, UIDef = new MsgUIDef { Name = BaseLanguageStrings.Silk_Heart_Name, ShopDesc = BaseLanguageStrings.Silk_Heart_Desc, Sprite = BaseAtlasSprites.Silk_Heart } };
    public static Item Mask_Shard => new MaskShardItem { Name = ItemNames.Mask_Shard, Shards = 1, UIDef = new MsgUIDef { Name = BaseLanguageStrings.Mask_Shard_Name, ShopDesc = BaseLanguageStrings.Mask_Shard_Desc, Sprite = BaseAtlasSprites.Mask_Shard } };
    public static Item Spool_Fragment => new SpoolFragmentItem { Name = ItemNames.Spool_Fragment, Fragments = 1, UIDef = new MsgUIDef { Name = BaseLanguageStrings.Spool_Fragment_Name, ShopDesc = BaseLanguageStrings.Spool_Fragment_Desc, Sprite = BaseAtlasSprites.Spool_Fragment } };
    public static Item Hunter_s_Journal => new PDBoolItem { Name = ItemNames.Hunter_s_Journal, BoolName = nameof(PlayerData.hasJournal), UIDef = null! };
    public static Item Crafting_Kit => ItemChangerSavedItem.Create(//refers to same internal item as tool pouch
        name: ItemNames.Crafting_Kit,
        id: "Tool Pouch&Kit Inv",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Craftmetal => ItemChangerSavedItem.Create(
        name: ItemNames.Craftmetal,
        id: "Tool Metal",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Memory_Locket => ItemChangerSavedItem.Create(
        name: ItemNames.Memory_Locket,
        id: "Crest Socket Unlocker",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Pale_Oil => ItemChangerSavedItem.Create(
        name: ItemNames.Pale_Oil,
        id: "Pale_Oil",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Plasmium_Gland => ItemChangerSavedItem.Create(
        name: ItemNames.Plasmium_Gland,
        id: "Plasmium Gland",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Tool_Pouch => ItemChangerSavedItem.Create(//shares same internal item as crafting kit
        name: ItemNames.Tool_Pouch,
        id: "Tool Pouch&Kit Inv",
        type: BaseGameSavedItem.ItemType.CollectableItem);


    //keys
    public static Item Architect_s_Key => ItemChangerSavedItem.Create(
        name: ItemNames.Architect_s_Key,
        id: "Architect Key",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Bellhome_Key => ItemChangerSavedItem.Create(
        name: ItemNames.Bellhome_Key,
        id: "Belltown House Key",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Craw_Summons => ItemChangerSavedItem.Create(
        name: ItemNames.Craw_Summons,
        id: "Craw Summons",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Diving_Bell_Key => ItemChangerSavedItem.Create(
        name: ItemNames.Diving_Bell_Key,
        id: "Dock Key",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Key_of_Apostate => ItemChangerSavedItem.Create(//refers to Slab Key; PlayerData bool HasSlabKeyC
        name: ItemNames.Key_of_Apostate,
        id: "Slab Key",
        type: BaseGameSavedItem.ItemType.CollectableItem,
        playerDataBoolName: nameof(PlayerData.HasSlabKeyC));
    public static Item Key_of_Heretic => ItemChangerSavedItem.Create(//refers to Slab Key; PlayerData bool HasSlabKeyB
        name: ItemNames.Key_of_Heretic,
        id: "Slab Key",
        type: BaseGameSavedItem.ItemType.CollectableItem,
        playerDataBoolName: nameof(PlayerData.HasSlabKeyB));
    public static Item Key_of_Indolent => ItemChangerSavedItem.Create(//refers to Slab Key; PlayerData bool HasSlabKeyA
        name: ItemNames.Key_of_Indolent,
        id: "Slab Key",
        type: BaseGameSavedItem.ItemType.CollectableItem,
        playerDataBoolName: nameof(PlayerData.HasSlabKeyA));
    public static Item Simple_Key => ItemChangerSavedItem.Create(
        name: ItemNames.Simple_Key,
        id: "Simple Key",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Surgeon_s_Key => ItemChangerSavedItem.Create(
        name: ItemNames.Surgeon_s_Key,
        id: "Ward Boss Key",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item White_Key => ItemChangerSavedItem.Create(
        name: ItemNames.White_Key,
        id: "Ward Key",
        type: BaseGameSavedItem.ItemType.CollectableItem);
}
