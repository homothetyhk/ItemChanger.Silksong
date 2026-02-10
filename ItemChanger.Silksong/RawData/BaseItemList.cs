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

    public static Item Swift_Step_Item => new PDBoolItem { Name = ItemNames.Swift_Step, BoolName = "hasDash" };
    public static Item Cling_Grip_Item => new PDBoolItem { Name = ItemNames.Cling_Grip, BoolName = "hasWalljump" };
    public static Item Clawline_Item => new PDBoolItem { Name = ItemNames.Clawline, BoolName = "hasHarpoonDash" };
    public static Item Silk_Soar_Item => new PDBoolItem { Name = ItemNames.Silk_Soar, BoolName = "hasSuperJump" };
    public static Item Needolin_Item => new PDBoolItem { Name = ItemNames.Needolin, BoolName = "hasNeedolin" };
    public static Item Sylphsong_Item => new PDBoolItem { Name = ItemNames.Sylphsong, BoolName = "HasBoundCrestUpgrader" };
    public static Item Beastling_Call_Item => new PDBoolItem { Name = ItemNames.Beastling_Call, BoolName = "UnlockedFastTravelTeleport" };
    public static Item Elegy_of_the_Deep_Item => new PDBoolItem { Name = ItemNames.Elegy_of_the_Deep, BoolName = "hasNeedolinMemoryPowerup" };
    public static Item Drifter_s_Cloak_Item => new PDBoolItem { Name = ItemNames.Drifter_s_Cloak, BoolName = "hasBrolly" };
    public static Item Faydown_Cloak_Item => new PDBoolItem { Name = ItemNames.Faydown_Cloak, BoolName = "hasDoubleJump" };
    public static Item Needle_Strike_Item => new PDBoolItem { Name = ItemNames.Needle_Strike, BoolName = "hasChargeSlash" };

    public static Dictionary<string, Item> GetBaseItems()
    {
        return typeof(BaseItemList).GetProperties().Select(p => (Item)p.GetValue(null)).ToDictionary(i => i.Name);
    }
}
