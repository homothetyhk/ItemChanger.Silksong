using ItemChanger.Items;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.UIDefs;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseItemList
{
    public static Item CreateLoreItem(string name, string sheet, string key)
    {
        return new NullItem()
        {
            Name = name,
            UIDef = new LoreUIDef()
            {
                Fallback = new MsgUIDef()
                {
                    Name = new BoxedString("Lore"),
                    Sprite = new EmptySprite(),
                },
                Text = new LanguageString(sheet, key)
            }
        };
    }

    public static Item Lore_Tablet__Abyss_Bottom_Left => CreateLoreItem(ItemNames.Lore_Tablet__Abyss_Bottom_Left, "Inspect", "ABYSS_LORE_STONE_TOP");
}
