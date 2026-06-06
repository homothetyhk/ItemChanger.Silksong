using ItemChanger.Costs;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Serialization;
using UnityEngine;

namespace ItemChanger.Silksong.Costs
{
    public class PDBoolCost(string boolName, IValueProvider<string> costText) : ThresholdBoolCost
    {
        public string BoolName { get; init; } = boolName;
        public IValueProvider<string> CostText { get; init; } = costText;

        public override string GetCostText()
        {
            return CostText?.Value ?? $"Have set {BoolName}";  // This should never be displayed, so we shouldn't need to bother localizing
        }

        protected override IValueProvider<bool> GetValueSource()
        {
            return new PDBool(BoolName);
        }
    }

    // Note - this class probably shouldn't be used in general, as the naked icon
    // intuitively implies a pay effect.
    /// <summary>
    /// Variant of <see cref="PDBoolCost"/> which supports displaying an icon.
    /// </summary>
    internal class DisplayablePDBoolCost(string boolName, IValueProvider<string> costText, IValueProvider<Sprite> sprite)
        : PDBoolCost(boolName, costText), IDisplayCost
    {
        public IValueProvider<Sprite> Sprite { get; init; } = sprite;

        int IDisplayCost.Amount => 1;
        Sprite IDisplayCost.DisplaySprite => Sprite.Value;
    }
}
