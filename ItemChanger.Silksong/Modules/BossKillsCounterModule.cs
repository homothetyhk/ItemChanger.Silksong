using System.Diagnostics.CodeAnalysis;
using ItemChanger.Modules;
using ItemChanger.Silksong.RawData;
using Newtonsoft.Json;
using PrepatcherPlugin;
using Module = ItemChanger.Modules.Module;

namespace ItemChanger.Silksong.Modules;

/// <summary>
/// A module which counts how many bosses have been defeated.
/// </summary>
[SingletonModule]
public class BossKillsCounterModule : Module
{
    /// <summary>
    /// A list of bosses which should count towards the boss kill count.
    /// </summary>
    [JsonIgnore]
    public IReadOnlyCollection<BossDefinition> BossDefinitions => BossDefinitionsInternal;

    [JsonProperty] private HashSet<BossDefinition> BossDefinitionsInternal { get; init; } = [];

    /// <summary>
    /// <para>Note that several of the bosses in this list are missable in vanilla playthroughs (Garmond + Zaza,
    /// Shakra). However, these are not missable in Rando due to the ability to return to Act 2. </para>
    /// The following bosses are excluded: <br/>
    /// * Palestag - Missable unless Lost Verdania is repeatable - TODO? <br/>
    /// * Lace (Deep Docks) - Can be skipped, not sure how this journal entry is handled in base game <br/>
    /// * Moss Mothers - Needs unique handling due to refight being a double fight, probably cannot use journal entry
    /// </summary>
    private static IReadOnlyCollection<BossDefinition> DefaultBosses =>
    [
        new JournalEntryBossDefinition(JournalEntries.Bone_Beast), // Bell Beast
        new JournalEntryBossDefinition(JournalEntries.Song_Golem), // Fourth Chorus
        new JournalEntryBossDefinition(JournalEntries.Coral_Conch_Driller_Giant, 2), // Great Conchflies
        new JournalEntryBossDefinition(JournalEntries.Lace), // TODO refight
        new JournalEntryBossDefinition(JournalEntries.Last_Judge),
        new JournalEntryBossDefinition(JournalEntries.Vampire_Gnat), // Moorwing
        new JournalEntryBossDefinition(JournalEntries.Mossbone_Mother), // Moss Mother - TODO refight
        new JournalEntryBossDefinition(JournalEntries.Phantom),
        new JournalEntryBossDefinition(JournalEntries.Bone_Flyer_Giant, 2), // Savage Beastfly
        new JournalEntryBossDefinition(JournalEntries.Splinter_Queen), // Sister Splinter
        new JournalEntryBossDefinition(JournalEntries.Skull_King), // Skull Tyrant
        new JournalEntryBossDefinition(JournalEntries.Spinner_Boss), // Widow
        new JournalEntryBossDefinition(JournalEntries.Slab_Fly_Broodmother),
        new JournalEntryBossDefinition(JournalEntries.Clockwork_Dancer), // Cogwork Dancers
        new JournalEntryBossDefinition(JournalEntries.Roachkeeper_Chef), // Disgraced Chef Lugoli
        new JournalEntryBossDefinition(JournalEntries.Wisp_Pyre_Effigy), // Father of the Flame
        new JournalEntryBossDefinition(JournalEntries.First_Weaver), // First Sinner
        new JournalEntryBossDefinition(JournalEntries.Dock_Guard_Thrower), // Forebrothers Signis & Grom
        new JournalEntryBossDefinition(JournalEntries.Garmond_Zaza), // Garmond & Zaza
        new JournalEntryBossDefinition(JournalEntries.Silk_Boss), // Grand Mother Silk
        new JournalEntryBossDefinition(JournalEntries.Swamp_Shaman), // Groal the Great
        new JournalEntryBossDefinition(JournalEntries.Song_Knight), // Second Sentinel
        new JournalEntryBossDefinition(JournalEntries.Shakra),
        new JournalEntryBossDefinition(JournalEntries.Abyss_Mass), // Summoned Saviour
        new JournalEntryBossDefinition(JournalEntries.Conductor_Boss), // The Unravelled
        new JournalEntryBossDefinition(JournalEntries.Trobbio),
        new JournalEntryBossDefinition(JournalEntries.Zap_Core_Enemy), // Voltvyrm
        new JournalEntryBossDefinition(JournalEntries.Giant_Centipede), // Bell Eater
        new JournalEntryBossDefinition(JournalEntries.Clover_Dancer), // Clover Dancers
        new JournalEntryBossDefinition(JournalEntries.Crawfather),
        new JournalEntryBossDefinition(JournalEntries.Coral_King), // Crust King Khann
        new JournalEntryBossDefinition(JournalEntries.Bone_Hunter_Trapper), // Gurr the Outcast
        new JournalEntryBossDefinition(JournalEntries.Garmond), // Lost Garmond
        new JournalEntryBossDefinition(JournalEntries.Lost_Lace),
        new JournalEntryBossDefinition(JournalEntries.Flower_Queen), // Nyleth
        new JournalEntryBossDefinition(JournalEntries.Pinstress_Boss),
        new JournalEntryBossDefinition(JournalEntries.Blue_Assistant), // Plasmified Zango
        new JournalEntryBossDefinition(JournalEntries.Seth), // Shrine Guardian Seth
        new JournalEntryBossDefinition(JournalEntries.Hunter_Queen), // Skarrsinger Karmelita
        new JournalEntryBossDefinition(JournalEntries.Tormented_Trobbio),
        new JournalEntryBossDefinition(JournalEntries.Coral_Warrior_Grey), // Watcher at the Edge
    ];


