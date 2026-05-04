using ItemChanger.Silksong.Modules;
using ItemChanger.Silksong.Util;
using Newtonsoft.Json;
using UnityEngine;

namespace ItemChanger.Silksong.Costs;

public class MossberryCost : CumulativeCost, IDisplayCost
{
    private MossberryCostModule? _module;

    protected override void DoLoad()
    {
        _module = ActiveProfile!.Modules.GetOrAdd<MossberryCostModule>();
    }

    protected override void DoUnload()
    {
        _module = null;
    }

    protected override ICumulativeCostModule Module
    {
        get
        {
            if (_module == null)
            {
                throw new InvalidOperationException("MossberryCostModule not loaded");
            }
            return _module;
        }
    }

    protected override string GetCostText(int amount) =>
        string.Format("FMT_PAY_MOSSBERRIES".GetLanguageString(), amount);

    [JsonIgnore]
    public int Amount => Math.Max(0, Value - Module.TotalSpent);

    [JsonIgnore]
    public Sprite DisplaySprite { get => field ??= CollectableItemManager.GetItemByName("Mossberry").GetPopupIcon(); }
}
