using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Placements;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;
using ItemChanger.Silksong.Tags;

namespace ItemChangerTesting.LocationTests;

internal class ThreeFoldMelodyLocationsTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Threefold Melody locations",
        MenuDescription = "Tests giving items from the Threefold Melody locations in Cog_09, Hang_12, &...",
        Revision = 2026092200,
    };

    public override void Setup(TestArgs args)
    {
        StartAt(new CoordinateStartDef() { SceneName = "Cog_09", X = 31.11f, Y = 50.58f, MapZone = GlobalEnums.MapZone.COG_CORE });
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Architect_s_Melody)!.Wrap()
            .WithVariousItems().WithAllPersistent());
        Placement needolin1 = new CoordinateLocation
        {
            Name = "Needolin",
            SceneName = SceneNames.Cog_09,
            X = 28.11f,
            Y = 50.58f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
        }.Wrap().Add(Finder.GetItem(ItemNames.Needolin)!);
        needolin1.AddTag(new PlacementItemsHintBoxTag());
        Profile.AddPlacement(needolin1);

        Profile.AddPlacement(Finder.GetLocation(LocationNames.Conductor_s_Melody)!.Wrap()
            .WithVariousItems().WithAllPersistent());
        Placement needolin2 = new CoordinateLocation
        {
            Name = "Needolin",
            SceneName = SceneNames.Hang_12,
            X = 28.11f,
            Y = 4.58f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
        }.Wrap().Add(Finder.GetItem(ItemNames.Needolin)!);
        needolin2.AddTag(new PlacementItemsHintBoxTag());
        Profile.AddPlacement(needolin2);
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
    }
}
