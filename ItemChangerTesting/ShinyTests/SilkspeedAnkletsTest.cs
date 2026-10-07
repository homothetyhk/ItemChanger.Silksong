using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;
using PrepatcherPlugin;

namespace ItemChangerTesting.ShinyTests;

internal class SilkspeedAnkletsTest : AbstractShinyPlacementTest<SilkspeedAnkletsTest>
{
    protected override string MenuName => "Silkspeed Anklets";

    protected override string MenuDescription => "Tests modifying the Silkspeed Anklets location and the Rune Harp below.";

    protected override int Revision => 2026100300;

    protected override void OnEnterGame()
    {
        PlayerDataAccess.hasDash = true;
        PlayerDataAccess.silkRegenMax = 3;
        BaseItemList.Silkspeed_Anklets.GiveImmediate(new());
        BaseItemList.Flea_Brew.GiveImmediate(new());
    }

    protected override IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> LocationTests =>
        [(new BenchwarpStartDef(Benchwarp.Data.BaseBenchList.WeavenestCindril), [LocationNames.Silkspeed_Anklets])];
}
