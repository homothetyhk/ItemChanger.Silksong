using Benchwarp.Data;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Extensions;
using ItemChanger.Silksong;
using ItemChanger.Silksong.Costs;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.Util;
using PrepatcherPlugin;
using Silksong.FsmUtil;
using UnityEngine.SceneManagement;

namespace ItemChangerTesting.ShopTests;

internal class CustomShopTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        MenuDescription = "Adds a custom shop at Mask Maker.",
        MenuName = "Custom Shop Test",
        Folder = TestFolder.ShopTests,
        Revision = 2026091400,
    };

    protected override void OnEnterGame() => PlayerDataAccess.geo = 10000;

    private const string SHOP_NAME = "Mask_Maker";
    private const string OBJECT_NAME = "Peak Mask Maker";

    private static readonly BaseShop BASE_SHOP = new()
    {
        Name = SHOP_NAME,
        Matcher = shopOwnerBase => shopOwnerBase.name == OBJECT_NAME,
        Inventory = []
    };

    protected override void DoLoad()
    {
        base.DoLoad();

        Using(new SceneEditGroup() { { SceneNames.Peak_Mask_Maker, AddMaskMakerShop } });
        BaseShopList.AddBaseShop(BASE_SHOP);
    }

    protected override void DoUnload()
    {
        base.DoUnload();
        BaseShopList.RemoveBaseShop(BASE_SHOP.Name);
    }

    private void AddMaskMakerShop(Scene scene)
    {
        GameObject obj = scene.FindGameObject(OBJECT_NAME)!;

        var shopOwner = obj.CreateShopOwner(ItemChangerLanguageStrings.CUSTOM_SHOP_TITLE().ToLocalisedString());
        var fsm = obj.LocateMyFSM("Dialogue");

        var endDialogueState = fsm.MustGetState("End Dialogue");
        endDialogueState.RemoveTransition("FINISHED");
        endDialogueState.GetFirstActionOfType<EndDialogue>()?.ReturnControl.Value = false;
        endDialogueState.AddAction(shopOwner.ShopUpAction());

        var shopDownState = fsm.AddState("Shop Down");
        shopDownState.AddAction(new EndDialogue()
        {
            ReturnControl = true,
            ReturnHUD = true,
            Target = new() { OwnerOption = OwnerDefaultOption.UseOwner },
            UseChildren = false,
        });
        shopDownState.AddTransition("FINISHED", "Idle");

        foreach (var shopEvent in CustomShopUtil.ShopDownEvents())
            endDialogueState.AddTransition(shopEvent.Name, shopDownState.Name);
    }

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Peak_Mask_Maker, PrimitiveGateNames.right1);

        var placement = new ShopLocation()
        {
            Name = SHOP_NAME,
            BaseShopName = SHOP_NAME,
            SceneName = SceneNames.Peak_Mask_Maker
        }.Wrap();
        placement.Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!.WithCosts(new RosaryCost(111)));
        placement.Add(Finder.GetItem(ItemNames.Swift_Step)!.WithCosts(new RosaryCost(222)));
        placement.Add(Finder.GetItem(ItemNames.Cling_Grip)!.WithCosts(new RosaryCost(333)));
        Profile.AddPlacement(placement);
    }
}
