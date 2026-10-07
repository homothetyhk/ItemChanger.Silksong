using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.ShinyTests;

internal class SimpleKeySandsOfKarakTest : AbstractShinyPlacementTest<SimpleKeySandsOfKarakTest>
{
    protected override string MenuName => "Simple Key - Sands of Karak";

    protected override string MenuDescription => "Test the simple key at the Sands of Karak location.";

    protected override int Revision => 2026100700;

    protected override void OnEnterGame()
    {
        for (int i = 0; i < 4; i++)
            BaseItemList.Simple_Key.GiveImmediate(new());
    }

    protected override IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> LocationTests =>
        [(new BenchwarpStartDef(Benchwarp.Data.BaseBenchList.SandsOfKarak), [LocationNames.Simple_Key__Sands_of_Karak])];
}
