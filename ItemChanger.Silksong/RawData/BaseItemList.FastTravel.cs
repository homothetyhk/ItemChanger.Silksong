using GlobalEnums;
using ItemChanger.Items;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Items;
using ItemChanger.Silksong.Modules.FastTravel;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.Tags;
using ItemChanger.Silksong.UIDefs;
using UnityEngine;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseItemList
{
    private static IValueProvider<Sprite> BellwaySprite => new AtlasSprite()
    {
        BundleName = "atlases_assets_assets/sprites/_atlases/hornet_map.spriteatlas",
        SpriteName = "pin_stag_station"
    };

    private static IValueProvider<Sprite> VentricaSprite = new AtlasSprite()
    {
        BundleName = "atlases_assets_assets/sprites/_atlases/hornet_map.spriteatlas",
        SpriteName = "pin_tube_station"
    };

    // Bellway

    public static Item Bellway__Bone_Bottom => new PDBoolItem()
    {
        Name = ItemNames.Bellway__Bone_Bottom,
        BoolName = CustomFastTravelLocations.GetBoolStringForLocation(FastTravelLocations.Bonetown),
        UIDef = new MsgUIDef()
        {
            // TODO - this might have to be something composite so that it says bonetown bellway or bonetown station
            // Could either be a CompositeString (sum of sub-IStrings) or LanguageString from i18n
            // Note that (UI, KEY_BELLWAY) gives "Bellway"
            Name = new LanguageString("Fast Travel", "STATION_NAME_BONETOWN"),
            // TODO - shopdesc
            Sprite = BellwaySprite
        },
        Tags = [
            new RequireModuleTag<CustomFastTravelLocationsModule<FastTravelLocations>>()
            ]
    };

    public static Item Bellway__The_Marrow => new PDBoolItem()
    {
        Name = ItemNames.Bellway__The_Marrow,
        BoolName = CustomFastTravelLocations.GetBoolStringForLocation(FastTravelLocations.Bone),
        UIDef = new MsgUIDef()
        {
            // TODO - this might have to be something composite
            Name = new LanguageString("Fast Travel", "STATION_NAME_BONE"),
            // TODO - shopdesc
            Sprite = BellwaySprite
        },
        Tags = [
            new RequireModuleTag<CustomFastTravelLocationsModule<FastTravelLocations>>()
            ]
    };

    public static Item Bellway__Bilewater => new PDBoolItem()
    {
        Name = ItemNames.Bellway__Bilewater,
        BoolName = nameof(PlayerData.UnlockedShadowStation),
        UIDef = new MsgUIDef()
        {
            // TODO - this might have to be something composite
            Name = new LanguageString("Fast Travel", "STATION_NAME_SHADOW"),
            // TODO - shopdesc
            Sprite = BellwaySprite
        }
    };

    // Ventrica

    public static Item Ventrica__Terminus => new PDBoolItem()
    {
        Name = ItemNames.Ventrica__Terminus,
        BoolName = CustomFastTravelLocations.GetBoolStringForLocation(TubeTravelLocations.Hub),
        UIDef = new MsgUIDef()
        {
            // TODO - this might have to be something composite
            Name = new LanguageString("Fast Travel", "TUBE_NAME_HUB"),
            // TODO - shopdesc
            Sprite = VentricaSprite
        },
        Tags = [
            new RequireModuleTag<CustomFastTravelLocationsModule<TubeTravelLocations>>()
            ]
    };

    public static Item Ventrica__High_Halls => new PDBoolItem()
    {
        Name = ItemNames.Ventrica__High_Halls,
        BoolName = nameof(PlayerData.UnlockedHangTube),
        UIDef = new MsgUIDef()
        {
            // TODO - this might have to be something composite
            Name = new LanguageString("Fast Travel", "TUBE_NAME_HANG"),
            // TODO - shopdesc
            Sprite = VentricaSprite
        }
    };

    // TODO - others (bellway/ventrica can be done as part of a refactored #37
}
