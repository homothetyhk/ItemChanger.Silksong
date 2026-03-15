using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.LocationTests;

internal class CrestLocationTest : Test
{
    // TODO: check multiple async UIDefs
    // TODO: check respawning items

    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Crest Location",
        MenuDescription = "Tests giving various items from Crest_of_Shaman",
        Revision = 2026031400,
    };

    public override void Setup(TestArgs args)
    {
        StartAt(new CoordinateStartDef() { SceneName = SceneNames.Tut_05, X = 283.79f, Y = 58.77f, MapZone = GlobalEnums.MapZone.NONE });
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Crest_of_Shaman)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!));
    }
}
