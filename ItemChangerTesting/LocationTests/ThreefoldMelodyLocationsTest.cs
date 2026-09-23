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
        MenuDescription = "Tests giving items from the Threefold Melody locations in Cog_09, Hang...",
        Revision = 2026092200,
    };

    public override void Setup(TestArgs args)
    {
        StartAt(new CoordinateStartDef() { SceneName = "Cog_09", X = 31.11f, Y = 50.58f, MapZone = GlobalEnums.MapZone.COG_CORE });
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Architect_s_Melody)!.Wrap()
            .WithVariousItems().WithAllPersistent());
        Placement needolin = new CoordinateLocation
        {
            Name = "Needolin",
            SceneName = SceneNames.Cog_09,
            X = 28.11f,
            Y = 50.58f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
        }.Wrap().Add(Finder.GetItem(ItemNames.Needolin)!);
        needolin.AddTag(new PlacementItemsHintBoxTag());
        Profile.AddPlacement(needolin);
    }

    protected override void OnEnterGame()
    {
        base.OnEnterGame();

        PlayerData.instance.hasDash = true;
        PlayerData.instance.hasWalljump = true;
        PlayerData.instance.hasHarpoonDash = true;
        PlayerData.instance.silkRegenMax = 1;
    }
}
