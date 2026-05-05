using Benchwarp.Data;
using ItemChanger.Silksong.RawData;

namespace ItemChangerTesting.LocationTests;

internal class CogheartPieceTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Modified Cogheart_Piece__Choral_Chambers",
        MenuDescription = "Tests modifying Cogheart_Piece__Choral_Chambers in-place to give Surgeon's_Key and Flea. (requires fighting voltvyrm)",
        Revision = 2026041300,
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Song_26, PrimitiveGateNames.right1);
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Cogheart_Piece__Choral_Chambers)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!)
            .Add(Finder.GetItem(ItemNames.Flea)!));
    }
}
