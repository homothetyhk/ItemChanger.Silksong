using ItemChanger.Items;
using ItemChanger.Silksong.Items;
using ItemChanger.Silksong.Serialization;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseItemList
{
    //plot items
    /* unimplemented items
        Architect_s_Melody -> HasMelodyArchitect
        Conductor_s_Melody -> HasMelodyConductor
        Vaultkeeper_s_Melody -> HasMelodyLibrarian
    */
    public static Item Conjoined_Heart => ItemChangerSavedItem.Create(
        name: ItemNames.Conjoined_Heart,
        id: "Clover Heart",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Encrusted_Heart => ItemChangerSavedItem.Create(
        name: ItemNames.Encrusted_Heart,
        id: "Coral Heart",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Everbloom => ItemChangerSavedItem.Create(
        name: ItemNames.Everbloom,
        id: "White Flower",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Hermit_s_Soul => ItemChangerSavedItem.Create(
        name: ItemNames.Hermit_s_Soul,
        id: "Snare Soul Bell Hermit",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Hunter_s_Heart => ItemChangerSavedItem.Create(
        name: ItemNames.Hunter_s_Heart,
        id: "Hunter Heart",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Maiden_s_Soul => ItemChangerSavedItem.Create(
        name: ItemNames.Maiden_s_Soul,
        id: "Snare Soul Churchkeeper",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Pollen_Heart => ItemChangerSavedItem.Create(
        name: ItemNames.Pollen_Heart,
        id: "Flower Heart",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Seeker_s_Soul => ItemChangerSavedItem.Create(
        name: ItemNames.Seeker_s_Soul,
        id: "Snare Soul Swamp Bug",
        type: BaseGameSavedItem.ItemType.CollectableItem);

    //mementos
    public static Item Craw_Memento => ItemChangerSavedItem.Create(
        name: ItemNames.Craw_Memento,
        id: "Crowman Memento",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Grey_Memento => ItemChangerSavedItem.Create(
        name: ItemNames.Grey_Memento,
        id: "Grey Memento",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Guardian_s_Memento => ItemChangerSavedItem.Create(
        name: ItemNames.Guardian_s_Memento,
        id: "Memento Seth",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Hero_s_Memento => ItemChangerSavedItem.Create(
        name: ItemNames.Hero_s_Memento,
        id: "Memento Garmond",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Hunter_s_Memento => ItemChangerSavedItem.Create(
        name: ItemNames.Hunter_s_Memento,
        id: "Hunter Memento",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Sprintmaster_Memento => ItemChangerSavedItem.Create(
        name: ItemNames.Sprintmaster_Memento,
        id: "Sprintmaster Memento",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Surface_Memento => ItemChangerSavedItem.Create(
        name: ItemNames.Surface_Memento,
        id: "Memento Surface",
        type: BaseGameSavedItem.ItemType.CollectableItem);
}
