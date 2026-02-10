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

    public static Item Quill_White => new QuillItem { Name = ItemNames.Quill__White, QuillState = 1 };
    public static Item Quill_Red => new QuillItem { Name = ItemNames.Quill__Red, QuillState = 2 };
    public static Item Quill_Purple => new QuillItem { Name = ItemNames.Quill__Purple, QuillState = 3 };

    public static Dictionary<string, Item> GetBaseItems()
    {
        return typeof(BaseItemList).GetProperties().Select(p => (Item)p.GetValue(null)).ToDictionary(i => i.Name);
    }
}
