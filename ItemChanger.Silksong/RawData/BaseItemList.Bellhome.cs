using ItemChanger.Items;
using ItemChanger.Silksong.Items;
using ItemChanger.Silksong.Serialization;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseItemList
{
    //bellhome upgrades
    //TODO: implement ItemChanger support for non-CollectableItem bellhome upgrades
    //note: i think the bellhome lacquers are one time use and can not overlap
    //also the bought bellhome upgrades seem to be based on PlayerData bool values
    /* unimplemented items
        Bell_Lacquer__Black -> BelltownHouseColour
        Bell_Lacquer__Blue -> BelltownHouseColour
        Bell_Lacquer__Bronze -> BelltownHouseColour
        Bell_Lacquer__Chrome -> BelltownHouseColour [steel soul exclusive]
        Bell_Lacquer__Red -> BelltownHouseColour
        Bell_Lacquer__White -> BelltownHouseColour [replaced by Bell_Lacquer__Chrome in steel soul]
        Desk -> BelltownFurnishingDesk
        Gleamlights -> BelltownFurnishingFairyLights
        Gramophone -> BelltownFurnishingGramaphone
        Personal_Spa -> BelltownFurnishingSpa
    */
    public static Item Crawbell => ItemChangerSavedItem.Create(
        name: ItemNames.Crawbell,
        id: "Crawbell",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Farsight => ItemChangerSavedItem.Create(
        name: ItemNames.Farsight,
        id: "Farsight",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Materium => ItemChangerSavedItem.Create(
        name: ItemNames.Materium,
        id: "Materium",
        type: BaseGameSavedItem.ItemType.CollectableItem);
}
