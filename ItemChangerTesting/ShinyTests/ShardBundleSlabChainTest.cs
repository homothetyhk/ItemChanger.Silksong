using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.ShinyTests;

internal class ShardBundleSlabChainTest : AbstractShinyPlacementTest<ShardBundleSlabChainTest>
{
    protected override string MenuName => "Shard Bundle Slab Chain";

    protected override string MenuDescription => "Test the Shard Bundle location in The Slab.";

    protected override int Revision => 2026100500;

    protected override IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> LocationTests =>
        [(new CoordinateStartDef { SceneName = SceneNames.Slab_04, X = 15.5f, Y = 11f, RespawnFacingRight = false }, [LocationNames.Shard_Bundle__The_Slab])];
}
