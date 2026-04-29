using ItemChanger.Silksong.Modules;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.LocationTests;

internal class SprintmasterMementoLocationTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Sprintmaster - Bonus Memento",
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
            .Wrap().Add(Finder.GetItem(ItemNames.Flea)!));

        Profile.AddPlacement(Finder.GetLocation(LocationNames.Sprintmaster_Memento)!
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
        PlayerData.instance.SprintMasterExtraRaceAvailable = true;
    }
}