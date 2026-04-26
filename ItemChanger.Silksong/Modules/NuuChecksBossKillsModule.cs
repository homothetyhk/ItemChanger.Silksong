using Benchwarp.Data;
using HarmonyLib;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Modules;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.RawData;
using MonoMod.RuntimeDetour;
using PrepatcherPlugin;
using Silksong.FsmUtil;
using Silksong.FsmUtil.Actions;
using Module = ItemChanger.Modules.Module;

namespace ItemChanger.Silksong.Modules;

/// <summary>
/// A module which replaces Nuu's journal entry requirements with requiring a count of bosses to be defeated.
/// </summary>
[SingletonModule]
public class NuuChecksBossKillsModule : Module
{
    /// <summary>
    /// Number of boss kills required for completing Bugs of Pharloom wish
    /// </summary>
    public int KillsForWish { get; init; } = 10;

    /// <summary>
    /// Number of boss kills required for obtaining Hunters' Memento
    /// </summary>
    public int KillsForMemento { get; init; } = 30;

    /// <summary>
    /// A list of bosses which should count towards Nuu's kill count. Defaults to all distinct bosses.
    /// <remarks>
    /// <para>Note that several of the bosses in this list are missable in vanilla playthroughs (Garmond + Zaza,
    /// Shakra). However, these are not missable in Rando due to the ability to return to Act 2.</para>
    /// <para>Boss refights currently do not count for Nuu's kill check (Lace 2, Moss Mothers, Raging Conchfly,
    /// Savage Beastfly 2).</para></remarks>
    /// </summary>
    public HashSet<string> Bosses { get; init; } =
    [
        JournalEntries.Bone_Beast, // Bell Beast
        JournalEntries.Song_Golem, // Fourth Chorus
        JournalEntries.Coral_Conch_Driller_Giant, // Great Conchflies
        JournalEntries.Lace,
        JournalEntries.Last_Judge,
        JournalEntries.Vampire_Gnat, // Moorwing
        JournalEntries.Mossbone_Mother, // Moss Mother
        JournalEntries.Phantom,
        JournalEntries.Bone_Flyer_Giant, // Savage Beastfly
        JournalEntries.Splinter_Queen, // Sister Splinter
        JournalEntries.Skull_King, // Skull Tyrant
        JournalEntries.Spinner_Boss, // Widow
        JournalEntries.Slab_Fly_Broodmother,
        JournalEntries.Clockwork_Dancer, // Cogwork Dancers
        JournalEntries.Roachkeeper_Chef, // Disgraced Chef Lugoli
        JournalEntries.Wisp_Pyre_Effigy, // Father of the Flame
        JournalEntries.First_Weaver, // First Sinner
        JournalEntries.Dock_Guard_Thrower, // Forebrothers Signis & Grom
        JournalEntries.Garmond_Zaza, // Garmond & Zaza
        JournalEntries.Silk_Boss, // Grand Mother Silk
        JournalEntries.Swamp_Shaman, // Groal the Great
        JournalEntries.Song_Knight, // Second Sentinel
        JournalEntries.Shakra,
        JournalEntries.Abyss_Mass, // Summoned Saviour
        JournalEntries.Conductor_Boss, // The Unravelled
        JournalEntries.Trobbio,
        JournalEntries.Zap_Core_Enemy, // Voltvyrm
        JournalEntries.Giant_Centipede, // Bell Eater
        JournalEntries.Clover_Dancer, // Clover Dancers
        JournalEntries.Crawfather,
        JournalEntries.Coral_King, // Crust King Khann
        JournalEntries.Bone_Hunter_Trapper, // Gurr the Outcast
        JournalEntries.Garmond, // Lost Garmond
        JournalEntries.Lost_Lace,
        JournalEntries.Flower_Queen, // Nyleth
        JournalEntries.Cloverstag_White, // Palestag
        JournalEntries.Pinstress_Boss,
        JournalEntries.Blue_Assistant, // Plasmified Zango
        JournalEntries.Seth, // Shrine Guardian Seth
        JournalEntries.Hunter_Queen, // Skarrsinger Karmelita
        JournalEntries.Tormented_Trobbio,
        JournalEntries.Coral_Warrior_Grey, // Watcher at the Edge
    ];

    private static NuuChecksBossKillsModule? _instance;

    protected override void DoLoad()
    {
        if (_instance != null)
            throw new InvalidOperationException($"Cannot load two instances of {nameof(NuuChecksBossKillsModule)}.");
        _instance = this;

        Using(new FsmEditGroup()
        {
            { new(SceneNames.Halfway_01, "Nuu", "Dialogue"), HookNuuDialogue }
        });

        Using(new Hook(
            AccessTools.Method(
                typeof(JournalQuestTarget),
                nameof(JournalQuestTarget.GetCompletionAmount)
            ), BossKillCountHook));

        QuestManager.GetQuest(Quests.Journal).ModifyTargetAmount(KillsForWish);
    }

    protected override void DoUnload()
    {
        _instance = null;
    }

    private void HookNuuDialogue(PlayMakerFSM fsm)
    {
        // Replace journal completion check
        FsmState completionEvaluateState = fsm.MustGetState("Completion Evaluate");
        completionEvaluateState.ReplaceAction(2, new LambdaAction
        {
            Method = () =>
            {
                if (GetBossKillCount() >= KillsForMemento) fsm.SendEvent("COMPLETED ALL");
            }
        });

        // Prevent needing a room reload between obtaining/completing quest and obtaining memento
        fsm.MustGetState("Talk Journal Give").RemoveFirstActionOfType<SetBoolValue>();
        fsm.MustGetState("Talk Journal Given").RemoveFirstActionOfType<SetBoolValue>();
        fsm.MustGetState("Quest Completed").RemoveFirstActionOfType<SetBoolValue>();
    }

    private static int BossKillCountHook(
        Func<JournalQuestTarget, QuestCompletionData.Completion, int> orig,
        JournalQuestTarget self,
        QuestCompletionData.Completion sourceCompletion)
    {
        return _instance!.GetBossKillCount();
    }

    private int GetBossKillCount()
    {
        return Bosses.Count(boss => PlayerDataAccess.EnemyJournalKillData.GetKillData(boss).Kills > 0);
    }
}