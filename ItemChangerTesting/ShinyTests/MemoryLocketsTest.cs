using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.StartDefs;
using PrepatcherPlugin;

namespace ItemChangerTesting.ShinyTests;

internal class MemoryLocketsTest : AbstractShinyPlacementTest<MemoryLocketsTest>
{
    protected override string MenuName => "Memory Lockets";

    protected override string MenuDescription => "Test all shiny Memory Locket locations.";

    protected override int Revision => 2026100600;

    protected override void OnEnterGame()
    {
        PlayerDataAccess.SeenBelltownCutscene = true;
        PlayerDataAccess.spinnerDefeated = true;  // Just to prevent the Webbed Hero trap.

        new SDBool(SceneNames.Bellway_City, "GG_Breakable_Junk_Pile_red (1)").Value = true;
        new SDBool(SceneNames.Slab_Cell_Quiet, "Breakable Wall").Value = true;
        for (int i = 0; i < 20; i++)
            BaseItemList.Memory_Locket.GiveImmediate(new());
    }

    protected override IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> LocationTests =>
    [
        (new CoordinateStartDef { SceneName = SceneNames.Belltown, X = 63.5f, Y = 68, RespawnFacingRight = false }, [LocationNames.Memory_Locket__Bellhart_BellhomeCiel]),
        (new CoordinateStartDef { SceneName = SceneNames.Shadow_20, X = 15, Y = 25, RespawnFacingRight = true }, [LocationNames.Memory_Locket__Bilewater_Hidden_Room_West_Bench]),
        (new CoordinateStartDef { SceneName = SceneNames.Coral_02, X = 201, Y = 45.5f, RespawnFacingRight = true }, [LocationNames.Memory_Locket__Blasted_Steps]),
        (new CoordinateStartDef { SceneName = SceneNames.Bellway_City, X = 70.5f, Y = 26, RespawnFacingRight = false }, [LocationNames.Memory_Locket__Choral_Chambers]),
        (new CoordinateStartDef { SceneName = SceneNames.Dock_13, X = 17, Y = 5, RespawnFacingRight = false }, [LocationNames.Memory_Locket__Deep_Docks]),
        (new CoordinateStartDef { SceneName = SceneNames.Bone_East_25, X = 148, Y = 8, RespawnFacingRight = true }, [LocationNames.Memory_Locket__Far_Fields_Secret]),
        (new CoordinateStartDef { SceneName = SceneNames.Halfway_01, X = 11, Y = 14.5f, RespawnFacingRight = false }, [LocationNames.Memory_Locket__Greymoor_HH]),
        (new CoordinateStartDef { SceneName = SceneNames.Greymoor_16, X = 127, Y = 53, RespawnFacingRight = true }, [LocationNames.Memory_Locket__Greymoor_Sewer]),
        (new CoordinateStartDef { SceneName = SceneNames.Ant_20, X = 142, Y = 12, RespawnFacingRight = true }, [LocationNames.Memory_Locket__Hunters_March]),
        (new CoordinateStartDef { SceneName = SceneNames.Arborium_05, X = 5.5f, Y = 9, RespawnFacingRight = false }, [LocationNames.Memory_Locket__Memorium]),
        (new CoordinateStartDef { SceneName = SceneNames.Coral_23, X = 95.5f, Y = 52, RespawnFacingRight = false }, [LocationNames.Memory_Locket__Sands_of_Karak]),
        (new CoordinateStartDef { SceneName = SceneNames.Slab_Cell_Quiet, X = 38.5f, Y = 32, RespawnFacingRight = true }, [LocationNames.Memory_Locket__The_Slab]),
        (new CoordinateStartDef { SceneName = SceneNames.Under_08, X = 65.5f, Y = 18, RespawnFacingRight = false }, [LocationNames.Memory_Locket__Underworks_Confessional]),
        (new CoordinateStartDef { SceneName = SceneNames.Library_08, X = 106.5f, Y = 35, RespawnFacingRight = false }, [LocationNames.Memory_Locket__Whispering_Vaults]),
        (new CoordinateStartDef { SceneName = SceneNames.Crawl_09, X = 126, Y = 5, RespawnFacingRight = true }, [LocationNames.Memory_Locket__Wormways])
    ];
}
