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

        // Resources (stat upgrades)
        public static Item Silk_Heart => new PlayerDataIntItem { Name = ItemNames.Silk_Heart, FieldName = "silkRegenMax" };
        public static Item Mask_Shard => new MaskShardItem { Name = ItemNames.Mask_Shard };
        public static Item Spool_Fragment => new SpoolFragmentItem { Name = ItemNames.Spool_Fragment };
        public static Item Tool_Pouch => new PlayerDataIntItem { Name = ItemNames.Tool_Pouch, FieldName = "ToolPouchUpgrades" };
        public static Item Crafting_Kit => new PlayerDataIntItem { Name = ItemNames.Crafting_Kit, FieldName = "ToolKitUpgrades" };

        // Vesticrests
        public static Item Vesticrest_Blue_Expansion => new PlayerDataBoolItem { Name = "Vesticrest_Blue-Expansion", FieldName = "UnlockedExtraBlueSlot" };
        public static Item Vesticrest_Yellow => new PlayerDataBoolItem { Name = "Vesticrest_Yellow", FieldName = "UnlockedExtraYellowSlot" };

        // Keys (PlayerData)
        public static Item Key_of_Apostate => new PlayerDataBoolItem { Name = "Key_of_Apostate", FieldName = "HasSlabKeyC" };
        public static Item Key_of_Heretic => new PlayerDataBoolItem { Name = "Key_of_Heretic", FieldName = "HasSlabKeyB" };
        public static Item Key_of_Indolent => new PlayerDataBoolItem { Name = "Key_of_Indolent", FieldName = "HasSlabKeyA" };

        // Melodies
        public static Item Architect_s_Melody => new PlayerDataBoolItem { Name = "Architect's_Melody", FieldName = "HasMelodyArchitect" };
        public static Item Conductor_s_Melody => new PlayerDataBoolItem { Name = "Conductor's_Melody", FieldName = "HasMelodyConductor" };
        public static Item Vaultkeeper_s_Melody => new PlayerDataBoolItem { Name = "Vaultkeeper's_Melody", FieldName = "HasMelodyLibrarian" };

        public static Dictionary<string, Item> GetBaseItems()
        {
            return typeof(BaseItemList).GetProperties().Select(p => (Item)p.GetValue(null)).ToDictionary(i => i.Name);
        }
    }
}
