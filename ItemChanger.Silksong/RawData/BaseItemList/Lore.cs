using ItemChanger.Items;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.UIDefs;

namespace ItemChanger.Silksong.RawData;

// TODO - think of if we want the lore tablet item to do anything else,
// e.g. playing the audio like in HK
// TODO - add a sprite

internal static partial class BaseItemList
{
    public static Item CreateLoreItem(string name, string sheet, string key, DialogueBox.DisplayOptions? displayOptions = null)
    {
        // *All* lore tablets are DefaultTopCenter
        DialogueBox.DisplayOptions actualDisplayOptions = displayOptions ?? LoreUIDef.DefaultTopCenter;

        return new NullItem()
        {
            Name = name,
            UIDef = new LoreUIDef()
            {
                Fallback = new MsgUIDef()
                {
                    Name = ItemChangerLanguageStrings.INV_NAME_LORE,
                    Sprite = new EmptySprite(),
                },
                Text = new LanguageString(sheet, key),
                DisplayOptions = actualDisplayOptions
            }
        };
    }

    public static Item Lore_Tablet__Abyss_Top
        => CreateLoreItem(ItemNames.Lore_Tablet__Abyss_Top, "Inspect", "ABYSS_LORE_STONE_TOP");

    public static Item Lore_Tablet__Abyss_Bottom_Left
        => CreateLoreItem(ItemNames.Lore_Tablet__Abyss_Bottom_Left, "Inspect", "ABYSS_LORE_STONE_BASE");

    public static Item Lore_Tablet__Fleatopia_Weaver_Harp
        => CreateLoreItem(ItemNames.Lore_Tablet__Fleatopia_Weaver_Harp, "Inspect", "WEAVE_WHITE_LAKE");

    public static Item Lore_Tablet__Memorium_Entrance
        => CreateLoreItem(ItemNames.Lore_Tablet__Memorium_Entrance, "Inspect", "ARBORIUM_PLAQUE");

    public static Item Lore_Tablet__Memorium_Orders
        => CreateLoreItem(ItemNames.Lore_Tablet__Memorium_Orders, "Inspect", "ARBORIUM_ORDERS");

    public static Item Lore_Tablet__Bellhart_East
        => CreateLoreItem(ItemNames.Lore_Tablet__Bellhart_East, "Inspect", "BELLTOWN_OUTER_SIGN");

    public static Item Lore_Tablet__Bellhart_West
        => CreateLoreItem(ItemNames.Lore_Tablet__Bellhart_West, "Inspect", "BELLTOWN_OUTER_SIGN");

    public static Item Lore_Tablet__Marrow_Start
        => CreateLoreItem(ItemNames.Lore_Tablet__Marrow_Start, "Lore", "MARROW_START_SIGN");

    public static Item Lore_Tablet__Marrow_Top
        => CreateLoreItem(ItemNames.Lore_Tablet__Marrow_Top, "Inspect", "CURIOUS_PILGRIM_DIARY");

    public static Item Lore_Tablet__Pilgrims_Rest
        => CreateLoreItem(ItemNames.Lore_Tablet__Pilgrims_Rest, "Inspect", "PILGRIM_REST_SIGN");

    public static Item Lore_Tablet__Weavenest_Cindril
        => CreateLoreItem(ItemNames.Lore_Tablet__Weavenest_Cindril, "Inspect", "WEAVE_WILDS");

    public static Item Lore_Tablet__Verdania_End
        => CreateLoreItem(ItemNames.Lore_Tablet__Verdania_End, "Inspect", "CLOVER_OATH_PLAQUE");

    public static Item Lore_Tablet__Verdania_Fountains
        => CreateLoreItem(ItemNames.Lore_Tablet__Verdania_Fountains, "Inspect", "CLOVER_LAKE_PLAQUE");

    public static Item Lore_Tablet__Blasted_Steps_Start
        => CreateLoreItem(ItemNames.Lore_Tablet__Blasted_Steps_Start, "Inspect", "CORAL_JUDGEMENT_SIGN");

    public static Item Lore_Tablet__Karak_Entrance
        => CreateLoreItem(ItemNames.Lore_Tablet__Karak_Entrance, "Lore", "CORAL_CRUST_TAB_1");

    public static Item Lore_Tablet__Blasted_Steps_Nursery
        => CreateLoreItem(ItemNames.Lore_Tablet__Blasted_Steps_Nursery, "Inspect", "JUDGE_NURSERY");

    public static Item Lore_Tablet__Coral_Tower
        => CreateLoreItem(ItemNames.Lore_Tablet__Coral_Tower, "Lore", "CORAL_CRUST_TAB_2");

    public static Item Lore_Tablet__Cradle_Cage_1
        => CreateLoreItem(ItemNames.Lore_Tablet__Cradle_Cage_1, "Inspect", "CRADLE_CAGE_02");

    public static Item Lore_Tablet__Cradle_Cage_2
        => CreateLoreItem(ItemNames.Lore_Tablet__Cradle_Cage_2, "Inspect", "CRADLE_CAGE_01");

