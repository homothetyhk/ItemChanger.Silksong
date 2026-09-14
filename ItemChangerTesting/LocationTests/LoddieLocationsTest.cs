using Benchwarp.Data;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.LocationTests;

internal class LoddieLocationsTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Loddie Locations",
        MenuDescription = "Tests modifying Loddie Challenges in-place to give Surgeon's_Key and Flea.",
        Revision = 2026050200
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Bone_12, PrimitiveGateNames.left1);
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Tool_Pouch__Loddie)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!)
            .Add(Finder.GetItem(ItemNames.Flea)!));
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Heavy_Rosary_Necklace__Loddie_Challenge_2)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!)
            .Add(Finder.GetItem(ItemNames.Flea)!));
    }

    protected override void OnEnterGame()
    {
        base.OnEnterGame();
        PlayerData.instance.IsPinGallerySetup = true;
        PlayerData.instance.SetInt(nameof(PlayerData.instance.geo), 9999999);
    }
}