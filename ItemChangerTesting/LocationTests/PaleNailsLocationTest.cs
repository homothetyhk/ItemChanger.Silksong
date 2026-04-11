using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.LocationTests;

internal class PaleNailsLocationTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Pale Nails Location",
        MenuDescription = "Tests giving items at Pale Nails",
        Revision = 2026041000,
    };

    protected override void OnEnterGame() => StartAct3();

    public override void Setup(TestArgs args)
    {
        StartAt(new CoordinateStartDef()
        {
            SceneName = SceneNames.Cradle_03_Destroyed,
            MapZone = GlobalEnums.MapZone.CRADLE,
            X = 43,
            Y = 144,
        });
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Pale_Nails)!.Wrap().Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!));
    }
}
