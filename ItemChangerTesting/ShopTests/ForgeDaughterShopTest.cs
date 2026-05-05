using ItemChanger.Silksong.Costs;
using ItemChanger.Silksong.RawData;
using PrepatcherPlugin;

namespace ItemChangerTesting.ShopTests;

internal class ForgeDaughterShopTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        MenuDescription = "Places items at Forge Daughter's shop with Craftmetal costs.",
        MenuName = "Forge Daughter Shop Test",
        Folder = TestFolder.ShopTests,
        Revision = 20260141300,
    };

    protected override void OnEnterGame() => PlayerDataAccess.geo = 10000;

    public override void Setup(TestArgs args)
    {
        StartAt(Benchwarp.Data.BaseBenchList.Forge);

        var placement = Finder.GetLocation(LocationNames.Forge_Daughter)!.Wrap();
        for (int i = 0; i < 6; i++) placement.Add(Finder.GetItem(ItemNames.Craftmetal)!.WithCost(new RosaryCost(i + 1)));
        placement.Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!.WithCosts(new RosaryCost(100), new CollectableItemCost() { Amount = 1, CollectableItemId = "Tool Metal" }));
        placement.Add(Finder.GetItem(ItemNames.Swift_Step)!.WithCosts(new RosaryCost(200), new CollectableItemCost() { Amount = 2, CollectableItemId = "Tool Metal" }));
        placement.Add(Finder.GetItem(ItemNames.Cling_Grip)!.WithCosts(new RosaryCost(300), new CollectableItemCost() { Amount = 3, CollectableItemId = "Tool Metal" }));
        Profile.AddPlacement(placement);
    }
}
