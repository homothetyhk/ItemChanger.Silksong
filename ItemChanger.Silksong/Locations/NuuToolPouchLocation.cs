using ItemChanger.Locations;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.Modules;
using ItemChanger.Silksong.RawData;

namespace ItemChanger.Silksong.Locations;

public class NuuToolPouchLocation : AutoLocation
{
    protected override void DoLoad()
    {
        ItemChangerHost.Singleton.ActiveProfile!.Modules.GetOrAdd<NuuChecksBossKillsModule>();
        
        QuestManager.GetQuest(Quests.Journal).ModifyReward(Placement!);
    }

    protected override void DoUnload()
    { }
}