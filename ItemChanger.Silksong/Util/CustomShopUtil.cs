using HutongGames.PlayMaker;
using ItemChanger.Silksong.Assets;
using TeamCherry.Localization;

namespace ItemChanger.Silksong.Util;

public static class CustomShopUtil
{
    /// <summary>
    /// Mark an NPC as a shop owner, so it can support opening a shop dialogue.
    /// For this shop to display inventory, there must exist a loaded ShopPlacement that matches on this ShopOwner.
    /// </summary>
    public static ShopOwner CreateShopOwner(this GameObject self, LocalisedString shopTitle)
    {
        var owner = self.AddComponent<ShopOwner>();
        var template = GameObjectKeys.SHOP.GetGameObjectPrefab().GetComponent<ShopOwner>();

        owner.shopPrefab = template.shopPrefab;
        owner.shopTitle = shopTitle;
        owner.stockList = null;
        owner.stock = [];

        return owner;
    }

    /// <summary>
    /// Returns an FSM state action that opens the shop UI.
    /// </summary>
    public static FsmStateAction ShopUp(this ShopOwner self) => new ShopUpAction(self);

    public static FsmEvent CLOSE_SHOP_WINDOW => FsmEvent.GetFsmEvent("CLOSE SHOP WINDOW");
    public static FsmEvent CLOSE_SHOP_WINDOW_INACTIVE => FsmEvent.GetFsmEvent("CLOSE SHOP WINDOW INACTIVE");
    public static FsmEvent SHOP_CLOSED => FsmEvent.GetFsmEvent("SHOP CLOSED");
    public static FsmEvent SHOP_NO_STOCK => FsmEvent.GetFsmEvent("SHOP NO STOCK");

    /// <summary>
    /// An fsm state with the ShopUp action must respond to all of these events.
    /// </summary>
    public static IReadOnlyList<FsmEvent> ShopDownEvents() => [
        CLOSE_SHOP_WINDOW,
        CLOSE_SHOP_WINDOW_INACTIVE,
        SHOP_CLOSED,
        SHOP_NO_STOCK
    ];
}

file class ShopUpAction(ShopOwner ShopOwner) : FsmStateAction
{
    public override void OnEnter() => Fsm.Event(ShopOwner.ShopObject, FsmEvent.GetFsmEvent("SHOP UP"));
}
