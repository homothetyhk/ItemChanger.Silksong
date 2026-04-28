using ItemChanger.Silksong.Costs;
using ItemChanger.Silksong.RawData;
using PrepatcherPlugin;

namespace ItemChangerTesting.ShopTests;

internal class ArchitectShopTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        MenuDescription = "Places items at the Twelfth Architect's shop.",
        MenuName = "Architect Shop Test",
        Folder = TestFolder.ShopTests,
        Revision = 2026041300,
    };

    protected override void OnEnterGame()
    {
        base.OnEnterGame();

        PlayerDataAccess.hasDash = true;
        PlayerDataAccess.hasDoubleJump = true;
        PlayerDataAccess.geo = 10000;
    }

    public override void Setup(TestArgs args)
    {
        StartAt(Benchwarp.Data.BaseBenchList.TwelfthArchitect);
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Twelfth_Architect)!.Wrap().Add(Finder.GetItem(ItemNames.Sting_Shard)!.WithCost(new RosaryCost(1))));
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Twelfth_Architect_Requires_Tools)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Flintslate)!.WithCosts(new RosaryCost(1), new ToolCountCost(1)))
            .Add(Finder.GetItem(ItemNames.Cogfly)!.WithCosts(new RosaryCost(2), new ToolCountCost(2)))
            .Add(Finder.GetItem(ItemNames.Cling_Grip)!.WithCosts(new RosaryCost(3), new ToolCountCost(3))));
    }
}
