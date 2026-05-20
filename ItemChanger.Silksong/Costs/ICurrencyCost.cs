using ItemChanger.Costs;
using ItemChanger.Silksong.Extensions;

namespace ItemChanger.Silksong.Costs;

public interface ICurrencyCost
{
    CurrencyType CurrencyType { get; }

    int Amount { get; }

    public static int Get(Cost cost, CurrencyType type) => cost.GetCostsOfType<ICurrencyCost>().Where(c => c.CurrencyType == type).Select(c => c.Amount).Sum();
}
