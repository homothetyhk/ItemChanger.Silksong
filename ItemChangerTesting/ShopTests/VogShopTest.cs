using AssetHelperLib.BundleTools;
using GlobalEnums;
using ItemChanger.Silksong.Costs;
using ItemChanger.Silksong.RawData;
using PrepatcherPlugin;

namespace ItemChangerTesting.ShopTests;

internal class VogShopTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        MenuDescription = "Places items at Vog's shop.",
        MenuName = "Vog Shop Test",
        Folder = TestFolder.ShopTests,
        Revision = 2026041900,
    };

    public bool Initialized = false;

    protected override void OnEnterGame()
    {
        if (Initialized) return;

        PlayerDataAccess.geo += 1000;
        PlayerDataAccess.MetTroupeHunterWild = true;
        PlayerDataAccess.defeatedLastJudge = true;
        Initialized = true;
    }

    public override void Setup(TestArgs args)
    {
        StartAt(Benchwarp.Data.BaseBenchList.FleaCaravanMarrow);
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Vog)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!.WithCost(new RosaryCost(100)))
            .Add(Finder.GetItem(ItemNames.Ascendant_s_Grip)!.WithCost(new RosaryCost(200))));
    }

    public override IEnumerable<(string, Action)> TestMethods()
    {
        yield return ("Move Caravan", () =>
        {
            PlayerDataAccess.CaravanTroupeLocation = PlayerDataAccess.CaravanTroupeLocation switch
            {
                CaravanTroupeLocations.Bone => CaravanTroupeLocations.Greymoor,
                CaravanTroupeLocations.Greymoor => CaravanTroupeLocations.CoralJudge,
                CaravanTroupeLocations.CoralJudge => CaravanTroupeLocations.Aqueduct,
                CaravanTroupeLocations.Aqueduct => CaravanTroupeLocations.Bone,
            };
        });
        yield return ("Toggle Flea Games", () => PlayerDataAccess.FleaGamesStarted = !PlayerDataAccess.FleaGamesStarted);
        yield return ("Toggle Vog", () => PlayerDataAccess.MetTroupeHunterWild = !PlayerDataAccess.MetTroupeHunterWild);
    }
}
