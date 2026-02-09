using ItemChanger.Items;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Items;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.UIDefs;

namespace ItemChanger.Silksong.RawData
{
    internal static partial class BaseItemList
    {

        //silk skills
        public static Item Cross_Stitch => new ItemChangerToolItemSkill
        {
            Name = ItemNames.Cross_Stitch,
            CollectableName = "Parry",
            UIDef = new CollectableUIDef { CollectableName = "Parry" },
        };
        public static Item Pale_Nails => new ItemChangerToolItemSkill
        {
            Name = ItemNames.Pale_Nails,
            CollectableName = "Silk Boss Needle",
            UIDef = new CollectableUIDef { CollectableName = "Silk Boss Needle" },
        };
        public static Item Rune_Rage => new ItemChangerToolItemSkill
        {
            Name = ItemNames.Rune_Rage,
            CollectableName = "Silk Bomb",
            UIDef = new CollectableUIDef { CollectableName = "Silk Bomb" },
        };
        public static Item Sharpdart => new ItemChangerToolItemSkill
        {
            Name = ItemNames.Sharpdart,
            CollectableName = "Silk Charge",
            UIDef = new CollectableUIDef { CollectableName = "Silk Charge" },
        };
        public static Item Silkspear => new ItemChangerToolItemSkill
        {
            Name = ItemNames.Silkspear,
            CollectableName = "Silk Spear",
            UIDef = new CollectableUIDef { CollectableName = "Silk Spear" },
        };
        public static Item Thread_Storm => new ItemChangerToolItemSkill
        {
            Name = ItemNames.Thread_Storm,
            CollectableName = "Thread Sphere",
            UIDef = new CollectableUIDef { CollectableName = "Thread Sphere" },
        };


        //TODO: find a way to implement items that do not follow the CollectableItem class format
        //they seem to not have a CollectableName field or a function for getting their internal names
        //they seem to work through bool operations in PlayerData (example: setting hasDash to true gives Swift Step)
        //listing the ability with corresponding boolean values in PlayerData and other notes


        //ancestral arts
        /*
            Beastling_Call -> UnlockedFastTravelTeleport [requires needolin beforehand]
            Clawline -> hasHarpoonDash
            Cling_Grip -> hasWalljump
            Elegy_of_the_Deep -> hasNeedolinMemoryPowerup [requires needolin beforehand]
            Needolin -> hasNeedolin
            Silk_Soar -> hasSuperJump
            Swift_Step -> hasDash
            Sylphsong -> HasBoundCrestUpgrader
        */


        //other abilities
        //note: drifter cloak and faydown cloak refer to the same internal item Dresses
        /*
            Bind -> [unknown]
            Drifter_s_Cloak -> hasBrolly
            Faydown_Cloak -> hasDoubleJump [can be used without having drifter cloak beforehand]
            Needle_Strike -> hasChargeSlash
        */


        //plot items
        /* unimplemented items
            Architect_s_Melody -> HasMelodyArchitect
            Conductor_s_Melody -> HasMelodyConductor
            Vaultkeeper_s_Melody -> HasMelodyLibrarian
        */
        public static Item Conjoined_Heart => new ItemChangerCollectableItem
        {
            Name = ItemNames.Conjoined_Heart,
            CollectableName = "Clover Heart",
            UIDef = new CollectableUIDef { CollectableName = "Clover Heart" },
        };
        public static Item Encrusted_Heart => new ItemChangerCollectableItem
        {
            Name = ItemNames.Encrusted_Heart,
            CollectableName = "Coral Heart",
            UIDef = new CollectableUIDef { CollectableName = "Coral Heart" },
        };
        public static Item Everbloom => new ItemChangerCollectableItem
        {
            Name = ItemNames.Everbloom,
            CollectableName = "White Flower",
            UIDef = new CollectableUIDef { CollectableName = "White Flower" },
        };
        public static Item Hermit_s_Soul => new ItemChangerCollectableItem
        {
            Name = ItemNames.Hermit_s_Soul,
            CollectableName = "Snare Soul Bell Hermit",
            UIDef = new CollectableUIDef { CollectableName = "Snare Soul Bell Hermit" },
        };
        public static Item Hunter_s_Heart => new ItemChangerCollectableItem
        {
            Name = ItemNames.Hunter_s_Heart,
            CollectableName = "Hunter Heart",
            UIDef = new CollectableUIDef { CollectableName = "Hunter Heart" },
        };
        public static Item Maiden_s_Soul => new ItemChangerCollectableItem
        {
            Name = ItemNames.Maiden_s_Soul,
            CollectableName = "Snare Soul Churchkeeper",
            UIDef = new CollectableUIDef { CollectableName = "Snare Soul Churchkeeper" },
        };
        public static Item Pollen_Heart => new ItemChangerCollectableItem
        {
            Name = ItemNames.Pollen_Heart,
            CollectableName = "Flower Heart",
            UIDef = new CollectableUIDef { CollectableName = "Flower Heart" },
        };
        public static Item Seeker_s_Soul => new ItemChangerCollectableItem
        {
            Name = ItemNames.Seeker_s_Soul,
            CollectableName = "Snare Soul Swamp Bug",
            UIDef = new CollectableUIDef { CollectableName = "Snare Soul Swamp Bug" },
        };


