using ItemChanger.Items;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Items;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.UIDefs;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.RawData;

// This is a partial class containing a large number of Item properties. The properties are arranged in the files in the BaseItemList folder.

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

    

    public static Dictionary<string, Item> GetBaseItems()
    {
        return typeof(BaseItemList).GetProperties().Select(p => (Item)p.GetValue(null)).ToDictionary(i => i.Name);
    }
}
