using ItemChanger.Silksong.Costs;
using ItemChanger.Silksong.RawData;
using PrepatcherPlugin;

namespace ItemChangerTesting.ShopTests;

internal class SongclaveShopTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        MenuDescription = "Places items at Jubilana before and after second rescue.",
        MenuName = "Songclave Shop Test",
        Folder = TestFolder.ShopTests,
        Revision = 2026041300,
    };

    protected override void OnEnterGame()
    {
        base.OnEnterGame();

        PlayerDataAccess.bellShrineEnclave = true;
        PlayerDataAccess.enclaveLevel = 1;
        PlayerDataAccess.geo = 10000;
        QuestUtil.SetCompleted(Quests.Save_City_Merchant);
    }

    public override void Setup(TestArgs args)
    {
        StartAt(Benchwarp.Data.BaseBenchList.Songclave);
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Jubilana)!.Wrap().Add(Finder.GetItem(ItemNames.Swift_Step)!.WithCost(new RosaryCost(1))));
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Jubilana_Requires_Rescue)!.Wrap().Add(Finder.GetItem(ItemNames.Cling_Grip)!.WithCost(new RosaryCost(2))));
    }

    public override IEnumerable<(string, Action)> TestMethods()
    {
        yield return ("Lose Merchant", () => QuestUtil.SetAvailable(Quests.Save_City_Merchant_Bridge));
        yield return ("Save Merchant", () => QuestUtil.SetCompleted(Quests.Save_City_Merchant_Bridge));
    }
}
