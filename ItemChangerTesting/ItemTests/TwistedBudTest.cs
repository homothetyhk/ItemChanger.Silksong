using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using PrepatcherPlugin;

namespace ItemChangerTesting.ItemTests;

internal class TwistedBudTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.ItemTests,
        MenuName = "Twisted Bud",
        MenuDescription = "Tests Twisted Bud tag effects",
        Revision = 2026051000,
    };

    protected override void OnEnterGame()
    {
        PlayerDataAccess.hasDash = true;
        PlayerDataAccess.hasDoubleJump = true;
        PlayerDataAccess.hasHarpoonDash = true;
        PlayerDataAccess.hasNeedolin = true;
        PlayerDataAccess.hasWalljump = true;
        PlayerDataAccess.silkRegenMax = 3;
    }

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Clover_20, PrimitiveGateNames.left1);
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Map__Verdania)!.Wrap().Add(Finder.GetItem(ItemNames.Twisted_Bud)!));

        // TODO: Animate and SFX in shop UI
    }
}
