using ItemChanger.Items;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Items;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.UIDefs;

namespace ItemChanger.Silksong.RawData
{
    internal static class BaseItemList
    {
        public static IEnumerable<Item> GetItems()
        {
            // bellway stations
            yield return new PlayerDataBoolItem { Name = Bellway__Deep_Docks, FieldName = "UnlockedDocksStation" };
            yield return new PlayerDataBoolItem { Name = Bellway__Far_Fields, FieldName = "UnlockedBoneforestEastStation" };
            yield return new PlayerDataBoolItem { Name = Bellway__Greymoor, FieldName = "UnlockedGreymoorStation" };
            yield return new PlayerDataBoolItem { Name = Bellway__Bellhart, FieldName = "UnlockedBelltownStation" };
            yield return new PlayerDataBoolItem { Name = Bellway__Blasted_Steps, FieldName = "UnlockedCoralTowerStation" };
            yield return new PlayerDataBoolItem { Name = Bellway__Grand_Bellway, FieldName = "UnlockedCityStation" };
            yield return new PlayerDataBoolItem { Name = Bellway__The_Slab, FieldName = "UnlockedPeakStation" };
            yield return new PlayerDataBoolItem { Name = Bellway__Shellwood, FieldName = "UnlockedShellwoodStation" };
            yield return new PlayerDataBoolItem { Name = Bellway__Bilewater, FieldName = "UnlockedShadowStation" };
            yield return new PlayerDataBoolItem { Name = Bellway__Putrified_Ducts, FieldName = "UnlockedAqueductStation" };

            // ventrica tubes
            yield return new PlayerDataBoolItem { Name = Ventrica__Choral_Chambers, FieldName = "UnlockedSongTube" };
            yield return new PlayerDataBoolItem { Name = Ventrica__Underworks, FieldName = "UnlockedUnderTube" };
            yield return new PlayerDataBoolItem { Name = Ventrica__Grand_Bellway, FieldName = "UnlockedCityBellwayTube" };
            yield return new PlayerDataBoolItem { Name = Ventrica__High_Halls, FieldName = "UnlockedHangTube" };
            yield return new PlayerDataBoolItem { Name = Ventrica__First_Shrine, FieldName = "UnlockedEnclaveTube" };
            yield return new PlayerDataBoolItem { Name = Ventrica__Memorium, FieldName = "UnlockedArboriumTube" };
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
