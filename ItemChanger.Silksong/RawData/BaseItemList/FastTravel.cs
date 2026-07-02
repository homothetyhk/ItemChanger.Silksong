using GlobalEnums;
using ItemChanger.Items;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Items;
using ItemChanger.Silksong.Modules.FastTravel;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.UIDefs;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseItemList
{
    // TODO - add shop descs

    private static UIDef GetBellwayUIDef(LanguageString station, bool format = true)
    {
        return new MsgUIDef()
        {
            Name = format ? CompositeString.Create(
                LanguageString.FromItemChanger("FMT_FAST_TRAVEL_PATTERN"),
                new Dictionary<string, IValueProvider<object>>
                {
                    { "TRAVEL_TYPE", BaseLanguageStrings.Bellway },
                    { "STATION_NAME", station }
                }) : station,
            ShopDesc = null!,
            Sprite = BaseAtlasSprites.Bellway, 
        };
    }

    private static UIDef GetVentricaUIDef(LanguageString station)
    {
        return new MsgUIDef()
        {
            Name = CompositeString.Create(
                LanguageString.FromItemChanger("FMT_FAST_TRAVEL_PATTERN"),
                new Dictionary<string, IValueProvider<object>>
                {
                    { "TRAVEL_TYPE", BaseLanguageStrings.Ventrica },
                    { "STATION_NAME", station } 
                }),
            ShopDesc = null!,
            Sprite = BaseAtlasSprites.Ventrica,
        };
    }

    // Bellways
    public static Item Bellway__Bone_Bottom => new PDBoolItem()
    {
        Name = ItemNames.Bellway__Bone_Bottom,
        BoolName = CustomFastTravelLocations.GetBoolStringForLocation(FastTravelLocations.Bonetown),
        UIDef = GetBellwayUIDef(BaseLanguageStrings.Fast_Travel__STATION_NAME_BONETOWN),
    };
    public static Item Bellway__The_Marrow => new PDBoolItem()
    {
        Name = ItemNames.Bellway__The_Marrow,
        BoolName = CustomFastTravelLocations.GetBoolStringForLocation(FastTravelLocations.Bone),
        UIDef = GetBellwayUIDef(BaseLanguageStrings.Fast_Travel__STATION_NAME_BONE),
    };
    public static Item Bellway__Deep_Docks => new PDBoolItem
    {
        Name = ItemNames.Bellway__Deep_Docks,
        BoolName = nameof(PlayerData.UnlockedDocksStation),
        UIDef = GetBellwayUIDef(BaseLanguageStrings.Fast_Travel__STATION_NAME_DOCKS),
    };
    public static Item Bellway__Far_Fields => new PDBoolItem
    {
        Name = ItemNames.Bellway__Far_Fields,
        BoolName = nameof(PlayerData.UnlockedBoneforestEastStation),
        UIDef = GetBellwayUIDef(BaseLanguageStrings.Fast_Travel__STATION_NAME_BONEFOREST_EAST),
    };
    public static Item Bellway__Greymoor => new PDBoolItem
    {
        Name = ItemNames.Bellway__Greymoor,
        BoolName = nameof(PlayerData.UnlockedGreymoorStation),
        UIDef = GetBellwayUIDef(BaseLanguageStrings.Fast_Travel__STATION_NAME_GREYMOOR),
    };
    public static Item Bellway__Bellhart => new PDBoolItem
    {
        Name = ItemNames.Bellway__Bellhart,
        BoolName = nameof(PlayerData.UnlockedBelltownStation),
        UIDef = GetBellwayUIDef(BaseLanguageStrings.Fast_Travel__STATION_NAME_BELLTOWN),
    };
    public static Item Bellway__Blasted_Steps => new PDBoolItem
    {
        Name = ItemNames.Bellway__Blasted_Steps,
        BoolName = nameof(PlayerData.UnlockedCoralTowerStation),
        UIDef = GetBellwayUIDef(BaseLanguageStrings.Fast_Travel__STATION_NAME_CORAL_TOWER),
    };
    public static Item Bellway__Grand_Bellway => new PDBoolItem
    {
        Name = ItemNames.Bellway__Grand_Bellway,
        BoolName = nameof(PlayerData.UnlockedCityStation),
        UIDef = GetBellwayUIDef(BaseLanguageStrings.Fast_Travel__STATION_NAME_CITADEL, format: false),
    };
    public static Item Bellway__The_Slab => new PDBoolItem
    {
        Name = ItemNames.Bellway__The_Slab,
        BoolName = nameof(PlayerData.UnlockedPeakStation),
        UIDef = GetBellwayUIDef(BaseLanguageStrings.Fast_Travel__STATION_NAME_PEAK),
    };
    public static Item Bellway__Shellwood => new PDBoolItem
    {
        Name = ItemNames.Bellway__Shellwood,
        BoolName = nameof(PlayerData.UnlockedShellwoodStation),
        UIDef = GetBellwayUIDef(BaseLanguageStrings.Fast_Travel__STATION_NAME_SHELLWOOD),
    };
    public static Item Bellway__Bilewater => new PDBoolItem()
    {
        Name = ItemNames.Bellway__Bilewater,
        BoolName = nameof(PlayerData.UnlockedShadowStation),
        UIDef = GetBellwayUIDef(BaseLanguageStrings.Fast_Travel__STATION_NAME_SHADOW),
    };
    public static Item Bellway__Putrified_Ducts => new PDBoolItem
    {
        Name = ItemNames.Bellway__Putrified_Ducts,
        BoolName = nameof(PlayerData.UnlockedAqueductStation),
        UIDef = GetBellwayUIDef(BaseLanguageStrings.Fast_Travel__STATION_NAME_AQUEDUCT),
    };

    // Ventrica
    public static Item Ventrica__Terminus => new PDBoolItem()
    {
        Name = ItemNames.Ventrica__Terminus,
        BoolName = CustomFastTravelLocations.GetBoolStringForLocation(TubeTravelLocations.Hub),
        UIDef = GetVentricaUIDef(BaseLanguageStrings.Fast_Travel__TUBE_NAME_HUB),
    };
    public static Item Ventrica__Memorium => new PDBoolItem
    {
        Name = ItemNames.Ventrica__Memorium,
        BoolName = nameof(PlayerData.UnlockedArboriumTube),
        UIDef = GetVentricaUIDef(BaseLanguageStrings.Fast_Travel__TUBE_NAME_ARBORIUM),
    };
    public static Item Ventrica__High_Halls => new PDBoolItem()
    {
        Name = ItemNames.Ventrica__High_Halls,
        BoolName = nameof(PlayerData.UnlockedHangTube),
        UIDef = GetVentricaUIDef(BaseLanguageStrings.Fast_Travel__TUBE_NAME_HANG),
    };
    public static Item Ventrica__First_Shrine => new PDBoolItem
    {
        Name = ItemNames.Ventrica__First_Shrine,
        BoolName = nameof(PlayerData.UnlockedEnclaveTube),
        UIDef = GetVentricaUIDef(BaseLanguageStrings.Fast_Travel__TUBE_NAME_ENCLAVE),
    };
    public static Item Ventrica__Choral_Chambers => new PDBoolItem
    {
        Name = ItemNames.Ventrica__Choral_Chambers,
        BoolName = nameof(PlayerData.UnlockedSongTube),
        UIDef = GetVentricaUIDef(BaseLanguageStrings.Fast_Travel__TUBE_NAME_SONG),
    };
    public static Item Ventrica__Grand_Bellway => new PDBoolItem
    {
        Name = ItemNames.Ventrica__Grand_Bellway,
        BoolName = nameof(PlayerData.UnlockedCityBellwayTube),
        UIDef = GetVentricaUIDef(BaseLanguageStrings.Fast_Travel__TUBE_NAME_CITYBELLWAY),
    };
    public static Item Ventrica__Underworks => new PDBoolItem
    {
        Name = ItemNames.Ventrica__Underworks,
        BoolName = nameof(PlayerData.UnlockedUnderTube),
        UIDef = GetVentricaUIDef(BaseLanguageStrings.Fast_Travel__TUBE_NAME_UNDER),
    };
}
