using System;
using System.Collections.Generic;
using System.Text;

namespace ItemChanger.Silksong.Costs;

public interface ICurrencyCost
{
    CurrencyType CurrencyType { get; }

    int Amount { get; }

}
