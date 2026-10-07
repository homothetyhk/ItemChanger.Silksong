using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.ShinyTests;

internal class ShardBundlesTest : AbstractShinyPlacementTest<ShardBundlesTest>
{
    protected override string MenuName => "Shard Bundles";

    protected override string MenuDescription => "Test all generic shiny Shard Bundles placements.";

    protected override int Revision => 2026100500;

    protected override IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> LocationTests =>
    [
        (new CoordinateStartDef { SceneName = SceneNames.Cog_10, X = 12, Y = 45, RespawnFacingRight = false }, [LocationNames.Shard_Bundle__Cogwork_Core]),
        (new CoordinateStartDef { SceneName = SceneNames.Room_Forge, X = 8.5f, Y = 39, RespawnFacingRight = false }, [LocationNames.Shard_Bundle__Deep_Docks_Forge]),
        (new CoordinateStartDef { SceneName = SceneNames.Dock_02, X = 42, Y = 5, RespawnFacingRight = false }, [LocationNames.Shard_Bundle__Deep_Docks_South]),
        (new CoordinateStartDef { SceneName = SceneNames.Greymoor_05, X = 64, Y = 63, RespawnFacingRight = false }, [LocationNames.Shard_Bundle__Greymoor_Lower]),
        (new CoordinateStartDef { SceneName = SceneNames.Greymoor_12, X = 65, Y = 16, RespawnFacingRight = false }, [LocationNames.Shard_Bundle__Greymoor_Upper]),
        (new CoordinateStartDef { SceneName = SceneNames.Arborium_11, X = 206, Y = 12, RespawnFacingRight = true }, [LocationNames.Shard_Bundle__Memorium]),
        (new TransitionOffsetStartDef { SceneName = SceneNames.Dust_06, GateName = PrimitiveGateNames.right1 }, [LocationNames.Shard_Bundle__Sinners_Road]),
        (new CoordinateStartDef { SceneName = SceneNames.Shellwood_13, X = 79, Y = 69, RespawnFacingRight = true }, [LocationNames.Shard_Bundle__Shellwood]),
        (new CoordinateStartDef { SceneName = SceneNames.Under_18, X = 36, Y = 36, RespawnFacingRight = false }, [LocationNames.Shard_Bundle__The_Cauldron]),
        (new CoordinateStartDef { SceneName = SceneNames.Library_12, X = 128.5f, Y = 26, RespawnFacingRight = false }, [LocationNames.Shard_Bundle__Underworks_Exhaust]),
        (new CoordinateStartDef { SceneName = SceneNames.Under_03, X = 12.5f, Y = 8, RespawnFacingRight = false }, [LocationNames.Shard_Bundle__Underworks_West]),
    ];
}
