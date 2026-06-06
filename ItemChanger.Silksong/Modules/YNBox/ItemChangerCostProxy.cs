using ItemChanger.Costs;
using ItemChanger.Silksong.Costs;
using UnityEngine;

namespace ItemChanger.Silksong.Modules.YNBox;

public class ItemChangerCostProxy : SavedItem
{
    public Cost Cost;

    public static ItemChangerCostProxy FromCost(Cost cost)
    {
        ItemChangerCostProxy proxy = ScriptableObject.CreateInstance<ItemChangerCostProxy>();
        proxy.Cost = cost;
        return proxy;
    }

    public override bool CanGetMore()
    {
        return true;
    }

    public override void Get(bool showPopup = true)
    {
        Cost.Pay();
    }

    // CanPay is effectively equivalent to GetSavedAmount >= the number passed to DialogueYNBox.Open
    public override int GetSavedAmount()
    {
        if (!Cost.CanPay()) return 0;

        if (Cost is IDisplayCost displayCost)
        {
            return displayCost.Amount;
        }

        return 1;
    }

    public override Sprite? GetPopupIcon()
    {
        return Cost is IDisplayCost displayCost ? displayCost.DisplaySprite : null;
    }
}
