using Benchwarp.Data;
using ItemChanger.Silksong.Costs;
using ItemChanger.Silksong.Modules.ShopsModule;
using ItemChanger.Silksong.RawData;
using PrepatcherPlugin;

namespace ItemChangerTesting.ShopTests;

internal class BellhartShopTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        MenuDescription = "Simple item placements and costs at the Belhart shop.",
        MenuName = "Bellhart Shop Test",
        Folder = TestFolder.ShopTests,
        Revision = 2026041300,
    };

    protected override void OnEnterGame()
    {
        base.OnEnterGame();

        PlayerDataAccess.bellShrineBellhart = true;
        PlayerDataAccess.spinnerDefeated = true;
        PlayerDataAccess.hasNeedolin = true;
        PlayerDataAccess.SeenBelltownCutscene = true;
        PlayerDataAccess.visitedBellhart = true;
        PlayerDataAccess.visitedBellhartHaunted = true;
        PlayerDataAccess.visitedBellhartSaved = true;
    }

    public override void Setup(TestArgs args)
    {
        StartAt(BaseBenchList.Bellhart);
        Profile.Modules.Add<ShopsModule>().RemovedCategories.Add(DefaultShopItems.Tools);  // Removes Multibinder.

        Profile.AddPlacement(BaseLocationList.Frey.Wrap().Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!.WithCost(new RosaryCost(123))));
        Profile.AddPlacement(BaseLocationList.Frey_Requires_Bellhome.Wrap().Add(Finder.GetItem(ItemNames.Ascendant_s_Grip)!.WithCost(new RosaryCost(234))));
    }

    public override IEnumerable<(string, Action)> TestMethods()
    {
        yield return ("Get Money", () =>
        {
            PlayerDataAccess.geo += 1000;
            PlayerDataAccess.ShellShards += 100;
        });
        yield return ("Unlock Bellhome", () =>
        {
            PlayerDataAccess.BelltownHouseUnlocked = true;
            PlayerDataAccess.scenesVisited.Add(SceneNames.Belltown_Room_Spare);
        });
    }
}
