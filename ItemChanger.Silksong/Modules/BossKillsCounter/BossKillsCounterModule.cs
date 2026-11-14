using Benchwarp.Data;
using ItemChanger.Modules;
using ItemChanger.Serialization;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.Serialization;
using System.Collections.ObjectModel;

namespace ItemChanger.Silksong.Modules.BossKillsCounter;

/// <summary>
/// A module which counts how many bosses have been defeated.
/// </summary>
[SingletonModule]
public class BossKillsCounterModule : Module
{
    /// <summary>
    /// The total number of bosses killed, as determined by the list of boss counters.
    /// </summary>
    public int GetKillCount() => BossCounters.Values.Sum(counter => counter.GetKillCount());

    /// <summary>
    /// Bosses tracked by the module, keyed by name. A single BossDefinition can count multiple kills.
    /// </summary>
    public Dictionary<string, IBossCounter> BossCounters { get; init; } = [];

    /// <summary>
    /// <para>Note that several of the bosses in this list are missable in vanilla playthroughs (Garmond + Zaza,
    /// Shakra). However, these are not missable in Rando due to the ability to return to Act 2. </para>
    /// </summary>
    public static ReadOnlyDictionary<string, IBossCounter> DefaultBossCounters { get; } = new(new Dictionary<string, IBossCounter>
    {
        [JournalEntries.Bone_Beast] = new JournalBossCounter(JournalEntries.Bone_Beast), // Bell Beast
        [JournalEntries.Song_Golem] = new JournalBossCounter(JournalEntries.Song_Golem), // Fourth Chorus
        [JournalEntries.Coral_Conch_Driller_Giant] = new JournalBossCounter(JournalEntries.Coral_Conch_Driller_Giant, 2), // Great Conchflies
        [JournalEntries.Last_Judge] = new JournalBossCounter(JournalEntries.Last_Judge), // Last Judge
        [JournalEntries.Vampire_Gnat] = new JournalBossCounter(JournalEntries.Vampire_Gnat), // Moorwing
        [JournalEntries.Phantom] = new JournalBossCounter(JournalEntries.Phantom), // Phantom
        [JournalEntries.Bone_Flyer_Giant] = new JournalBossCounter(JournalEntries.Bone_Flyer_Giant, 2), // Savage Beastfly
        [JournalEntries.Splinter_Queen] = new JournalBossCounter(JournalEntries.Splinter_Queen), // Sister Splinter
        [JournalEntries.Skull_King] = new JournalBossCounter(JournalEntries.Skull_King), // Skull Tyrant
        [JournalEntries.Spinner_Boss] = new JournalBossCounter(JournalEntries.Spinner_Boss), // Widow
        [JournalEntries.Slab_Fly_Broodmother] = new JournalBossCounter(JournalEntries.Slab_Fly_Broodmother), // Broodmother
        [JournalEntries.Clockwork_Dancer] = new JournalBossCounter(JournalEntries.Clockwork_Dancer), // Cogwork Dancers
        [JournalEntries.Roachkeeper_Chef] = new JournalBossCounter(JournalEntries.Roachkeeper_Chef), // Disgraced Chef Lugoli
        [JournalEntries.Wisp_Pyre_Effigy] = new JournalBossCounter(JournalEntries.Wisp_Pyre_Effigy), // Father of the Flame
        [JournalEntries.First_Weaver] = new JournalBossCounter(JournalEntries.First_Weaver), // First Sinner
        [JournalEntries.Dock_Guard_Thrower] = new JournalBossCounter(JournalEntries.Dock_Guard_Thrower), // Forebrothers Signis & Grom
        [JournalEntries.Garmond_Zaza] = new JournalBossCounter(JournalEntries.Garmond_Zaza), // Garmond & Zaza
        [JournalEntries.Silk_Boss] = new JournalBossCounter(JournalEntries.Silk_Boss), // Grand Mother Silk
        [JournalEntries.Swamp_Shaman] = new JournalBossCounter(JournalEntries.Swamp_Shaman), // Groal the Great
        [JournalEntries.Song_Knight] = new JournalBossCounter(JournalEntries.Song_Knight), // Second Sentinel
        [JournalEntries.Shakra] = new JournalBossCounter(JournalEntries.Shakra), // Shakra
        [JournalEntries.Abyss_Mass] = new JournalBossCounter(JournalEntries.Abyss_Mass), // Summoned Saviour
        [JournalEntries.Conductor_Boss] = new JournalBossCounter(JournalEntries.Conductor_Boss), // The Unravelled
        [JournalEntries.Trobbio] = new JournalBossCounter(JournalEntries.Trobbio), // Trobbio
        [JournalEntries.Zap_Core_Enemy] = new JournalBossCounter(JournalEntries.Zap_Core_Enemy), // Voltvyrm
        [JournalEntries.Giant_Centipede] = new JournalBossCounter(JournalEntries.Giant_Centipede), // Bell Eater
        [JournalEntries.Clover_Dancer] = new JournalBossCounter(JournalEntries.Clover_Dancer), // Clover Dancers
        [JournalEntries.Crawfather] = new JournalBossCounter(JournalEntries.Crawfather), // Crawfather
        [JournalEntries.Coral_King] = new JournalBossCounter(JournalEntries.Coral_King), // Crust King Khann
        [JournalEntries.Bone_Hunter_Trapper] = new JournalBossCounter(JournalEntries.Bone_Hunter_Trapper), // Gurr the Outcast
        [JournalEntries.Garmond] = new JournalBossCounter(JournalEntries.Garmond), // Lost Garmond
        [JournalEntries.Lost_Lace] = new JournalBossCounter(JournalEntries.Lost_Lace), // Lost Lace
        [JournalEntries.Flower_Queen] = new JournalBossCounter(JournalEntries.Flower_Queen), // Nyleth
        [JournalEntries.Pinstress_Boss] = new JournalBossCounter(JournalEntries.Pinstress_Boss), // Pinstress
        [JournalEntries.Blue_Assistant] = new JournalBossCounter(JournalEntries.Blue_Assistant), // Plasmified Zango
        [JournalEntries.Seth] = new JournalBossCounter(JournalEntries.Seth), // Shrine Guardian Seth
        [JournalEntries.Hunter_Queen] = new JournalBossCounter(JournalEntries.Hunter_Queen), // Skarrsinger Karmelita
        [JournalEntries.Tormented_Trobbio] = new JournalBossCounter(JournalEntries.Tormented_Trobbio), // Tormented Trobbio
        [JournalEntries.Coral_Warrior_Grey] = new JournalBossCounter(JournalEntries.Coral_Warrior_Grey), // Watcher at the Edge

        [JournalEntries.Lace] = new SpecialBossCounter(JournalEntries.Lace,
            new Disjunction(new PDBool(nameof(PlayerData.defeatedLace1)), new PDBool(nameof(PlayerData.laceLeftDocks)),
                new PDBool(nameof(PlayerData.visitedCitadel))), // Lace-Docks, encounteredLace1Grotto also deactivates the Docks fight, but this seems to be unused
            new PDBool(nameof(PlayerData.defeatedLaceTower))), // Lace-Cradle
        [JournalEntries.Mossbone_Mother] = new SpecialBossCounter(JournalEntries.Mossbone_Mother,
            new PDBool(nameof(PlayerData.defeatedMossMother)), // Moss Mother-Grotto
            new CoalescingValueProvider<bool>(new ComponentFieldOption<BattleScene, bool>(SceneNames.Weave_03, "Boss Scene", nameof(BattleScene.completed)),
                new SDBool(SceneNames.Weave_03, "Boss Scene"))), // Moss Mother Duo-Weavenest
        [JournalEntries.Cloverstag_White] = new SpecialBossCounter(JournalEntries.Cloverstag_White,
            new Disjunction(new IntComparisonBool
            {
                ToCompare = new JournalKillDataKills(JournalEntries.Cloverstag_White),
                Amount = 0,
                Operator = Enums.ComparisonOperator.Gt, // Palestag
            }, new PDBool(nameof(PlayerData.defeatedCloverDancers)))), // locks out Palestag without granting entry
        // TODO: if Verdania is made reaccessible, Palestag should be replaced by a JournalBossCounter
    });

    internal static BossKillsCounterModule CreateDefault() => new() { BossCounters = new(DefaultBossCounters) };

    protected override void DoLoad()
    {
    }

    protected override void DoUnload()
    {
    }
}