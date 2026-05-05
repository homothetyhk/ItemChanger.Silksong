using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.LocationTests;

internal class BilewaterCocoonMemoryLocketTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Modify Memory_Locket__Bilewater_Cocoon_Corpse",
        MenuDescription = "Tests modifying Memory_Locket__Bilewater_Cocoon_Corpse in-place to give Surgeon's_Key and Flea.",
        Revision = 2026041200,
    };

    public override void Setup(TestArgs args)
    {
        StartAt(new CoordinateStartDef()
        {
            SceneName = SceneNames.Shadow_27,
            X = 192.63f,
            Y = 9.58f,
            MapZone = GlobalEnums.MapZone.NONE
        });
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Memory_Locket__Bilewater_Cocoon_Corpse)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!)
            .Add(Finder.GetItem(ItemNames.Flea)!));

    }
}
