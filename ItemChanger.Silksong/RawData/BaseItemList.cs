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
            // bellhome furnishings
            yield return new PlayerDataBoolItem { Name = Desk, FieldName = "BelltownFurnishingDesk" };
            yield return new PlayerDataBoolItem { Name = Gleamlights, FieldName = "BelltownFurnishingFairyLights" };
            yield return new PlayerDataBoolItem { Name = Gramophone, FieldName = "BelltownFurnishingGramaphone" };
            yield return new PlayerDataBoolItem { Name = Personal_Spa, FieldName = "BelltownFurnishingSpa" };

            // bell lacquers (house paint colors: 0=Unpainted, 1=Red, 2=White, 3=Black, 4=Bronze, 5=Blue, 6=Chrome)
            yield return new PlayerDataIntItem { Name = Bell_Lacquer__Red, FieldName = "BelltownHouseColour", Amount = 1 };
            yield return new PlayerDataIntItem { Name = Bell_Lacquer__White, FieldName = "BelltownHouseColour", Amount = 2 };
            yield return new PlayerDataIntItem { Name = Bell_Lacquer__Black, FieldName = "BelltownHouseColour", Amount = 3 };
            yield return new PlayerDataIntItem { Name = Bell_Lacquer__Bronze, FieldName = "BelltownHouseColour", Amount = 4 };
            yield return new PlayerDataIntItem { Name = Bell_Lacquer__Blue, FieldName = "BelltownHouseColour", Amount = 5 };
            yield return new PlayerDataIntItem { Name = Bell_Lacquer__Chrome, FieldName = "BelltownHouseColour", Amount = 6 };
        }

        public static Dictionary<string, Item> GetBaseItems()
        {
            return GetItems().ToDictionary(item => item.Name);
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
