using ItemChanger.Items;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Modules;

namespace ItemChanger.Silksong.Items;

internal class FleaItem : Item
{
    protected override void DoLoad()
    {
        AnonymousFleasModule mod = ItemChangerHost.Singleton.ActiveProfile!.Modules.GetOrAdd<AnonymousFleasModule>();
        mod.AnonymousFleas += 1;
    }

    protected override void DoUnload()
    {
        AnonymousFleasModule mod = ItemChangerHost.Singleton.ActiveProfile!.Modules.Get<AnonymousFleasModule>()!;
        mod.AnonymousFleas -= 1;
    }

    public override string GetPreferredContainer()
    {
        return ContainerNames.Flea;
    }

    public override void GiveImmediate(GiveInfo info)
    {
        AnonymousFleasModule mod = ItemChangerHost.Singleton.ActiveProfile!.Modules.GetOrAdd<AnonymousFleasModule>();
        mod.AnonymousFleasSaved += 1;
    }
}
