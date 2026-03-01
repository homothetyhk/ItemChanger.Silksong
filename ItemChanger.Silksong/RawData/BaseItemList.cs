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

    public static Item Bellway__Deep_Docks => new PDBoolItem { Name = ItemNames.Bellway__Deep_Docks, BoolName = nameof(PlayerData.UnlockedDocksStation) };
    public static Item Bellway__Far_Fields => new PDBoolItem { Name = ItemNames.Bellway__Far_Fields, BoolName = nameof(PlayerData.UnlockedBoneforestEastStation) };
    public static Item Bellway__Greymoor => new PDBoolItem { Name = ItemNames.Bellway__Greymoor, BoolName = nameof(PlayerData.UnlockedGreymoorStation) };
    public static Item Bellway__Bellhart => new PDBoolItem { Name = ItemNames.Bellway__Bellhart, BoolName = nameof(PlayerData.UnlockedBelltownStation) };
    public static Item Bellway__Blasted_Steps => new PDBoolItem { Name = ItemNames.Bellway__Blasted_Steps, BoolName = nameof(PlayerData.UnlockedCoralTowerStation) };
    public static Item Bellway__Grand_Bellway => new PDBoolItem { Name = ItemNames.Bellway__Grand_Bellway, BoolName = nameof(PlayerData.UnlockedCityStation) };
    public static Item Bellway__The_Slab => new PDBoolItem { Name = ItemNames.Bellway__The_Slab, BoolName = nameof(PlayerData.UnlockedPeakStation) };
    public static Item Bellway__Shellwood => new PDBoolItem { Name = ItemNames.Bellway__Shellwood, BoolName = nameof(PlayerData.UnlockedShellwoodStation) };
    public static Item Bellway__Bilewater => new PDBoolItem { Name = ItemNames.Bellway__Bilewater, BoolName = nameof(PlayerData.UnlockedShadowStation) };
    public static Item Bellway__Putrified_Ducts => new PDBoolItem { Name = ItemNames.Bellway__Putrified_Ducts, BoolName = nameof(PlayerData.UnlockedAqueductStation) };

    public static Item Ventrica__Choral_Chambers => new PDBoolItem { Name = ItemNames.Ventrica__Choral_Chambers, BoolName = nameof(PlayerData.UnlockedSongTube) };
    public static Item Ventrica__Underworks => new PDBoolItem { Name = ItemNames.Ventrica__Underworks, BoolName = nameof(PlayerData.UnlockedUnderTube) };
    public static Item Ventrica__Grand_Bellway => new PDBoolItem { Name = ItemNames.Ventrica__Grand_Bellway, BoolName = nameof(PlayerData.UnlockedCityBellwayTube) };
    public static Item Ventrica__High_Halls => new PDBoolItem { Name = ItemNames.Ventrica__High_Halls, BoolName = nameof(PlayerData.UnlockedHangTube) };
    public static Item Ventrica__First_Shrine => new PDBoolItem { Name = ItemNames.Ventrica__First_Shrine, BoolName = nameof(PlayerData.UnlockedEnclaveTube) };
    public static Item Ventrica__Memorium => new PDBoolItem { Name = ItemNames.Ventrica__Memorium, BoolName = nameof(PlayerData.UnlockedArboriumTube) };

    public static Dictionary<string, Item> GetBaseItems()
    {
        return typeof(BaseItemList).GetProperties().Select(p => (Item)p.GetValue(null)).ToDictionary(i => i.Name);
    }
}
