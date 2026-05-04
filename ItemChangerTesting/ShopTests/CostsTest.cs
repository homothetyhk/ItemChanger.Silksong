using ItemChanger.Silksong.Costs;
using ItemChanger.Silksong.Modules.ShopsModule;
using ItemChanger.Silksong.RawData;
using PrepatcherPlugin;

namespace ItemChangerTesting.ShopTests;

internal class CostsTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        MenuDescription = "Places items with varying cost types at Pilgrim's Rest.",
        MenuName = "Costs Test",
        Folder = TestFolder.ShopTests,
        Revision = 20260141300,
    };

    protected override void OnEnterGame()
    {
        PlayerDataAccess.geo = 10000;
        PlayerDataAccess.ShellShards = 400;
        CollectableItemManager.AddItem(CollectableItemManager.GetItemByName("Tool Metal"), 10);
    }

    public override void Setup(TestArgs args)
    {
        StartAt(Benchwarp.Data.BaseBenchList.PilgrimsRest);

        // Remove all of Mort's inventory except the infinites.
        // Mort's Dialogue FSM bricks if his shop is empty.
        var removed = Profile.Modules.Add<ShopsModule>().RemovedCategories;
        removed.Add(DefaultShopItems.Tools);
        removed.Add(DefaultShopItems.ToolUpgrades);
        removed.Add(DefaultShopItems.MemoryLockets);

        var placement = Finder.GetLocation(LocationNames.Mort)!.Wrap();
        placement.Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!.WithCosts(new RosaryCost(100), new ShellShardCost(100)));
        placement.Add(Finder.GetItem(ItemNames.Sting_Shard)!);  // Free!  Shows as 0 rosaries.
        placement.Add(Finder.GetItem(ItemNames.Multibinder)!.WithCosts(new RosaryCost(100), new CollectableItemCost() { Amount = 2, CollectableItemId = "Tool Metal" }));
        placement.Add(Finder.GetItem(ItemNames.Faydown_Cloak)!.WithCosts(new RosaryCost(200), new CollectableItemCost() { Amount = 4, CollectableItemId = "Tool Metal" }));
        placement.Add(Finder.GetItem(ItemNames.Swift_Step)!.WithCosts(new RosaryCost(300), new ToolCountCost(1)));
        Profile.AddPlacement(placement);
    }
}