        //crests
        public static Item Crest_of_Architect => new ItemChangerToolCrest
        {
            Name = ItemNames.Crest_of_Architect,
            CollectableName = "Toolmaster",
            UIDef = new CollectableUIDef { CollectableName = "Toolmaster" },
        };
        public static Item Crest_of_Beast => new ItemChangerToolCrest
        {
            Name = ItemNames.Crest_of_Beast,
            CollectableName = "Warrior",
            UIDef = new CollectableUIDef { CollectableName = "Warrior" },
        };
        public static Item Crest_of_Hunter => new ItemChangerToolCrest
        {
            Name = ItemNames.Crest_of_Hunter,
            CollectableName = "Hunter",
            UIDef = new CollectableUIDef { CollectableName = "Hunter" },
        };
        public static Item Crest_of_Hunter_v2 => new ItemChangerToolCrest
        {
            Name = ItemNames.Crest_of_Hunter_v2,
            CollectableName = "Hunter_v2",
            UIDef = new CollectableUIDef { CollectableName = "Hunter_v2" },
        };
        public static Item Crest_of_Hunter_v3 => new ItemChangerToolCrest
        {
            Name = ItemNames.Crest_of_Hunter_v3,
            CollectableName = "Hunter_v3",
            UIDef = new CollectableUIDef { CollectableName = "Hunter_v3" },
        };
        public static Item Crest_of_Reaper => new ItemChangerToolCrest
        {
            Name = ItemNames.Crest_of_Reaper,
            CollectableName = "Reaper",
            UIDef = new CollectableUIDef { CollectableName = "Reaper" },
        };
        public static Item Crest_of_Shaman => new ItemChangerToolCrest
        {
            Name = ItemNames.Crest_of_Shaman,
            CollectableName = "Spell",
            UIDef = new CollectableUIDef { CollectableName = "Spell" },
        };
        public static Item Crest_of_Wanderer => new ItemChangerToolCrest
        {
            Name = ItemNames.Crest_of_Wanderer,
            CollectableName = "Wanderer",
            UIDef = new CollectableUIDef { CollectableName = "Wanderer" },
        };
        public static Item Crest_of_Witch => new ItemChangerToolCrest
        {
            Name = ItemNames.Crest_of_Witch,
            CollectableName = "Witch",
            UIDef = new CollectableUIDef { CollectableName = "Witch" },
        };
        public static Item Cursed => new ItemChangerToolCrest//not sure to include
        {
            Name = ItemNames.Cursed,
            CollectableName = "Cursed",
            UIDef = new CollectableUIDef { CollectableName = "Cursed" },
        };
        public static Item Cloakless => new ItemChangerToolCrest//not sure to include
        {
            Name = ItemNames.Cloakless,
            CollectableName = "Cloakless",
            UIDef = new CollectableUIDef { CollectableName = "Cloakless" },
        };


        //red tools
        public static Item Cogfly => new ItemChangerToolItem
        {
            Name = ItemNames.Cogfly,
            CollectableName = "Cogwork Flier",
            UIDef = new CollectableUIDef { CollectableName = "Cogwork Flier" },
        };
        public static Item Cogwork_Wheel => new ItemChangerToolItem
        {
            Name = ItemNames.Cogwork_Wheel,
            CollectableName = "Cogwork Saw",
            UIDef = new CollectableUIDef { CollectableName = "Cogwork Saw" },
        };
        public static Item Conchcutter => new ItemChangerToolItem
        {
            Name = ItemNames.Conchcutter,
            CollectableName = "Conch Drill",
            UIDef = new CollectableUIDef { CollectableName = "Conch Drill" },
        };
        public static Item Curveclaw => new ItemChangerToolItem
        {
            Name = ItemNames.Curveclaw,
            CollectableName = "Curve Claws",
            UIDef = new CollectableUIDef { CollectableName = "Curve Claws" },
        };
        public static Item Curvesickle => new ItemChangerToolItem
        {
            Name = ItemNames.Curvesickle,
            CollectableName = "Curve Claws Upgraded",
            UIDef = new CollectableUIDef { CollectableName = "Curve Claws Upgraded" },
        };
        public static Item Delver_s_Drill => new ItemChangerToolItem
        {
            Name = ItemNames.Delver_s_Drill,
            CollectableName = "Screw Attack",
            UIDef = new CollectableUIDef { CollectableName = "Screw Attack" },
        };
        public static Item Flea_Brew => new ItemChangerToolItem
        {
            Name = ItemNames.Flea_Brew,
            CollectableName = "Flea Brew",
            UIDef = new CollectableUIDef { CollectableName = "Flea Brew" },
        };
        public static Item Flintslate => new ItemChangerToolItem
        {
            Name = ItemNames.Flintslate,
            CollectableName = "Flintstone",
            UIDef = new CollectableUIDef { CollectableName = "Flintstone" },
        };
        public static Item Longpin => new ItemChangerToolItem
        {
            Name = ItemNames.Longpin,
            CollectableName = "Harpoon",
            UIDef = new CollectableUIDef { CollectableName = "Harpoon" },
        };
        public static Item Needle_Phial => new ItemChangerToolItem
        {
            Name = ItemNames.Needle_Phial,
            CollectableName = "Extractor",
            UIDef = new CollectableUIDef { CollectableName = "Extractor" },
        };
        public static Item Pimpillo => new ItemChangerToolItem
        {
            Name = ItemNames.Pimpillo,
            CollectableName = "Pimpilo",
            UIDef = new CollectableUIDef { CollectableName = "Pimpilo" },
        };
        public static Item Plasmium_Phial => new ItemChangerToolItem
        {
            Name = ItemNames.Plasmium_Phial,
            CollectableName = "Lifeblood Syringe",
            UIDef = new CollectableUIDef { CollectableName = "Lifeblood Syringe" },
        };
        public static Item Rosary_Cannon => new ItemChangerToolItem
        {
            Name = ItemNames.Rosary_Cannon,
            CollectableName = "Rosary Cannon",
            UIDef = new CollectableUIDef { CollectableName = "Rosary Cannon" },
        };
        public static Item Silkshot__Forge_Daughter => new ItemChangerToolItem
        {
            Name = ItemNames.Silkshot__Forge_Daughter,
            CollectableName = "WebShot Forge",
            UIDef = new CollectableUIDef { CollectableName = "WebShot Forge" },
        };
        public static Item Silkshot__Original => new ItemChangerToolItem
        {
            Name = ItemNames.Silkshot__Original,
            CollectableName = "WebShot Weaver",
            UIDef = new CollectableUIDef { CollectableName = "WebShot Weaver" },
        };
        public static Item Silkshot__Twelfth_Architect => new ItemChangerToolItem
        {
            Name = ItemNames.Silkshot__Twelfth_Architect,
            CollectableName = "WebShot Architect",
            UIDef = new CollectableUIDef { CollectableName = "WebShot Architect" },
        };
        public static Item Snare_Setter => new ItemChangerToolItem
        {
            Name = ItemNames.Snare_Setter,
            CollectableName = "Silk Snare",
            UIDef = new CollectableUIDef { CollectableName = "Silk Snare" },
        };
        public static Item Sting_Shard => new ItemChangerToolItem
        {
            Name = ItemNames.Sting_Shard,
            CollectableName = "Sting Shard",
            UIDef = new CollectableUIDef { CollectableName = "Sting Shard" },
        };
        public static Item Straight_Pin => new ItemChangerToolItem
        {
            Name = ItemNames.Straight_Pin,
            CollectableName = "Straight Pin",
            UIDef = new CollectableUIDef { CollectableName = "Straight Pin" },
        };
        public static Item Tacks => new ItemChangerToolItem
        {
            Name = ItemNames.Tacks,
            CollectableName = "Tack",
            UIDef = new CollectableUIDef { CollectableName = "Tack" },
        };
        public static Item Threefold_Pin => new ItemChangerToolItem
        {
            Name = ItemNames.Threefold_Pin,
            CollectableName = "Tri Pin",
            UIDef = new CollectableUIDef { CollectableName = "Tri Pin" },
        };
        public static Item Throwing_Ring => new ItemChangerToolItem
        {
            Name = ItemNames.Throwing_Ring,
            CollectableName = "Shakra Ring",
            UIDef = new CollectableUIDef { CollectableName = "Shakra Ring" },
        };
        public static Item Voltvessels => new ItemChangerToolItem
        {
            Name = ItemNames.Voltvessels,
            CollectableName = "Lightning Rod",
            UIDef = new CollectableUIDef { CollectableName = "Lightning Rod" },
        };


