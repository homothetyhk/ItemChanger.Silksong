using Benchwarp.Data;
using ItemChanger.Silksong;
using ItemChanger.Silksong.Costs;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.Items;
using ItemChanger.Silksong.Modules;
using ItemChanger.Silksong.RawData;
using PrepatcherPlugin;
using UnityEngine.SceneManagement;

namespace ItemChangerTesting.LocationTests;

internal class StyxAndSkynxTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Styx and Stynx",
        MenuDescription = "Tests for Styx Silkeater placements and Skynx shop.",
        Revision = 2026051100,
    };

    protected override void OnEnterGame()
    {
        PlayerDataAccess.hasDash = true;
        PlayerDataAccess.hasWalljump = true;
        PlayerDataAccess.hasDoubleJump = true;
    }

    protected override void DoLoad()
    {
        base.DoLoad();
        Using(new SceneEditGroup() { { SceneNames.Dust_11, NerfRoaches } });
    }

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Dust_11, PrimitiveGateNames.left1);

        var module = Profile.Modules.Add<StyxAndSkynxModule>();
        module.DebtGrowTimeMillis = 20_000;  // 20s
        module.DefaultGrowTimeMillis = 60_000;  // 60s

        Profile.AddPlacement(Finder.GetLocation(LocationNames.Silkeater__Styx)!.Wrap().Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!));
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Silkeater__Queen_s_Egg)!.Wrap().Add(Finder.GetItem(ItemNames.White_Key)!));

        var shop = Finder.GetLocation(LocationNames.Skynx)!.Wrap();
        shop.Add(Finder.GetItem(ItemNames.Pale_Oil)!);
        shop.Add(Finder.GetItem(ItemNames.Ascendant_s_Grip)!.WithCost(new SilkeaterCost() { Value = 1 }));
        shop.Add(Finder.GetItem(ItemNames.Barbed_Bracelet)!.WithCost(new SilkeaterCost() { Value = 2 }));
        shop.Add(Finder.GetItem(ItemNames.Claw_Mirror)!.WithCost(new SilkeaterCost() { Value = 3 }));
        shop.Add(RosariesItem.MakeRosariesItem(50).WithCost(new SilkeaterCost() { Value = 6 }));
        Profile.AddPlacement(shop);
    }

    public override IEnumerable<(string, Action)> TestMethods()
    {
        yield return ("Give Grub", () => CollectableItemManager.GetItemByName("Silk Grub").AddAmount(1));
        yield return ("Queen's Egg", () => QuestUtil.SetReadyToComplete(Quests.Courier_Delivery_Dustpens_Slave));
        yield return ("Act 3", StartAct3);
    }

    private void NerfRoaches(Scene scene)
    {
        foreach (var obj in scene.AllGameObjects()) if (obj.TryGetComponent(out HealthManager health)) health.hp = 1;
    }
}
