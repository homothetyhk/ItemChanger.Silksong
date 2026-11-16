using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;
using PrepatcherPlugin;

namespace ItemChangerTesting.LocationTests;

internal class ThreeFoldMelodyLocationsTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Threefold Melody locations",
        MenuDescription = "Tests giving items from the Threefold Melody locations in Cog_09, Hang_12, & Library_08",
        Revision = 2026092200,
    };

    public override void Setup(TestArgs args)
    {
        StartAt(new CoordinateStartDef() { SceneName = "Cog_09", X = 31.11f, Y = 50.58f, MapZone = GlobalEnums.MapZone.COG_CORE });
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Architect_s_Melody)!.Wrap()
            .WithVariousItems().WithAllPersistent());

        Profile.AddPlacement(Finder.GetLocation(LocationNames.Conductor_s_Melody)!.Wrap()
            .WithVariousItems().WithAllPersistent());

        Profile.AddPlacement(Finder.GetLocation(LocationNames.Start)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Sacred_Cylinder)!));
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Vaultkeeper_s_Melody)!.Wrap()
            .WithVariousItems().WithAllPersistent());
    }

    protected override void OnEnterGame()
    {
        base.OnEnterGame();

        PlayerData.instance.hasDash = true;
        PlayerData.instance.hasWalljump = true;
        PlayerData.instance.hasHarpoonDash = true;
        PlayerData.instance.silkRegenMax = 1;
    }
    public override IEnumerable<(string, Action)> TestMethods()
    {
        yield return ("Start Quest", () => QuestUtil.SetAccepted(Quests.Citadel_Ascent_Melodies));
        yield return ("Give Needolin", () => PlayerDataAccess.hasNeedolin = true);
        yield return ("Remove Needolin", () => PlayerDataAccess.hasNeedolin = false);
        yield return ("Give Melodies", () =>
        {
            PlayerDataAccess.HasMelodyArchitect = true;
            PlayerDataAccess.HasMelodyConductor = true;
            PlayerDataAccess.HasMelodyLibrarian = true;
        });
    }
}