        //blue tools
        public static Item Claw_Mirror => new ItemChangerToolItem
        {
            Name = ItemNames.Claw_Mirror,
            CollectableName = "Dazzle Bind",
            UIDef = new CollectableUIDef { CollectableName = "Dazzle Bind" },
        };
        public static Item Claw_Mirrors => new ItemChangerToolItem
        {
            Name = ItemNames.Claw_Mirrors,
            CollectableName = "Dazzle Bind Upgraded",
            UIDef = new CollectableUIDef { CollectableName = "Dazzle Bind Upgraded" },
        };
        public static Item Druid_s_Eye => new ItemChangerToolItem
        {
            Name = ItemNames.Druid_s_Eye,
            CollectableName = "Mosscreep Tool 1",
            UIDef = new CollectableUIDef { CollectableName = "Mosscreep Tool 1" },
        };
        public static Item Druid_s_Eyes => new ItemChangerToolItem
        {
            Name = ItemNames.Druid_s_Eyes,
            CollectableName = "Mosscreep Tool 2",
            UIDef = new CollectableUIDef { CollectableName = "Mosscreep Tool 2" },
        };
        public static Item Egg_of_Flealia => new ItemChangerToolItem
        {
            Name = ItemNames.Egg_of_Flealia,
            CollectableName = "Flea Charm",
            UIDef = new CollectableUIDef { CollectableName = "Flea Charm" },
        };
        public static Item Fractured_Mask => new ItemChangerToolItem
        {
            Name = ItemNames.Fractured_Mask,
            CollectableName = "Fractured Mask",
            UIDef = new CollectableUIDef { CollectableName = "Fractured Mask" },
        };
        public static Item Injector_Band => new ItemChangerToolItem
        {
            Name = ItemNames.Injector_Band,
            CollectableName = "Quickbind",
            UIDef = new CollectableUIDef { CollectableName = "Quickbind" },
        };
        public static Item Longclaw => new ItemChangerToolItem
        {
            Name = ItemNames.Longclaw,
            CollectableName = "Longneedle",
            UIDef = new CollectableUIDef { CollectableName = "Longneedle" },
        };
        public static Item Magma_Bell => new ItemChangerToolItem
        {
            Name = ItemNames.Magma_Bell,
            CollectableName = "Lava Charm",
            UIDef = new CollectableUIDef { CollectableName = "Lava Charm" },
        };
        public static Item Memory_Crystal => new ItemChangerToolItem
        {
            Name = ItemNames.Memory_Crystal,
            CollectableName = "Revenge Crystal",
            UIDef = new CollectableUIDef { CollectableName = "Revenge Crystal" },
        };
        public static Item Multibinder => new ItemChangerToolItem
        {
            Name = ItemNames.Multibinder,
            CollectableName = "Multibind",
            UIDef = new CollectableUIDef { CollectableName = "Multibind" },
        };
        public static Item Pin_Badge => new ItemChangerToolItem
        {
            Name = ItemNames.Pin_Badge,
            CollectableName = "Pinstress Tool",
            UIDef = new CollectableUIDef { CollectableName = "Pinstress Tool" },
        };
        public static Item Pollip_Pouch => new ItemChangerToolItem
        {
            Name = ItemNames.Pollip_Pouch,
            CollectableName = "Poison Pouch",
            UIDef = new CollectableUIDef { CollectableName = "Poison Pouch" },
        };
        public static Item Quick_Sling => new ItemChangerToolItem
        {
            Name = ItemNames.Quick_Sling,
            CollectableName = "Quick Sling",
            UIDef = new CollectableUIDef { CollectableName = "Quick Sling" },
        };
        public static Item Reserve_Bind => new ItemChangerToolItem
        {
            Name = ItemNames.Reserve_Bind,
            CollectableName = "Reserve Bind",
            UIDef = new CollectableUIDef { CollectableName = "Reserve Bind" },
        };
        public static Item Sawtooth_Circlet => new ItemChangerToolItem
        {
            Name = ItemNames.Sawtooth_Circlet,
            CollectableName = "Brolly Spike",
            UIDef = new CollectableUIDef { CollectableName = "Brolly Spike" },
        };
        public static Item Snitch_Pick => new ItemChangerToolItem
        {
            Name = ItemNames.Snitch_Pick,
            CollectableName = "Thief Claw",
            UIDef = new CollectableUIDef { CollectableName = "Thief Claw" },
        };
        public static Item Spool_Extender => new ItemChangerToolItem
        {
            Name = ItemNames.Spool_Extender,
            CollectableName = "Spool Extender",
            UIDef = new CollectableUIDef { CollectableName = "Spool Extender" },
        };
        public static Item Volt_Filament => new ItemChangerToolItem
        {
            Name = ItemNames.Volt_Filament,
            CollectableName = "Zap Imbuement",
            UIDef = new CollectableUIDef { CollectableName = "Zap Imbuement" },
        };
        public static Item Warding_Bell => new ItemChangerToolItem
        {
            Name = ItemNames.Warding_Bell,
            CollectableName = "Bell Bind",
            UIDef = new CollectableUIDef { CollectableName = "Bell Bind" },
        };
        public static Item Weavelight => new ItemChangerToolItem
        {
            Name = ItemNames.Weavelight,
            CollectableName = "White Ring",
            UIDef = new CollectableUIDef { CollectableName = "White Ring" },
        };
        public static Item Wispfire_Lantern => new ItemChangerToolItem
        {
            Name = ItemNames.Wispfire_Lantern,
            CollectableName = "Wisp Lantern",
            UIDef = new CollectableUIDef { CollectableName = "Wisp Lantern" },
        };
        public static Item Wreath_of_Purity => new ItemChangerToolItem
        {
            Name = ItemNames.Wreath_of_Purity,
            CollectableName = "Maggot Charm",
            UIDef = new CollectableUIDef { CollectableName = "Maggot Charm" },
        };


