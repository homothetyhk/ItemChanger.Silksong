using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.LocationTests;

internal class WeaverBurialSpireAbyss : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Weaver Burial Spire Abyss",
        MenuDescription = "Tests Void Tendrils event removed from Abyss Weaver Corpse",
        Revision = 2026040700
    };
    
    public override void Setup(TestArgs args)
    {
        StartAt(new CoordinateStartDef()
        {
            SceneName = SceneNames.Abyss_08,
            X = 77.63f,
            Y = 10.20f,
            MapZone = GlobalEnums.MapZone.NONE
        });

        Profile.AddPlacement(Finder.GetLocation(LocationNames.Silk_Soar)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Rosary_String)!));
    }
}