using HarmonyLib;
using ItemChanger.Locations;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.Modules;
using ItemChanger.Silksong.RawData;
using MonoMod.RuntimeDetour;

namespace ItemChanger.Silksong.Locations;

public class NuuToolPouchLocation : AutoLocation
{
    /// <summary>
    /// Number of boss kills required for completing Bugs of Pharloom wish
    /// </summary>
    public required int RequiredBossKills { get; init; }
    
    protected override void DoLoad()
    {
        Using(new Hook(
            AccessTools.Method(
                typeof(JournalQuestTarget),
                nameof(JournalQuestTarget.GetCompletionAmount)
            ), BossKillCountHook));

        QuestManager.GetQuest(Quests.Journal).ModifyTargetAmount(RequiredBossKills);
        QuestManager.GetQuest(Quests.Journal).ModifyReward(Placement!);
    }

    protected override void DoUnload()
    {
    }

    private static int BossKillCountHook(
        Func<JournalQuestTarget, QuestCompletionData.Completion, int> orig,
        JournalQuestTarget self,
        QuestCompletionData.Completion sourceCompletion)
    {
        return ActiveProfile!.Modules.GetOrAdd<BossKillsCounterModule>().BossKillCount;
    }
}