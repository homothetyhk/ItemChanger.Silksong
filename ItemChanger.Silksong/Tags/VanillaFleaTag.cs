using ItemChanger.Silksong.Modules;
using ItemChanger.Tags;
using ItemChanger.Tags.Constraints;

namespace ItemChanger.Silksong.Tags;

/// <summary>
/// Tag indicating that the attached location represents a vanilla flea,
/// and should be excluded from the total number of fleas available
/// as computed by the base game code.
/// </summary>
[LocationTag]
public class VanillaFleaTag : Tag
{
    protected override void DoLoad(TaggableObject parent)
    {
        AnonymousFleasModule mod = ItemChangerHost.Singleton.ActiveProfile!.Modules.GetOrAdd<AnonymousFleasModule>();
        mod.RemovedVanillaFleas += 1;
    }

    protected override void DoUnload(TaggableObject parent)
    {
        AnonymousFleasModule mod = ItemChangerHost.Singleton.ActiveProfile!.Modules.Get<AnonymousFleasModule>()!;
        mod.RemovedVanillaFleas -= 1;
    }
}
