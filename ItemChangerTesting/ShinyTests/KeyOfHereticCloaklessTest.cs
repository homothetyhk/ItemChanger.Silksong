using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;
using PrepatcherPlugin;

namespace ItemChangerTesting.ShinyTests;

internal class KeyOfHereticCloaklessTest : AbstractShinyPlacementTest<KeyOfHereticCloaklessTest>
{
    protected override string MenuName => "Key of Heretic (Cloakless)";

    protected override string MenuDescription => "Test the Key of Heretic location cloakless.";

    protected override int Revision => 2026100400;

    protected override void OnEnterGame()
    {
        ToolItemManager.SetEquippedCrest("Cloakless");
        PlayerDataAccess.IsCurrentCrestTemp = false;
        ToolItemManager.SendEquippedChangedEvent();

        BaseItemList.Key_of_Heretic.GiveImmediate(new());
    }

    protected override IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> LocationTests => KeyOfHereticTest.CommonLocationTests;
}