        //yellow tools
        public static Item Ascendant_s_Grip => new ItemChangerToolItem
        {
            Name = ItemNames.Ascendant_s_Grip,
            CollectableName = "Wallcling",
            UIDef = new CollectableUIDef { CollectableName = "Wallcling" },
        };
        public static Item Barbed_Bracelet => new ItemChangerToolItem
        {
            Name = ItemNames.Barbed_Bracelet,
            CollectableName = "Barbed Wire",
            UIDef = new CollectableUIDef { CollectableName = "Barbed Wire" },
        };
        public static Item Compass => new ItemChangerToolItem
        {
            Name = ItemNames.Compass,
            CollectableName = "Compass",
            UIDef = new CollectableUIDef { CollectableName = "Compass" },
        };
        public static Item Dead_Bug_s_Purse => new ItemChangerToolItem
        {
            Name = ItemNames.Dead_Bug_s_Purse,
            CollectableName = "Dead Mans Purse",
            UIDef = new CollectableUIDef { CollectableName = "Dead Mans Purse" },
        };
        public static Item Magnetite_Brooch => new ItemChangerToolItem
        {
            Name = ItemNames.Magnetite_Brooch,
            CollectableName = "Rosary Magnet",
            UIDef = new CollectableUIDef { CollectableName = "Rosary Magnet" },
        };
        public static Item Magnetite_Dice => new ItemChangerToolItem
        {
            Name = ItemNames.Magnetite_Dice,
            CollectableName = "Magnetite Dice",
            UIDef = new CollectableUIDef { CollectableName = "Magnetite Dice" },
        };
        public static Item Scuttlebrace => new ItemChangerToolItem
        {
            Name = ItemNames.Scuttlebrace,
            CollectableName = "Scuttlebrace",
            UIDef = new CollectableUIDef { CollectableName = "Scuttlebrace" },
        };
        public static Item Shard_Pendant => new ItemChangerToolItem
        {
            Name = ItemNames.Shard_Pendant,
            CollectableName = "Bone Necklace",
            UIDef = new CollectableUIDef { CollectableName = "Bone Necklace" },
        };
        public static Item Shell_Satchel => new ItemChangerToolItem
        {
            Name = ItemNames.Shell_Satchel,
            CollectableName = "Shell Satchel",
            UIDef = new CollectableUIDef { CollectableName = "Shell Satchel" },
        };
        public static Item Silkspeed_Anklets => new ItemChangerToolItem
        {
            Name = ItemNames.Silkspeed_Anklets,
            CollectableName = "Sprintmaster",
            UIDef = new CollectableUIDef { CollectableName = "Sprintmaster" },
        };
        public static Item Spider_Strings => new ItemChangerToolItem
        {
            Name = ItemNames.Spider_Strings,
            CollectableName = "Musician Charm",
            UIDef = new CollectableUIDef { CollectableName = "Musician Charm" },
        };
        public static Item Thief_s_Mark => new ItemChangerToolItem
        {
            Name = ItemNames.Thief_s_Mark,
            CollectableName = "Thief Charm",
            UIDef = new CollectableUIDef { CollectableName = "Thief Charm" },
        };
        public static Item Weighted_Belt => new ItemChangerToolItem
        {
            Name = ItemNames.Weighted_Belt,
            CollectableName = "Weighted Anklet",
            UIDef = new CollectableUIDef { CollectableName = "Weighted Anklet" },
        };


        //items
        /* note: crafting kit and tool pouch refer to the same internal CollectableItem Tool Pouch&Kit Inv
         * unsure if PlayerData has specific bools for crafting kit and tool pouch; PlayerData values ToolPouchUpgrades and ToolKitUpgrades could control tool pouch and crafting kit
         */
        /* unimplemented/todo items
            Mask_Shard
            Silk_Heart
            Spool_Fragment
         */
        //TODO: implement ItemChanger classes for the above items that support changing health/silk values
        //TODO: extend ItemChangerCollectableItem class to support separate values for crafting kit and tool pouch
        public static Item Crafting_Kit => new ItemChangerCollectableItem//refers to same internal item as tool pouch
        {
            Name = ItemNames.Crafting_Kit,
            CollectableName = "Tool Pouch&Kit Inv",
            UIDef = new CollectableUIDef { CollectableName = "Tool Pouch&Kit Inv" },
        };
        public static Item Craftmetal => new ItemChangerCollectableItem
        {
            Name = ItemNames.Craftmetal,
            CollectableName = "Tool Metal",
            UIDef = new CollectableUIDef { CollectableName = "Tool Metal" },
        };
        public static Item Memory_Locket => new ItemChangerCollectableItem
        {
            Name = ItemNames.Memory_Locket,
            CollectableName = "Crest Socket Unlocker",
            UIDef = new CollectableUIDef { CollectableName = "Crest Socket Unlocker" },
        };
        public static Item Pale_Oil => new ItemChangerCollectableItem
        {
            Name = ItemNames.Pale_Oil,
            CollectableName = "Pale_Oil",
            UIDef = new CollectableUIDef { CollectableName = "Pale_Oil" },
        };
        public static Item Plasmium_Gland => new ItemChangerCollectableItem
        {
            Name = ItemNames.Plasmium_Gland,
            CollectableName = "Plasmium Gland",
            UIDef = new CollectableUIDef { CollectableName = "Plasmium Gland" },
        };
        public static Item Tool_Pouch => new ItemChangerCollectableItem//shares same internal item as crafting kit
        {
            Name = ItemNames.Tool_Pouch,
            CollectableName = "Tool Pouch&Kit Inv",
            UIDef = new CollectableUIDef { CollectableName = "Tool Pouch&Kit Invy" },
        };