    public static Item Lore_Tablet__Cradle_Cage_3
        => CreateLoreItem(ItemNames.Lore_Tablet__Cradle_Cage_3, "Inspect", "CRADLE_CAGE_03");

    public static Item Lore_Tablet__Greymoor_Orders_Above_Home
        => CreateLoreItem(ItemNames.Lore_Tablet__Greymoor_Orders_Above_Home, "Inspect", "GREY_ORDERS");

    public static Item Lore_Tablet__Greymoor_Bottom
        => CreateLoreItem(ItemNames.Lore_Tablet__Greymoor_Bottom, "Inspect", "GREY_LORE");

    public static Item Lore_Tablet__Trobbio_Sign
        => CreateLoreItem(ItemNames.Lore_Tablet__Trobbio_Sign, "Inspect", "TROBBIO_SIGN");

    public static Item Lore_Tablet__Trobbio_Notes
        => CreateLoreItem(ItemNames.Lore_Tablet__Trobbio_Notes, "Inspect", "LIBRARY_THEATRE_LINES");

    public static Item Lore_Tablet__Tormented_Trobbio_Notes
        => CreateLoreItem(ItemNames.Lore_Tablet__Tormented_Trobbio_Notes, "Inspect", "LIBRARY_THEATRE_LINES_ACT3");

    public static Item Lore_Tablet__Mosshome_Below_Silkspear
        => CreateLoreItem(ItemNames.Lore_Tablet__Mosshome_Below_Silkspear, "Inspect", "MOSSTOWN_STONE");

    public static Item Lore_Tablet__Mosshome_Below_Silkspear_Harp
        => CreateLoreItem(ItemNames.Lore_Tablet__Mosshome_Below_Silkspear_Harp, "Inspect", "WEAVE_HARP_MOSSTOWN");

    public static Item Lore_Tablet__Mount_Fay_Bottom
        => CreateLoreItem(ItemNames.Lore_Tablet__Mount_Fay_Bottom, "Inspect", "WEAVE_PEAK");

    public static Item Lore_Tablet__Deep_Docks_Forge
        => CreateLoreItem(ItemNames.Lore_Tablet__Deep_Docks_Forge, "Inspect", "DOCKS_NOTE_1");

    public static Item Lore_Tablet__Bilewater_Shortcut
        => CreateLoreItem(ItemNames.Lore_Tablet__Bilewater_Shortcut, "Inspect", "SWAMP_STOREROOM");

    public static Item Lore_Tablet__Bilewater_Above_Groal
        => CreateLoreItem(ItemNames.Lore_Tablet__Bilewater_Above_Groal, "Inspect", "BILEHAVEN_PLAQUE");

    public static Item Lore_Tablet__Weavenest_Murglin
        => CreateLoreItem(ItemNames.Lore_Tablet__Weavenest_Murglin, "Inspect", "WEAVE_WORKSHOP_SCROLL");

    public static Item Lore_Tablet__Shellwood_West
        => CreateLoreItem(ItemNames.Lore_Tablet__Shellwood_West, "Inspect", "SHELLGRAVE");

    public static Item Lore_Tablet__Shellwood_Harp
        => CreateLoreItem(ItemNames.Lore_Tablet__Shellwood_Harp, "Inspect", "WEAVE_HARP_SHELLWOOD");

    public static Item Lore_Tablet__Nyleth
        => CreateLoreItem(ItemNames.Lore_Tablet__Nyleth, "Inspect", "SHELLWOOD_SHRINE_SIGN");

    public static Item Lore_Tablet__Slab_Orders_1
        => CreateLoreItem(ItemNames.Lore_Tablet__Slab_Orders_1, "Inspect", "SLAB_ORDERS_1");

    public static Item Lore_Tablet__Slab_Orders_2
        => CreateLoreItem(ItemNames.Lore_Tablet__Slab_Orders_2, "Inspect", "SLAB_ORDERS_2");

    public static Item Lore_Tablet__First_Sinner
        => CreateLoreItem(ItemNames.Lore_Tablet__First_Sinner, "Inspect", "SLAB_WEAVER_GATE");

    public static Item Lore_Tablet__Ventrica_Hub
        => CreateLoreItem(ItemNames.Lore_Tablet__Ventrica_Hub, "Inspect", "TUBE_HUB_NOTICE");

    public static Item Lore_Tablet__Moss_Grotto_Chapel_Entrance
        => CreateLoreItem(ItemNames.Lore_Tablet__Moss_Grotto_Chapel_Entrance, "Inspect", "SHAMAN_STORE_ROOM");

    public static Item Lore_Tablet__Moss_Grotto_Chapel_Inner
        => CreateLoreItem(ItemNames.Lore_Tablet__Moss_Grotto_Chapel_Inner, "Inspect", "SHAMAN_STONE_CHAPEL");

    public static Item Lore_Tablet__Whiteward_Oath
        => CreateLoreItem(ItemNames.Lore_Tablet__Whiteward_Oath, "Inspect", "WARD_OATH");

    public static Item Lore_Tablet__Weavenest_Atla_East
        => CreateLoreItem(ItemNames.Lore_Tablet__Weavenest_Atla_East, "Inspect", "WEAVE_ARCHIVE_RIGHT");
}
