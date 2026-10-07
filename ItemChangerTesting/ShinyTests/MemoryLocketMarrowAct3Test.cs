using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.ShinyTests;

internal class MemoryLocketMarrowAct3Test : AbstractShinyPlacementTest<MemoryLocketMarrowAct3Test>
{
    protected override string MenuName => "Memory Locket - Marrow (Act3)";

    protected override string MenuDescription => "Test the Memory Locket - Marrow location in Act 3.";

    protected override int Revision => 2026100600;

    protected override void OnEnterGame()
    {
        StartAct3();
        for (int i = 0; i < 20; i++)
            BaseItemList.Memory_Locket.GiveImmediate(new());
    }

    protected override IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> LocationTests => MemoryLocketMarrowTest.CommonLocationTests;
}
