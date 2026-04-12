using ItemChanger.Modules;

namespace ItemChanger.Silksong.Modules;

[SingletonModule]
public class MossberryCostModule : CollectableCumulativeCostModule
{
    public override string ItemId => "Mossberry";
}
