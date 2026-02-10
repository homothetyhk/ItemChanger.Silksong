using ItemChanger.Items;
using ItemChanger.Silksong.Items;
using ItemChanger.Silksong.Serialization;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseItemList
{
    //consumables
    public static Item Beast_Shard => ItemChangerSavedItem.Create(
        name: ItemNames.Beast_Shard,
        id: "Great Shard",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Frayed_Rosary_String => ItemChangerSavedItem.Create(
        name: ItemNames.Frayed_Rosary_String,
        id: "Rosary_Set_Frayed",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Growstone => ItemChangerSavedItem.Create(//steel soul exclusive, not sure if we should keep
        name: ItemNames.Growstone,
        id: "Growstone",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Heavy_Rosary_Necklace => ItemChangerSavedItem.Create(
        name: ItemNames.Heavy_Rosary_Necklace,
        id: "Rosary_Set_Large",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Hornet_Statuette => ItemChangerSavedItem.Create(
        name: ItemNames.Hornet_Statuette,
        id: "Fixer Idol",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Pale_Rosary_Necklace => ItemChangerSavedItem.Create(
        name: ItemNames.Pale_Rosary_Necklace,
        id: "Rosary_Set_Huge_White",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Pristine_Core => ItemChangerSavedItem.Create(
        name: ItemNames.Pristine_Core,
        id: "Pristine Core",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Rosary_Necklace => ItemChangerSavedItem.Create(
        name: ItemNames.Rosary_Necklace,
        id: "Rosary_Set_Medium",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Rosary_String => ItemChangerSavedItem.Create(
        name: ItemNames.Rosary_String,
        id: "Rosary_Set_Small",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Shard_Bundle => ItemChangerSavedItem.Create(
        name: ItemNames.Shard_Bundle,
        id: "Shard Pouch",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Silkeater => ItemChangerSavedItem.Create(
        name: ItemNames.Silkeater,
        id: "Silk Grub",
        type: BaseGameSavedItem.ItemType.CollectableItem);
}
