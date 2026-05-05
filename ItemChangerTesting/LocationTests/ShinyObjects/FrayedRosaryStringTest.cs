using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.LocationTests;

internal class FrayedRosaryStringTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Modify Frayed_Rosary__The_Slab_Choral_Entrance",
        MenuDescription = "Tests modifying Frayed_Rosary__The_Slab_Choral_Entrance shiny in-place to give Surgeon's_Key and Flea.",
        Revision = 2026042200,
    };

    public override void Setup(TestArgs args)
    {
        StartAt(new CoordinateStartDef()
        {
            SceneName = SceneNames.Slab_02,
            X = 61.77f,
            Y = 17.17f,
            MapZone = GlobalEnums.MapZone.NONE
        });
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Frayed_Rosary__The_Slab_Choral_Entrance)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!)
            .Add(Finder.GetItem(ItemNames.Flea)!));

    }
}
