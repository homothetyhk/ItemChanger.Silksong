using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;
using PrepatcherPlugin;

namespace ItemChangerTesting.ShinyTests;

internal class VintageNectarTest : AbstractShinyPlacementTest<VintageNectarTest>
{
    protected override string MenuName => "Vintage Nectar";

    protected override string MenuDescription => "Test the Vintage Nectar location.";

    protected override int Revision => 2026100400;

    protected override void OnEnterGame()
    {
        PlayerDataAccess.hasDash = true;
        PlayerDataAccess.hasDoubleJump = true;
        PlayerDataAccess.geo = 1000;

        // Skip dialogue.
        PlayerDataAccess.MetHalfwayBartender = true;

        // Nectar isn't available into the Crow Feathers quest is accepted.
        QuestUtil.SetAccepted(Quests.Crow_Feathers);

        // Check that actual vintage nectar usage doesn't interfere with the placement.
        QuestUtil.SetCompleted(Quests.Great_Gourmand);
        BaseItemList.Vintage_Nectar.GiveImmediate(new());
    }

    protected override IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> LocationTests =>
        [(new BenchwarpStartDef(Benchwarp.Data.BaseBenchList.HalfwayHouse), [LocationNames.Vintage_Nectar])];
}
