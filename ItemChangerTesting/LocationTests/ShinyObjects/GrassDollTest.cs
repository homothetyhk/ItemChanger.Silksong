using Benchwarp.Data;
using ItemChanger.Silksong.RawData;

namespace ItemChangerTesting.LocationTests;

internal class GrassDollTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Modify Grass_Doll",
        MenuDescription = "Tests modifying Grass_Doll shiny in-place to give Surgeon's_Key and Flea.",
        Revision = 2026041200,
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Bone_East_18b, PrimitiveGateNames.top1);
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Grass_Doll)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!)
            .Add(Finder.GetItem(ItemNames.Flea)!));

    }
}
