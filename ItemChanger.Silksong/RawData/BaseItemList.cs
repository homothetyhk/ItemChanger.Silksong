using ItemChanger.Items;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Items;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.UIDefs;

namespace ItemChanger.Silksong.RawData
{
    internal static class BaseItemList
    {
        // bellway stations
        public static Item Bellway__Deep_Docks => new PDBoolItem { Name = ItemNames.Bellway__Deep_Docks, BoolName = "UnlockedDocksStation" };
        public static Item Bellway__Far_Fields => new PDBoolItem { Name = ItemNames.Bellway__Far_Fields, BoolName = "UnlockedBoneforestEastStation" };
        public static Item Bellway__Greymoor => new PDBoolItem { Name = ItemNames.Bellway__Greymoor, BoolName = "UnlockedGreymoorStation" };
        public static Item Bellway__Bellhart => new PDBoolItem { Name = ItemNames.Bellway__Bellhart, BoolName = "UnlockedBelltownStation" };
        public static Item Bellway__Blasted_Steps => new PDBoolItem { Name = ItemNames.Bellway__Blasted_Steps, BoolName = "UnlockedCoralTowerStation" };
        public static Item Bellway__Grand_Bellway => new PDBoolItem { Name = ItemNames.Bellway__Grand_Bellway, BoolName = "UnlockedCityStation" };
        public static Item Bellway__The_Slab => new PDBoolItem { Name = ItemNames.Bellway__The_Slab, BoolName = "UnlockedPeakStation" };
        public static Item Bellway__Shellwood => new PDBoolItem { Name = ItemNames.Bellway__Shellwood, BoolName = "UnlockedShellwoodStation" };
        public static Item Bellway__Bilewater => new PDBoolItem { Name = ItemNames.Bellway__Bilewater, BoolName = "UnlockedShadowStation" };
        public static Item Bellway__Putrified_Ducts => new PDBoolItem { Name = ItemNames.Bellway__Putrified_Ducts, BoolName = "UnlockedAqueductStation" };

        // ventrica tubes
        public static Item Ventrica__Choral_Chambers => new PDBoolItem { Name = ItemNames.Ventrica__Choral_Chambers, BoolName = "UnlockedSongTube" };
        public static Item Ventrica__Underworks => new PDBoolItem { Name = ItemNames.Ventrica__Underworks, BoolName = "UnlockedUnderTube" };
        public static Item Ventrica__Grand_Bellway => new PDBoolItem { Name = ItemNames.Ventrica__Grand_Bellway, BoolName = "UnlockedCityBellwayTube" };
        public static Item Ventrica__High_Halls => new PDBoolItem { Name = ItemNames.Ventrica__High_Halls, BoolName = "UnlockedHangTube" };
        public static Item Ventrica__First_Shrine => new PDBoolItem { Name = ItemNames.Ventrica__First_Shrine, BoolName = "UnlockedEnclaveTube" };
        public static Item Ventrica__Memorium => new PDBoolItem { Name = ItemNames.Ventrica__Memorium, BoolName = "UnlockedArboriumTube" };

        public static Item Surgeon_s_Key => new ItemChangerCollectableItem
        {
            Name = ItemNames.Surgeon_s_Key,
            CollectableName = "Ward Boss Key",
            UIDef = new CollectableUIDef { CollectableName = "Ward Boss Key" },
        };

        public static Dictionary<string, Item> GetBaseItems()
        {
            return typeof(BaseItemList).GetProperties().Select(p => (Item)p.GetValue(null)).ToDictionary(i => i.Name);
        }
    }
}
