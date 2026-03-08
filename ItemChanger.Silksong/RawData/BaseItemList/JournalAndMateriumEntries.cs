using ItemChanger.Items;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Items;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.UIDefs;
using UnityEngine;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseItemList
{
    private static Item CreateJournalEntryItem(string name, string id, LanguageString uiName, LanguageString uiDesc) => new ItemChangerSavedItem
    {
        Name = name,
        Item = new() { Id = id, Type = BaseGameSavedItem.ItemType.EnemyJournalRecord },
        UIDef = new MsgUIDef
        {
            Name = CompositeString.Create(new LanguageString("Mods.io.github.silksong.itemchanger", "FMT_JOURNAL_ENTRY_NAME"), uiName),
            ShopDesc = uiDesc,
            Sprite = new FramedSprite()
            {
                BaseSprite = new SavedItemSprite()
                {
                    Item = new() { Id = id, Type = BaseGameSavedItem.ItemType.EnemyJournalRecord },
                },
                FrameStyle = FrameStyle.Completed,
                CacheKey = $"Journal/{id}/Completed"
            },
        },
    };

    private static Item CreateMateriumEntryItem(string name, string id, LanguageString uiName, LanguageString uiDesc) => new ItemChangerSavedItem
    {
        Name = name,
        Item = new() { Id = id, Type = BaseGameSavedItem.ItemType.MateriumItem },
        UIDef = new MsgUIDef
        {
            Name = CompositeString.Create(new LanguageString("Mods.io.github.silksong.itemchanger", "FMT_MATERIUM_ENTRY_NAME"), uiName),
            ShopDesc = uiDesc,
            Sprite = new FramedSprite
            {
                BaseSprite = new SavedItemSprite { Item = new() { Id = id, Type = BaseGameSavedItem.ItemType.MateriumItem } },
                CacheKey = $"Materium/{id}/0.65/Completed",
                // SpriteScale chosen to (roughly) take the height of a materium sprite to match the height of a journal sprite
                SpriteScale = 0.65f
            }
        },
    };

    // materium entries
    public static Item Materium_Entry__Silk => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Silk,
        id: "Silk",
        uiName: new LanguageString("UI", "MAT_NAME_SILK"),
        uiDesc: new LanguageString("UI", "MAT_DESC_SILK"));
    public static Item Materium_Entry__Shell_Shards => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Shell_Shards,
        id: "Shell Shard",
        uiName: new LanguageString("UI", "INV_NAME_SHARD"),
        uiDesc: new LanguageString("UI", "INV_DESC_SHARD"));
    public static Item Materium_Entry__Rosaries => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Rosaries,
        id: "Rosary Small",
        uiName: new LanguageString("UI", "INV_NAME_COIN"),
        uiDesc: new LanguageString("UI", "INV_DESC_COIN"));
    public static Item Materium_Entry__Shell_Rosaries => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Shell_Rosaries,
        id: "Rosary Mid",
        uiName: new LanguageString("UI", "INV_NAME_COIN_MID"),
        uiDesc: new LanguageString("UI", "INV_DESC_COIN_MID"));
    public static Item Materium_Entry__Pearl_Rosaries => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Pearl_Rosaries,
        id: "Rosary Big",
        uiName: new LanguageString("UI", "INV_NAME_COIN_BIG"),
        uiDesc: new LanguageString("UI", "INV_DESC_COIN_BIG"));
    public static Item Materium_Entry__Mossberry => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Mossberry,
        id: "Mossberry",
        uiName: new LanguageString("UI", "INV_NAME_MOSSBERRY"),
        uiDesc: new LanguageString("UI", "INV_DESC_MOSSBERRY"));
    public static Item Materium_Entry__Mossberry_Stew => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Mossberry_Stew,
        id: "Mossberry Stew",
        uiName: new LanguageString("UI", "INV_NAME_GOURMAND_MOSSBERRY_STEW"),
        uiDesc: new LanguageString("UI", "INV_DESC_GOURMAND_MOSSBERRY_STEW"));
    public static Item Materium_Entry__Pilgrim_Shawl => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Pilgrim_Shawl,
        id: "Pilgrim Rag",
        uiName: new LanguageString("UI", "INV_NAME_PILGRIM_RAGS_ITEM"),
        uiDesc: new LanguageString("UI", "INV_DESC_PILGRIM_RAGS_ITEM"));
    public static Item Materium_Entry__Flintstone => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Flintstone,
        id: "Smeltstone",
        uiName: new LanguageString("UI", "MAT_NAME_SMELTSTONE"),
        uiDesc: new LanguageString("UI", "MAT_DESC_SMELTSTONE"));
    public static Item Materium_Entry__Flintgem => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Flintgem,
        id: "Rock Roller Item",
        uiName: new LanguageString("UI", "INV_NAME_ROCK_ROLLER_ITEM"),
        uiDesc: new LanguageString("UI", "INV_DESC_ROCK_ROLLER_ITEM"));
    public static Item Materium_Entry__Craftmetal => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Craftmetal,
        id: "Tool Metal",
        uiName: new LanguageString("UI", "INV_NAME_TOOL_METAL"),
        uiDesc: new LanguageString("UI", "INV_DESC_TOOL_METAL"));
    public static Item Materium_Entry__Spine_Core => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Spine_Core,
        id: "Common Spine",
        uiName: new LanguageString("UI", "INV_NAME_SPINE"),
        uiDesc: new LanguageString("UI", "INV_DESC_SPINE"));
    public static Item Materium_Entry__Crown_Fragment => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Crown_Fragment,
        id: "Skull King Fragment",
        uiName: new LanguageString("UI", "INV_NAME_SK_FRAGMENT"),
        uiDesc: new LanguageString("UI", "INV_DESC_SK_FRAGMENT"));
    public static Item Materium_Entry__Horn_Fragment => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Horn_Fragment,
        id: "Beastfly Remains",
        uiName: new LanguageString("UI", "INV_NAME_BEASTFLY_REMAINS"),
        uiDesc: new LanguageString("UI", "INV_DESC_BEASTFLY_REMAINS"));
    public static Item Materium_Entry__Silver_Bell => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Silver_Bell,
        id: "Silver Bellclapper",
        uiName: new LanguageString("UI", "INV_NAME_BELLCLAPPER_ITEM"),
        uiDesc: new LanguageString("UI", "INV_DESC_BELLCLAPPER_ITEM"));
    public static Item Materium_Entry__Pale_Oil => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Pale_Oil,
        id: "Pale Oil",
        uiName: new LanguageString("UI", "INV_NAME_PLINNEY_TOOLS"),
        uiDesc: new LanguageString("UI", "INV_DESC_PLINNEY_TOOLS"));
    public static Item Materium_Entry__Plasmium => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Plasmium,
        id: "Lifeblood",
        uiName: new LanguageString("UI", "MAT_NAME_LIFEBLOOD"),
        uiDesc: new LanguageString("UI", "MAT_DESC_LIFEBLOOD"));
    public static Item Materium_Entry__Plasmified_Blood => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Plasmified_Blood,
        id: "Plasmium Blood",
        uiName: new LanguageString("UI", "INV_NAME_PLASMIUM_BLOOD"),
        uiDesc: new LanguageString("UI", "INV_DESC_PLASMIUM_BLOOD"));
    public static Item Materium_Entry__Ragpelt => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Ragpelt,
        id: "Crow Feathers",
        uiName: new LanguageString("UI", "INV_NAME_CROW_FEATHER"),
        uiDesc: new LanguageString("UI", "INV_DESC_CROW_FEATHER"));
    public static Item Materium_Entry__Pollip_Heart => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Pollip_Heart,
        id: "Shell Flower",
        uiName: new LanguageString("UI", "INV_NAME_SHELL_FLOWER"),
        uiDesc: new LanguageString("UI", "INV_DESC_SHELL_FLOWER"));
    public static Item Materium_Entry__Roach_Guts => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Roach_Guts,
        id: "Roach Corpse Item",
        uiName: new LanguageString("UI", "INV_NAME_ROACH_CORPSE_ITEM"),
        uiDesc: new LanguageString("UI", "INV_DESC_ROACH_CORPSE_ITEM"));
    public static Item Materium_Entry__Courier_s_Swag => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Courier_s_Swag,
        id: "Courier Supplies Generic",
        uiName: new LanguageString("Quests", "COURIER_SUPPLIES_ITEM_NAME"),
        uiDesc: new LanguageString("Quests", "COURIER_SUPPLIES_ITEM_DESC"));
    public static Item Materium_Entry__Queen_s_Egg => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Queen_s_Egg,
        id: "Courier Supplies Slave",
        uiName: new LanguageString("Quests", "COURIER_SUPPLIES_SLAVE_NAME"),
        uiDesc: new LanguageString("Quests", "COURIER_SUPPLIES_SLAVE_DESC"));
    public static Item Materium_Entry__Liquid_Lacquer => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Liquid_Lacquer,
        id: "Courier Supplies Mask Maker",
        uiName: new LanguageString("Quests", "COURIER_SUPPLIES_MASK_NAME"),
        uiDesc: new LanguageString("Quests", "COURIER_SUPPLIES_MASK_DESC"));
    public static Item Materium_Entry__Courier_s_Rasher => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Courier_s_Rasher,
        id: "Courier Supplies Gourmand",
        uiName: new LanguageString("Quests", "COURIER_SUPPLIES_MEAT_NAME"),
        uiDesc: new LanguageString("Quests", "COURIER_SUPPLIES_MEAT_DESC"));
    public static Item Materium_Entry__Vintage_Nectar => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Vintage_Nectar,
        id: "Vintage Nectar",
        uiName: new LanguageString("UI", "INV_NAME_GOURMAND_VINTAGE_NECTAR"),
        uiDesc: new LanguageString("UI", "INV_DESC_GOURMAND_VINTAGE_NECTAR"));
    public static Item Materium_Entry__Pickled_Muckmaggot => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Pickled_Muckmaggot,
        id: "Pickled Roach Egg",
        uiName: new LanguageString("UI", "INV_NAME_PICKLEDEGG"),
        uiDesc: new LanguageString("UI", "INV_DESC_PICKLEDEGG"));
    public static Item Materium_Entry__Crustnut => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Crustnut,
        id: "Crustnut",
        uiName: new LanguageString("UI", "INV_NAME_GOURMAND_CORAL_INGREDIENT"),
        uiDesc: new LanguageString("UI", "INV_DESC_GOURMAND_CORAL_INGREDIENT"));
    public static Item Materium_Entry__Steel_Spines => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Steel_Spines,
        id: "Extractor Machine Pins",
        uiName: new LanguageString("Quests", "ITEM_EXTRACTOR_PINS_NAME"),
        uiDesc: new LanguageString("Quests", "ITEM_EXTRACTOR_PINS_DESC"));
    public static Item Materium_Entry__Choir_Cloak => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Choir_Cloak,
        id: "Song Pilgrim Cloak",
        uiName: new LanguageString("UI", "INV_NAME_SONGPILGRIM_CLOAK_ITEM"),
        uiDesc: new LanguageString("UI", "INV_DESC_SONGPILGRIM_CLOAK_ITEM"));
    public static Item Materium_Entry__Magnetite => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Magnetite,
        id: "Magnetite",
        uiName: new LanguageString("UI", "MAT_NAME_MAGNETITE"),
        uiDesc: new LanguageString("UI", "MAT_DESC_MAGNETITE"));
    public static Item Materium_Entry__Broodmother_s_Eye => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Broodmother_s_Eye,
        id: "Broodmother Remains",
        uiName: new LanguageString("UI", "INV_NAME_BROODMOTHER_REMAINS"),
        uiDesc: new LanguageString("UI", "INV_DESC_BROODMOTHER_REMAINS"));
    public static Item Materium_Entry__Seared_Organ => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Seared_Organ,
        id: "Enemy Morsel Seared",
        uiName: new LanguageString("UI", "INV_NAME_MORSEL_SEARED"),
        uiDesc: new LanguageString("UI", "INV_DESC_MORSEL_SEARED"));
    public static Item Materium_Entry__Shredded_Organ => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Shredded_Organ,
        id: "Enemy Morsel Shredded",
        uiName: new LanguageString("UI", "INV_NAME_MORSEL_SHREDDED"),
        uiDesc: new LanguageString("UI", "INV_DESC_MORSEL_SHREDDED"));
    public static Item Materium_Entry__Skewered_Organ => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Skewered_Organ,
        id: "Enemy Morsel Speared",
        uiName: new LanguageString("UI", "INV_NAME_MORSEL_SPEARED"),
        uiDesc: new LanguageString("UI", "INV_DESC_MORSEL_SPEARED"));
    public static Item Materium_Entry__Voltridian => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Voltridian,
        id: "Voltstone",
        uiName: new LanguageString("UI", "MAT_NAME_VOLTSTONE"),
        uiDesc: new LanguageString("UI", "MAT_DESC_VOLTSTONE"));
    public static Item Materium_Entry__Hermit_s_Soul => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Hermit_s_Soul,
        id: "Snare Soul Bell Hermit",
        uiName: new LanguageString("UI", "INV_NAME_SNARE_SOUL_BELLHERMIT"),
        uiDesc: new LanguageString("UI", "INV_DESC_SNARE_SOUL_BELLHERMIT"));
    public static Item Materium_Entry__Maiden_s_Soul => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Maiden_s_Soul,
        id: "Snare Soul Churchkeeper",
        uiName: new LanguageString("UI", "INV_NAME_SNARE_SOUL_CHURCHKEEPER"),
        uiDesc: new LanguageString("UI", "INV_DESC_SNARE_SOUL_CHURCHKEEPER"));
    public static Item Materium_Entry__Seeker_s_Soul => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Seeker_s_Soul,
        id: "Snare Soul Swamp Bug",
        uiName: new LanguageString("UI", "INV_NAME_SNARE_SOUL_SWAMP"),
        uiDesc: new LanguageString("UI", "INV_DESC_SNARE_SOUL_SWAMP"));
    public static Item Materium_Entry__Pollen_Heart => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Pollen_Heart,
        id: "Flower Heart",
        uiName: new LanguageString("UI", "INV_NAME_HEART_BLOOM"),
        uiDesc: new LanguageString("UI", "INV_DESC_HEART_BLOOM"));
    public static Item Materium_Entry__Hunter_s_Heart => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Hunter_s_Heart,
        id: "Hunter Heart",
        uiName: new LanguageString("UI", "INV_NAME_HEART_HUNTER"),
        uiDesc: new LanguageString("UI", "INV_DESC_HEART_HUNTER"));
    public static Item Materium_Entry__Encrusted_Heart => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Encrusted_Heart,
        id: "Coral Heart",
        uiName: new LanguageString("UI", "INV_NAME_HEART_CORAL"),
        uiDesc: new LanguageString("UI", "INV_DESC_HEART_CORAL"));
    public static Item Materium_Entry__Conjoined_Heart => CreateMateriumEntryItem(
        name: ItemNames.Materium_Entry__Conjoined_Heart,
        id: "Clover Heart",
        uiName: new LanguageString("UI", "INV_NAME_HEART_CLOVER"),
        uiDesc: new LanguageString("UI", "INV_DESC_HEART_CLOVER"));


    // journal entries
    public static Item Journal_Entry__Mossgrub => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Mossgrub,
        id: "MossBone Crawler",
        uiName: new LanguageString("Journal", "NAME_MOSSBONE_CRAWLER"),
        uiDesc: new LanguageString("Journal", "DESC_MOSSBONE_CRAWLER"));
    public static Item Journal_Entry__Massive_Mossgrub => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Massive_Mossgrub,
        id: "MossBone Crawler Fat",
        uiName: new LanguageString("Journal", "NAME_MOSSBONE_CRAWLER_FAT"),
        uiDesc: new LanguageString("Journal", "DESC_MOSSBONE_CRAWLER_FAT"));
    public static Item Journal_Entry__Mossmir => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Mossmir,
        id: "MossBone Fly",
        uiName: new LanguageString("Journal", "NAME_MOSSBONE_FLY"),
        uiDesc: new LanguageString("Journal", "DESC_MOSSBONE_FLY"));
    public static Item Journal_Entry__Moss_Mother => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Moss_Mother,
        id: "Mossbone Mother",
        uiName: new LanguageString("Journal", "NAME_MOSSBONE_MOTHER"),
        uiDesc: new LanguageString("Journal", "DESC_MOSSBONE_MOTHER"));
    public static Item Journal_Entry__Aknid => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Aknid,
        id: "Aspid Collector",
        uiName: new LanguageString("Journal", "NAME_ASPID_COLLECTOR"),
        uiDesc: new LanguageString("Journal", "DESC_ASPID_COLLECTOR"));
    public static Item Journal_Entry__Skull_Scuttler => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Skull_Scuttler,
        id: "Bone Goomba",
        uiName: new LanguageString("Journal", "NAME_BONE_GOOMBA"),
        uiDesc: new LanguageString("Journal", "DESC_BONE_GOOMBA"));
    public static Item Journal_Entry__Skullwing => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Skullwing,
        id: "Bone Goomba Bounce Fly",
        uiName: new LanguageString("Journal", "NAME_BONE_GOOMBA_BOUNCE_FLY"),
        uiDesc: new LanguageString("Journal", "DESC_BONE_GOOMBA_BOUNCE_FLY"));
    public static Item Journal_Entry__Skull_Brute => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Skull_Brute,
        id: "Bone Goomba Large",
        uiName: new LanguageString("Journal", "NAME_BONE_GOOMBA_LARGE"),
        uiDesc: new LanguageString("Journal", "DESC_BONE_GOOMBA_LARGE"));
    public static Item Journal_Entry__Skull_Tyrant => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Skull_Tyrant,
        id: "Skull King",
        uiName: new LanguageString("Journal", "NAME_SKULL_KING"),
        uiDesc: new LanguageString("Journal", "DESC_SKULL_KING"));
    public static Item Journal_Entry__Kilik => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Kilik,
        id: "Bone Crawler",
        uiName: new LanguageString("Journal", "NAME_BONE_CRAWLER"),
        uiDesc: new LanguageString("Journal", "DESC_BONE_CRAWLER"));
    public static Item Journal_Entry__Beastfly => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Beastfly,
        id: "Bone Flyer",
        uiName: new LanguageString("Journal", "NAME_BONE_FLYER"),
        uiDesc: new LanguageString("Journal", "DESC_BONE_FLYER"));
    public static Item Journal_Entry__Savage_Beastfly => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Savage_Beastfly,
        id: "Bone Flyer Giant",
        uiName: new LanguageString("Journal", "NAME_BONE_FLYER_GIANT"),
        uiDesc: new LanguageString("Journal", "DESC_BONE_FLYER_GIANT"));
    public static Item Journal_Entry__Caranid => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Caranid,
        id: "Bone Circler",
        uiName: new LanguageString("Journal", "NAME_BONE_CIRCLER"),
        uiDesc: new LanguageString("Journal", "DESC_BONE_CIRCLER"));
    public static Item Journal_Entry__Vicious_Caranid => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Vicious_Caranid,
        id: "Bone Circler Vicious",
        uiName: new LanguageString("Journal", "NAME_BONE_CIRCLER_VICIOUS"),
        uiDesc: new LanguageString("Journal", "DESC_BONE_CIRCLER_VICIOUS"));
    public static Item Journal_Entry__Hardbone_Hopper => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Hardbone_Hopper,
        id: "Bone Hopper",
        uiName: new LanguageString("Journal", "NAME_BONE_HOPPER"),
        uiDesc: new LanguageString("Journal", "DESC_BONE_HOPPER"));
    public static Item Journal_Entry__Hardbone_Elder => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Hardbone_Elder,
        id: "Bone Hopper Giant",
        uiName: new LanguageString("Journal", "NAME_BONE_HOPPER_GIANT"),
        uiDesc: new LanguageString("Journal", "DESC_BONE_HOPPER_GIANT"));
    public static Item Journal_Entry__Tarmite => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Tarmite,
        id: "Bone Spitter",
        uiName: new LanguageString("Journal", "NAME_BONE_SPITTER"),
        uiDesc: new LanguageString("Journal", "DESC_BONE_SPITTER"));
    public static Item Journal_Entry__Mawling => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Mawling,
        id: "Bone Roller",
        uiName: new LanguageString("Journal", "NAME_BONE_ROLLER"),
        uiDesc: new LanguageString("Journal", "DESC_BONE_ROLLER"));
    public static Item Journal_Entry__Marrowmaw => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Marrowmaw,
        id: "Bone Thumper",
        uiName: new LanguageString("Journal", "NAME_BONE_THUMPER"),
        uiDesc: new LanguageString("Journal", "DESC_BONE_THUMPER"));
    public static Item Journal_Entry__Hoker => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Hoker,
        id: "Spine Floater",
        uiName: new LanguageString("Journal", "NAME_SPINE_FLOATER"),
        uiDesc: new LanguageString("Journal", "DESC_SPINE_FLOATER"));
    public static Item Journal_Entry__Flintbeetle => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Flintbeetle,
        id: "Rock Roller",
        uiName: new LanguageString("Journal", "NAME_ROCK_ROLLER"),
        uiDesc: new LanguageString("Journal", "DESC_ROCK_ROLLER"));
    public static Item Journal_Entry__Rhinogrund => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Rhinogrund,
        id: "Rhino",
        uiName: new LanguageString("Journal", "NAME_RHINO"),
        uiDesc: new LanguageString("Journal", "DESC_RHINO"));
    public static Item Journal_Entry__Gromling => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Gromling,
        id: "Crypt Worm",
        uiName: new LanguageString("Journal", "NAME_CRYPT_WORM"),
        uiDesc: new LanguageString("Journal", "DESC_CRYPT_WORM"));
    public static Item Journal_Entry__Grom => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Grom,
        id: "Bone Worm",
        uiName: new LanguageString("Journal", "NAME_BONE_WORM"),
        uiDesc: new LanguageString("Journal", "DESC_BONE_WORM"));
    public static Item Journal_Entry__Bell_Beast => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Bell_Beast,
        id: "Bone Beast",
        uiName: new LanguageString("Journal", "NAME_BONE_BEAST"),
        uiDesc: new LanguageString("Journal", "DESC_BONE_BEAST"));
    public static Item Journal_Entry__Pilgrim_Groveller => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Pilgrim_Groveller,
        id: "Pilgrim 03",
        uiName: new LanguageString("Journal", "NAME_PILGRIM_03"),
        uiDesc: new LanguageString("Journal", "DESC_PILGRIM_03"));
    public static Item Journal_Entry__Pilgrim_Pouncer => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Pilgrim_Pouncer,
        id: "Pilgrim 01",
        uiName: new LanguageString("Journal", "NAME_PILGRIM_01"),
        uiDesc: new LanguageString("Journal", "DESC_PILGRIM_01"));
    public static Item Journal_Entry__Pilgrim_Hornfly => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Pilgrim_Hornfly,
        id: "Pilgrim 04",
        uiName: new LanguageString("Journal", "NAME_PILGRIM_04"),
        uiDesc: new LanguageString("Journal", "DESC_PILGRIM_04"));
    public static Item Journal_Entry__Pilgrim_Hulk => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Pilgrim_Hulk,
        id: "Pilgrim 02",
        uiName: new LanguageString("Journal", "NAME_PILGRIM_02"),
        uiDesc: new LanguageString("Journal", "DESC_PILGRIM_02"));
    public static Item Journal_Entry__Pilgrim_Bellbearer => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Pilgrim_Bellbearer,
        id: "Pilgrim Bell Thrower",
        uiName: new LanguageString("Journal", "NAME_PILGRIM_BELL_THROWER"),
        uiDesc: new LanguageString("Journal", "DESC_PILGRIM_BELL_THROWER"));
    public static Item Journal_Entry__Winged_Pilgrim => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Winged_Pilgrim,
        id: "Pilgrim Fly",
        uiName: new LanguageString("Journal", "NAME_PILGRIM_FLY"),
        uiDesc: new LanguageString("Journal", "DESC_PILGRIM_FLY"));
    public static Item Journal_Entry__Elder_Pilgrim => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Elder_Pilgrim,
        id: "Pilgrim 05",
        uiName: new LanguageString("Journal", "NAME_PILGRIM_05"),
        uiDesc: new LanguageString("Journal", "DESC_PILGRIM_05"));
    public static Item Journal_Entry__Winged_Pilgrim_Bellbearer => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Winged_Pilgrim_Bellbearer,
        id: "Pilgrim Bellthrower Fly",
        uiName: new LanguageString("Journal", "NAME_PILGRIM_BELLTHROWER_FLY"),
        uiDesc: new LanguageString("Journal", "DESC_PILGRIM_BELLTHROWER_FLY"));
    public static Item Journal_Entry__Pilgrim_Hiker => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Pilgrim_Hiker,
        id: "Pilgrim Hiker",
        uiName: new LanguageString("Journal", "NAME_PILGRIM_HIKER"),
        uiDesc: new LanguageString("Journal", "DESC_PILGRIM_HIKER"));
    public static Item Journal_Entry__Pilgrim_Guide => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Pilgrim_Guide,
        id: "Pilgrim StaffWielder",
        uiName: new LanguageString("Journal", "NAME_PILGRIM_STAFFWIELDER"),
        uiDesc: new LanguageString("Journal", "DESC_PILGRIM_STAFFWIELDER"));
    public static Item Journal_Entry__Overgrown_Pilgrim => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Overgrown_Pilgrim,
        id: "Pilgrim Moss Spitter",
        uiName: new LanguageString("Journal", "NAME_PILGRIM_MOSS_SPITTER"),
        uiDesc: new LanguageString("Journal", "DESC_PILGRIM_MOSS_SPITTER"));
    public static Item Journal_Entry__Covetous_Pilgrim => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Covetous_Pilgrim,
        id: "Rosary Pilgrim",
        uiName: new LanguageString("Journal", "NAME_ROSARY_PILGRIM"),
        uiDesc: new LanguageString("Journal", "DESC_ROSARY_PILGRIM"));
    public static Item Journal_Entry__Snitchfly => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Snitchfly,
        id: "Rosary Thief",
        uiName: new LanguageString("Journal", "NAME_ROSARY_THIEF"),
        uiDesc: new LanguageString("Journal", "DESC_ROSARY_THIEF"));
    public static Item Journal_Entry__Lavalug => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Lavalug,
        id: "Tar Slug",
        uiName: new LanguageString("Journal", "NAME_TAR_SLUG"),
        uiDesc: new LanguageString("Journal", "DESC_TAR_SLUG"));
    public static Item Journal_Entry__Lavalarga => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Lavalarga,
        id: "Tar Slug Huge",
        uiName: new LanguageString("Journal", "NAME_TAR_SLUG_HUGE"),
        uiDesc: new LanguageString("Journal", "DESC_TAR_SLUG_HUGE"));
    public static Item Journal_Entry__Smelt_Shoveller => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Smelt_Shoveller,
        id: "Dock Worker",
        uiName: new LanguageString("Journal", "NAME_DOCK_WORKER"),
        uiDesc: new LanguageString("Journal", "DESC_DOCK_WORKER"));
    public static Item Journal_Entry__Flintstone_Flyer => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Flintstone_Flyer,
        id: "Dock Flyer",
        uiName: new LanguageString("Journal", "NAME_DOCK_FLYER"),
        uiDesc: new LanguageString("Journal", "DESC_DOCK_FLYER"));
    public static Item Journal_Entry__Flintflame_Flyer => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Flintflame_Flyer,
        id: "Dock Bomber",
        uiName: new LanguageString("Journal", "NAME_DOCK_BOMBER"),
        uiDesc: new LanguageString("Journal", "DESC_DOCK_BOMBER"));
    public static Item Journal_Entry__Smokerock_Sifter => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Smokerock_Sifter,
        id: "Shield Dock Worker",
        uiName: new LanguageString("Journal", "NAME_SHIELD_DOCK_WORKER"),
        uiDesc: new LanguageString("Journal", "DESC_SHIELD_DOCK_WORKER"));
    public static Item Journal_Entry__Deep_Diver => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Deep_Diver,
        id: "Dock Charger",
        uiName: new LanguageString("Journal", "NAME_DOCK_CHARGER"),
        uiDesc: new LanguageString("Journal", "DESC_DOCK_CHARGER"));
    public static Item Journal_Entry__Forebrothers_Signis_and_Gron => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Forebrothers_Signis_and_Gron,
        id: "Dock Guard Thrower",
        uiName: new LanguageString("Journal", "NAME_DOCK_GUARD_THROWER"),
        uiDesc: new LanguageString("Journal", "DESC_DOCK_GUARD_THROWER"));
    public static Item Journal_Entry__Cragglite => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Cragglite,
        id: "Small Crab",
        uiName: new LanguageString("Journal", "NAME_SMALL_CRAB"),
        uiDesc: new LanguageString("Journal", "DESC_SMALL_CRAB"));
    public static Item Journal_Entry__Craggler => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Craggler,
        id: "Roof Crab",
        uiName: new LanguageString("Journal", "NAME_ROOF_CRAB"),
        uiDesc: new LanguageString("Journal", "DESC_ROOF_CRAB"));
    public static Item Journal_Entry__Brushflit => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Brushflit,
        id: "Fields Flock Flyers",
        uiName: new LanguageString("Journal", "NAME_FIELDS_FLOCK_FLYERS"),
        uiDesc: new LanguageString("Journal", "DESC_FIELDS_FLOCK_FLYERS"));
    public static Item Journal_Entry__Fertid => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Fertid,
        id: "Fields Goomba",
        uiName: new LanguageString("Journal", "NAME_FIELDS_GOOMBA"),
        uiDesc: new LanguageString("Journal", "DESC_FIELDS_GOOMBA"));
    public static Item Journal_Entry__Flapping_Fertid => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Flapping_Fertid,
        id: "Fields Flyer",
        uiName: new LanguageString("Journal", "NAME_FIELDS_FLYER"),
        uiDesc: new LanguageString("Journal", "DESC_FIELDS_FLYER"));
    public static Item Journal_Entry__Fourth_Chorus => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Fourth_Chorus,
        id: "Song Golem",
        uiName: new LanguageString("Journal", "NAME_SONG_GOLEM"),
        uiDesc: new LanguageString("Journal", "DESC_SONG_GOLEM"));
    public static Item Journal_Entry__Skarrlid => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Skarrlid,
        id: "Bone Hunter Tiny",
        uiName: new LanguageString("Journal", "NAME_BONE_HUNTER_TINY"),
        uiDesc: new LanguageString("Journal", "DESC_BONE_HUNTER_TINY"));
    public static Item Journal_Entry__Skarrwing => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Skarrwing,
        id: "Bone Hunter Buzzer",
        uiName: new LanguageString("Journal", "NAME_BONE_HUNTER_BUZZER"),
        uiDesc: new LanguageString("Journal", "DESC_BONE_HUNTER_BUZZER"));
    public static Item Journal_Entry__Skarr_Scout => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Skarr_Scout,
        id: "Bone Hunter Child",
        uiName: new LanguageString("Journal", "NAME_BONE_HUNTER_CHILD"),
        uiDesc: new LanguageString("Journal", "DESC_BONE_HUNTER_CHILD"));
    public static Item Journal_Entry__Skarr_Stalker => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Skarr_Stalker,
        id: "Bone Hunter",
        uiName: new LanguageString("Journal", "NAME_BONE_HUNTER"),
        uiDesc: new LanguageString("Journal", "DESC_BONE_HUNTER"));
    public static Item Journal_Entry__Spear_Skarr => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Spear_Skarr,
        id: "Bone Hunter Fly",
        uiName: new LanguageString("Journal", "NAME_BONE_HUNTER_FLY"),
        uiDesc: new LanguageString("Journal", "DESC_BONE_HUNTER_FLY"));
    public static Item Journal_Entry__Skarrgard => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Skarrgard,
        id: "Bone Hunter Throw",
        uiName: new LanguageString("Journal", "NAME_BONE_HUNTER_THROW"),
        uiDesc: new LanguageString("Journal", "DESC_BONE_HUNTER_THROW"));
    public static Item Journal_Entry__Gurr_the_Outcast => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Gurr_the_Outcast,
        id: "Bone Hunter Trapper",
        uiName: new LanguageString("Journal", "NAME_BONE_HUNTER_TRAPPER"),
        uiDesc: new LanguageString("Journal", "DESC_BONE_HUNTER_TRAPPER"));
    public static Item Journal_Entry__Last_Claw => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Last_Claw,
        id: "Bone Hunter Chief",
        uiName: new LanguageString("Journal", "NAME_BONE_HUNTER_CHIEF"),
        uiDesc: new LanguageString("Journal", "DESC_BONE_HUNTER_CHIEF"));
    public static Item Journal_Entry__Skarrsinger_Karmelita => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Skarrsinger_Karmelita,
        id: "Hunter Queen",
        uiName: new LanguageString("Journal", "NAME_HUNTER_QUEEN"),
        uiDesc: new LanguageString("Journal", "DESC_HUNTER_QUEEN"));
    public static Item Journal_Entry__Mite => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Mite,
        id: "Mite",
        uiName: new LanguageString("Journal", "NAME_MITE"),
        uiDesc: new LanguageString("Journal", "DESC_MITE"));
    public static Item Journal_Entry__Fluttermite => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Fluttermite,
        id: "Mitefly",
        uiName: new LanguageString("Journal", "NAME_MITEFLY"),
        uiDesc: new LanguageString("Journal", "DESC_MITEFLY"));
    public static Item Journal_Entry__Mitemother => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Mitemother,
        id: "Gnat Giant",
        uiName: new LanguageString("Journal", "NAME_GNAT_GIANT"),
        uiDesc: new LanguageString("Journal", "DESC_GNAT_GIANT"));
    public static Item Journal_Entry__Dreg_Catcher => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Dreg_Catcher,
        id: "Farmer Catcher",
        uiName: new LanguageString("Journal", "NAME_FARMER_CATCHER"),
        uiDesc: new LanguageString("Journal", "DESC_FARMER_CATCHER"));
    public static Item Journal_Entry__Silk_Snipper => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Silk_Snipper,
        id: "Farmer Scissors",
        uiName: new LanguageString("Journal", "NAME_FARMER_SCISSORS"),
        uiDesc: new LanguageString("Journal", "DESC_FARMER_SCISSORS"));
    public static Item Journal_Entry__Thread_Raker => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Thread_Raker,
        id: "Farmer Centipede",
        uiName: new LanguageString("Journal", "NAME_FARMER_CENTIPEDE"),
        uiDesc: new LanguageString("Journal", "DESC_FARMER_CENTIPEDE"));
    public static Item Journal_Entry__Moorwing => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Moorwing,
        id: "Vampire Gnat",
        uiName: new LanguageString("Journal", "NAME_VAMPIRE_GNAT"),
        uiDesc: new LanguageString("Journal", "DESC_VAMPIRE_GNAT"));
    public static Item Journal_Entry__Wisp => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Wisp,
        id: "Wisp",
        uiName: new LanguageString("Journal", "NAME_WISP"),
        uiDesc: new LanguageString("Journal", "DESC_WISP"));
    public static Item Journal_Entry__Burning_Bug => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Burning_Bug,
        id: "Farmer Wisp",
        uiName: new LanguageString("Journal", "NAME_FARMER_WISP"),
        uiDesc: new LanguageString("Journal", "DESC_FARMER_WISP"));
    public static Item Journal_Entry__Father_of_the_Flame => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Father_of_the_Flame,
        id: "Wisp Pyre Effigy",
        uiName: new LanguageString("Journal", "NAME_WISP_PYRE_EFFIGY"),
        uiDesc: new LanguageString("Journal", "DESC_WISP_PYRE_EFFIGY"));
    public static Item Journal_Entry__Craw => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Craw,
        id: "Crow",
        uiName: new LanguageString("Journal", "NAME_CROW"),
        uiDesc: new LanguageString("Journal", "DESC_CROW"));
    public static Item Journal_Entry__Tallcraw => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Tallcraw,
        id: "Crowman",
        uiName: new LanguageString("Journal", "NAME_CROWMAN"),
        uiDesc: new LanguageString("Journal", "DESC_CROWMAN"));
    public static Item Journal_Entry__Squatcraw => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Squatcraw,
        id: "Crowman Dagger",
        uiName: new LanguageString("Journal", "NAME_CROWMAN_DAGGER"),
        uiDesc: new LanguageString("Journal", "DESC_CROWMAN_DAGGER"));
    public static Item Journal_Entry__Craw_Juror => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Craw_Juror,
        id: "Crowman Juror Tiny",
        uiName: new LanguageString("Journal", "NAME_CROWMAN_JUROR_TINY"),
        uiDesc: new LanguageString("Journal", "DESC_CROWMAN_JUROR_TINY"));
    public static Item Journal_Entry__Tallcraw_Juror => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Tallcraw_Juror,
        id: "Crowman Juror",
        uiName: new LanguageString("Journal", "NAME_CROWMAN_JUROR"),
        uiDesc: new LanguageString("Journal", "DESC_CROWMAN_JUROR"));
    public static Item Journal_Entry__Squatcraw_Juror => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Squatcraw_Juror,
        id: "Crowman Dagger Juror",
        uiName: new LanguageString("Journal", "NAME_CROWMAN_DAGGER_JUROR"),
        uiDesc: new LanguageString("Journal", "DESC_CROWMAN_DAGGER_JUROR"));
    public static Item Journal_Entry__Crawfather => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Crawfather,
        id: "Crawfather",
        uiName: new LanguageString("Journal", "NAME_CRAWFATHER"),
        uiDesc: new LanguageString("Journal", "DESC_CRAWFATHER"));
    public static Item Journal_Entry__Muckmaggot => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Muckmaggot,
        id: "Maggots",
        uiName: new LanguageString("Journal", "NAME_MAGGOTS"),
        uiDesc: new LanguageString("Journal", "DESC_MAGGOTS"));
    public static Item Journal_Entry__Slubberlug => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Slubberlug,
        id: "Dustroach Pollywog",
        uiName: new LanguageString("Journal", "NAME_DUSTROACH_POLLYWOG"),
        uiDesc: new LanguageString("Journal", "DESC_DUSTROACH_POLLYWOG"));
    public static Item Journal_Entry__Muckroach => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Muckroach,
        id: "Dustroach",
        uiName: new LanguageString("Journal", "NAME_DUSTROACH"),
        uiDesc: new LanguageString("Journal", "DESC_DUSTROACH"));
    public static Item Journal_Entry__Bloatroach => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Bloatroach,
        id: "Bloat Roach",
        uiName: new LanguageString("Journal", "NAME_BLOAT_ROACH"),
        uiDesc: new LanguageString("Journal", "DESC_BLOAT_ROACH"));
    public static Item Journal_Entry__Roachcatcher => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Roachcatcher,
        id: "Roachfeeder Short",
        uiName: new LanguageString("Journal", "NAME_ROACHFEEDER_SHORT"),
        uiDesc: new LanguageString("Journal", "DESC_ROACHFEEDER_SHORT"));
    public static Item Journal_Entry__Roachfeeder => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Roachfeeder,
        id: "Roachfeeder Tall",
        uiName: new LanguageString("Journal", "NAME_ROACHFEEDER_TALL"),
        uiDesc: new LanguageString("Journal", "DESC_ROACHFEEDER_TALL"));
    public static Item Journal_Entry__Roachkeeper => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Roachkeeper,
        id: "Roachkeeper",
        uiName: new LanguageString("Journal", "NAME_ROACHKEEPER"),
        uiDesc: new LanguageString("Journal", "DESC_ROACHKEEPER"));
    public static Item Journal_Entry__Roachserver => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Roachserver,
        id: "Roachkeeper Chef Tiny",
        uiName: new LanguageString("Journal", "NAME_ROACHKEEPER_CHEF_TINY"),
        uiDesc: new LanguageString("Journal", "DESC_ROACHKEEPER_CHEF_TINY"));
    public static Item Journal_Entry__Disgraced_Chef_Lugoli => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Disgraced_Chef_Lugoli,
        id: "Roachkeeper Chef",
        uiName: new LanguageString("Journal", "NAME_ROACHKEEPER_CHEF"),
        uiDesc: new LanguageString("Journal", "DESC_ROACHKEEPER_CHEF"));
    public static Item Journal_Entry__Wraith => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Wraith,
        id: "Wraith",
        uiName: new LanguageString("Journal", "NAME_WRAITH"),
        uiDesc: new LanguageString("Journal", "DESC_WRAITH"));
    public static Item Journal_Entry__Mothleaf_Lagnia => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Mothleaf_Lagnia,
        id: "Swamp Drifter",
        uiName: new LanguageString("Journal", "NAME_SWAMP_DRIFTER"),
        uiDesc: new LanguageString("Journal", "DESC_SWAMP_DRIFTER"));
    public static Item Journal_Entry__Miremite => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Miremite,
        id: "Swamp Goomba",
        uiName: new LanguageString("Journal", "NAME_SWAMP_GOOMBA"),
        uiDesc: new LanguageString("Journal", "DESC_SWAMP_GOOMBA"));
    public static Item Journal_Entry__Swamp_Squit => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Swamp_Squit,
        id: "Swamp Mosquito",
        uiName: new LanguageString("Journal", "NAME_SWAMP_MOSQUITO"),
        uiDesc: new LanguageString("Journal", "DESC_SWAMP_MOSQUITO"));
    public static Item Journal_Entry__Spit_Squit => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Spit_Squit,
        id: "Swamp Mosquito Skinny",
        uiName: new LanguageString("Journal", "NAME_SWAMP_MOSQUITO_SKINNY"),
        uiDesc: new LanguageString("Journal", "DESC_SWAMP_MOSQUITO_SKINNY"));
    public static Item Journal_Entry__Stilkin => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Stilkin,
        id: "Swamp Muckman",
        uiName: new LanguageString("Journal", "NAME_SWAMP_MUCKMAN"),
        uiDesc: new LanguageString("Journal", "DESC_SWAMP_MUCKMAN"));
    public static Item Journal_Entry__Stilkin_Trapper => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Stilkin_Trapper,
        id: "Swamp Muckman Tall",
        uiName: new LanguageString("Journal", "NAME_SWAMP_MUCKMAN_TALL"),
        uiDesc: new LanguageString("Journal", "DESC_SWAMP_MUCKMAN_TALL"));
    public static Item Journal_Entry__Groal_the_Great => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Groal_the_Great,
        id: "Swamp Shaman",
        uiName: new LanguageString("Journal", "NAME_SWAMP_SHAMAN"),
        uiDesc: new LanguageString("Journal", "DESC_SWAMP_SHAMAN"));
    public static Item Journal_Entry__Barnak => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Barnak,
        id: "Swamp Barnacle",
        uiName: new LanguageString("Journal", "NAME_SWAMP_BARNACLE"),
        uiDesc: new LanguageString("Journal", "DESC_SWAMP_BARNACLE"));
    public static Item Journal_Entry__Ductsucker => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Ductsucker,
        id: "Swamp Ductsucker",
        uiName: new LanguageString("Journal", "NAME_SWAMP_DUCTSUCKER"),
        uiDesc: new LanguageString("Journal", "DESC_SWAMP_DUCTSUCKER"));
    public static Item Journal_Entry__Pond_Skipper => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Pond_Skipper,
        id: "Pond Skater",
        uiName: new LanguageString("Journal", "NAME_POND_SKATER"),
        uiDesc: new LanguageString("Journal", "DESC_POND_SKATER"));
    public static Item Journal_Entry__Pondcatcher => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Pondcatcher,
        id: "Pilgrim Fisher",
        uiName: new LanguageString("Journal", "NAME_PILGRIM_FISHER"),
        uiDesc: new LanguageString("Journal", "DESC_PILGRIM_FISHER"));
    public static Item Journal_Entry__Shellwood_Gnat => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Shellwood_Gnat,
        id: "Shellwood Gnat",
        uiName: new LanguageString("Journal", "NAME_SHELLWOOD_GNAT"),
        uiDesc: new LanguageString("Journal", "DESC_SHELLWOOD_GNAT"));
    public static Item Journal_Entry__Wood_Wasp => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Wood_Wasp,
        id: "Shellwood Wasp",
        uiName: new LanguageString("Journal", "NAME_SHELLWOOD_WASP"),
        uiDesc: new LanguageString("Journal", "DESC_SHELLWOOD_WASP"));
    public static Item Journal_Entry__Splinter => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Splinter,
        id: "Stick Insect",
        uiName: new LanguageString("Journal", "NAME_STICK_INSECT"),
        uiDesc: new LanguageString("Journal", "DESC_STICK_INSECT"));
    public static Item Journal_Entry__Splinterhorn => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Splinterhorn,
        id: "Stick Insect Charger",
        uiName: new LanguageString("Journal", "NAME_STICK_INSECT_CHARGER"),
        uiDesc: new LanguageString("Journal", "DESC_STICK_INSECT_CHARGER"));
    public static Item Journal_Entry__Splinterbark => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Splinterbark,
        id: "Stick Insect Flyer",
        uiName: new LanguageString("Journal", "NAME_STICK_INSECT_FLYER"),
        uiDesc: new LanguageString("Journal", "DESC_STICK_INSECT_FLYER"));
    public static Item Journal_Entry__Sister_Splinter => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Sister_Splinter,
        id: "Splinter Queen",
        uiName: new LanguageString("Journal", "NAME_SPLINTER_QUEEN"),
        uiDesc: new LanguageString("Journal", "DESC_SPLINTER_QUEEN"));
    public static Item Journal_Entry__Phacia => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Phacia,
        id: "Flower Drifter",
        uiName: new LanguageString("Journal", "NAME_FLOWER_DRIFTER"),
        uiDesc: new LanguageString("Journal", "DESC_FLOWER_DRIFTER"));
    public static Item Journal_Entry__Pollenica => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Pollenica,
        id: "Bloom Shooter",
        uiName: new LanguageString("Journal", "NAME_BLOOM_SHOOTER"),
        uiDesc: new LanguageString("Journal", "DESC_BLOOM_SHOOTER"));
    public static Item Journal_Entry__Gahlia => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Gahlia,
        id: "Bloom Puncher",
        uiName: new LanguageString("Journal", "NAME_BLOOM_PUNCHER"),
        uiDesc: new LanguageString("Journal", "DESC_BLOOM_PUNCHER"));
    public static Item Journal_Entry__Shrine_Guardian_Seth => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Shrine_Guardian_Seth,
        id: "Seth",
        uiName: new LanguageString("Journal", "NAME_SETH"),
        uiDesc: new LanguageString("Journal", "DESC_SETH"));
    public static Item Journal_Entry__Nyleth => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Nyleth,
        id: "Flower Queen",
        uiName: new LanguageString("Journal", "NAME_FLOWER_QUEEN"),
        uiDesc: new LanguageString("Journal", "DESC_FLOWER_QUEEN"));
    public static Item Journal_Entry__Furm => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Furm,
        id: "Bell Goomba",
        uiName: new LanguageString("Journal", "NAME_BELL_GOOMBA"),
        uiDesc: new LanguageString("Journal", "DESC_BELL_GOOMBA"));
    public static Item Journal_Entry__Winged_Furm => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Winged_Furm,
        id: "Bell Fly",
        uiName: new LanguageString("Journal", "NAME_BELL_FLY"),
        uiDesc: new LanguageString("Journal", "DESC_BELL_FLY"));
    public static Item Journal_Entry__Pharlid => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Pharlid,
        id: "Blade Spider",
        uiName: new LanguageString("Journal", "NAME_BLADE_SPIDER"),
        uiDesc: new LanguageString("Journal", "DESC_BLADE_SPIDER"));
    public static Item Journal_Entry__Pharlid_Diver => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Pharlid_Diver,
        id: "Blade Spider Hang",
        uiName: new LanguageString("Journal", "NAME_BLADE_SPIDER_HANG"),
        uiDesc: new LanguageString("Journal", "DESC_BLADE_SPIDER_HANG"));
    public static Item Journal_Entry__Shardillard => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Shardillard,
        id: "Shell Fossil Mimic",
        uiName: new LanguageString("Journal", "NAME_SHELL_FOSSIL_MIMIC"),
        uiDesc: new LanguageString("Journal", "DESC_SHELL_FOSSIL_MIMIC"));
    public static Item Journal_Entry__Sandcarver => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Sandcarver,
        id: "Sand Centipede",
        uiName: new LanguageString("Journal", "NAME_SAND_CENTIPEDE"),
        uiDesc: new LanguageString("Journal", "DESC_SAND_CENTIPEDE"));
    public static Item Journal_Entry__Squirrm => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Squirrm,
        id: "Coral Judge Child",
        uiName: new LanguageString("Journal", "NAME_CORAL_JUDGE_CHILD"),
        uiDesc: new LanguageString("Journal", "DESC_CORAL_JUDGE_CHILD"));
    public static Item Journal_Entry__Judge => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Judge,
        id: "Coral Judge",
        uiName: new LanguageString("Journal", "NAME_CORAL_JUDGE"),
        uiDesc: new LanguageString("Journal", "DESC_CORAL_JUDGE"));
    public static Item Journal_Entry__Last_Judge => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Last_Judge,
        id: "Last Judge",
        uiName: new LanguageString("Journal", "NAME_LAST_JUDGE"),
        uiDesc: new LanguageString("Journal", "DESC_LAST_JUDGE"));
    public static Item Journal_Entry__Coral_Furm => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Coral_Furm,
        id: "Coral Spike Goomba",
        uiName: new LanguageString("Journal", "NAME_CORAL_SPIKE_GOOMBA"),
        uiDesc: new LanguageString("Journal", "DESC_CORAL_SPIKE_GOOMBA"));
    public static Item Journal_Entry__Driznit => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Driznit,
        id: "Coral Conch Shooter",
        uiName: new LanguageString("Journal", "NAME_CORAL_CONCH_SHOOTER"),
        uiDesc: new LanguageString("Journal", "DESC_CORAL_CONCH_SHOOTER"));
    public static Item Journal_Entry__Driznarga => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Driznarga,
        id: "Coral Conch Shooter Heavy",
        uiName: new LanguageString("Journal", "NAME_CORAL_CONCH_SHOOTER_HEAVY"),
        uiDesc: new LanguageString("Journal", "DESC_CORAL_CONCH_SHOOTER_HEAVY"));
    public static Item Journal_Entry__Pokenabbin => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Pokenabbin,
        id: "Coral Conch Stabber",
        uiName: new LanguageString("Journal", "NAME_CORAL_CONCH_STABBER"),
        uiDesc: new LanguageString("Journal", "DESC_CORAL_CONCH_STABBER"));
    public static Item Journal_Entry__Conchfly => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Conchfly,
        id: "Coral Conch Driller",
        uiName: new LanguageString("Journal", "NAME_CORAL_CONCH_DRILLER"),
        uiDesc: new LanguageString("Journal", "DESC_CORAL_CONCH_DRILLER"));
    public static Item Journal_Entry__Great_Conchfly => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Great_Conchfly,
        id: "Coral Conch Driller Giant",
        uiName: new LanguageString("Journal", "NAME_CORAL_CONCH_DRILLER_GIANT"),
        uiDesc: new LanguageString("Journal", "DESC_CORAL_CONCH_DRILLER_GIANT"));
    public static Item Journal_Entry__Crustcrawler => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Crustcrawler,
        id: "Coral Goombas",
        uiName: new LanguageString("Journal", "NAME_CORAL_GOOMBAS"),
        uiDesc: new LanguageString("Journal", "DESC_CORAL_GOOMBAS"));
    public static Item Journal_Entry__Crustcrag => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Crustcrag,
        id: "Coral Goomba Large",
        uiName: new LanguageString("Journal", "NAME_CORAL_GOOMBA_LARGE"),
        uiDesc: new LanguageString("Journal", "DESC_CORAL_GOOMBA_LARGE"));
    public static Item Journal_Entry__Kai => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Kai,
        id: "Coral Swimmer Fat",
        uiName: new LanguageString("Journal", "NAME_CORAL_SWIMMER_FAT"),
        uiDesc: new LanguageString("Journal", "DESC_CORAL_SWIMMER_FAT"));
    public static Item Journal_Entry__Spinebeak_Kai => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Spinebeak_Kai,
        id: "Poke Swimmer",
        uiName: new LanguageString("Journal", "NAME_POKE_SWIMMER"),
        uiDesc: new LanguageString("Journal", "DESC_POKE_SWIMMER"));
    public static Item Journal_Entry__Steelspine_Kai => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Steelspine_Kai,
        id: "Spike Swimmer",
        uiName: new LanguageString("Journal", "NAME_SPIKE_SWIMMER"),
        uiDesc: new LanguageString("Journal", "DESC_SPIKE_SWIMMER"));
    public static Item Journal_Entry__Yuma => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Yuma,
        id: "Coral Swimmer Small",
        uiName: new LanguageString("Journal", "NAME_CORAL_SWIMMER_SMALL"),
        uiDesc: new LanguageString("Journal", "DESC_CORAL_SWIMMER_SMALL"));
    public static Item Journal_Entry__Yumama => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Yumama,
        id: "Coral Big Jellyfish",
        uiName: new LanguageString("Journal", "NAME_CORAL_BIG_JELLYFISH"),
        uiDesc: new LanguageString("Journal", "DESC_CORAL_BIG_JELLYFISH"));
    public static Item Journal_Entry__Karaka => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Karaka,
        id: "Coral Warrior",
        uiName: new LanguageString("Journal", "NAME_CORAL_WARRIOR"),
        uiDesc: new LanguageString("Journal", "DESC_CORAL_WARRIOR"));
    public static Item Journal_Entry__Kakri => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Kakri,
        id: "Coral Flyer",
        uiName: new LanguageString("Journal", "NAME_CORAL_FLYER"),
        uiDesc: new LanguageString("Journal", "DESC_CORAL_FLYER"));
    public static Item Journal_Entry__Yago => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Yago,
        id: "Coral Flyer Throw",
        uiName: new LanguageString("Journal", "NAME_CORAL_FLYER_THROW"),
        uiDesc: new LanguageString("Journal", "DESC_CORAL_FLYER_THROW"));
    public static Item Journal_Entry__Karak_Gor => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Karak_Gor,
        id: "Coral Brawler",
        uiName: new LanguageString("Journal", "NAME_CORAL_BRAWLER"),
        uiDesc: new LanguageString("Journal", "DESC_CORAL_BRAWLER"));
    public static Item Journal_Entry__Alita => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Alita,
        id: "Coral Hunter",
        uiName: new LanguageString("Journal", "NAME_CORAL_HUNTER"),
        uiDesc: new LanguageString("Journal", "DESC_CORAL_HUNTER"));
    public static Item Journal_Entry__Corrcrust_Karaka => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Corrcrust_Karaka,
        id: "Coral Bubble Brute",
        uiName: new LanguageString("Journal", "NAME_CORAL_BUBBLE_BRUTE"),
        uiDesc: new LanguageString("Journal", "DESC_CORAL_BUBBLE_BRUTE"));
    public static Item Journal_Entry__Crust_King_Khann => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Crust_King_Khann,
        id: "Coral King",
        uiName: new LanguageString("Journal", "NAME_CORAL_KING"),
        uiDesc: new LanguageString("Journal", "DESC_CORAL_KING"));
    public static Item Journal_Entry__Watcher_at_the_Edge => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Watcher_at_the_Edge,
        id: "Coral Warrior Grey",
        uiName: new LanguageString("Journal", "NAME_CORAL_WARRIOR_GREY"),
        uiDesc: new LanguageString("Journal", "DESC_CORAL_WARRIOR_GREY"));
    public static Item Journal_Entry__Voltvyrm => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Voltvyrm,
        id: "Zap Core Enemy",
        uiName: new LanguageString("Journal", "NAME_ZAP_CORE_ENEMY"),
        uiDesc: new LanguageString("Journal", "DESC_ZAP_CORE_ENEMY"));
    public static Item Journal_Entry__Drapefly => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Drapefly,
        id: "Citadel Bat",
        uiName: new LanguageString("Journal", "NAME_CITADEL_BAT"),
        uiDesc: new LanguageString("Journal", "DESC_CITADEL_BAT"));
    public static Item Journal_Entry__Drapelord => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Drapelord,
        id: "Citadel Bat Large",
        uiName: new LanguageString("Journal", "NAME_CITADEL_BAT_LARGE"),
        uiDesc: new LanguageString("Journal", "DESC_CITADEL_BAT_LARGE"));
    public static Item Journal_Entry__Drapemite => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Drapemite,
        id: "Mite Heavy",
        uiName: new LanguageString("Journal", "NAME_MITE_HEAVY"),
        uiDesc: new LanguageString("Journal", "DESC_MITE_HEAVY"));
    public static Item Journal_Entry__Giant_Drapemite => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Giant_Drapemite,
        id: "Understore Mite Giant",
        uiName: new LanguageString("Journal", "NAME_UNDERSTORE_MITE_GIANT"),
        uiDesc: new LanguageString("Journal", "DESC_UNDERSTORE_MITE_GIANT"));
    public static Item Journal_Entry__Underworker => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Underworker,
        id: "Understore Small",
        uiName: new LanguageString("Journal", "NAME_UNDERSTORE_SMALL"),
        uiDesc: new LanguageString("Journal", "DESC_UNDERSTORE_SMALL"));
    public static Item Journal_Entry__Underscrub => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Underscrub,
        id: "Pilgrim 03 Understore",
        uiName: new LanguageString("Journal", "NAME_PILGRIM_03_UNDERSTORE"),
        uiDesc: new LanguageString("Journal", "DESC_PILGRIM_03_UNDERSTORE"));
    public static Item Journal_Entry__Undersweep => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Undersweep,
        id: "Pilgrim Staff Understore",
        uiName: new LanguageString("Journal", "NAME_PILGRIM_STAFF_UNDERSTORE"),
        uiDesc: new LanguageString("Journal", "DESC_PILGRIM_STAFF_UNDERSTORE"));
    public static Item Journal_Entry__Underpoke => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Underpoke,
        id: "Understore Poker",
        uiName: new LanguageString("Journal", "NAME_UNDERSTORE_POKER"),
        uiDesc: new LanguageString("Journal", "DESC_UNDERSTORE_POKER"));
    public static Item Journal_Entry__Underloft => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Underloft,
        id: "Understore Thrower",
        uiName: new LanguageString("Journal", "NAME_UNDERSTORE_THROWER"),
        uiDesc: new LanguageString("Journal", "DESC_UNDERSTORE_THROWER"));
    public static Item Journal_Entry__Undercrank => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Undercrank,
        id: "Understore Heavy",
        uiName: new LanguageString("Journal", "NAME_UNDERSTORE_HEAVY"),
        uiDesc: new LanguageString("Journal", "DESC_UNDERSTORE_HEAVY"));
    public static Item Journal_Entry__Envoy => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Envoy,
        id: "Song Pilgrim 01",
        uiName: new LanguageString("Journal", "NAME_SONG_PILGRIM_01"),
        uiDesc: new LanguageString("Journal", "DESC_SONG_PILGRIM_01"));
    public static Item Journal_Entry__Choir_Pouncer => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Choir_Pouncer,
        id: "Pilgrim 01 Song",
        uiName: new LanguageString("Journal", "NAME_PILGRIM_01_SONG"),
        uiDesc: new LanguageString("Journal", "DESC_PILGRIM_01_SONG"));
    public static Item Journal_Entry__Choir_Hornhead => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Choir_Hornhead,
        id: "Pilgrim 02 Song",
        uiName: new LanguageString("Journal", "NAME_PILGRIM_02_SONG"),
        uiDesc: new LanguageString("Journal", "DESC_PILGRIM_02_SONG"));
    public static Item Journal_Entry__Choir_Bellbearer => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Choir_Bellbearer,
        id: "Pilgrim 03 Song",
        uiName: new LanguageString("Journal", "NAME_PILGRIM_03_SONG"),
        uiDesc: new LanguageString("Journal", "DESC_PILGRIM_03_SONG"));
    public static Item Journal_Entry__Choir_Flyer => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Choir_Flyer,
        id: "Pilgrim 04 Song",
        uiName: new LanguageString("Journal", "NAME_PILGRIM_04_SONG"),
        uiDesc: new LanguageString("Journal", "DESC_PILGRIM_04_SONG"));
    public static Item Journal_Entry__Choir_Elder => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Choir_Elder,
        id: "Pilgrim Stomper Song",
        uiName: new LanguageString("Journal", "NAME_PILGRIM_STOMPER_SONG"),
        uiDesc: new LanguageString("Journal", "DESC_PILGRIM_STOMPER_SONG"));
    public static Item Journal_Entry__Choristor => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Choristor,
        id: "Song Pilgrim 03",
        uiName: new LanguageString("Journal", "NAME_SONG_PILGRIM_03"),
        uiDesc: new LanguageString("Journal", "DESC_SONG_PILGRIM_03"));
    public static Item Journal_Entry__Reed => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Reed,
        id: "Song Reed",
        uiName: new LanguageString("Journal", "NAME_SONG_REED"),
        uiDesc: new LanguageString("Journal", "DESC_SONG_REED"));
    public static Item Journal_Entry__Grand_Reed => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Grand_Reed,
        id: "Song Reed Grand",
        uiName: new LanguageString("Journal", "NAME_SONG_REED_GRAND"),
        uiDesc: new LanguageString("Journal", "DESC_SONG_REED_GRAND"));
    public static Item Journal_Entry__Choir_Clapper => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Choir_Clapper,
        id: "Song Heavy Sentry",
        uiName: new LanguageString("Journal", "NAME_SONG_HEAVY_SENTRY"),
        uiDesc: new LanguageString("Journal", "DESC_SONG_HEAVY_SENTRY"));
    public static Item Journal_Entry__Clawmaiden => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Clawmaiden,
        id: "Song Handmaiden",
        uiName: new LanguageString("Journal", "NAME_SONG_HANDMAIDEN"),
        uiDesc: new LanguageString("Journal", "DESC_SONG_HANDMAIDEN"));
    public static Item Journal_Entry__Memoria => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Memoria,
        id: "Arborium Keeper",
        uiName: new LanguageString("Journal", "NAME_ARBORIUM_KEEPER"),
        uiDesc: new LanguageString("Journal", "DESC_ARBORIUM_KEEPER"));
    public static Item Journal_Entry__Minister => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Minister,
        id: "Song Administrator",
        uiName: new LanguageString("Journal", "NAME_SONG_ADMINISTRATOR"),
        uiDesc: new LanguageString("Journal", "DESC_SONG_ADMINISTRATOR"));
    public static Item Journal_Entry__Maestro => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Maestro,
        id: "Song Pilgrim Maestro",
        uiName: new LanguageString("Journal", "NAME_SONG_PILGRIM_MAESTRO"),
        uiDesc: new LanguageString("Journal", "DESC_SONG_PILGRIM_MAESTRO"));
    public static Item Journal_Entry__Second_Sentinel => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Second_Sentinel,
        id: "Song Knight",
        uiName: new LanguageString("Journal", "NAME_SONG_KNIGHT"),
        uiDesc: new LanguageString("Journal", "DESC_SONG_KNIGHT"));
    public static Item Journal_Entry__Dreg_Husk => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Dreg_Husk,
        id: "Song Threaded Husk",
        uiName: new LanguageString("Journal", "NAME_SONG_THREADED_HUSK"),
        uiDesc: new LanguageString("Journal", "DESC_SONG_THREADED_HUSK"));
    public static Item Journal_Entry__Dregwheel => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Dregwheel,
        id: "Song Threaded Husk Spin",
        uiName: new LanguageString("Journal", "NAME_SONG_THREADED_HUSK_SPIN"),
        uiDesc: new LanguageString("Journal", "DESC_SONG_THREADED_HUSK_SPIN"));
    public static Item Journal_Entry__Surgeon => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Surgeon,
        id: "Song Pilgrim 02",
        uiName: new LanguageString("Journal", "NAME_SONG_PILGRIM_02"),
        uiDesc: new LanguageString("Journal", "DESC_SONG_PILGRIM_02"));
    public static Item Journal_Entry__Mortician => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Mortician,
        id: "Song Creeper",
        uiName: new LanguageString("Journal", "NAME_SONG_CREEPER"),
        uiDesc: new LanguageString("Journal", "DESC_SONG_CREEPER"));
    public static Item Journal_Entry__The_Unravelled => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__The_Unravelled,
        id: "Conductor Boss",
        uiName: new LanguageString("Journal", "NAME_CONDUCTOR_BOSS"),
        uiDesc: new LanguageString("Journal", "DESC_CONDUCTOR_BOSS"));
    public static Item Journal_Entry__Cogwork_Underfly => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Cogwork_Underfly,
        id: "Understore Automaton",
        uiName: new LanguageString("Journal", "NAME_UNDERSTORE_AUTOMATON"),
        uiDesc: new LanguageString("Journal", "DESC_UNDERSTORE_AUTOMATON"));
    public static Item Journal_Entry__Cogwork_Hauler => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Cogwork_Hauler,
        id: "Understore Automaton EX",
        uiName: new LanguageString("Journal", "NAME_UNDERSTORE_AUTOMATON_EX"),
        uiDesc: new LanguageString("Journal", "DESC_UNDERSTORE_AUTOMATON_EX"));
    public static Item Journal_Entry__Cogwork_Crawler => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Cogwork_Crawler,
        id: "Song Automaton Goomba",
        uiName: new LanguageString("Journal", "NAME_SONG_AUTOMATON_GOOMBA"),
        uiDesc: new LanguageString("Journal", "DESC_SONG_AUTOMATON_GOOMBA"));
    public static Item Journal_Entry__Cogworker => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Cogworker,
        id: "Song Automaton Fly",
        uiName: new LanguageString("Journal", "NAME_SONG_AUTOMATON_FLY"),
        uiDesc: new LanguageString("Journal", "DESC_SONG_AUTOMATON_FLY"));
    public static Item Journal_Entry__Cogwork_Spine => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Cogwork_Spine,
        id: "Song Automaton Fly Spike",
        uiName: new LanguageString("Journal", "NAME_SONG_AUTOMATON_FLY_SPIKE"),
        uiDesc: new LanguageString("Journal", "DESC_SONG_AUTOMATON_FLY_SPIKE"));
    public static Item Journal_Entry__Cogwork_Choirbug => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Cogwork_Choirbug,
        id: "Song Automaton 01",
        uiName: new LanguageString("Journal", "NAME_SONG_AUTOMATON_01"),
        uiDesc: new LanguageString("Journal", "DESC_SONG_AUTOMATON_01"));
    public static Item Journal_Entry__Cogwork_Cleanser => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Cogwork_Cleanser,
        id: "Song Automaton 02",
        uiName: new LanguageString("Journal", "NAME_SONG_AUTOMATON_02"),
        uiDesc: new LanguageString("Journal", "DESC_SONG_AUTOMATON_02"));
    public static Item Journal_Entry__Cogwork_Defender => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Cogwork_Defender,
        id: "Song Automaton Shield",
        uiName: new LanguageString("Journal", "NAME_SONG_AUTOMATON_SHIELD"),
        uiDesc: new LanguageString("Journal", "DESC_SONG_AUTOMATON_SHIELD"));
    public static Item Journal_Entry__Cogwork_Clapper => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Cogwork_Clapper,
        id: "Song Automaton Ball",
        uiName: new LanguageString("Journal", "NAME_SONG_AUTOMATON_BALL"),
        uiDesc: new LanguageString("Journal", "DESC_SONG_AUTOMATON_BALL"));
    public static Item Journal_Entry__Cogwork_Dancers => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Cogwork_Dancers,
        id: "Clockwork Dancer",
        uiName: new LanguageString("Journal", "NAME_CLOCKWORK_DANCER"),
        uiDesc: new LanguageString("Journal", "DESC_CLOCKWORK_DANCER"));
    public static Item Journal_Entry__Vaultborn => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Vaultborn,
        id: "Song Scholar Acolyte",
        uiName: new LanguageString("Journal", "NAME_SONG_SCHOLAR_ACOLYTE"),
        uiDesc: new LanguageString("Journal", "DESC_SONG_SCHOLAR_ACOLYTE"));
    public static Item Journal_Entry__Lampbearer => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Lampbearer,
        id: "Lightbearer",
        uiName: new LanguageString("Journal", "NAME_LIGHTBEARER"),
        uiDesc: new LanguageString("Journal", "DESC_LIGHTBEARER"));
    public static Item Journal_Entry__Scrollreader => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Scrollreader,
        id: "Scrollkeeper",
        uiName: new LanguageString("Journal", "NAME_SCROLLKEEPER"),
        uiDesc: new LanguageString("Journal", "DESC_SCROLLKEEPER"));
    public static Item Journal_Entry__Vaultkeeper => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Vaultkeeper,
        id: "Scholar",
        uiName: new LanguageString("Journal", "NAME_SCHOLAR"),
        uiDesc: new LanguageString("Journal", "DESC_SCHOLAR"));
    public static Item Journal_Entry__Trobbio => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Trobbio,
        id: "Trobbio",
        uiName: new LanguageString("Journal", "NAME_TROBBIO"),
        uiDesc: new LanguageString("Journal", "DESC_TROBBIO"));
    public static Item Journal_Entry__Tormented_Trobbio => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Tormented_Trobbio,
        id: "Tormented Trobbio",
        uiName: new LanguageString("Journal", "NAME_TORMENTED_TROBBIO"),
        uiDesc: new LanguageString("Journal", "DESC_TORMENTED_TROBBIO"));
    public static Item Journal_Entry__Penitent => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Penitent,
        id: "Slab Prisoner Leaper New",
        uiName: new LanguageString("Journal", "NAME_SLAB_PRISONER_LEAPER_NEW"),
        uiDesc: new LanguageString("Journal", "DESC_SLAB_PRISONER_LEAPER_NEW"));
    public static Item Journal_Entry__Puny_Penitent => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Puny_Penitent,
        id: "Slab Prisoner Fly New",
        uiName: new LanguageString("Journal", "NAME_SLAB_PRISONER_FLY_NEW"),
        uiDesc: new LanguageString("Journal", "DESC_SLAB_PRISONER_FLY_NEW"));
    public static Item Journal_Entry__Freshfly => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Freshfly,
        id: "Slab Fly Small Fresh",
        uiName: new LanguageString("Journal", "NAME_SLAB_FLY_SMALL_FRESH"),
        uiDesc: new LanguageString("Journal", "DESC_SLAB_FLY_SMALL_FRESH"));
    public static Item Journal_Entry__Scabfly => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Scabfly,
        id: "Slab Fly Small",
        uiName: new LanguageString("Journal", "NAME_SLAB_FLY_SMALL"),
        uiDesc: new LanguageString("Journal", "DESC_SLAB_FLY_SMALL"));
    public static Item Journal_Entry__Guardfly => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Guardfly,
        id: "Slab Fly Mid",
        uiName: new LanguageString("Journal", "NAME_SLAB_FLY_MID"),
        uiDesc: new LanguageString("Journal", "DESC_SLAB_FLY_MID"));
    public static Item Journal_Entry__Wardenfly => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Wardenfly,
        id: "Slab Fly Large",
        uiName: new LanguageString("Journal", "NAME_SLAB_FLY_LARGE"),
        uiDesc: new LanguageString("Journal", "DESC_SLAB_FLY_LARGE"));
    public static Item Journal_Entry__Broodmother => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Broodmother,
        id: "Slab Fly Broodmother",
        uiName: new LanguageString("Journal", "NAME_SLAB_FLY_BROODMOTHER"),
        uiDesc: new LanguageString("Journal", "DESC_SLAB_FLY_BROODMOTHER"));
    public static Item Journal_Entry__Driftlin => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Driftlin,
        id: "Peaks Drifter",
        uiName: new LanguageString("Journal", "NAME_PEAKS_DRIFTER"),
        uiDesc: new LanguageString("Journal", "DESC_PEAKS_DRIFTER"));
    public static Item Journal_Entry__Mnemonid => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Mnemonid,
        id: "Crystal Drifter",
        uiName: new LanguageString("Journal", "NAME_CRYSTAL_DRIFTER"),
        uiDesc: new LanguageString("Journal", "DESC_CRYSTAL_DRIFTER"));
    public static Item Journal_Entry__Mnemonord => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Mnemonord,
        id: "Crystal Drifter Giant",
        uiName: new LanguageString("Journal", "NAME_CRYSTAL_DRIFTER_GIANT"),
        uiDesc: new LanguageString("Journal", "DESC_CRYSTAL_DRIFTER_GIANT"));
    public static Item Journal_Entry__Servitor_Ignim => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Servitor_Ignim,
        id: "Weaver Servitor",
        uiName: new LanguageString("Journal", "NAME_WEAVER_SERVITOR"),
        uiDesc: new LanguageString("Journal", "DESC_WEAVER_SERVITOR"));
    public static Item Journal_Entry__Servitor_Boran => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Servitor_Boran,
        id: "Weaver Servitor Large",
        uiName: new LanguageString("Journal", "NAME_WEAVER_SERVITOR_LARGE"),
        uiDesc: new LanguageString("Journal", "DESC_WEAVER_SERVITOR_LARGE"));
    public static Item Journal_Entry__Winged_Lifeseed => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Winged_Lifeseed,
        id: "Lifeblood Fly",
        uiName: new LanguageString("Journal", "NAME_LIFEBLOOD_FLY"),
        uiDesc: new LanguageString("Journal", "DESC_LIFEBLOOD_FLY"));
    public static Item Journal_Entry__Plasmid => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Plasmid,
        id: "Bone Worm BlueBlood",
        uiName: new LanguageString("Journal", "NAME_BONE_WORM_BLUEBLOOD"),
        uiDesc: new LanguageString("Journal", "DESC_BONE_WORM_BLUEBLOOD"));
    public static Item Journal_Entry__Plasmidas => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Plasmidas,
        id: "Bone Worm BlueTurret",
        uiName: new LanguageString("Journal", "NAME_BONE_WORM_BLUETURRET"),
        uiDesc: new LanguageString("Journal", "DESC_BONE_WORM_BLUETURRET"));
    public static Item Journal_Entry__Plasmified_Zango => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Plasmified_Zango,
        id: "Blue Assistant",
        uiName: new LanguageString("Journal", "NAME_BLUE_ASSISTANT"),
        uiDesc: new LanguageString("Journal", "DESC_BLUE_ASSISTANT"));
    public static Item Journal_Entry__Leaf_Glider => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Leaf_Glider,
        id: "Lilypad Fly",
        uiName: new LanguageString("Journal", "NAME_LILYPAD_FLY"),
        uiDesc: new LanguageString("Journal", "DESC_LILYPAD_FLY"));
    public static Item Journal_Entry__Leaf_Roller => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Leaf_Roller,
        id: "Grass Goomba",
        uiName: new LanguageString("Journal", "NAME_GRASS_GOOMBA"),
        uiDesc: new LanguageString("Journal", "DESC_GRASS_GOOMBA"));
    public static Item Journal_Entry__Pendra => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Pendra,
        id: "Hornet Dragonfly",
        uiName: new LanguageString("Journal", "NAME_HORNET_DRAGONFLY"),
        uiDesc: new LanguageString("Journal", "DESC_HORNET_DRAGONFLY"));
    public static Item Journal_Entry__Pendragor => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Pendragor,
        id: "Dragonfly Large",
        uiName: new LanguageString("Journal", "NAME_DRAGONFLY_LARGE"),
        uiDesc: new LanguageString("Journal", "DESC_DRAGONFLY_LARGE"));
    public static Item Journal_Entry__Nuphar => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Nuphar,
        id: "Lilypad Trap",
        uiName: new LanguageString("Journal", "NAME_LILYPAD_TRAP"),
        uiDesc: new LanguageString("Journal", "DESC_LILYPAD_TRAP"));
    public static Item Journal_Entry__Cloverstag => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Cloverstag,
        id: "Cloverstag",
        uiName: new LanguageString("Journal", "NAME_CLOVERSTAG"),
        uiDesc: new LanguageString("Journal", "DESC_CLOVERSTAG"));
    public static Item Journal_Entry__Palestag => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Palestag,
        id: "Cloverstag White",
        uiName: new LanguageString("Journal", "NAME_CLOVERSTAG_WHITE"),
        uiDesc: new LanguageString("Journal", "DESC_CLOVERSTAG_WHITE"));
    public static Item Journal_Entry__Kindanir => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Kindanir,
        id: "Grasshopper Child",
        uiName: new LanguageString("Journal", "NAME_GRASSHOPPER_CHILD"),
        uiDesc: new LanguageString("Journal", "DESC_GRASSHOPPER_CHILD"));
    public static Item Journal_Entry__Verdanir => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Verdanir,
        id: "Grasshopper Slasher",
        uiName: new LanguageString("Journal", "NAME_GRASSHOPPER_SLASHER"),
        uiDesc: new LanguageString("Journal", "DESC_GRASSHOPPER_SLASHER"));
    public static Item Journal_Entry__Escalion => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Escalion,
        id: "Grasshopper Fly",
        uiName: new LanguageString("Journal", "NAME_GRASSHOPPER_FLY"),
        uiDesc: new LanguageString("Journal", "DESC_GRASSHOPPER_FLY"));
    public static Item Journal_Entry__Clover_Dancers => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Clover_Dancers,
        id: "Clover Dancer",
        uiName: new LanguageString("Journal", "NAME_CLOVER_DANCER"),
        uiDesc: new LanguageString("Journal", "DESC_CLOVER_DANCER"));
    public static Item Journal_Entry__Shadow_Creeper => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Shadow_Creeper,
        id: "Abyss Crawler",
        uiName: new LanguageString("Journal", "NAME_ABYSS_CRAWLER"),
        uiDesc: new LanguageString("Journal", "DESC_ABYSS_CRAWLER"));
    public static Item Journal_Entry__Shadow_Charger => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Shadow_Charger,
        id: "Abyss Crawler Large",
        uiName: new LanguageString("Journal", "NAME_ABYSS_CRAWLER_LARGE"),
        uiDesc: new LanguageString("Journal", "DESC_ABYSS_CRAWLER_LARGE"));
    public static Item Journal_Entry__Gloomsac => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Gloomsac,
        id: "Gloomfly",
        uiName: new LanguageString("Journal", "NAME_GLOOMFLY"),
        uiDesc: new LanguageString("Journal", "DESC_GLOOMFLY"));
    public static Item Journal_Entry__Gargant_Gloom => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Gargant_Gloom,
        id: "Gloom Beast",
        uiName: new LanguageString("Journal", "NAME_GLOOM_BEAST"),
        uiDesc: new LanguageString("Journal", "DESC_GLOOM_BEAST"));
    public static Item Journal_Entry__Void_Tendrils => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Void_Tendrils,
        id: "Void Tendrils",
        uiName: new LanguageString("Journal", "NAME_ABYSS_TENDRIL"),
        uiDesc: new LanguageString("Journal", "DESC_ABYSS_TENDRIL"));
    public static Item Journal_Entry__Void_Mass => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Void_Mass,
        id: "Black Thread Core",
        uiName: new LanguageString("Journal", "NAME_BLACK_THREAD_CORE"),
        uiDesc: new LanguageString("Journal", "DESC_BLACK_THREAD_CORE"));
    public static Item Journal_Entry__Summoned_Saviour => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Summoned_Saviour,
        id: "Abyss Mass",
        uiName: new LanguageString("Journal", "NAME_ABYSS_MASS"),
        uiDesc: new LanguageString("Journal", "DESC_ABYSS_MASS"));
    public static Item Journal_Entry__Wingmould => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Wingmould,
        id: "White Palace Fly",
        uiName: new LanguageString("Journal", "NAME_WHITE_PALACE_FLY"),
        uiDesc: new LanguageString("Journal", "DESC_WHITE_PALACE_FLY"));
    public static Item Journal_Entry__Garpid => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Garpid,
        id: "Centipede Trap",
        uiName: new LanguageString("Journal", "NAME_CENTIPEDE_TRAP"),
        uiDesc: new LanguageString("Journal", "DESC_CENTIPEDE_TRAP"));
    public static Item Journal_Entry__Imoba => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Imoba,
        id: "Spike Lazy Flyer",
        uiName: new LanguageString("Journal", "NAME_SPIKE_LAZY_FLYER"),
        uiDesc: new LanguageString("Journal", "DESC_SPIKE_LAZY_FLYER"));
    public static Item Journal_Entry__Skrill => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Skrill,
        id: "Surface Scuttler",
        uiName: new LanguageString("Journal", "NAME_SURFACE_SCUTTLER"),
        uiDesc: new LanguageString("Journal", "DESC_SURFACE_SCUTTLER"));
    public static Item Journal_Entry__Bell_Eater => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Bell_Eater,
        id: "Giant Centipede",
        uiName: new LanguageString("Journal", "NAME_GIANT_CENTIPEDE"),
        uiDesc: new LanguageString("Journal", "DESC_GIANT_CENTIPEDE"));
    public static Item Journal_Entry__Huge_Flea => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Huge_Flea,
        id: "Giant Flea",
        uiName: new LanguageString("Journal", "NAME_GIANT_FLEA"),
        uiDesc: new LanguageString("Journal", "DESC_GIANT_FLEA"));
    public static Item Journal_Entry__Shakra => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Shakra,
        id: "Shakra",
        uiName: new LanguageString("Journal", "NAME_SHAKRA"),
        uiDesc: new LanguageString("Journal", "DESC_SHAKRA"));
    public static Item Journal_Entry__Garmond_and_Zaza => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Garmond_and_Zaza,
        id: "Garmond_Zaza",
        uiName: new LanguageString("Journal", "NAME_GARMOND_ZAZA"),
        uiDesc: new LanguageString("Journal", "DESC_GARMOND_ZAZA"));
    public static Item Journal_Entry__Lost_Garmond => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Lost_Garmond,
        id: "Garmond",
        uiName: new LanguageString("Journal", "NAME_GARMOND"),
        uiDesc: new LanguageString("Journal", "DESC_GARMOND"));
    public static Item Journal_Entry__Pinstress => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Pinstress,
        id: "Pinstress Boss",
        uiName: new LanguageString("Journal", "NAME_PINSTRESS_BOSS"),
        uiDesc: new LanguageString("Journal", "DESC_PINSTRESS_BOSS"));
    public static Item Journal_Entry__Widow => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Widow,
        id: "Spinner Boss",
        uiName: new LanguageString("Journal", "NAME_SPINNER_BOSS"),
        uiDesc: new LanguageString("Journal", "DESC_SPINNER_BOSS"));
    public static Item Journal_Entry__First_Sinner => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__First_Sinner,
        id: "First Weaver",
        uiName: new LanguageString("Journal", "NAME_FIRST_WEAVER"),
        uiDesc: new LanguageString("Journal", "DESC_FIRST_WEAVER"));
    public static Item Journal_Entry__Phantom => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Phantom,
        id: "Phantom",
        uiName: new LanguageString("Journal", "NAME_PHANTOM"),
        uiDesc: new LanguageString("Journal", "DESC_PHANTOM"));
    public static Item Journal_Entry__Lace => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Lace,
        id: "Lace",
        uiName: new LanguageString("Journal", "NAME_LACE"),
        uiDesc: new LanguageString("Journal", "DESC_LACE"));
    public static Item Journal_Entry__Grand_Mother_Silk => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Grand_Mother_Silk,
        id: "Silk Boss",
        uiName: new LanguageString("Journal", "NAME_SILK_BOSS"),
        uiDesc: new LanguageString("Journal", "DESC_SILK_BOSS"));
    public static Item Journal_Entry__Lost_Lace => CreateJournalEntryItem(
        name: ItemNames.Journal_Entry__Lost_Lace,
        id: "Lost Lace",
        uiName: new LanguageString("Journal", "NAME_LOST_LACE"),
        uiDesc: new LanguageString("Journal", "DESC_LOST_LACE"));
}
