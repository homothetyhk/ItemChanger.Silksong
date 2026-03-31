using Benchwarp.Data;
using ItemChanger;
using ItemChanger.Locations;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.LocationTests;

internal class CorpsecrestSilkSkillRegressionTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Corpsecrest Silk Regression",
        MenuDescription = "Tests that collecting a non-skill item at Crest_of_Shaman does not break a later silk skill pickup.",
        Revision = 2026033100,
    };

    public override void Setup(TestArgs args)
    {
        StartAt(new CoordinateStartDef() { SceneName = SceneNames.Tut_05, X = 283.79f, Y = 58.77f, MapZone = GlobalEnums.MapZone.NONE });

        Profile.AddPlacement(Finder.GetLocation(LocationNames.Crest_of_Shaman)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!));

        Profile.AddPlacement(new CoordinateLocation
        {
            Name = "Cross Stitch after corpse",
            SceneName = SceneNames.Tut_05,
            X = 288f,
            Y = 58.77f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
        }.Wrap().Add(Finder.GetItem(ItemNames.Cross_Stitch)!));
    }
}
