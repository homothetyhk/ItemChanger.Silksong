using ItemChanger.Items;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Items;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.UIDefs;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseItemList
{
    public static Item Left_Cling_Grip_Item => new ClingGripItem { Name = ItemNames.Left_Cling_Grip, Direction = ClingGripDirection.Left };
    public static Item Right_Cling_Grip_Item => new ClingGripItem { Name = ItemNames.Right_Cling_Grip, Direction = ClingGripDirection.Right };

    public static Item Left_Swift_Step_Item => new SwiftStepItem { Name = ItemNames.Left_Swift_Step, Direction = SwiftStepDirection.Left };
    public static Item Right_Swift_Step_Item => new SwiftStepItem { Name = ItemNames.Right_Swift_Step, Direction = SwiftStepDirection.Right };

    public static Item Left_Clawline_Item => new ClawlineItem { Name = ItemNames.Left_Clawline, Direction = ClawlineDirection.Left };
    public static Item Right_Clawline_Item => new ClawlineItem { Name = ItemNames.Right_Clawline, Direction = ClawlineDirection.Right };

    public static Item Leftslash_Item => new SlashItem { Name = ItemNames.Leftslash, Direction = SlashDirection.Left };
    public static Item Rightslash_Item => new SlashItem { Name = ItemNames.Rightslash, Direction = SlashDirection.Right };
    public static Item Upslash_Item => new SlashItem { Name = ItemNames.Upslash, Direction = SlashDirection.Up };
    public static Item Downslash_Item => new SlashItem { Name = ItemNames.Downslash, Direction = SlashDirection.Down };

    // TODO: Taunt not yet implemented
    // public static Item Taunt_Item => new TauntItem { Name = ItemNames.Taunt };

    public static Item Bind_Item => new BindItem { Name = ItemNames.Bind };

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
