using ItemChanger.Items;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Items;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.UIDefs;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseItemList
{
    public static Item Flea => new FleaItem
    {
        Name = ItemNames.Flea,
        UIDef = new MsgUIDef()
        {
            // TODO - improve the shopdesc
            Name = new CountedString() { Prefix = new LanguageString("UI", "KEY_FLEA"), Amount = new FleaCount() },
            Sprite = new FleaSprite(),
            ShopDesc = new BoxedString("Flea flea flea flea flea"),
            PreviewName = new LanguageString("UI", "KEY_FLEA")
        },
    };

    public static Item Desk => new PDBoolItem { Name = "Desk", BoolName = "BelltownFurnishingDesk" };
    public static Item Gleamlights => new PDBoolItem { Name = "Gleamlights", BoolName = "BelltownFurnishingFairyLights" };
    public static Item Gramophone => new PDBoolItem { Name = "Gramophone", BoolName = "BelltownFurnishingGramaphone" };
    public static Item Personal_Spa => new PDBoolItem { Name = "Personal_Spa", BoolName = "BelltownFurnishingSpa" };

    public static Item Bell_Lacquer__Red => new PlayerDataIntItem { Name = "Bell_Lacquer__Red", FieldName = "BelltownHouseColour", Amount = 1, Increment = false };
    public static Item Bell_Lacquer__White => new PlayerDataIntItem { Name = "Bell_Lacquer__White", FieldName = "BelltownHouseColour", Amount = 2, Increment = false };
    public static Item Bell_Lacquer__Black => new PlayerDataIntItem { Name = "Bell_Lacquer__Black", FieldName = "BelltownHouseColour", Amount = 3, Increment = false };
    public static Item Bell_Lacquer__Bronze => new PlayerDataIntItem { Name = "Bell_Lacquer__Bronze", FieldName = "BelltownHouseColour", Amount = 4, Increment = false };
    public static Item Bell_Lacquer__Blue => new PlayerDataIntItem { Name = "Bell_Lacquer__Blue", FieldName = "BelltownHouseColour", Amount = 5, Increment = false };
    public static Item Bell_Lacquer__Chrome => new PlayerDataIntItem { Name = "Bell_Lacquer__Chrome", FieldName = "BelltownHouseColour", Amount = 6, Increment = false };

    public static Dictionary<string, Item> GetBaseItems()
    {
        return typeof(BaseItemList).GetProperties().Select(p => (Item)p.GetValue(null)).ToDictionary(i => i.Name);
    }
}
