using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.ShinyTests;

internal class ToolsTest : AbstractShinyPlacementTest<ToolsTest>
{
    protected override string MenuName => "Tools";

    protected override string MenuDescription => "Test all generic shiny locations for Tools.";

    protected override int Revision => 2026100500;

    protected override void OnEnterGame()
    {
        BaseItemList.Barbed_Bracelet.GiveImmediate(new());
        BaseItemList.Delver_s_Drill.GiveImmediate(new());
        BaseItemList.Flintslate.GiveImmediate(new());
        BaseItemList.Injector_Band.GiveImmediate(new());
        BaseItemList.Longpin.GiveImmediate(new());
        new SDBool(SceneNames.Shadow_11, "plank_wall_cluster_swamp (1)").Value = true;
        BaseItemList.Quick_Sling.GiveImmediate(new());
        BaseItemList.Ruined_Tool.GiveImmediate(new());
        BaseItemList.Shard_Pendant.GiveImmediate(new());
        BaseItemList.Snare_Setter.GiveImmediate(new());
        BaseItemList.Straight_Pin.GiveImmediate(new());
        BaseItemList.Warding_Bell.GiveImmediate(new());
        BaseItemList.Wreath_of_Purity.GiveImmediate(new());
    }

    protected override IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> LocationTests =>
    [
        (new CoordinateStartDef { SceneName = SceneNames.Dust_Barb, X = 26.5f, Y = 6, RespawnFacingRight = true }, [LocationNames.Barbed_Bracelet]),
        (new CoordinateStartDef { SceneName = SceneNames.Under_14, X = 67.5f, Y = 7, RespawnFacingRight = true }, [LocationNames.Delver_s_Drill]),
        (new CoordinateStartDef { SceneName = SceneNames.Dock_02b, X = 34, Y = 106, RespawnFacingRight = false }, [LocationNames.Flintslate]),
        (new CoordinateStartDef { SceneName = SceneNames.Ward_03, X = 100, Y = 33, RespawnFacingRight = false }, [LocationNames.Injector_Band]),
        (new TransitionOffsetStartDef { SceneName = SceneNames.Belltown_Room_shellwood, GateName = PrimitiveGateNames.left1 }, [LocationNames.Longpin]),
        (new CoordinateStartDef { SceneName = SceneNames.Shadow_11, X = 14, Y = 86, RespawnFacingRight = false }, [LocationNames.Quick_Sling]),
        (new CoordinateStartDef { SceneName = SceneNames.Shadow_Weavehome, X = 73, Y = 55, RespawnFacingRight = true }, [LocationNames.Ruined_Tool]),
        (new TransitionOffsetStartDef { SceneName = SceneNames.Bone_17, GateName = PrimitiveGateNames.right1 }, [LocationNames.Shard_Pendant]),
        (new CoordinateStartDef { SceneName = SceneNames.Weave_14, X = 62.5f, Y = 7, RespawnFacingRight = true }, [LocationNames.Snare_Setter]),
        (new CoordinateStartDef { SceneName = SceneNames.Bone_12, X = 44.5f, Y = 28, RespawnFacingRight = false }, [LocationNames.Straight_Pin]),
        (new CoordinateStartDef { SceneName = SceneNames.Dock_03b, X = 150, Y = 109, RespawnFacingRight = true }, [LocationNames.Warding_Bell]),
        (new CoordinateStartDef { SceneName = SceneNames.Aqueduct_06, X = 119, Y = 10, RespawnFacingRight = true }, [LocationNames.Wreath_of_Purity])
    ];
}
