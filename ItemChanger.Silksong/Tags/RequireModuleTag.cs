using ItemChanger.Modules;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.Tags;

public class RequireModuleTag<TModule> : Tag where TModule : Module, new()
{
    protected override void DoLoad(TaggableObject parent)
    {
        ItemChangerHost.Singleton.ActiveProfile!.Modules.GetOrAdd<TModule>();
    }
}
