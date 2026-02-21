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

    public static Item Silk_Heart => new PlayerDataIntItem { Name = ItemNames.Silk_Heart, FieldName = "silkRegenMax" };
    public static Item Mask_Shard => new MaskShardItem { Name = ItemNames.Mask_Shard };
    public static Item Spool_Fragment => new SpoolFragmentItem { Name = ItemNames.Spool_Fragment };

    public static Item Vesticrest_Blue_Expansion => new PDBoolItem { Name = "Vesticrest_Blue-Expansion", BoolName = "UnlockedExtraBlueSlot" };
    public static Item Vesticrest_Yellow => new PDBoolItem { Name = "Vesticrest_Yellow", BoolName = "UnlockedExtraYellowSlot" };

    public static Item Architect_s_Melody => new PDBoolItem { Name = "Architect's_Melody", BoolName = "HasMelodyArchitect" };
    public static Item Conductor_s_Melody => new PDBoolItem { Name = "Conductor's_Melody", BoolName = "HasMelodyConductor" };

    public static Dictionary<string, Item> GetBaseItems()
    {
        return typeof(BaseItemList).GetProperties().Select(p => (Item)p.GetValue(null)).ToDictionary(i => i.Name);
    }
}
