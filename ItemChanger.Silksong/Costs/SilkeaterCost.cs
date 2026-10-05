using ItemChanger.Costs;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.Modules;
using ItemChanger.Silksong.RawData;

namespace ItemChanger.Silksong.Costs;

public class SilkeaterCost : CumulativeCost
{
    protected override ICumulativeCostModule Module => ItemChangerHost.Singleton.ActiveProfile!.Modules.Get<StyxAndSkynxModule>()!;

    protected override string GetCostText(int toSpend) => ItemChangerLanguageStrings.FMT_PAY_SILKEATERS().Format(toSpend);

    public static int Get(Cost cost) => cost.GetCostsOfType<SilkeaterCost>().Select(c => c.Value).DefaultIfEmpty().Max();
}
