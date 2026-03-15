using ItemChanger.Items;
using ItemChanger.Silksong.Items;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.UIDefs;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseItemList
{
    //bellhome upgrades
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

    public static Item Desk => new PDBoolItem { Name = ItemNames.Desk, BoolName = nameof(PlayerData.BelltownFurnishingDesk), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Desk_Name, ShopDesc = BaseLanguageStrings.Desk_Desc, Sprite = BaseAtlasSprites.Desk } };
    public static Item Gleamlights => new PDBoolItem { Name = ItemNames.Gleamlights, BoolName = nameof(PlayerData.BelltownFurnishingFairyLights), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Gleamlights_Name, ShopDesc = BaseLanguageStrings.Gleamlights_Desc, Sprite = BaseAtlasSprites.Gleamlights } };
    public static Item Gramophone => new PDBoolItem { Name = ItemNames.Gramophone, BoolName = nameof(PlayerData.BelltownFurnishingGramaphone), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Gramophone_Name, ShopDesc = BaseLanguageStrings.Gramophone_Desc, Sprite = BaseAtlasSprites.Gramophone } };
    public static Item Personal_Spa => new PDBoolItem { Name = ItemNames.Personal_Spa, BoolName = nameof(PlayerData.BelltownFurnishingSpa), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Personal_Spa_Name, ShopDesc = BaseLanguageStrings.Personal_Spa_Desc, Sprite = BaseAtlasSprites.Personal_Spa } };

    public static Item Bell_Lacquer__Red => new PDIntItem { Name = ItemNames.Bell_Lacquer__Red, IntName = nameof(PlayerData.BelltownHouseColour), Amount = 1, Increment = false, UIDef = new MsgUIDef { Name = BaseLanguageStrings.Bell_Lacquer_Name, ShopDesc = BaseLanguageStrings.Bell_Lacquer_Desc, Sprite = BaseAtlasSprites.Bell_Lacquer__Red } };
    public static Item Bell_Lacquer__White => new PDIntItem { Name = ItemNames.Bell_Lacquer__White, IntName = nameof(PlayerData.BelltownHouseColour), Amount = 2, Increment = false, UIDef = new MsgUIDef { Name = BaseLanguageStrings.Bell_Lacquer_Name, ShopDesc = BaseLanguageStrings.Bell_Lacquer_Desc, Sprite = BaseAtlasSprites.Bell_Lacquer__White } };
    public static Item Bell_Lacquer__Black => new PDIntItem { Name = ItemNames.Bell_Lacquer__Black, IntName = nameof(PlayerData.BelltownHouseColour), Amount = 3, Increment = false, UIDef = new MsgUIDef { Name = BaseLanguageStrings.Bell_Lacquer_Name, ShopDesc = BaseLanguageStrings.Bell_Lacquer_Desc, Sprite = BaseAtlasSprites.Bell_Lacquer__Black } };
    public static Item Bell_Lacquer__Bronze => new PDIntItem { Name = ItemNames.Bell_Lacquer__Bronze, IntName = nameof(PlayerData.BelltownHouseColour), Amount = 4, Increment = false, UIDef = new MsgUIDef { Name = BaseLanguageStrings.Bell_Lacquer_Name, ShopDesc = BaseLanguageStrings.Bell_Lacquer_Desc, Sprite = BaseAtlasSprites.Bell_Lacquer__Bronze } };
    public static Item Bell_Lacquer__Blue => new PDIntItem { Name = ItemNames.Bell_Lacquer__Blue, IntName = nameof(PlayerData.BelltownHouseColour), Amount = 5, Increment = false, UIDef = new MsgUIDef { Name = BaseLanguageStrings.Bell_Lacquer_Name, ShopDesc = BaseLanguageStrings.Bell_Lacquer_Desc, Sprite = BaseAtlasSprites.Bell_Lacquer__Blue } };
    public static Item Bell_Lacquer__Chrome => new PDIntItem { Name = ItemNames.Bell_Lacquer__Chrome, IntName = nameof(PlayerData.BelltownHouseColour), Amount = 6, Increment = false, UIDef = new MsgUIDef { Name = BaseLanguageStrings.Bell_Lacquer_Name, ShopDesc = BaseLanguageStrings.Bell_Lacquer_Desc, Sprite = BaseAtlasSprites.Bell_Lacquer__Chrome } };
}
