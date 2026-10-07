using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.ShinyTests;

internal class RelicsTest : AbstractShinyPlacementTest<RelicsTest>
{
    protected override string MenuName => "Relics";

    protected override string MenuDescription => "Test all shiny relic locations.";

    protected override int Revision => 2026100600;

    protected override void OnEnterGame()
    {
        BaseItemList.Arcane_Egg.GiveImmediate(new());
        BaseItemList.Bone_Scroll__Burning_Bug.GiveImmediate(new());
        BaseItemList.Bone_Scroll__Lost_Pilgrim.GiveImmediate(new());
        new SDBool(SceneNames.Bone_East_14, "explode_wall (1)").Value = true;
        BaseItemList.Bone_Scroll__Singed_Pilgrim.GiveImmediate(new());
        BaseItemList.Bone_Scroll__Underworker.GiveImmediate(new());
        new SDBool(SceneNames.Ward_05, "Breakable Wall").Value = true;
        BaseItemList.Choral_Commandment__Light.GiveImmediate(new());
        BaseItemList.Choral_Commandment__White_Wyrm.GiveImmediate(new());
        BaseItemList.Psalm_Cylinder__Ascendence_Theme.GiveImmediate(new());
        BaseItemList.Psalm_Cylinder__Choir_Voices.GiveImmediate(new());
        BaseItemList.Psalm_Cylinder__Sermon.GiveImmediate(new());
        BaseItemList.Psalm_Cylinder__Surgery.GiveImmediate(new());
        BaseItemList.Rune_Harp__Escape.GiveImmediate(new());
        BaseItemList.Rune_Harp__Eva.GiveImmediate(new());
        BaseItemList.Sacred_Cylinder.GiveImmediate(new());
        BaseItemList.Weaver_Effigy__Atla.GiveImmediate(new());
        BaseItemList.Weaver_Effigy__Camora.GiveImmediate(new());
        BaseItemList.Weaver_Effigy__Keelal.GiveImmediate(new());
    }

    protected override IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> LocationTests =>
    [
        (new CoordinateStartDef { SceneName = SceneNames.Abyss_04, X = 93.5f, Y = 51, RespawnFacingRight = true }, [LocationNames.Arcane_Egg]),
        (new CoordinateStartDef { SceneName = SceneNames.Wisp_08, X = 13, Y = 116.5f, RespawnFacingRight = false }, [LocationNames.Bone_Scroll__Burning_Bug]),
        (new CoordinateStartDef { SceneName = SceneNames.Greymoor_21, X = 68.5f, Y = 10, RespawnFacingRight = true }, [LocationNames.Bone_Scroll__Lost_Pilgrim]),
        (new CoordinateStartDef { SceneName = SceneNames.Bone_East_14, X = 46.5f, Y = 42, RespawnFacingRight = false }, [LocationNames.Bone_Scroll__Singed_Pilgrim]),
        (new CoordinateStartDef { SceneName = SceneNames.Under_16, X = 63.5f, Y = 17, RespawnFacingRight = true }, [LocationNames.Bone_Scroll__Underworker]),
        (new CoordinateStartDef { SceneName = SceneNames.Ward_05, X = 128.5f, Y = 6, RespawnFacingRight = true }, [LocationNames.Choral_Commandment__Light]),
        (new CoordinateStartDef { SceneName = SceneNames.Aspid_01, X = 68.5f, Y = 67, RespawnFacingRight = false }, [LocationNames.Choral_Commandment__White_Wyrm]),
        (new CoordinateStartDef { SceneName = SceneNames.Hang_10, X = 72, Y = 17, RespawnFacingRight = true }, [LocationNames.Psalm_Cylinder__Ascendence_Theme]),
        (new CoordinateStartDef { SceneName = SceneNames.Library_08, X = 90, Y = 32, RespawnFacingRight = true }, [LocationNames.Psalm_Cylinder__Choir_Voices]),
        (new CoordinateStartDef { SceneName = SceneNames.Library_09, X = 29.5f, Y = 13, RespawnFacingRight = false }, [LocationNames.Psalm_Cylinder__Sermon]),
        (new CoordinateStartDef { SceneName = SceneNames.Under_08, X = 71.5f, Y = 48, RespawnFacingRight = false }, [LocationNames.Psalm_Cylinder__Surgery]),
        (new CoordinateStartDef { SceneName = SceneNames.Bone_East_Weavehome, X = 55.5f, Y = 65, RespawnFacingRight = false }, [LocationNames.Rune_Harp__Escape]),
        (new CoordinateStartDef { SceneName = SceneNames.Weave_08, X = 46.5f, Y = 54.5f, RespawnFacingRight = false }, [LocationNames.Rune_Harp__Eva]),
        (new CoordinateStartDef { SceneName = SceneNames.Library_10, X = 18, Y = 4, RespawnFacingRight = false }, [LocationNames.Sacred_Cylinder]),
        (new CoordinateStartDef { SceneName = SceneNames.Slab_12, X = 94.5f, Y = 29, RespawnFacingRight = true }, [LocationNames.Weaver_Effigy__Atla]),
        (new CoordinateStartDef { SceneName = SceneNames.Bonetown, X = 138.5f, Y = 63, RespawnFacingRight = false }, [LocationNames.Weaver_Effigy__Camora]),
        (new CoordinateStartDef { SceneName = SceneNames.Shellwood_25, X = 279, Y = 28, RespawnFacingRight = true }, [LocationNames.Weaver_Effigy__Keelal])
    ];
}
