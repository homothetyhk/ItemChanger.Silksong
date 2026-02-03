using ItemChanger.Items;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Items;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.UIDefs;

namespace ItemChanger.Silksong.RawData
{
    internal static class BaseItemList
    {
        public static Item Surgeon_s_Key => new ItemChangerCollectableItem
        {
            Name = ItemNames.Surgeon_s_Key,
            CollectableName = "Ward Boss Key",
            UIDef = new CollectableUIDef { CollectableName = "Ward Boss Key" },
        };

        // Map Markers
        public static Item Shell_Marker => new MarkerItem { Name = ItemNames.Shell_Marker, FieldName = "hasMarker_a" };
        public static Item Ring_Marker => new MarkerItem { Name = ItemNames.Ring_Marker, FieldName = "hasMarker_b" };
        public static Item Hunt_Marker => new MarkerItem { Name = ItemNames.Hunt_Marker, FieldName = "hasMarker_c" };
        public static Item Dark_Marker => new MarkerItem { Name = ItemNames.Dark_Marker, FieldName = "hasMarker_d" };
        public static Item Bronze_Marker => new MarkerItem { Name = ItemNames.Bronze_Marker, FieldName = "hasMarker_e" };

        // Map Pins
        public static Item Bench_Pins => new PlayerDataBoolItem { Name = ItemNames.Bench_Pins, FieldName = "hasPinBench" };
        public static Item Bellway_Pins => new PlayerDataBoolItem { Name = ItemNames.Bellway_Pins, FieldName = "hasPinStag" };
        public static Item Vendor_Pins => new PlayerDataBoolItem { Name = ItemNames.Vendor_Pins, FieldName = "hasPinShop" };
        public static Item Ventrica_Pins => new PlayerDataBoolItem { Name = ItemNames.Ventrica_Pins, FieldName = "hasPinTube" };

        // Flea Pins (regional)
        public static Item Flea_Pins_Marrowlands => new PlayerDataBoolItem { Name = "Flea_Pins_Marrowlands", FieldName = "hasPinFleaMarrowlands" };
        public static Item Flea_Pins_Midlands => new PlayerDataBoolItem { Name = "Flea_Pins_Midlands", FieldName = "hasPinFleaMidlands" };
        public static Item Flea_Pins_Blastedlands => new PlayerDataBoolItem { Name = "Flea_Pins_Blastedlands", FieldName = "hasPinFleaBlastedlands" };
        public static Item Flea_Pins_Citadel => new PlayerDataBoolItem { Name = "Flea_Pins_Citadel", FieldName = "hasPinFleaCitadel" };
        public static Item Flea_Pins_Peaklands => new PlayerDataBoolItem { Name = "Flea_Pins_Peaklands", FieldName = "hasPinFleaPeaklands" };
        public static Item Flea_Pins_Mucklands => new PlayerDataBoolItem { Name = "Flea_Pins_Mucklands", FieldName = "hasPinFleaMucklands" };

        // Area Maps
        public static Item Bellhart_Map => new PlayerDataBoolItem { Name = ItemNames.Bellhart_Map, FieldName = "HasBellhartMap" };
        public static Item Bilewater_Map => new PlayerDataBoolItem { Name = ItemNames.Bilewater_Map, FieldName = "HasBilewaterMap" };
        public static Item Blasted_Steps_Map => new PlayerDataBoolItem { Name = ItemNames.Blasted_Steps_Map, FieldName = "HasBlastedStepsMap" };
        public static Item Choral_Chambers_Map => new PlayerDataBoolItem { Name = ItemNames.Choral_Chambers_Map, FieldName = "HasChoralChambersMap" };
        public static Item Deep_Docks_Map => new PlayerDataBoolItem { Name = ItemNames.Deep_Docks_Map, FieldName = "HasDeepDocksMap" };
        public static Item Far_Fields_Map => new PlayerDataBoolItem { Name = ItemNames.Far_Fields_Map, FieldName = "HasFarFieldsMap" };
        public static Item Greymoor_Map => new PlayerDataBoolItem { Name = ItemNames.Greymoor_Map, FieldName = "HasGreymoorMap" };
        public static Item Mosslands_Map => new PlayerDataBoolItem { Name = ItemNames.Mosslands_Map, FieldName = "HasMosslandsMap" };
        public static Item Shellwood_Map => new PlayerDataBoolItem { Name = ItemNames.Shellwood_Map, FieldName = "HasShellwoodMap" };
        public static Item The_Marrow_Map => new PlayerDataBoolItem { Name = ItemNames.The_Marrow_Map, FieldName = "HasTheMarrowMap" };
        public static Item The_Slab_Map => new PlayerDataBoolItem { Name = ItemNames.The_Slab_Map, FieldName = "HasTheSlabMap" };
        public static Item Underworks_Map => new PlayerDataBoolItem { Name = ItemNames.Underworks_Map, FieldName = "HasUnderworksMap" };
        public static Item Verdania_Map => new PlayerDataBoolItem { Name = ItemNames.Verdania_Map, FieldName = "HasVerdaniaMap" };

        public static Dictionary<string, Item> GetBaseItems()
        {
            return typeof(BaseItemList).GetProperties().Select(p => (Item)p.GetValue(null)).ToDictionary(i => i.Name);
        }
    }
}