        //keys
        /* note: all slab keys refer to the same internal CollectableItem Slab Key
         * the slab keys have corresponding PLayerData bools
            Key_of_Apostate -> HasSlabKeyC
            Key_of_Heretic -> HasSlabKeyB
            Key_of_Indolent -> HasSlabKeyA
         */
        //TODO: extend ItemChangerCollectibleItem class for the slab keys to support changing corresponding PlayerData bools for the different key types
        public static Item Architect_s_Key => new ItemChangerCollectableItem
        {
            Name = ItemNames.Architect_s_Key,
            CollectableName = "Architect Key",
            UIDef = new CollectableUIDef { CollectableName = "Architect Key" },
        };
        public static Item Bellhome_Key => new ItemChangerCollectableItem
        {
            Name = ItemNames.Bellhome_Key,
            CollectableName = "Belltown House Key",
            UIDef = new CollectableUIDef { CollectableName = "Belltown House Key" },
        };
        public static Item Craw_Summons => new ItemChangerCollectableItem
        {
            Name = ItemNames.Craw_Summons,
            CollectableName = "Craw Summons",
            UIDef = new CollectableUIDef { CollectableName = "Craw Summons" },
        };
        public static Item Diving_Bell_Key => new ItemChangerCollectableItem
        {
            Name = ItemNames.Diving_Bell_Key,
            CollectableName = "Dock Key",
            UIDef = new CollectableUIDef { CollectableName = "Dock Key" },
        };
        public static Item Key_of_Apostate => new ItemChangerCollectableItem//refers to Slab Key; PlayerData bool HasSlabKeyC
        {
            Name = ItemNames.Key_of_Apostate,
            CollectableName = "Slab Key",
            UIDef = new CollectableUIDef { CollectableName = "Slab Key" },
        };
        public static Item Key_of_Heretic => new ItemChangerCollectableItem//refers to Slab Key; PlayerData bool HasSlabKeyB
        {
            Name = ItemNames.Key_of_Heretic,
            CollectableName = "Slab Key",
            UIDef = new CollectableUIDef { CollectableName = "Slab Key" },
        };
        public static Item Key_of_Indolent => new ItemChangerCollectableItem//refers to Slab Key; PlayerData bool HasSlabKeyA
        {
            Name = ItemNames.Key_of_Indolent,
            CollectableName = "Slab Key",
            UIDef = new CollectableUIDef { CollectableName = "Slab Key" },
        };
        public static Item Simple_Key => new ItemChangerCollectableItem
        {
            Name = ItemNames.Simple_Key,
            CollectableName = "Simple Key",
            UIDef = new CollectableUIDef { CollectableName = "Simple Key" },
        };
        public static Item Surgeon_s_Key => new ItemChangerCollectableItem
        {
            Name = ItemNames.Surgeon_s_Key,
            CollectableName = "Ward Boss Key",
            UIDef = new CollectableUIDef { CollectableName = "Ward Boss Key" },
        };
        public static Item White_Key => new ItemChangerCollectableItem
        {
            Name = ItemNames.White_Key,
            CollectableName = "Ward Key",
            UIDef = new CollectableUIDef { CollectableName = "Ward Key" },
        };


        //relics
        public static Item Arcane_Egg => new ItemChangerCollectableRelic
        {
            Name = ItemNames.Arcane_Egg,
            CollectableName = "Ancient Egg Abyss Middle",
            UIDef = new CollectableUIDef { CollectableName = "Ancient Egg Abyss Middle" },
        };
        public static Item Bone_Scroll__Burning_Bug => new ItemChangerCollectableRelic
        {
            Name = ItemNames.Bone_Scroll__Burning_Bug,
            CollectableName = "Bone Record Wisp Top",
            UIDef = new CollectableUIDef { CollectableName = "Bone Record Wisp Top" },
        };
        public static Item Bone_Scroll__Lost_Pilgrim => new ItemChangerCollectableRelic
        {
            Name = ItemNames.Bone_Scroll__Lost_Pilgrim,
            CollectableName = "Bone Record Greymoor_flooded_corridor",
            UIDef = new CollectableUIDef { CollectableName = "Bone Record Greymoor_flooded_corridor" },
        };
        public static Item Bone_Scroll__Singed_Pilgrim => new ItemChangerCollectableRelic
        {
            Name = ItemNames.Bone_Scroll__Singed_Pilgrim,
            CollectableName = "Bone Record Bone_East_14",
            UIDef = new CollectableUIDef { CollectableName = "Bone Record Bone_East_14" },
        };
        public static Item Bone_Scroll__Underworker => new ItemChangerCollectableRelic
        {
            Name = ItemNames.Bone_Scroll__Underworker,
            CollectableName = "Bone Record Understore_Map_Room",
            UIDef = new CollectableUIDef { CollectableName = "Bone Record Understore_Map_Room" },
        };
        public static Item Choral_Commandment__Eternity => new ItemChangerCollectableRelic
        {
            Name = ItemNames.Choral_Commandment__Eternity,
            CollectableName = "Seal Chit City Merchant",
            UIDef = new CollectableUIDef { CollectableName = "Seal Chit City Merchant" },
        };
        public static Item Choral_Commandment__Light => new ItemChangerCollectableRelic
        {
            Name = ItemNames.Choral_Commandment__Light,
            CollectableName = "Seal Chit Silk Siphon",
            UIDef = new CollectableUIDef { CollectableName = "Seal Chit Silk Siphon" },
        };
        public static Item Choral_Commandment__Surgeon => new ItemChangerCollectableRelic
        {
            Name = ItemNames.Choral_Commandment__Surgeon,
            CollectableName = "Seal Chit Ward Corpse",
            UIDef = new CollectableUIDef { CollectableName = "Seal Chit Ward Corpse" },
        };
        public static Item Choral_Commandment__White_Wyrm => new ItemChangerCollectableRelic
        {
            Name = ItemNames.Choral_Commandment__White_Wyrm,
            CollectableName = "Seal Chit Aspid_01",
            UIDef = new CollectableUIDef { CollectableName = "Seal Chit Aspid_01" },
        };
        public static Item Psalm_Cylinder__Ascendence_Theme => new ItemChangerCollectableRelic
        {
            Name = ItemNames.Psalm_Cylinder__Ascendence_Theme,
            CollectableName = "Psalm Cylinder Hang",
            UIDef = new CollectableUIDef { CollectableName = "Psalm Cylinder Hang" },
        };
        public static Item Psalm_Cylinder__Choir_Voices => new ItemChangerCollectableRelic
        {
            Name = ItemNames.Psalm_Cylinder__Choir_Voices,
            CollectableName = "Psalm Cylinder Librarian",
            UIDef = new CollectableUIDef { CollectableName = "Psalm Cylinder Librarian" },
        };
        public static Item Psalm_Cylinder__Salvation_Theme => new ItemChangerCollectableRelic
        {
            Name = ItemNames.Psalm_Cylinder__Salvation_Theme,
            CollectableName = "Psalm Cylinder Grindle",
            UIDef = new CollectableUIDef { CollectableName = "Psalm Cylinder Grindle" },
        };
        public static Item Psalm_Cylinder__Sermon => new ItemChangerCollectableRelic
        {
            Name = ItemNames.Psalm_Cylinder__Sermon,
            CollectableName = "Psalm Cylinder Library Roof",
            UIDef = new CollectableUIDef { CollectableName = "Psalm Cylinder Library Roof" },
        };
        public static Item Psalm_Cylinder__Surgery => new ItemChangerCollectableRelic
        {
            Name = ItemNames.Psalm_Cylinder__Surgery,
            CollectableName = "Psalm Cylinder Ward",
            UIDef = new CollectableUIDef { CollectableName = "Psalm Cylinder Ward" },
        };
        public static Item Rune_Harp__Burden => new ItemChangerCollectableRelic
        {
            Name = ItemNames.Rune_Harp__Burden,
            CollectableName = "Weaver Record Conductor",
            UIDef = new CollectableUIDef { CollectableName = "Weaver Record Conductor" },
        };
        public static Item Rune_Harp__Escape => new ItemChangerCollectableRelic
        {
            Name = ItemNames.Rune_Harp__Escape,
            CollectableName = "Weaver Record Sprint_Challenge",
            UIDef = new CollectableUIDef { CollectableName = "Weaver Record Sprint_Challenge" },
        };
        public static Item Rune_Harp__Eva => new ItemChangerCollectableRelic
        {
            Name = ItemNames.Rune_Harp__Eva,
            CollectableName = "Weaver Record Weave_08",
            UIDef = new CollectableUIDef { CollectableName = "Weaver Record Weave_08" },
        };
        public static Item Vaultkeeper_s_Melody => new ItemChangerCollectableRelic
        {
            Name = ItemNames.Vaultkeeper_s_Melody,
            CollectableName = "Librarian Melody Cylinder",
            UIDef = new CollectableUIDef { CollectableName = "Librarian Melody Cylinder" },
        };
        public static Item Weaver_Effigy__Atla => new ItemChangerCollectableRelic
        {
            Name = ItemNames.Weaver_Effigy__Atla,
            CollectableName = "Weaver Totem Slab_Bottom",
            UIDef = new CollectableUIDef { CollectableName = "Weaver Totem Slab_Bottom" },
        };
        public static Item Weaver_Effigy__Camora => new ItemChangerCollectableRelic
        {
            Name = ItemNames.Weaver_Effigy__Camora,
            CollectableName = "Weaver Totem Bonetown_upper_room",
            UIDef = new CollectableUIDef { CollectableName = "Weaver Totem Bonetown_upper_room" },
        };
        public static Item Weaver_Effigy__Keelal => new ItemChangerCollectableRelic
        {
            Name = ItemNames.Weaver_Effigy__Keelal,
            CollectableName = "Weaver Totem Witch",
            UIDef = new CollectableUIDef { CollectableName = "Weaver Totem Witch" },
        };


