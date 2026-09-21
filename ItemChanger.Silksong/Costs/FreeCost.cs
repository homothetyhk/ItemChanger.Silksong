using ItemChanger.Costs;
using Newtonsoft.Json;

namespace ItemChanger.Silksong.Costs;

/// <summary>
/// Helper class to substitute the absence of a cost.
/// </summary>
public class FreeCost : Cost
{
    public static readonly FreeCost Instance = new();

    [JsonIgnore]
    public override bool IsFree => true;

    public override bool CanPay() => true;

    public override string GetCostText() => "!!FREE!!";

    public override bool HasPayEffects() => false;

    public override void OnPay() { }
}
