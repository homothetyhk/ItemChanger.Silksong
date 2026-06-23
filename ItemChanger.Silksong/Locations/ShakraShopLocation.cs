using ItemChanger.Silksong.Modules.ShopsModule;

namespace ItemChanger.Silksong.Locations;

// Shakra locations require the ShakraShopsModule.
public class ShakraShopLocation : ShopLocation
{
    protected override void DoLoad()
    {
        base.DoLoad();
        ActiveProfile!.Modules.GetOrAdd<ShakraModule>();
    }
}