    protected override void DoLoad()
    {
    }

    protected override void DoUnload()
    {
    }

    internal void AddDefaultBosses()
    {
        BossDefinitionsInternal.UnionWith(DefaultBosses);
    }

    /// <summary>
    /// Add a boss definition to the boss collection.
    /// </summary>
    /// <returns>True if the definition was added to the collection. False if the definition was already present.
    /// </returns>
    /// <exception cref="ArgumentException">Thrown if <c>def</c> is a <see cref="JournalEntryBossDefinition"/> and a
    /// definition for that journal entry already exists in the collection.</exception>
    public bool AddBossDefinition(BossDefinition def)
    {
        // Don't allow adding the same journal entry definition twice
        if (def is JournalEntryBossDefinition journalDef
            && BossDefinitionsInternal.Any(internalDef =>
                internalDef is JournalEntryBossDefinition internalJournalDef
                && journalDef.BossName == internalJournalDef.BossName))
        {
            throw new ArgumentException(nameof(def), $"Boss definition for {journalDef.BossName} already exists");
        }

        return BossDefinitionsInternal.Add(def);
    }

    /// <summary>
    /// Removes a boss definition from the boss collection.
    /// </summary>
    /// <returns>True if the definition was removed from the collection. False if the definition was not present.
    /// </returns>
    public bool RemoveBossDefinition(BossDefinition def) => BossDefinitionsInternal.Remove(def);

    public int BossKillCount
    {
        get { return BossDefinitions.Sum(killDef => killDef.BossesKilledContribution); }
    }
}

/// <summary>
/// Abstract definition of a boss. Used by <see cref="BossKillsCounterModule"/> to count how many bosses have been
/// defeated.
/// </summary>
public abstract class BossDefinition
{
    /// <summary>
    /// Returns the number of boss kills contributed by this definition. Typically, this should return <c>0</c>
    /// when the boss hasn't been killed and <c>1</c> when the boss has been killed, but can be greater e.g. for
    /// boss refights.
    /// </summary>
    public abstract int BossesKilledContribution { get; }
}

/// <summary>
/// Definition for a boss whose defeat is tracked by obtaining its journal entry.
/// </summary>
public class JournalEntryBossDefinition : BossDefinition
{
    /// <inheritdoc cref="JournalEntryBossDefinition" />
    /// <param name="bossName">Internal name for the journal entry.</param>
    /// <param name="maxContrib">Number of boss kills that should contribute to the count. Set > 1 to include
    /// boss refights which grant the same journal entry.</param>
    [SetsRequiredMembers]
    public JournalEntryBossDefinition(string bossName, int maxContrib = 1)
    {
        BossName = bossName;
        MaxContribution = maxContrib;
    }

    /// <inheritdoc cref="JournalEntryBossDefinition" />
    public JournalEntryBossDefinition()
    {
    }

    /// <summary>
    /// Internal name of the boss
    /// </summary>
    public required string BossName { get; init; }

    /// <summary>
    /// Maximum number of kills that this boss's journal entry can contribute to the boss kill count. Will typically
    /// equal the number of distinct fights this boss has.
    /// </summary>
    public int MaxContribution { get; init; } = 1;

    /// <inheritdoc/>
    public override int BossesKilledContribution
    {
        get
        {
            int kills = PlayerDataAccess.EnemyJournalKillData.GetKillData(BossName).Kills;
            return kills > MaxContribution ? MaxContribution : kills;
        }
    }
}