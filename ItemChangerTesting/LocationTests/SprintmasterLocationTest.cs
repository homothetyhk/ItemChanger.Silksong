using ItemChanger.Silksong.Modules;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;
using ItemChangerTesting.ModuleTests;

namespace ItemChangerTesting.LocationTests;

/// <summary>
/// Tests the bonus Memento reward hook (Give Reward FSM state, Extra Track path).
/// Requires <see cref="PlayerData.SprintMasterExtraRaceAvailable"/> (set after completing the main quest
/// and delivering three hearts to the snail shamans). Accept the bonus race offer and win to receive
/// the IC item instead of the vanilla Sprintmaster Memento.
/// </summary>
internal class SprintmasterMementoLocationTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Sprintmaster - Bonus Memento",
        MenuDescription = "Hooks bonus Memento reward. Accept the extra race offer and win to receive IC item.",
        Revision = 2026042200,
    };

    public override void Setup(TestArgs args)
    {
        StartAt(new CoordinateStartDef() { SceneName = "Sprintmaster_Cave", X = 60.82f, Y = 8.57f, MapZone = GlobalEnums.MapZone.NONE });

        Profile.AddPlacement(Finder.GetLocation(LocationNames.Sprintmaster_Memento)!
            .Wrap().Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!));
    }

    protected override void OnEnterGame()
    {
        base.OnEnterGame();

        PlayerData.instance.silkRegenMax = 3;
        PlayerData.instance.hasDash = true;
        PlayerData.instance.hasWalljump = true;
        PlayerData.instance.hasDoubleJump = true;
        PlayerData.instance.hasHarpoonDash = true;
        PlayerData.instance.HasWildsMap = true;
        PlayerData.instance.SprintMasterExtraRaceAvailable = true;
    }
}

/// <summary>
/// Logs all Sprintmaster FSM state transitions for the bonus memento race.
/// Complete the race and check BepInEx/LogOutput.log for [Sprintmaster Memento FSM] entries
/// to identify which state delivers the memento reward.
/// </summary>
internal class SprintmasterMementoLoggerTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Sprintmaster - Memento FSM Logger",
        MenuDescription = "Logs all FSM state entries for the bonus memento race. Complete the race and check BepInEx log.",
        Revision = 2026042200,
    };

    public override void Setup(TestArgs args)
    {
        StartAt(new CoordinateStartDef() { SceneName = "Sprintmaster_Cave", X = 60.82f, Y = 8.57f, MapZone = GlobalEnums.MapZone.NONE });
        Profile.Modules.GetOrAdd<SprintmasterMementoFsmLoggerModule>();
    }

    protected override void OnEnterGame()
    {
        base.OnEnterGame();

        PlayerData.instance.silkRegenMax = 3;
        PlayerData.instance.hasDash = true;
        PlayerData.instance.hasWalljump = true;
        PlayerData.instance.hasDoubleJump = true;
        PlayerData.instance.hasHarpoonDash = true;
        PlayerData.instance.HasWildsMap = true;
        PlayerData.instance.SprintMasterExtraRaceAvailable = true;
    }
}

/// <summary>
/// Tests the Race 2 Beast Shard reward hook (Give Reward FSM state, IsQuestCompletion = false).
/// Win Race 2 to receive the IC item instead of the vanilla Beast Shard.
/// Race 1 (Rosary String) should still be given normally.
/// </summary>
internal class SprintmasterRace2LocationTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Sprintmaster - Race 2 (Beast Shard)",
        MenuDescription = "Hooks Race 2 Beast Shard reward. Win Race 1 normally, then win Race 2 to receive IC item.",
        Revision = 2026041600,
    };

    public override void Setup(TestArgs args)
    {
        StartAt(new CoordinateStartDef() { SceneName = "Sprintmaster_Cave", X = 60.82f, Y = 8.57f, MapZone = GlobalEnums.MapZone.NONE });

        Profile.AddPlacement(Finder.GetLocation(LocationNames.Beast_Shard__Sprintmaster_Race_2)!
            .Wrap().Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!));
    }

    protected override void OnEnterGame()
    {
        base.OnEnterGame();

        PlayerData.instance.silkRegenMax = 3;
        PlayerData.instance.hasDash = true;
        PlayerData.instance.hasWalljump = true;
        PlayerData.instance.hasDoubleJump = true;
        PlayerData.instance.hasHarpoonDash = true;
        PlayerData.instance.HasWildsMap = true;
    }
}

/// <summary>
/// Tests the final quest Mask Shard reward hook (End Dialogue 3 FSM state, IsQuestCompletion = true).
/// Complete all races to trigger quest completion and receive the IC item.
/// </summary>
internal class SprintmasterQuestLocationTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Sprintmaster - Quest Completion (Mask Shard)",
        MenuDescription = "Hooks final quest Mask Shard reward. Complete all races to receive IC item instead.",
        Revision = 2026041600,
    };

    public override void Setup(TestArgs args)
    {
        StartAt(new CoordinateStartDef() { SceneName = "Sprintmaster_Cave", X = 60.82f, Y = 8.57f, MapZone = GlobalEnums.MapZone.NONE });

        Profile.AddPlacement(Finder.GetLocation(LocationNames.Mask_Shard__Sprintmaster)!
            .Wrap().Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!));
    }

    protected override void OnEnterGame()
    {
        base.OnEnterGame();

        PlayerData.instance.silkRegenMax = 3;
        PlayerData.instance.hasDash = true;
        PlayerData.instance.hasWalljump = true;
        PlayerData.instance.hasDoubleJump = true;
        PlayerData.instance.hasHarpoonDash = true;
        PlayerData.instance.HasWildsMap = true;
    }
}

/// <summary>
/// Tests <see cref="SprintmasterSkipModule"/> with all three IC placements registered.
/// Either choice at the skip prompt should yield all three IC items:
/// choose YES to receive Race 1 and Race 2 rewards immediately on quest completion,
/// or choose NO to win each race normally and collect one IC item per race.
/// </summary>
internal class SprintmasterSkipModuleTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Sprintmaster - Skip Module (all rewards)",
        MenuDescription = "All three IC placements active. Choose YES to skip races or NO to play normally — all three IC items should be received either way.",
        Revision = 2026042200,
    };

    public override void Setup(TestArgs args)
    {
        StartAt(new CoordinateStartDef() { SceneName = "Sprintmaster_Cave", X = 60.82f, Y = 8.57f, MapZone = GlobalEnums.MapZone.NONE });

        Profile.AddPlacement(Finder.GetLocation(LocationNames.Rosary_String__Sprintmaster_Race_1)!
            .Wrap().Add(Finder.GetItem(ItemNames.Compass)!));

        Profile.AddPlacement(Finder.GetLocation(LocationNames.Beast_Shard__Sprintmaster_Race_2)!
            .Wrap().Add(Finder.GetItem(ItemNames.Crest_of_Beast)!));

        Profile.AddPlacement(Finder.GetLocation(LocationNames.Mask_Shard__Sprintmaster)!
            .Wrap().Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!));

        Profile.Modules.GetOrAdd<SprintmasterSkipModule>();
    }

    protected override void OnEnterGame()
    {
        base.OnEnterGame();

        PlayerData.instance.silkRegenMax = 3;
        PlayerData.instance.hasDash = true;
        PlayerData.instance.hasWalljump = true;
        PlayerData.instance.hasDoubleJump = true;
        PlayerData.instance.hasHarpoonDash = true;
        PlayerData.instance.HasWildsMap = true;
    }
}
