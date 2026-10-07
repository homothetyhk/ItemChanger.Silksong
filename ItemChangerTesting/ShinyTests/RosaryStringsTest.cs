using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.ShinyTests;

internal class RosaryStringsTest : AbstractShinyPlacementTest<RosaryStringsTest>
{
    protected override string MenuName => "Rosary Strings";

    protected override string MenuDescription => "Test all rosary string (item) locations.";

    protected override int Revision => 2026100500;

    protected override IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> LocationTests =>
    [
        (new CoordinateStartDef { SceneName = SceneNames.Shadow_02, X = 45.5f, Y = 163, RespawnFacingRight = true }, [LocationNames.Frayed_Rosary__Bilewater]),
        (new CoordinateStartDef { SceneName = SceneNames.Coral_03, X = 36.5f, Y = 9.5f, RespawnFacingRight = true }, [LocationNames.Frayed_Rosary__Blasted_Steps]),
        (new CoordinateStartDef { SceneName = SceneNames.Bone_East_04b, X = 3.5f, Y = 84, RespawnFacingRight = true }, [LocationNames.Frayed_Rosary__Deep_Docks]),
        (new CoordinateStartDef { SceneName = SceneNames.Wisp_03, X = 59, Y = 37, RespawnFacingRight = true }, [LocationNames.Frayed_Rosary__Greymoor_Above_Yarnaby]),
        (new CoordinateStartDef { SceneName = SceneNames.Greymoor_15, X = 59, Y = 26, RespawnFacingRight = true }, [LocationNames.Frayed_Rosary__Greymoor_Craw_Lake_Ledge]),
        (new CoordinateStartDef { SceneName = SceneNames.Greymoor_15b, X = 98, Y = 36, RespawnFacingRight = false }, [LocationNames.Frayed_Rosary__Greymoor_West_Craw_Lake]),
        (new CoordinateStartDef { SceneName = SceneNames.Hang_16, X = 77, Y = 4, RespawnFacingRight = false }, [LocationNames.Frayed_Rosary__High_Halls]),
        (new CoordinateStartDef { SceneName = SceneNames.Tut_01, X = 45.5f, Y = 22, RespawnFacingRight = false }, [LocationNames.Frayed_Rosary__Moss_Grotto_Above_Start]),
        (new CoordinateStartDef { SceneName = SceneNames.Mosstown_02, X = 29, Y = 42, RespawnFacingRight = false }, [LocationNames.Frayed_Rosary__Mosshome]),
        (new CoordinateStartDef { SceneName = SceneNames.Aqueduct_01, X = 250, Y = 16, RespawnFacingRight = true }, [LocationNames.Frayed_Rosary__Putrified_Ducts]),
        (new CoordinateStartDef { SceneName = SceneNames.Shellwood_01, X = 51, Y = 25, RespawnFacingRight = false }, [LocationNames.Frayed_Rosary__Shellwood]),
        (new CoordinateStartDef { SceneName = SceneNames.Dust_01, X = 82.5f, Y = 4, RespawnFacingRight = false }, [LocationNames.Frayed_Rosary__Sinners_Road]),
        (new CoordinateStartDef { SceneName = SceneNames.Bone_10, X = 95.5f, Y = 46, RespawnFacingRight = true }, [LocationNames.Frayed_Rosary__The_Marrow]),
        (new CoordinateStartDef { SceneName = SceneNames.Slab_18, X = 20, Y = 23, RespawnFacingRight = false }, [LocationNames.Frayed_Rosary__The_Slab_East]),
        (new CoordinateStartDef { SceneName = SceneNames.Slab_22, X = 33, Y = 32, RespawnFacingRight = false }, [LocationNames.Frayed_Rosary__The_Slab_West]),
        (new CoordinateStartDef { SceneName = SceneNames.Under_07c, X = 77, Y = 77, RespawnFacingRight = false }, [LocationNames.Frayed_Rosary__Underworks_Choral_Exit]),
        (new CoordinateStartDef { SceneName = SceneNames.Under_12, X = 25, Y = 12, RespawnFacingRight = false }, [LocationNames.Frayed_Rosary__Underworks_Ventrica]),
        (new CoordinateStartDef { SceneName = SceneNames.Crawl_02, X = 4, Y = 144, RespawnFacingRight = false }, [LocationNames.Frayed_Rosary__Wormways]),
    ];
}
