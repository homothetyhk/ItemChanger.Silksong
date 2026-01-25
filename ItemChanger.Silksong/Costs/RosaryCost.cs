using ItemChanger.Costs;
using TeamCherry.Localization;

namespace ItemChanger.Silksong.Costs
{
    public class RosaryCost(int Amount) : Cost
    {
        /// <summary>
        /// Amount after accounting for any discount rate.
        /// </summary>
        public int ActualAmount => (int)(Amount * base.DiscountRate);

        public override bool CanPay() => PlayerData.instance.GetInt(nameof(PlayerData.geo)) >= ActualAmount;

        public override string GetCostText() => string.Format(Language.Get("PAY_ROSARIES", "Fmt"), ActualAmount);
        
        public override bool HasPayEffects() => true;

        public override void OnPay()
        {
            if (ActualAmount > 0) HeroController.instance.TakeGeo(ActualAmount);
        }

        public override bool IsFree => ActualAmount <= 0;
    }
}
