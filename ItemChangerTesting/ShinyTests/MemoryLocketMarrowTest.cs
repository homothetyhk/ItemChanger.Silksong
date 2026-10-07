using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.ShinyTests;

internal class MemoryLocketMarrowTest : AbstractShinyPlacementTest<MemoryLocketMarrowTest>
{
    protected override string MenuName => "Memory Locket - Marrow";

    protected override string MenuDescription => "Test the Memory Locket - Marrow location.";

    protected override int Revision => 2026100600;

    protected override void OnEnterGame()
    {
        for (int i = 0; i < 20; i++)
            BaseItemList.Memory_Locket.GiveImmediate(new());
    }

    internal static IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> CommonLocationTests =>
        [(new CoordinateStartDef { SceneName = SceneNames.Bone_18, X = 28.5f, Y = 22, RespawnFacingRight = true }, [LocationNames.Memory_Locket__The_Marrow])];

    protected override IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> LocationTests => CommonLocationTests;
}
