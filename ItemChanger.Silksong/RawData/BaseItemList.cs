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
        public static Item Surgeon_s_Key => new ItemChangerCollectableItem
        {
            Name = ItemNames.Surgeon_s_Key,
            CollectableName = "Ward Boss Key",
            UIDef = new CollectableUIDef { CollectableName = "Ward Boss Key" },
        };

        public static Item Swift_Step_Item => new AbilityItem { Name = Swift_Step, FieldName = "hasDash", HasSeenField = "HasSeenDash" };
        public static Item Cling_Grip_Item => new AbilityItem { Name = Cling_Grip, FieldName = "hasWalljump", HasSeenField = "HasSeenWalljump" };
        public static Item Clawline_Item => new AbilityItem { Name = Clawline, FieldName = "hasHarpoonDash", HasSeenField = "HasSeenHarpoon" };
        public static Item Silk_Soar_Item => new AbilityItem { Name = Silk_Soar, FieldName = "hasSuperJump", HasSeenField = "HasSeenSuperJump" };
        public static Item Needolin_Item => new AbilityItem { Name = Needolin, FieldName = "hasNeedolin", HasSeenField = "HasSeenNeedolin" };
        public static Item Sylphsong_Item => new AbilityItem { Name = Sylphsong, FieldName = "HasBoundCrestUpgrader", HasSeenField = "HasSeenEvaHeal" };
        
        public static Item Bind_Item => new BindItem { Name = Bind };
        public static Item Beastling_Call_Item => new PlayerDataBoolItem { Name = Beastling_Call, FieldName = "UnlockedFastTravelTeleport" };
        public static Item Elegy_of_the_Deep_Item => new PlayerDataBoolItem { Name = Elegy_of_the_Deep, FieldName = "hasNeedolinMemoryPowerup" };
        public static Item Drifter_s_Cloak_Item => new PlayerDataBoolItem { Name = Drifter_s_Cloak, FieldName = "hasBrolly" };
        public static Item Faydown_Cloak_Item => new PlayerDataBoolItem { Name = Faydown_Cloak, FieldName = "hasDoubleJump" };
        public static Item Needle_Strike_Item => new PlayerDataBoolItem { Name = Needle_Strike, FieldName = "hasChargeSlash" };

        public static Dictionary<string, Item> GetBaseItems()
        {
            return typeof(BaseItemList).GetProperties().Select(p => (Item)p.GetValue(null)).ToDictionary(i => i.Name);
        }
    }
}
