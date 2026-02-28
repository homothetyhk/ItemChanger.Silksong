using ItemChanger.Items;
using ItemChanger.Silksong.Items;
using ItemChanger.Silksong.Serialization;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseItemList
{
    // maps
    public static Item Bellhart_Map => new PDBoolItem { Name = ItemNames.Bellhart_Map, BoolName = nameof(PlayerData.HasBellhartMap), UIDef = null! };
    public static Item Bilewater_Map => new PDBoolItem { Name = ItemNames.Bilewater_Map, BoolName = nameof(PlayerData.HasSwampMap), UIDef = null! };
    public static Item Blasted_Steps_Map => new PDBoolItem { Name = ItemNames.Blasted_Steps_Map, BoolName = nameof(PlayerData.HasJudgeStepsMap), UIDef = null! };
    public static Item Choral_Chambers_Map => new PDBoolItem { Name = ItemNames.Choral_Chambers_Map, BoolName = nameof(PlayerData.HasHallsMap), UIDef = null! };
    public static Item Cogwork_Core_Map => new PDBoolItem { Name = ItemNames.Cogwork_Core_Map, BoolName = nameof(PlayerData.HasCogMap), UIDef = null! };
    public static Item Cradle_Map => new PDBoolItem { Name = ItemNames.Cradle_Map, BoolName = nameof(PlayerData.HasCradleMap), UIDef = null! };
    public static Item Deep_Docks_Map => new PDBoolItem { Name = ItemNames.Deep_Docks_Map, BoolName = nameof(PlayerData.HasDocksMap), UIDef = null! };
    public static Item Far_Fields_Map => new PDBoolItem { Name = ItemNames.Far_Fields_Map, BoolName = nameof(PlayerData.HasWildsMap), UIDef = null! };
    public static Item Grand_Gate_Map => new PDBoolItem { Name = ItemNames.Grand_Gate_Map, BoolName = nameof(PlayerData.HasSongGateMap), UIDef = null! };
    public static Item Greymoor_Map => new PDBoolItem { Name = ItemNames.Greymoor_Map, BoolName = nameof(PlayerData.HasGreymoorMap), UIDef = null! };
    public static Item High_Halls_Map => new PDBoolItem { Name = ItemNames.High_Halls_Map, BoolName = nameof(PlayerData.HasHangMap), UIDef = null! };
    public static Item Hunter_s_March_Map => new PDBoolItem { Name = ItemNames.Hunter_s_March_Map, BoolName = nameof(PlayerData.HasHuntersNestMap), UIDef = null! };
    public static Item Memorium_Map => new PDBoolItem { Name = ItemNames.Memorium_Map, BoolName = nameof(PlayerData.HasArboriumMap), UIDef = null! };
    public static Item Mosslands_Map => new PDBoolItem { Name = ItemNames.Mosslands_Map, BoolName = nameof(PlayerData.HasMossGrottoMap), UIDef = null! };
    public static Item Mount_Fay_Map => new PDBoolItem { Name = ItemNames.Mount_Fay_Map, BoolName = nameof(PlayerData.HasPeakMap), UIDef = null! };
    public static Item Putrified_Ducts_Map => new PDBoolItem { Name = ItemNames.Putrified_Ducts_Map, BoolName = nameof(PlayerData.HasAqueductMap), UIDef = null! };
    public static Item Sands_of_Karak_Map => new PDBoolItem { Name = ItemNames.Sands_of_Karak_Map, BoolName = nameof(PlayerData.HasCoralMap), UIDef = null! };
    public static Item Shellwood_Map => new PDBoolItem { Name = ItemNames.Shellwood_Map, BoolName = nameof(PlayerData.HasShellwoodMap), UIDef = null! };
    public static Item Sinner_s_Road_Map => new PDBoolItem { Name = ItemNames.Sinner_s_Road_Map, BoolName = nameof(PlayerData.HasDustpensMap), UIDef = null! };
    public static Item The_Abyss_Map => new PDBoolItem { Name = ItemNames.The_Abyss_Map, BoolName = nameof(PlayerData.HasAbyssMap), UIDef = null! };
    public static Item The_Marrow_Map => new PDBoolItem { Name = ItemNames.The_Marrow_Map, BoolName = nameof(PlayerData.HasBoneforestMap), UIDef = null! };
    public static Item The_Slab_Map => new PDBoolItem { Name = ItemNames.The_Slab_Map, BoolName = nameof(PlayerData.HasSlabMap), UIDef = null! };
    public static Item Underworks_Map => new PDBoolItem { Name = ItemNames.Underworks_Map, BoolName = nameof(PlayerData.HasCitadelUnderstoreMap), UIDef = null! };
    public static Item Verdania_Map => new PDBoolItem { Name = ItemNames.Verdania_Map, BoolName = nameof(PlayerData.HasCloverMap), UIDef = null! };
    public static Item Weavenest_Alta_Map => new PDBoolItem { Name = ItemNames.Weavenest_Alta_Map, BoolName = nameof(PlayerData.HasWeavehomeMap), UIDef = null! };
    public static Item Whispering_Vaults_Map => new PDBoolItem { Name = ItemNames.Whispering_Vaults_Map, BoolName = nameof(PlayerData.HasLibraryMap), UIDef = null! };
    public static Item Whiteward_Map => new PDBoolItem { Name = ItemNames.Whiteward_Map, BoolName = nameof(PlayerData.HasWardMap), UIDef = null! };
    public static Item Wormways_Map => new PDBoolItem { Name = ItemNames.Wormways_Map, BoolName = nameof(PlayerData.HasCrawlMap), UIDef = null! };

    // quills
    public static Item Quill__White => new QuillItem { Name = ItemNames.Quill__White, QuillState = 1, UIDef = null! };
    public static Item Quill__Red => new QuillItem { Name = ItemNames.Quill__Red, QuillState = 2, UIDef = null! };
    public static Item Quill__Purple => new QuillItem { Name = ItemNames.Quill__Purple, QuillState = 3, UIDef = null! };

    // map markers
    public static Item Shell_Marker => new MarkerItem { Name = ItemNames.Shell_Marker, BoolName = nameof(PlayerData.hasMarker_a), UIDef = null! };
    public static Item Ring_Marker => new MarkerItem { Name = ItemNames.Ring_Marker, BoolName = nameof(PlayerData.hasMarker_b), UIDef = null! };
    public static Item Hunt_Marker => new MarkerItem { Name = ItemNames.Hunt_Marker, BoolName = nameof(PlayerData.hasMarker_c), UIDef = null! };
    public static Item Dark_Marker => new MarkerItem { Name = ItemNames.Dark_Marker, BoolName = nameof(PlayerData.hasMarker_d), UIDef = null! };
    public static Item Bronze_Marker => new MarkerItem { Name = ItemNames.Bronze_Marker, BoolName = nameof(PlayerData.hasMarker_e), UIDef = null! };

    // map pins
    public static Item Bench_Pins => new PDBoolItem { Name = ItemNames.Bench_Pins, BoolName = nameof(PlayerData.hasPinBench), UIDef = null! };
    public static Item Bellway_Pins => new PDBoolItem { Name = ItemNames.Bellway_Pins, BoolName = nameof(PlayerData.hasPinStag), UIDef = null! };
    public static Item Vendor_Pins => new PDBoolItem { Name = ItemNames.Vendor_Pins, BoolName = nameof(PlayerData.hasPinShop), UIDef = null! };
    public static Item Ventrica_Pins => new PDBoolItem { Name = ItemNames.Ventrica_Pins, BoolName = nameof(PlayerData.hasPinTube), UIDef = null! };

    // flea findings
    public static Item Flea_Findings__Bonelands => new PDBoolItem { Name = ItemNames.Flea_Findings__Bonelands, BoolName = nameof(PlayerData.hasPinFleaMarrowlands), UIDef = null! };
    public static Item Flea_Findings__Midlands => new PDBoolItem { Name = ItemNames.Flea_Findings__Midlands, BoolName = nameof(PlayerData.hasPinFleaMidlands), UIDef = null! };
    public static Item Flea_Findings__Blasted_Steps => new PDBoolItem { Name = ItemNames.Flea_Findings__Blasted_Steps, BoolName = nameof(PlayerData.hasPinFleaBlastedlands), UIDef = null! };
    public static Item Flea_Findings__The_Citadel => new PDBoolItem { Name = ItemNames.Flea_Findings__The_Citadel, BoolName = nameof(PlayerData.hasPinFleaCitadel), UIDef = null! };
    public static Item Flea_Findings__Mount_Fay => new PDBoolItem { Name = ItemNames.Flea_Findings__Mount_Fay, BoolName = nameof(PlayerData.hasPinFleaPeaklands), UIDef = null! };
    public static Item Flea_Findings__Bilelands => new PDBoolItem { Name = ItemNames.Flea_Findings__Bilelands, BoolName = nameof(PlayerData.hasPinFleaMucklands), UIDef = null! };
}
