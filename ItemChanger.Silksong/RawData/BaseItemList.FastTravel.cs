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
    private static IValueProvider<Sprite> BellwaySprite() => new AtlasSprite()
    {
        BundleName = "atlases_assets_assets/sprites/_atlases/hornet_map.spriteatlas",
        SpriteName = "pin_stag_station"
    };

    private static IValueProvider<Sprite> VentricaSprite() => new AtlasSprite()
    {
        BundleName = "atlases_assets_assets/sprites/_atlases/hornet_map.spriteatlas",
        SpriteName = "pin_tube_station"
    };

    private static IValueProvider<string> GetBellwayName(string baseLanguageKey)
    {
        return new CompositeString()
        {
            Pattern = new LanguageString($"Mods.{ItemChangerPlugin.Id}", "FMT_FAST_TRAVEL_PATTERN"),
            Params = new()
            {
                ["TRAVEL_TYPE"] = new LanguageString("UI", "KEY_BELLWAY"),
                ["STATION_NAME"] = new LanguageString("Fast Travel", baseLanguageKey)
            }
        };
    }

    private static IValueProvider<string> GetVentricaName(string baseLanguageKey)
    {
        return new CompositeString()
        {
            Pattern = new LanguageString($"Mods.{ItemChangerPlugin.Id}", "FMT_FAST_TRAVEL_PATTERN"),
            Params = new()
            {
                ["TRAVEL_TYPE"] = new LanguageString("UI", "KEY_TUBE"),
                ["STATION_NAME"] = new LanguageString("Fast Travel", baseLanguageKey)
            }
        };
    }

    // TODO - add shop descs

    private static UIDef GetBellwayUIDef(string baseLanguageKey)
    {
        return new MsgUIDef() { Name = GetBellwayName(baseLanguageKey), Sprite = BellwaySprite() };
    }

    private static UIDef GetVentricaUIDef(string baseLanguageKey)
    {
        return new MsgUIDef() { Name = GetVentricaName(baseLanguageKey), Sprite = VentricaSprite() };
    }



    // Bellway

    public static Item Bellway__Bone_Bottom => new PDBoolItem()
    {
        Name = ItemNames.Bellway__Bone_Bottom,
        BoolName = CustomFastTravelLocations.GetBoolStringForLocation(FastTravelLocations.Bonetown),
        UIDef = GetBellwayUIDef("STATION_NAME_BONETOWN")
    };

    public static Item Bellway__The_Marrow => new PDBoolItem()
    {
        Name = ItemNames.Bellway__The_Marrow,
        BoolName = CustomFastTravelLocations.GetBoolStringForLocation(FastTravelLocations.Bone),
        UIDef = GetBellwayUIDef("STATION_NAME_BONE")
    };

    public static Item Bellway__Bilewater => new PDBoolItem()
    {
        Name = ItemNames.Bellway__Bilewater,
        BoolName = nameof(PlayerData.UnlockedShadowStation),
        UIDef = GetBellwayUIDef("STATION_NAME_SHADOW")
    };

    // Ventrica

    public static Item Ventrica__Terminus => new PDBoolItem()
    {
        Name = ItemNames.Ventrica__Terminus,
        BoolName = CustomFastTravelLocations.GetBoolStringForLocation(TubeTravelLocations.Hub),
        UIDef = GetVentricaUIDef("TUBE_NAME_HUB")
    };

    public static Item Ventrica__High_Halls => new PDBoolItem()
    {
        Name = ItemNames.Ventrica__High_Halls,
        BoolName = nameof(PlayerData.UnlockedHangTube),
        UIDef = GetVentricaUIDef("TUBE_NAME_HANG"),
    };

    // TODO - others (can be done as part of a refactored PR #37)
}