        //maps
        //TODO: implement ItemChanger item class that changes map-related bools in PlayerData (example: HasSlabMap gives map of slab)


        //quills
        /* note: the quill has several states based on the QuillState int value in PlayerData (0 = normal?, 1 = normal, 2 = red, 3 = purple)
         * the quill exists as a CollectableItem but may need an extension of the ItemChangerCollectableItem class to support the QuillState int value
         * also may need to support changing the hasQuill bool in PlayerData
         * when testing the quills appear with the name !!/!! and do not appear in the inventory; most likely requires changing the hasQuill bool in PlayerData to support
         */
        //TODO: implement ItemChanger item class that can change QuillState int value in PlayerData
        public static Item Quill__White => new ItemChangerCollectableItem//extend class to support QuillState int value (QuillState = 1)
        {
            Name = ItemNames.Quill__White,
            CollectableName = "Quill",
            UIDef = new CollectableUIDef { CollectableName = "Quill" },
        };
        public static Item Quill__Red => new ItemChangerCollectableItem//extend class to support QuillState int value (QuillState = 2)
        {
            Name = ItemNames.Quill__Red,
            CollectableName = "Quill",
            UIDef = new CollectableUIDef { CollectableName = "Quill" },
        };
        public static Item Quill__Purple => new ItemChangerCollectableItem//extend class to support QuillState int value (QuillState = 3)
        {
            Name = ItemNames.Quill__Purple,
            CollectableName = "Quill",
            UIDef = new CollectableUIDef { CollectableName = "Quill" },
        };


        //TODO: implement ItemChanger class that supports map markers


        //TODO: implement ItemChanger class that supports map pins


        //TODO: implement ItemChanger class that supports flea findings


        //consumables
        public static Item Beast_Shard => new ItemChangerCollectableItem
        {
            Name = ItemNames.Beast_Shard,
            CollectableName = "Great Shard",
            UIDef = new CollectableUIDef { CollectableName = "Great Shard" },
        };
        public static Item Frayed_Rosary_String => new ItemChangerCollectableItem
        {
            Name = ItemNames.Frayed_Rosary_String,
            CollectableName = "Rosary_Set_Frayed",
            UIDef = new CollectableUIDef { CollectableName = "Rosary_Set_Frayed" },
        };
        public static Item Growstone => new ItemChangerCollectableItem//steel soul exclusive, not sure if we should keep
        {
            Name = ItemNames.Growstone,
            CollectableName = "Growstone",
            UIDef = new CollectableUIDef { CollectableName = "Growstone" },
        };
        public static Item Heavy_Rosary_Necklace => new ItemChangerCollectableItem
        {
            Name = ItemNames.Heavy_Rosary_Necklace,
            CollectableName = "Rosary_Set_Large",
            UIDef = new CollectableUIDef { CollectableName = "Rosary_Set_Large" },
        };
        public static Item Hornet_Statuette => new ItemChangerCollectableItem
        {
            Name = ItemNames.Hornet_Statuette,
            CollectableName = "Fixer Idol",
            UIDef = new CollectableUIDef { CollectableName = "Fixer Idol" },
        };
        public static Item Pale_Rosary_Necklace => new ItemChangerCollectableItem
        {
            Name = ItemNames.Pale_Rosary_Necklace,
            CollectableName = "Rosary_Set_Huge_White",
            UIDef = new CollectableUIDef { CollectableName = "Rosary_Set_Huge_White" },
        };
        public static Item Pristine_Core => new ItemChangerCollectableItem
        {
            Name = ItemNames.Pristine_Core,
            CollectableName = "Pristine Core",
            UIDef = new CollectableUIDef { CollectableName = "Pristine Core" },
        };
        public static Item Rosary_Necklace => new ItemChangerCollectableItem
        {
            Name = ItemNames.Rosary_Necklace,
            CollectableName = "Rosary_Set_Medium",
            UIDef = new CollectableUIDef { CollectableName = "Rosary_Set_Medium" },
        };
        public static Item Rosary_String => new ItemChangerCollectableItem
        {
            Name = ItemNames.Rosary_String,
            CollectableName = "Rosary_Set_Small",
            UIDef = new CollectableUIDef { CollectableName = "Rosary_Set_Small" },
        };
        public static Item Shard_Bundle => new ItemChangerCollectableItem
        {
            Name = ItemNames.Shard_Bundle,
            CollectableName = "Shard Pouch",
            UIDef = new CollectableUIDef { CollectableName = "Shard Pouch" },
        };
        public static Item Silkeater => new ItemChangerCollectableItem
        {
            Name = ItemNames.Silkeater,
            CollectableName = "Silk Grub",
            UIDef = new CollectableUIDef { CollectableName = "Silk Grub" },
        };


