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

    public static Item Shell_Marker => new MarkerItem { Name = ItemNames.Shell_Marker, FieldName = "hasMarker_a" };
    public static Item Ring_Marker => new MarkerItem { Name = ItemNames.Ring_Marker, FieldName = "hasMarker_b" };
    public static Item Hunt_Marker => new MarkerItem { Name = ItemNames.Hunt_Marker, FieldName = "hasMarker_c" };
    public static Item Dark_Marker => new MarkerItem { Name = ItemNames.Dark_Marker, FieldName = "hasMarker_d" };
    public static Item Bronze_Marker => new MarkerItem { Name = ItemNames.Bronze_Marker, FieldName = "hasMarker_e" };

    public static Item Bench_Pins => new PDBoolItem { Name = ItemNames.Bench_Pins, BoolName = "hasPinBench" };
    public static Item Bellway_Pins => new PDBoolItem { Name = ItemNames.Bellway_Pins, BoolName = "hasPinStag" };
    public static Item Vendor_Pins => new PDBoolItem { Name = ItemNames.Vendor_Pins, BoolName = "hasPinShop" };
    public static Item Ventrica_Pins => new PDBoolItem { Name = ItemNames.Ventrica_Pins, BoolName = "hasPinTube" };

    public static Item Flea_Pins_Marrowlands => new PDBoolItem { Name = "Flea_Pins_Marrowlands", BoolName = "hasPinFleaMarrowlands" };
    public static Item Flea_Pins_Midlands => new PDBoolItem { Name = "Flea_Pins_Midlands", BoolName = "hasPinFleaMidlands" };
    public static Item Flea_Pins_Blastedlands => new PDBoolItem { Name = "Flea_Pins_Blastedlands", BoolName = "hasPinFleaBlastedlands" };
    public static Item Flea_Pins_Citadel => new PDBoolItem { Name = "Flea_Pins_Citadel", BoolName = "hasPinFleaCitadel" };
    public static Item Flea_Pins_Peaklands => new PDBoolItem { Name = "Flea_Pins_Peaklands", BoolName = "hasPinFleaPeaklands" };
    public static Item Flea_Pins_Mucklands => new PDBoolItem { Name = "Flea_Pins_Mucklands", BoolName = "hasPinFleaMucklands" };

    public static Item Bellhart_Map => new PDBoolItem { Name = ItemNames.Bellhart_Map, BoolName = "HasBellhartMap" };
    public static Item Bilewater_Map => new PDBoolItem { Name = ItemNames.Bilewater_Map, BoolName = "HasBilewaterMap" };
    public static Item Blasted_Steps_Map => new PDBoolItem { Name = ItemNames.Blasted_Steps_Map, BoolName = "HasBlastedStepsMap" };
    public static Item Choral_Chambers_Map => new PDBoolItem { Name = ItemNames.Choral_Chambers_Map, BoolName = "HasChoralChambersMap" };
    public static Item Deep_Docks_Map => new PDBoolItem { Name = ItemNames.Deep_Docks_Map, BoolName = "HasDeepDocksMap" };
    public static Item Far_Fields_Map => new PDBoolItem { Name = ItemNames.Far_Fields_Map, BoolName = "HasFarFieldsMap" };
    public static Item Greymoor_Map => new PDBoolItem { Name = ItemNames.Greymoor_Map, BoolName = "HasGreymoorMap" };
    public static Item Mosslands_Map => new PDBoolItem { Name = ItemNames.Mosslands_Map, BoolName = "HasMosslandsMap" };
    public static Item Shellwood_Map => new PDBoolItem { Name = ItemNames.Shellwood_Map, BoolName = "HasShellwoodMap" };
    public static Item The_Marrow_Map => new PDBoolItem { Name = ItemNames.The_Marrow_Map, BoolName = "HasTheMarrowMap" };
    public static Item The_Slab_Map => new PDBoolItem { Name = ItemNames.The_Slab_Map, BoolName = "HasTheSlabMap" };
    public static Item Underworks_Map => new PDBoolItem { Name = ItemNames.Underworks_Map, BoolName = "HasUnderworksMap" };
    public static Item Verdania_Map => new PDBoolItem { Name = ItemNames.Verdania_Map, BoolName = "HasVerdaniaMap" };

    public static Dictionary<string, Item> GetBaseItems()
    {
        return typeof(BaseItemList).GetProperties().Select(p => (Item)p.GetValue(null)).ToDictionary(i => i.Name);
    }
}
