using ItemChanger.Items;
using ItemChanger.Silksong.Items;
using ItemChanger.Silksong.Serialization;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseItemList
{
    //finite quest items
    public static Item Broodmother_s_Eye => ItemChangerSavedItem.Create(
        name: ItemNames.Broodmother_s_Eye,
        id: "Broodmother Remains",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Cogheart_Piece => ItemChangerSavedItem.Create(
        name: ItemNames.Cogheart_Piece,
        id: "Cog Heart Pieces",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Crown_Fragment => ItemChangerSavedItem.Create(
        name: ItemNames.Crown_Fragment,
        id: "Skull King Fragment",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Crustnut => ItemChangerSavedItem.Create(
        name: ItemNames.Crustnut,
        id: "Coral Ingredient",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Flintgem => ItemChangerSavedItem.Create(
        name: ItemNames.Flintgem,
        id: "Rock Roller Item",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Grass_Doll => ItemChangerSavedItem.Create(
        name: ItemNames.Grass_Doll,
        id: "Ant Trapper Item",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Horn_Fragment => ItemChangerSavedItem.Create(
        name: ItemNames.Horn_Fragment,
        id: "Beastfly Remains",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Mossberry => ItemChangerSavedItem.Create(
        name: ItemNames.Mossberry,
        id: "Mossberry",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Mossberry_Stew => ItemChangerSavedItem.Create(
        name: ItemNames.Mossberry_Stew,
        id: "Mossberry Stew",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Pickled_Muckmaggot => ItemChangerSavedItem.Create(
        name: ItemNames.Pickled_Muckmaggot,
        id: "Pickled Roach Egg",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Pollip_Heart => ItemChangerSavedItem.Create(
        name: ItemNames.Pollip_Heart,
        id: "Shell Flower",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Ruined_Tool => ItemChangerSavedItem.Create(
        name: ItemNames.Ruined_Tool,
        id: "Broken SilkShot",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Steel_Spines => ItemChangerSavedItem.Create(
        name: ItemNames.Steel_Spines,
        id: "Extractor Machine Pins",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Twisted_Bud => ItemChangerSavedItem.Create(
        name: ItemNames.Twisted_Bud,
        id: "Wood Witch Item",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Vintage_Nectar => ItemChangerSavedItem.Create(
        name: ItemNames.Vintage_Nectar,
        id: "Vintage Nectar",
        type: BaseGameSavedItem.ItemType.CollectableItem);


    //delivery items
    /* note: these items work like normal items during testing and do not take damage despite being delivery items
     * the only item that does take damage is the courier's rasher but it only takes one hit unlike taking 8 hits normally
     * the courier's rasher also does not degrade overtime
     * not sure whether to keep the damage aspect or make all delivery items immune to damage
     */
    //TODO: extend ItemChangerCollectableItem class to support delivery items taking damage
    public static Item Courier_s_Rasher => ItemChangerSavedItem.Create(
        name: ItemNames.Courier_s_Rasher,
        id: "Courier Supplies Gourmand",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Courier_s_Swag => ItemChangerSavedItem.Create(
        name: ItemNames.Courier_s_Swag,
        id: "Courier Supplies",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Liquid_Lacquer => ItemChangerSavedItem.Create(
        name: ItemNames.Liquid_Lacquer,
        id: "Courier Supplies Mask Maker",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Queen_s_Egg => ItemChangerSavedItem.Create(
        name: ItemNames.Queen_s_Egg,
        id: "Courier Supplies Slave",
        type: BaseGameSavedItem.ItemType.CollectableItem);


    //respawning quest items
    public static Item Choir_Cloak => ItemChangerSavedItem.Create(
        name: ItemNames.Choir_Cloak,
        id: "Song Pilgrim Cloak",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Fine_Pin => ItemChangerSavedItem.Create(
        name: ItemNames.Fine_Pin,
        id: "Fine Pin",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Pilgrim_Shawl => ItemChangerSavedItem.Create(
        name: ItemNames.Pilgrim_Shawl,
        id: "Pilgrim Rag",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Plasmified_Blood => ItemChangerSavedItem.Create(
        name: ItemNames.Plasmified_Blood,
        id: "Plasmium Blood",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Plasmium => ItemChangerSavedItem.Create(
        name: ItemNames.Plasmium,
        id: "Plasmium",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Ragpelt => ItemChangerSavedItem.Create(
        name: ItemNames.Ragpelt,
        id: "Crow Feather",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Roach_Guts => ItemChangerSavedItem.Create(
        name: ItemNames.Roach_Guts,
        id: "Roach Corpse Item",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Seared_Organ => ItemChangerSavedItem.Create(
        name: ItemNames.Seared_Organ,
        id: "Enemy Morsel Seared",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Shredded_Organ => ItemChangerSavedItem.Create(
        name: ItemNames.Shredded_Organ,
        id: "Enemy Morsel Shredded",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Silver_Bell => ItemChangerSavedItem.Create(
        name: ItemNames.Silver_Bell,
        id: "Silver Bellclapper",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Skewered_Organ => ItemChangerSavedItem.Create(
        name: ItemNames.Skewered_Organ,
        id: "Enemy Morsel Speared",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Spine_Core => ItemChangerSavedItem.Create(
        name: ItemNames.Spine_Core,
        id: "Common Spine",
        type: BaseGameSavedItem.ItemType.CollectableItem);
}
