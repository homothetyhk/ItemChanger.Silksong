using ItemChanger.Items;
using ItemChanger.Silksong.Items;
using ItemChanger.Silksong.Serialization;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseItemList
{
    //maps
    //TODO: implement ItemChanger item class that changes map-related bools in PlayerData (example: HasSlabMap gives map of slab)


    //quills
    /* note: the quill has several states based on the QuillState int value in PlayerData (0 = normal?, 1 = normal, 2 = red, 3 = purple)
     * the quill exists as a CollectableItem but may need an extension of the ItemChangerCollectableItem class to support the QuillState int value
     * also may need to support changing the hasQuill bool in PlayerData
     * when testing the quills appear with the name !!/!! and do not appear in the inventory; most likely requires changing the hasQuill bool in PlayerData to support
     */
    //TODO: implement ItemChanger item class that can change QuillState int value in PlayerData
    public static Item Quill__White => ItemChangerSavedItem.Create(//extend class to support QuillState int value (QuillState = 1)
        name: ItemNames.Quill__White,
        id: "Quill",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Quill__Red => ItemChangerSavedItem.Create(//extend class to support QuillState int value (QuillState = 2)
        name: ItemNames.Quill__Red,
        id: "Quill",
        type: BaseGameSavedItem.ItemType.CollectableItem);
    public static Item Quill__Purple => ItemChangerSavedItem.Create(//extend class to support QuillState int value (QuillState = 3)
        name: ItemNames.Quill__Purple,
        id: "Quill",
        type: BaseGameSavedItem.ItemType.CollectableItem);


    //TODO: implement ItemChanger class that supports map markers


    //TODO: implement ItemChanger class that supports map pins


    //TODO: implement ItemChanger class that supports flea findings
}
