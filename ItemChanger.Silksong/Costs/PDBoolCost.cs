using ItemChanger.Costs;
using ItemChanger.Serialization;
using System;

namespace ItemChanger.Silksong.Costs
{
    public class PDBoolCost(string BoolName) : ThresholdBoolCost
    {
        public override string GetCostText()
        {
            throw new NotImplementedException();
        }

        protected override IValueProvider<bool> GetValueSource()
        {
            throw new NotImplementedException();
        }
    }
}
