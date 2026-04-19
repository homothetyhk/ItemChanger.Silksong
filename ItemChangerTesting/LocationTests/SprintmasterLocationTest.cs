using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Modules;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.LocationTests;

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

        Profile.AddPlacement(new SprintmasterLocation
        {
            SceneName = "Sprintmaster_Cave",
            Name = LocationNames.Beast_Shard__Sprintmaster_Race_2,
            IsQuestCompletion = false,
        }.Wrap().Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!));
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

        Profile.AddPlacement(new SprintmasterLocation
        {
            SceneName = "Sprintmaster_Cave",
            Name = LocationNames.Mask_Shard__Sprintmaster,
            IsQuestCompletion = true,
        }.Wrap().Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!));
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
/// Tests the full skip flow via <see cref="SprintmasterSkipModule"/>.
/// All three IC placements (Race 1 Rosary String + Race 2 Beast Shard + Mask Shard) are registered.
/// On talking to Sprintmaster choose YES at the skip prompt to jump straight to the final race.
/// Winning the final race should yield all three IC items.
/// </summary>
internal class SprintmasterSkipTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Sprintmaster - Skip Module (all rewards)",
        MenuDescription = "Tests skip prompt. Choose YES, win final race, expect three IC items (Race 1, Race 2, Mask Shard).",
        Revision = 2026041700,
    };

    public override void Setup(TestArgs args)
    {
        StartAt(new CoordinateStartDef() { SceneName = "Sprintmaster_Cave", X = 60.82f, Y = 8.57f, MapZone = GlobalEnums.MapZone.NONE });

        Profile.AddPlacement(new SprintmasterLocation
        {
            SceneName = "Sprintmaster_Cave",
            Name = LocationNames.Rosary_String__Sprintmaster_Race_1,
            IsQuestCompletion = false,
        }.Wrap().Add(Finder.GetItem(ItemNames.Compass)!));

        Profile.AddPlacement(new SprintmasterLocation
        {
            SceneName = "Sprintmaster_Cave",
            Name = LocationNames.Beast_Shard__Sprintmaster_Race_2,
            IsQuestCompletion = false,
        }.Wrap().Add(Finder.GetItem(ItemNames.Crest_of_Beast)!));

        Profile.AddPlacement(new SprintmasterLocation
        {
            SceneName = "Sprintmaster_Cave",
            Name = LocationNames.Mask_Shard__Sprintmaster,
            IsQuestCompletion = true,
        }.Wrap().Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!));

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
