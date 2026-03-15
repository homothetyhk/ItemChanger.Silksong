using ItemChanger.Items;
using ItemChanger.Silksong.Items;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.UIDefs;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseItemList
{
    // maps
    public static Item Bellhart_Map => new PDBoolItem { Name = ItemNames.Bellhart_Map, BoolName = nameof(PlayerData.HasBellhartMap), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Bellhart_Map_Name, ShopDesc = BaseLanguageStrings.Bellhart_Map_Desc, Sprite = BaseAtlasSprites.Bellhart_Map } };
    
    public static Item Bilewater_Map => new PDBoolItem
    {
        Name = ItemNames.Bilewater_Map,
        BoolName = nameof(PlayerData.HasSwampMap),
        UIDef = new MsgUIDef()
        {
            Name = BaseLanguageStrings.Bilewater_Map_Name,
            ShopDesc = BaseLanguageStrings.Bilewater_Map_Desc,
            Sprite = new BilewaterMapSprite()
        }
    };

    public static Item Blasted_Steps_Map => new PDBoolItem { Name = ItemNames.Blasted_Steps_Map, BoolName = nameof(PlayerData.HasJudgeStepsMap), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Blasted_Steps_Map_Name, ShopDesc = BaseLanguageStrings.Blasted_Steps_Map_Desc, Sprite = BaseAtlasSprites.Blasted_Steps_Map } };
    public static Item Choral_Chambers_Map => new PDBoolItem { Name = ItemNames.Choral_Chambers_Map, BoolName = nameof(PlayerData.HasHallsMap), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Choral_Chambers_Map_Name, ShopDesc = null!, Sprite = BaseAtlasSprites.Choral_Chambers_Map } };
    public static Item Cogwork_Core_Map => new PDBoolItem { Name = ItemNames.Cogwork_Core_Map, BoolName = nameof(PlayerData.HasCogMap), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Cogwork_Core_Map_Name, ShopDesc = null!, Sprite = BaseAtlasSprites.Cogwork_Core_Map } };
    public static Item Cradle_Map => new PDBoolItem { Name = ItemNames.Cradle_Map, BoolName = nameof(PlayerData.HasCradleMap), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Cradle_Map_Name, ShopDesc = null!, Sprite = BaseAtlasSprites.Cradle_Map } };
    public static Item Deep_Docks_Map => new PDBoolItem { Name = ItemNames.Deep_Docks_Map, BoolName = nameof(PlayerData.HasDocksMap), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Deep_Docks_Map_Name, ShopDesc = BaseLanguageStrings.Deep_Docks_Map_Desc, Sprite = BaseAtlasSprites.Deep_Docks_Map } };
    public static Item Far_Fields_Map => new PDBoolItem { Name = ItemNames.Far_Fields_Map, BoolName = nameof(PlayerData.HasWildsMap), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Far_Fields_Map_Name, ShopDesc = BaseLanguageStrings.Far_Fields_Map_Desc, Sprite = BaseAtlasSprites.Far_Fields_Map } };
    public static Item Grand_Gate_Map => new PDBoolItem { Name = ItemNames.Grand_Gate_Map, BoolName = nameof(PlayerData.HasSongGateMap), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Grand_Gate_Map_Name, ShopDesc = null!, Sprite = BaseAtlasSprites.Grand_Gate_Map } };
    public static Item Greymoor_Map => new PDBoolItem { Name = ItemNames.Greymoor_Map, BoolName = nameof(PlayerData.HasGreymoorMap), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Greymoor_Map_Name, ShopDesc = BaseLanguageStrings.Greymoor_Map_Desc, Sprite = BaseAtlasSprites.Greymoor_Map } };
    public static Item High_Halls_Map => new PDBoolItem { Name = ItemNames.High_Halls_Map, BoolName = nameof(PlayerData.HasHangMap), UIDef = new MsgUIDef { Name = BaseLanguageStrings.High_Halls_Map_Name, ShopDesc = null!, Sprite = BaseAtlasSprites.High_Halls_Map } };
    public static Item Hunter_s_March_Map => new PDBoolItem { Name = ItemNames.Hunter_s_March_Map, BoolName = nameof(PlayerData.HasHuntersNestMap), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Hunter_s_March_Map_Name, ShopDesc = BaseLanguageStrings.Hunter_s_March_Map_Desc, Sprite = BaseAtlasSprites.Hunter_s_March_Map } };
    public static Item Memorium_Map => new PDBoolItem { Name = ItemNames.Memorium_Map, BoolName = nameof(PlayerData.HasArboriumMap), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Memorium_Map_Name, ShopDesc = null!, Sprite = BaseAtlasSprites.Memorium_Map } };
    public static Item Mosslands_Map => new PDBoolItem { Name = ItemNames.Mosslands_Map, BoolName = nameof(PlayerData.HasMossGrottoMap), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Mosslands_Map_Name, ShopDesc = BaseLanguageStrings.Mosslands_Map_Desc, Sprite = BaseAtlasSprites.Mosslands_Map } };
    public static Item Mount_Fay_Map => new PDBoolItem { Name = ItemNames.Mount_Fay_Map, BoolName = nameof(PlayerData.HasPeakMap), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Mount_Fay_Map_Name, ShopDesc = BaseLanguageStrings.Mount_Fay_Map_Desc, Sprite = BaseAtlasSprites.Mount_Fay_Map } };
    public static Item Putrified_Ducts_Map => new PDBoolItem { Name = ItemNames.Putrified_Ducts_Map, BoolName = nameof(PlayerData.HasAqueductMap), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Putrified_Ducts_Map_Name, ShopDesc = BaseLanguageStrings.Putrified_Ducts_Map_Desc, Sprite = BaseAtlasSprites.Putrified_Ducts_Map } };
    public static Item Sands_of_Karak_Map => new PDBoolItem { Name = ItemNames.Sands_of_Karak_Map, BoolName = nameof(PlayerData.HasCoralMap), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Sands_of_Karak_Map_Name, ShopDesc = BaseLanguageStrings.Sands_of_Karak_Map_Desc, Sprite = BaseAtlasSprites.Sands_of_Karak_Map } };
    public static Item Shellwood_Map => new PDBoolItem { Name = ItemNames.Shellwood_Map, BoolName = nameof(PlayerData.HasShellwoodMap), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Shellwood_Map_Name, ShopDesc = BaseLanguageStrings.Shellwood_Map_Desc, Sprite = BaseAtlasSprites.Shellwood_Map } };
    public static Item Sinner_s_Road_Map => new PDBoolItem { Name = ItemNames.Sinner_s_Road_Map, BoolName = nameof(PlayerData.HasDustpensMap), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Sinner_s_Road_Map_Name, ShopDesc = BaseLanguageStrings.Sinner_s_Road_Map_Desc, Sprite = BaseAtlasSprites.Sinner_s_Road_Map } };
    public static Item The_Abyss_Map => new PDBoolItem { Name = ItemNames.The_Abyss_Map, BoolName = nameof(PlayerData.HasAbyssMap), UIDef = new MsgUIDef { Name = BaseLanguageStrings.The_Abyss_Map_Name, ShopDesc = BaseLanguageStrings.The_Abyss_Map_Desc, Sprite = BaseAtlasSprites.The_Abyss_Map } };
    public static Item The_Marrow_Map => new PDBoolItem { Name = ItemNames.The_Marrow_Map, BoolName = nameof(PlayerData.HasBoneforestMap), UIDef = new MsgUIDef { Name = BaseLanguageStrings.The_Marrow_Map_Name, ShopDesc = BaseLanguageStrings.The_Marrow_Map_Desc, Sprite = BaseAtlasSprites.The_Marrow_Map } };
    public static Item The_Slab_Map => new PDBoolItem { Name = ItemNames.The_Slab_Map, BoolName = nameof(PlayerData.HasSlabMap), UIDef = new MsgUIDef { Name = BaseLanguageStrings.The_Slab_Map_Name, ShopDesc = BaseLanguageStrings.The_Slab_Map_Desc, Sprite = BaseAtlasSprites.The_Slab_Map } };
    public static Item Underworks_Map => new PDBoolItem { Name = ItemNames.Underworks_Map, BoolName = nameof(PlayerData.HasCitadelUnderstoreMap), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Underworks_Map_Name, ShopDesc = BaseLanguageStrings.Underworks_Map_Desc, Sprite = BaseAtlasSprites.Underworks_Map } };
    public static Item Verdania_Map => new PDBoolItem { Name = ItemNames.Verdania_Map, BoolName = nameof(PlayerData.HasCloverMap), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Verdania_Map_Name, ShopDesc = null!, Sprite = BaseAtlasSprites.Verdania_Map } };
    public static Item Weavenest_Alta_Map => new PDBoolItem { Name = ItemNames.Weavenest_Alta_Map, BoolName = nameof(PlayerData.HasWeavehomeMap), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Weavenest_Alta_Map_Name, ShopDesc = BaseLanguageStrings.Weavenest_Alta_Map_Desc, Sprite = BaseAtlasSprites.Weavenest_Alta_Map } };
    public static Item Whispering_Vaults_Map => new PDBoolItem { Name = ItemNames.Whispering_Vaults_Map, BoolName = nameof(PlayerData.HasLibraryMap), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Whispering_Vaults_Map_Name, ShopDesc = null!, Sprite = BaseAtlasSprites.Whispering_Vaults_Map } };
    public static Item Whiteward_Map => new PDBoolItem { Name = ItemNames.Whiteward_Map, BoolName = nameof(PlayerData.HasWardMap), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Whiteward_Map_Name, ShopDesc = null!, Sprite = BaseAtlasSprites.Whiteward_Map } };
    public static Item Wormways_Map => new PDBoolItem { Name = ItemNames.Wormways_Map, BoolName = nameof(PlayerData.HasCrawlMap), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Wormways_Map_Name, ShopDesc = BaseLanguageStrings.Wormways_Map_Desc, Sprite = BaseAtlasSprites.Wormways_Map } };

    // quills
    public static Item Quill__White => new QuillItem { Name = ItemNames.Quill__White, QuillState = 1, UIDef = new MsgUIDef { Name = BaseLanguageStrings.Quill_Name, ShopDesc = BaseLanguageStrings.Quill_Desc, Sprite = BaseAtlasSprites.Quill__White } };
    public static Item Quill__Red => new QuillItem { Name = ItemNames.Quill__Red, QuillState = 2, UIDef = new MsgUIDef { Name = BaseLanguageStrings.Quill_Name, ShopDesc = BaseLanguageStrings.Quill_Desc, Sprite = BaseAtlasSprites.Quill__Red } };
    public static Item Quill__Purple => new QuillItem { Name = ItemNames.Quill__Purple, QuillState = 3, UIDef = new MsgUIDef { Name = BaseLanguageStrings.Quill_Name, ShopDesc = BaseLanguageStrings.Quill_Desc, Sprite = BaseAtlasSprites.Quill__Purple } };

    // map markers
    public static Item Shell_Marker => new MarkerItem { Name = ItemNames.Shell_Marker, BoolName = nameof(PlayerData.hasMarker_a), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Shell_Marker_Name, ShopDesc = BaseLanguageStrings.Shell_Marker_Desc, Sprite = BaseAtlasSprites.Shell_Marker } };
    public static Item Ring_Marker => new MarkerItem { Name = ItemNames.Ring_Marker, BoolName = nameof(PlayerData.hasMarker_b), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Ring_Marker_Name, ShopDesc = BaseLanguageStrings.Ring_Marker_Desc, Sprite = BaseAtlasSprites.Ring_Marker } };
    public static Item Hunt_Marker => new MarkerItem { Name = ItemNames.Hunt_Marker, BoolName = nameof(PlayerData.hasMarker_c), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Hunt_Marker_Name, ShopDesc = BaseLanguageStrings.Hunt_Marker_Desc, Sprite = BaseAtlasSprites.Hunt_Marker } };
    public static Item Dark_Marker => new MarkerItem { Name = ItemNames.Dark_Marker, BoolName = nameof(PlayerData.hasMarker_d), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Dark_Marker_Name, ShopDesc = BaseLanguageStrings.Dark_Marker_Desc, Sprite = BaseAtlasSprites.Dark_Marker } };
    public static Item Bronze_Marker => new MarkerItem { Name = ItemNames.Bronze_Marker, BoolName = nameof(PlayerData.hasMarker_e), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Bronze_Marker_Name, ShopDesc = BaseLanguageStrings.Bronze_Marker_Desc, Sprite = BaseAtlasSprites.Bronze_Marker } };

    // map pins
    public static Item Bench_Pins => new PDBoolItem { Name = ItemNames.Bench_Pins, BoolName = nameof(PlayerData.hasPinBench), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Bench_Pins_Name, ShopDesc = BaseLanguageStrings.Bench_Pins_Desc, Sprite = BaseAtlasSprites.Bench_Pins } };
    public static Item Bellway_Pins => new PDBoolItem { Name = ItemNames.Bellway_Pins, BoolName = nameof(PlayerData.hasPinStag), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Bellway_Pins_Name, ShopDesc = BaseLanguageStrings.Bellway_Pins_Desc, Sprite = BaseAtlasSprites.Bellway_Pins } };
    public static Item Vendor_Pins => new PDBoolItem { Name = ItemNames.Vendor_Pins, BoolName = nameof(PlayerData.hasPinShop), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Vendor_Pins_Name, ShopDesc = BaseLanguageStrings.Vendor_Pins_Desc, Sprite = BaseAtlasSprites.Vendor_Pins } };
    public static Item Ventrica_Pins => new PDBoolItem { Name = ItemNames.Ventrica_Pins, BoolName = nameof(PlayerData.hasPinTube), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Ventrica_Pins_Name, ShopDesc = BaseLanguageStrings.Ventrica_Pins_Desc, Sprite = BaseAtlasSprites.Ventrica_Pins } };

    // flea findings
    //TODO: find better names and descriptions for flea findings (currently <flea finding location> : null)
    public static Item Flea_Findings__Bonelands => new PDBoolItem { Name = ItemNames.Flea_Findings__Bonelands, BoolName = nameof(PlayerData.hasPinFleaMarrowlands), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Flea_Findings__Bonelands_Desc, ShopDesc = null!, Sprite = BaseAtlasSprites.Flea_Findings_Icon } };
    public static Item Flea_Findings__Midlands => new PDBoolItem { Name = ItemNames.Flea_Findings__Midlands, BoolName = nameof(PlayerData.hasPinFleaMidlands), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Flea_Findings__Midlands_Desc, ShopDesc = null!, Sprite = BaseAtlasSprites.Flea_Findings_Icon } };
    public static Item Flea_Findings__Blasted_Steps => new PDBoolItem { Name = ItemNames.Flea_Findings__Blasted_Steps, BoolName = nameof(PlayerData.hasPinFleaBlastedlands), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Flea_Findings__Blasted_Steps_Desc, ShopDesc = null!, Sprite = BaseAtlasSprites.Flea_Findings_Icon } };
    public static Item Flea_Findings__The_Citadel => new PDBoolItem { Name = ItemNames.Flea_Findings__The_Citadel, BoolName = nameof(PlayerData.hasPinFleaCitadel), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Flea_Findings__The_Citadel_Desc, ShopDesc = null!, Sprite = BaseAtlasSprites.Flea_Findings_Icon } };
    public static Item Flea_Findings__Mount_Fay => new PDBoolItem { Name = ItemNames.Flea_Findings__Mount_Fay, BoolName = nameof(PlayerData.hasPinFleaPeaklands), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Flea_Findings__Mount_Fay_Desc, ShopDesc = null!, Sprite = BaseAtlasSprites.Flea_Findings_Icon } };
    public static Item Flea_Findings__Bilelands => new PDBoolItem { Name = ItemNames.Flea_Findings__Bilelands, BoolName = nameof(PlayerData.hasPinFleaMucklands), UIDef = new MsgUIDef { Name = BaseLanguageStrings.Flea_Findings__Bilelands_Desc, ShopDesc = null!, Sprite = BaseAtlasSprites.Flea_Findings_Icon } };
}