        //mementos
        public static Item Craw_Memento => new ItemChangerCollectableItem
        {
            Name = ItemNames.Craw_Memento,
            CollectableName = "Crowman Memento",
            UIDef = new CollectableUIDef { CollectableName = "Crowman Memento" },
        };
        public static Item Grey_Memento => new ItemChangerCollectableItem
        {
            Name = ItemNames.Grey_Memento,
            CollectableName = "Grey Memento",
            UIDef = new CollectableUIDef { CollectableName = "Grey Memento" },
        };
        public static Item Guardian_s_Memento => new ItemChangerCollectableItem
        {
            Name = ItemNames.Guardian_s_Memento,
            CollectableName = "Memento Seth",
            UIDef = new CollectableUIDef { CollectableName = "Memento Seth" },
        };
        public static Item Hero_s_Memento => new ItemChangerCollectableItem
        {
            Name = ItemNames.Hero_s_Memento,
            CollectableName = "Memento Garmond",
            UIDef = new CollectableUIDef { CollectableName = "Memento Garmond" },
        };
        public static Item Hunter_s_Memento => new ItemChangerCollectableItem
        {
            Name = ItemNames.Hunter_s_Memento,
            CollectableName = "Hunter Memento",
            UIDef = new CollectableUIDef { CollectableName = "Hunter Memento" },
        };
        public static Item Sprintmaster_Memento => new ItemChangerCollectableItem
        {
            Name = ItemNames.Sprintmaster_Memento,
            CollectableName = "Sprintmaster Memento",
            UIDef = new CollectableUIDef { CollectableName = "Sprintmaster Memento" },
        };
        public static Item Surface_Memento => new ItemChangerCollectableItem
        {
            Name = ItemNames.Surface_Memento,
            CollectableName = "Memento Surface",
            UIDef = new CollectableUIDef { CollectableName = "Memento Surface" },
        };


        //bellhome upgrades
        //TODO: implement ItemChanger support for non-CollectableItem bellhome upgrades
        //note: i think the bellhome lacquers are one time use and can not overlap
        //also the bought bellhome upgrades seem to be based on PlayerData bool values
        /* unimplemented items
            Bell_Lacquer__Black -> BelltownHouseColour
            Bell_Lacquer__Blue -> BelltownHouseColour
            Bell_Lacquer__Bronze -> BelltownHouseColour
            Bell_Lacquer__Chrome -> BelltownHouseColour [steel soul exclusive]
            Bell_Lacquer__Red -> BelltownHouseColour
            Bell_Lacquer__White -> BelltownHouseColour [replaced by Bell_Lacquer__Chrome in steel soul]
            Desk -> BelltownFurnishingDesk
            Gleamlights -> BelltownFurnishingFairyLights
            Gramophone -> BelltownFurnishingGramaphone
            Personal_Spa -> BelltownFurnishingSpa
        */
        public static Item Crawbell => new ItemChangerCollectableItem
        {
            Name = ItemNames.Crawbell,
            CollectableName = "Crawbell",
            UIDef = new CollectableUIDef { CollectableName = "Crawbell" },
        };
        public static Item Farsight => new ItemChangerCollectableItem
        {
            Name = ItemNames.Farsight,
            CollectableName = "Farsight",
            UIDef = new CollectableUIDef { CollectableName = "Farsight" },
        };
        public static Item Materium => new ItemChangerCollectableItem
        {
            Name = ItemNames.Materium,
            CollectableName = "Materium",
            UIDef = new CollectableUIDef { CollectableName = "Materium" },
        };


        //finite quest items
        public static Item Broodmother_s_Eye => new ItemChangerCollectableItem
        {
            Name = ItemNames.Broodmother_s_Eye,
            CollectableName = "Broodmother Remains",
            UIDef = new CollectableUIDef { CollectableName = "Broodmother Remains" },
        };
        public static Item Cogheart_Piece => new ItemChangerCollectableItem
        {
            Name = ItemNames.Cogheart_Piece,
            CollectableName = "Cog Heart Pieces",
            UIDef = new CollectableUIDef { CollectableName = "Cog Heart Pieces" },
        };
        public static Item Crown_Fragment => new ItemChangerCollectableItem
        {
            Name = ItemNames.Crown_Fragment,
            CollectableName = "Skull King Fragment",
            UIDef = new CollectableUIDef { CollectableName = "Skull King Fragment" },
        };
        public static Item Crustnut => new ItemChangerCollectableItem
        {
            Name = ItemNames.Crustnut,
            CollectableName = "Coral Ingredient",
            UIDef = new CollectableUIDef { CollectableName = "Coral Ingredient" },
        };
        public static Item Flintgem => new ItemChangerCollectableItem
        {
            Name = ItemNames.Flintgem,
            CollectableName = "Rock Roller Item",
            UIDef = new CollectableUIDef { CollectableName = "Rock Roller Item" },
        };
        public static Item Grass_Doll => new ItemChangerCollectableItem
        {
            Name = ItemNames.Grass_Doll,
            CollectableName = "Ant Trapper Item",
            UIDef = new CollectableUIDef { CollectableName = "Ant Trapper Item" },
        };
        public static Item Horn_Fragment => new ItemChangerCollectableItem
        {
            Name = ItemNames.Horn_Fragment,
            CollectableName = "Beastfly Remains",
            UIDef = new CollectableUIDef { CollectableName = "Beastfly Remains" },
        };
        public static Item Mossberry => new ItemChangerCollectableItem
        {
            Name = ItemNames.Mossberry,
            CollectableName = "Mossberry",
            UIDef = new CollectableUIDef { CollectableName = "Mossberry" },
        };
        public static Item Mossberry_Stew => new ItemChangerCollectableItem
        {
            Name = ItemNames.Mossberry_Stew,
            CollectableName = "Mossberry Stew",
            UIDef = new CollectableUIDef { CollectableName = "Mossberry Stew" },
        };
        public static Item Pickled_Muckmaggot => new ItemChangerCollectableItem
        {
            Name = ItemNames.Pickled_Muckmaggot,
            CollectableName = "Pickled Roach Egg",
            UIDef = new CollectableUIDef { CollectableName = "Pickled Roach Egg" },
        };
        public static Item Pollip_Heart => new ItemChangerCollectableItem
        {
            Name = ItemNames.Pollip_Heart,
            CollectableName = "Shell Flower",
            UIDef = new CollectableUIDef { CollectableName = "Shell Flower" },
        };
        public static Item Ruined_Tool => new ItemChangerCollectableItem
        {
            Name = ItemNames.Ruined_Tool,
            CollectableName = "Broken SilkShot",
            UIDef = new CollectableUIDef { CollectableName = "Broken SilkShot" },
        };
        public static Item Steel_Spines => new ItemChangerCollectableItem
        {
            Name = ItemNames.Steel_Spines,
            CollectableName = "Extractor Machine Pins",
            UIDef = new CollectableUIDef { CollectableName = "Extractor Machine Pins" },
        };
        public static Item Twisted_Bud => new ItemChangerCollectableItem
        {
            Name = ItemNames.Twisted_Bud,
            CollectableName = "Wood Witch Item",
            UIDef = new CollectableUIDef { CollectableName = "Wood Witch Item" },
        };
        public static Item Vintage_Nectar => new ItemChangerCollectableItem
        {
            Name = ItemNames.Vintage_Nectar,
            CollectableName = "Vintage Nectar",
            UIDef = new CollectableUIDef { CollectableName = "Vintage Nectar" },
        };


