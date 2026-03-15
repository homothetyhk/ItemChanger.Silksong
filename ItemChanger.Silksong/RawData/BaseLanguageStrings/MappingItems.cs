using ItemChanger.Silksong.Serialization;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLanguageStrings
{

    //quills
    //NOTE: the quill items share the same name and description regardless of color
    public static LanguageString Quill_Name => new("UI", "INV_NAME_QUILL");
    public static LanguageString Quill_Desc => new("UI", "INV_DESC_QUILL");
    public static LanguageString Map_Name => new("UI", "INV_NAME_MAP");
    public static LanguageString Map_Desc => new("UI", "INV_DESC_MAP");
    public static LanguageString Map_and_Quill_Name => new("UI", "INV_NAME_MAPANDQUILL");
    public static LanguageString Map_and_Quill_Desc => new("UI", "INV_DESC_MAPANDQUILL");

    //maps (shakra/non citadel map machines)
    public static LanguageString Bellhart_Map_Name => new("Wanderers", "MAPPER_ITEM_MAP_BELLHART_NAME");
    public static LanguageString Bellhart_Map_Desc => new("Wanderers", "MAPPER_ITEM_MAP_BELLHART_DESC");
    public static LanguageString Bilewater_Map_Name => new("Wanderers", "MAPPER_ITEM_MAP_SHADOW_NAME");
    public static LanguageString Bilewater_Map_Desc => new("Wanderers", "MAPPER_ITEM_MAP_SHADOW_DESC");
    public static LanguageString Blasted_Steps_Map_Name => new("Wanderers", "MAPPER_ITEM_MAP_JUDGESTEPS_NAME");
    public static LanguageString Blasted_Steps_Map_Desc => new("Wanderers", "MAPPER_ITEM_MAP_JUDGESTEPS_DESC");
    public static LanguageString Deep_Docks_Map_Name => new("Wanderers", "MAPPER_ITEM_MAP_DOCKS_NAME");
    public static LanguageString Deep_Docks_Map_Desc => new("Wanderers", "MAPPER_ITEM_MAP_DOCKS_DESC");
    public static LanguageString Far_Fields_Map_Name => new("Wanderers", "MAPPER_ITEM_MAP_WILDS_NAME");
    public static LanguageString Far_Fields_Map_Desc => new("Wanderers", "MAPPER_ITEM_MAP_WILDS_DESC");
    public static LanguageString Greymoor_Map_Name => new("Wanderers", "MAPPER_ITEM_MAP_GREYMOOR_NAME");
    public static LanguageString Greymoor_Map_Desc => new("Wanderers", "MAPPER_ITEM_MAP_GREYMOOR_DESC");
    public static LanguageString Hunter_s_March_Map_Name => new("Wanderers", "MAPPER_ITEM_MAP_HUNTERSNEST_NAME");
    public static LanguageString Hunter_s_March_Map_Desc => new("Wanderers", "MAPPER_ITEM_MAP_HUNTERSNEST_DESC");
    public static LanguageString Mosslands_Map_Name => new("Wanderers", "MAPPER_ITEM_MAP_MOSSGROTTO_NAME");
    public static LanguageString Mosslands_Map_Desc => new("Wanderers", "MAPPER_ITEM_MAP_MOSSGROTTO_DESC");
    public static LanguageString Mount_Fay_Map_Name => new("Wanderers", "MAPPER_ITEM_MAP_PEAK_NAME");
    public static LanguageString Mount_Fay_Map_Desc => new("Wanderers", "MAPPER_ITEM_MAP_PEAK_DESC");
    public static LanguageString Putrified_Ducts_Map_Name => new("Inspect", "AQUEDUCT_MAP_GET_MSG");
    public static LanguageString Putrified_Ducts_Map_Desc => new("Inspect", "AQUEDUCT_MAP");
    public static LanguageString Sands_of_Karak_Map_Name => new("Wanderers", "MAPPER_ITEM_MAP_CORALCAVERNS_NAME");
    public static LanguageString Sands_of_Karak_Map_Desc => new("Wanderers", "MAPPER_ITEM_MAP_CORALCAVERNS_DESC");
    public static LanguageString Shellwood_Map_Name => new("Wanderers", "MAPPER_ITEM_MAP_SHELLWOOD_NAME");
    public static LanguageString Shellwood_Map_Desc => new("Wanderers", "MAPPER_ITEM_MAP_SHELLWOOD_DESC");
    public static LanguageString Sinner_s_Road_Map_Name => new("Wanderers", "MAPPER_ITEM_MAP_DUSTPENS_NAME");
    public static LanguageString Sinner_s_Road_Map_Desc => new("Wanderers", "MAPPER_ITEM_MAP_DUSTPENS_DESC");
    public static LanguageString The_Abyss_Map_Name => new("Inspect", "ABYSS_MAP_GET_MSG");
    public static LanguageString The_Abyss_Map_Desc => new("Inspect", "ABYSS_HARP_MAP");
    public static LanguageString The_Marrow_Map_Name => new("Wanderers", "MAPPER_ITEM_MAP_BONEFOREST_NAME");
    public static LanguageString The_Marrow_Map_Desc => new("Wanderers", "MAPPER_ITEM_MAP_BONEFOREST_DESC");
    public static LanguageString The_Slab_Map_Name => new("Inspect", "SLAB_MAP_GET_MSG");
    public static LanguageString The_Slab_Map_Desc => new("Inspect", "SLAB_MAP_PROMPT");
    public static LanguageString Underworks_Map_Name => new("Inspect", "UNDERSTORE_MAP_GET_MSG");
    public static LanguageString Underworks_Map_Desc => new("Inspect", "UNDERSTORE_MAP");
    public static LanguageString Weavenest_Alta_Map_Name => new("Inspect", "WEAVE_MAP_GET_MSG");
    public static LanguageString Weavenest_Alta_Map_Desc => new("Inspect", "WEAVE_HARP_MAP");
    public static LanguageString Wormways_Map_Name => new("Wanderers", "MAPPER_ITEM_MAP_CRAWL_NAME");
    public static LanguageString Wormways_Map_Desc => new("Wanderers", "MAPPER_ITEM_MAP_CRAWL_DESC");

    //other maps (citadel map machines, misc)
    //TODO: find language strings that have unique descriptions for each of the citadel machine maps
        //current citadel machine map name and description pair is <citadel area> : A machine displaying a model of the Citadel.
    //TODO: find/create descriptions for verdania and cogwork core
    public static LanguageString Choral_Chambers_Map_Name => new("Inspect", "HALLS_MAP_GET_MSG");
    public static LanguageString Choral_Chambers_Map_Desc => new("Inspect", "CITADEL_MAP_PROMPT");
    public static LanguageString Cogwork_Core_Map_Name => new("Inspect", "COG_MAP_GET_MSG");
    public static LanguageString Cogwork_Core_Map_Desc => new("UI", "UI_BLANK");//no description
    public static LanguageString Cradle_Map_Name => new("Inspect", "CRADLE_MAP_GET_MSG");
    public static LanguageString Cradle_Map_Desc => new("Inspect", "CITADEL_MAP_PROMPT");
    public static LanguageString Grand_Gate_Map_Name => new("Inspect", "SONGGATE_MAP_GET_MSG");
    public static LanguageString Grand_Gate_Map_Desc => new("Inspect", "CITADEL_MAP_PROMPT");
    public static LanguageString High_Halls_Map_Name => new("Inspect", "HANG_MAP_GET_MSG");
    public static LanguageString High_Halls_Map_Desc => new("Inspect", "CITADEL_MAP_PROMPT");
    public static LanguageString Memorium_Map_Name => new("Inspect", "ARBORIUM_MAP_GET_MSG");
    public static LanguageString Memorium_Map_Desc => new("Inspect", "CITADEL_MAP_PROMPT");
    public static LanguageString Verdania_Map_Name => new("Inspect", "CLOVER_MAP_GET_MSG");
    public static LanguageString Verdania_Map_Desc => new("UI", "UI_BLANK");//no description
    public static LanguageString Whispering_Vaults_Map_Name => new("Inspect", "LIBRARY_MAP_GET_MSG");
    public static LanguageString Whispering_Vaults_Map_Desc => new("Inspect", "CITADEL_MAP_PROMPT");
    public static LanguageString Whiteward_Map_Name => new("Inspect", "WARD_MAP_GET_MSG");
    public static LanguageString Whiteward_Map_Desc => new("Inspect", "CITADEL_MAP_PROMPT");

    //map markers
    public static LanguageString Bronze_Marker_Name => new("Wanderers", "MAPPER_ITEM_MARKER_E_NAME");
    public static LanguageString Bronze_Marker_Desc => new("Wanderers", "MAPPER_ITEM_MARKER_E_DESC");
    public static LanguageString Dark_Marker_Name => new("Wanderers", "MAPPER_ITEM_MARKER_D_NAME");
    public static LanguageString Dark_Marker_Desc => new("Wanderers", "MAPPER_ITEM_MARKER_D_DESC");
    public static LanguageString Hunt_Marker_Name => new("Wanderers", "MAPPER_ITEM_MARKER_C_NAME");
    public static LanguageString Hunt_Marker_Desc => new("Wanderers", "MAPPER_ITEM_MARKER_C_DESC");
    public static LanguageString Ring_Marker_Name => new("Wanderers", "MAPPER_ITEM_MARKER_B_NAME");
    public static LanguageString Ring_Marker_Desc => new("Wanderers", "MAPPER_ITEM_MARKER_B_DESC");
    public static LanguageString Shell_Marker_Name => new("Wanderers", "MAPPER_ITEM_MARKER_A_NAME");
    public static LanguageString Shell_Marker_Desc => new("Wanderers", "MAPPER_ITEM_MARKER_A_DESC");

    //map pins
    public static LanguageString Bench_Pins_Name => new("Wanderers", "MAPPER_ITEM_PIN_BENCH_NAME");
    public static LanguageString Bench_Pins_Desc => new("Wanderers", "MAPPER_ITEM_PIN_BENCH_DESC");
    public static LanguageString Bellway_Pins_Name => new("Wanderers", "MAPPER_ITEM_PIN_BELLWAY_NAME");
    public static LanguageString Bellway_Pins_Desc => new("Wanderers", "MAPPER_ITEM_PIN_BELLWAY_DESC");
    public static LanguageString Vendor_Pins_Name => new("Wanderers", "MAPPER_ITEM_PIN_SHOP_NAME");
    public static LanguageString Vendor_Pins_Desc => new("Wanderers", "MAPPER_ITEM_PIN_SHOP_DESC");
    public static LanguageString Ventrica_Pins_Name => new("Wanderers", "MAPPER_ITEM_TUBE_PIN_NAME");
    public static LanguageString Ventrica_Pins_Desc => new("Wanderers", "MAPPER_ITEM_TUBE_PIN_DESC");

    //bellways
    //TODO: find/create descriptions
    public static LanguageString Bellway__Bone_Bottom_Name => new("Fast Travel", "STATION_NAME_BONETOWN");
    public static LanguageString Bellway__Bone_Bottom_Desc => new("UI", "UI_BLANK");//no description
    public static LanguageString Bellway__The_Marrow_Name => new("Fast Travel", "STATION_NAME_BONE");
    public static LanguageString Bellway__The_Marrow_Desc => new("UI", "UI_BLANK");//no description
    public static LanguageString Bellway__Deep_Docks_Name => new("Fast Travel", "STATION_NAME_DOCKS");
    public static LanguageString Bellway__Deep_Docks_Desc => new("UI", "UI_BLANK");//no description
    public static LanguageString Bellway__Far_Fields_Name => new("Fast Travel", "STATION_NAME_BONEFOREST_EAST");
    public static LanguageString Bellway__Far_Fields_Desc => new("UI", "UI_BLANK");//no description
    public static LanguageString Bellway__Greymoor_Name => new("Fast Travel", "STATION_NAME_GREYMOOR");
    public static LanguageString Bellway__Greymoor_Desc => new("UI", "UI_BLANK");//no description
    public static LanguageString Bellway__Bellhart_Name => new("Fast Travel", "STATION_NAME_BELLTOWN");
    public static LanguageString Bellway__Bellhart_Desc => new("UI", "UI_BLANK");//no description
    public static LanguageString Bellway__Shellwood_Name => new("Fast Travel", "STATION_NAME_SHELLWOOD");
    public static LanguageString Bellway__Shellwood_Desc => new("UI", "UI_BLANK");//no description
    public static LanguageString Bellway__Blasted_Steps_Name => new("Fast Travel", "STATION_NAME_CORAL_TOWER");
    public static LanguageString Bellway__Blasted_Steps_Desc => new("UI", "UI_BLANK");//no description
    public static LanguageString Bellway__The_Slab_Name => new("Fast Travel", "STATION_NAME_PEAK");
    public static LanguageString Bellway__The_Slab_Desc => new("UI", "UI_BLANK");//no description
    public static LanguageString Bellway__Grand_Bellway_Name => new("Fast Travel", "STATION_NAME_CITADEL");
    public static LanguageString Bellway__Grand_Bellway_Desc => new("UI", "UI_BLANK");//no description
    public static LanguageString Bellway__Bilewater_Name => new("Fast Travel", "STATION_NAME_SHADOW");
    public static LanguageString Bellway__Bilewater_Desc => new("UI", "UI_BLANK");//no description
    public static LanguageString Bellway__Putrified_Ducts_Name => new("Fast Travel", "STATION_NAME_AQUEDUCT");
    public static LanguageString Bellway__Putrified_Ducts_Desc => new("UI", "UI_BLANK");//no description

    //ventricas
    //TODO: find/create descriptions
    public static LanguageString Ventrica__Terminus_Name => new("Fast Travel", "TUBE_NAME_HUB");
    public static LanguageString Ventrica__Terminus_Desc => new("UI", "UI_BLANK");//no description
    public static LanguageString Ventrica__Memorium_Name => new("Fast Travel", "TUBE_NAME_ARBORIUM");
    public static LanguageString Ventrica__Memorium_Desc => new("UI", "UI_BLANK");//no description
    public static LanguageString Ventrica__High_Halls_Name => new("Fast Travel", "TUBE_NAME_HANG");
    public static LanguageString Ventrica__High_Halls_Desc => new("UI", "UI_BLANK");//no description
    public static LanguageString Ventrica__First_Shrine_Name => new("Fast Travel", "TUBE_NAME_ENCLAVE");
    public static LanguageString Ventrica__First_Shrine_Desc => new("UI", "UI_BLANK");//no description
    public static LanguageString Ventrica__Choral_Chambers_Name => new("Fast Travel", "TUBE_NAME_SONG");
    public static LanguageString Ventrica__Choral_Chambers_Desc => new("UI", "UI_BLANK");//no description
    public static LanguageString Ventrica__Grand_Bellway_Name => new("Fast Travel", "TUBE_NAME_CITYBELLWAY");
    public static LanguageString Ventrica__Grand_Bellway_Desc => new("UI", "UI_BLANK");//no description
    public static LanguageString Ventrica__Underworks_Name => new("Fast Travel", "TUBE_NAME_UNDER");
    public static LanguageString Ventrica__Underworks_Desc => new("UI", "UI_BLANK");//no description

    //flea findings
    //TODO: find better fitting descriptions and titles
        //name and description pair is currently Flea Findings : <area name>
    public static LanguageString Flea_Findings__Bilelands_Name => new("Shop", "THUNTER_ITEMS_TITLE");
    public static LanguageString Flea_Findings__Bilelands_Desc => new("Shop", "THUNTER_ITEM_MUCKLANDS");
    public static LanguageString Flea_Findings__Blasted_Steps_Name => new("Shop", "THUNTER_ITEMS_TITLE");
    public static LanguageString Flea_Findings__Blasted_Steps_Desc => new("Shop", "THUNTER_ITEM_BLASTEDLANDS");
    public static LanguageString Flea_Findings__Bonelands_Name => new("Shop", "THUNTER_ITEMS_TITLE");
    public static LanguageString Flea_Findings__Bonelands_Desc => new("Shop", "THUNTER_ITEM_MARROWLANDS");
    public static LanguageString Flea_Findings__Midlands_Name => new("Shop", "THUNTER_ITEMS_TITLE");
    public static LanguageString Flea_Findings__Midlands_Desc => new("Shop", "THUNTER_ITEM_MIDLANDS");
    public static LanguageString Flea_Findings__Mount_Fay_Name => new("Shop", "THUNTER_ITEMS_TITLE");
    public static LanguageString Flea_Findings__Mount_Fay_Desc => new("Shop", "THUNTER_ITEM_PEAKLANDS");
    public static LanguageString Flea_Findings__The_Citadel_Name => new("Shop", "THUNTER_ITEMS_TITLE");
    public static LanguageString Flea_Findings__The_Citadel_Desc => new("Shop", "THUNTER_ITEM_CITADEL");

}
