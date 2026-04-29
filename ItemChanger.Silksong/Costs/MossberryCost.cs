using ItemChanger.Silksong.Modules;
using ItemChanger.Silksong.Util;
using Newtonsoft.Json;

namespace ItemChanger.Silksong.Costs;

public class MossberryCost : CumulativeCost
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
}