        //delivery items
        /* note: these items work like normal items during testing and do not take damage despite being delivery items
         * the only item that does take damage is the courier's rasher but it only takes one hit unlike taking 8 hits normally
         * the courier's rasher also does not degrade overtime
         * not sure whether to keep the damage aspect or make all delivery items immune to damage
         */
        //TODO: extend ItemChangerCollectableItem class to support delivery items taking damage
        public static Item Courier_s_Rasher => new ItemChangerCollectableItem
        {
            Name = ItemNames.Courier_s_Rasher,
            CollectableName = "Courier Supplies Gourmand",
            UIDef = new CollectableUIDef { CollectableName = "Courier Supplies Gourmand" },
        };
        public static Item Courier_s_Swag => new ItemChangerCollectableItem
        {
            Name = ItemNames.Courier_s_Swag,
            CollectableName = "Courier Supplies",
            UIDef = new CollectableUIDef { CollectableName = "Courier Supplies" },
        };
        public static Item Liquid_Lacquer => new ItemChangerCollectableItem
        {
            Name = ItemNames.Liquid_Lacquer,
            CollectableName = "Courier Supplies Mask Maker",
            UIDef = new CollectableUIDef { CollectableName = "Courier Supplies Mask Maker" },
        };
        public static Item Queen_s_Egg => new ItemChangerCollectableItem
        {
            Name = ItemNames.Queen_s_Egg,
            CollectableName = "Courier Supplies Slave",
            UIDef = new CollectableUIDef { CollectableName = "Courier Supplies Slave" },
        };


        //respawning quest items
        public static Item Choir_Cloak => new ItemChangerCollectableItem
        {
            Name = ItemNames.Choir_Cloak,
            CollectableName = "Song Pilgrim Cloak",
            UIDef = new CollectableUIDef { CollectableName = "Song Pilgrim Cloak" },
        };
        public static Item Fine_Pin => new ItemChangerCollectableItem
        {
            Name = ItemNames.Fine_Pin,
            CollectableName = "Fine Pin",
            UIDef = new CollectableUIDef { CollectableName = "Fine Pin" },
        };
        public static Item Pilgrim_Shawl => new ItemChangerCollectableItem
        {
            Name = ItemNames.Pilgrim_Shawl,
            CollectableName = "Pilgrim Rag",
            UIDef = new CollectableUIDef { CollectableName = "Pilgrim Rag" },
        };
        public static Item Plasmified_Blood => new ItemChangerCollectableItem
        {
            Name = ItemNames.Plasmified_Blood,
            CollectableName = "Plasmium Blood",
            UIDef = new CollectableUIDef { CollectableName = "Plasmium Blood" },
        };
        public static Item Plasmium => new ItemChangerCollectableItem
        {
            Name = ItemNames.Plasmium,
            CollectableName = "Plasmium",
            UIDef = new CollectableUIDef { CollectableName = "Plasmium" },
        };
        public static Item Ragpelt => new ItemChangerCollectableItem
        {
            Name = ItemNames.Ragpelt,
            CollectableName = "Crow Feather",
            UIDef = new CollectableUIDef { CollectableName = "Crow Feather" },
        };
        public static Item Roach_Guts => new ItemChangerCollectableItem
        {
            Name = ItemNames.Roach_Guts,
            CollectableName = "Roach Corpse Item",
            UIDef = new CollectableUIDef { CollectableName = "Roach Corpse Item" },
        };
        public static Item Seared_Organ => new ItemChangerCollectableItem
        {
            Name = ItemNames.Seared_Organ,
            CollectableName = "Enemy Morsel Seared",
            UIDef = new CollectableUIDef { CollectableName = "Enemy Morsel Seared" },
        };
        public static Item Shredded_Organ => new ItemChangerCollectableItem
        {
            Name = ItemNames.Shredded_Organ,
            CollectableName = "Enemy Morsel Shredded",
            UIDef = new CollectableUIDef { CollectableName = "Enemy Morsel Shredded" },
        };
        public static Item Silver_Bell => new ItemChangerCollectableItem
        {
            Name = ItemNames.Silver_Bell,
            CollectableName = "Silver Bellclapper",
            UIDef = new CollectableUIDef { CollectableName = "Silver Bellclapper" },
        };
        public static Item Skewered_Organ => new ItemChangerCollectableItem
        {
            Name = ItemNames.Skewered_Organ,
            CollectableName = "Enemy Morsel Speared",
            UIDef = new CollectableUIDef { CollectableName = "Enemy Morsel Speared" },
        };
        public static Item Spine_Core => new ItemChangerCollectableItem
        {
            Name = ItemNames.Spine_Core,
            CollectableName = "Common Spine",
            UIDef = new CollectableUIDef { CollectableName = "Common Spine" },
        };


        //TODO: implement ItemChanger class that supports fast travel stations (bell beast + ventrica)


        //TODO: implement ItemChanger class that supports lore tablet locations


        //TODO: implement ItemChanger class that supports novelty items

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

        public static Dictionary<string, Item> GetBaseItems()
        {
            return typeof(BaseItemList).GetProperties().Select(p => (Item)p.GetValue(null)).ToDictionary(i => i.Name);
        }
    }
}
