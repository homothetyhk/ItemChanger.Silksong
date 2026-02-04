using ItemChanger.Items;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Items;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.UIDefs;
using static ItemChanger.Silksong.RawData.ItemNames;

namespace ItemChanger.Silksong.RawData
{
    internal static class BaseItemList
    {
        public static IEnumerable<Item> GetItems()
        {
            // directional walljump (cling grip)
            yield return new ClingGripItem { Name = Left_Cling_Grip, Direction = ClingGripDirection.Left };
            yield return new ClingGripItem { Name = Right_Cling_Grip, Direction = ClingGripDirection.Right };

            // directional dash (swift step)
            yield return new SwiftStepItem { Name = Left_Swift_Step, Direction = SwiftStepDirection.Left };
            yield return new SwiftStepItem { Name = Right_Swift_Step, Direction = SwiftStepDirection.Right };

            // directional clawline
            yield return new ClawlineItem { Name = Left_Clawline, Direction = ClawlineDirection.Left };
            yield return new ClawlineItem { Name = Right_Clawline, Direction = ClawlineDirection.Right };

            // directional slashes
            yield return new SlashItem { Name = Leftslash, Direction = SlashDirection.Left };
            yield return new SlashItem { Name = Rightslash, Direction = SlashDirection.Right };
            yield return new SlashItem { Name = Upslash, Direction = SlashDirection.Up };
            yield return new SlashItem { Name = Downslash, Direction = SlashDirection.Down };

            // taunt
            yield return new TauntItem { Name = Taunt };
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
