using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.LocationTests;

internal class Crawbelltest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Modified Crawbell",
        MenuDescription = "Tests modifying Crawbell in-place to give Surgeon's_Key and Flea.",
        Revision = 2026041000,
    };

    public override void Setup(TestArgs args)
    {
        StartAt(new CoordinateStartDef()
        {
            SceneName = SceneNames.Room_CrowCourt_02,
            X = 14.52f,
            Y = 45.57f,
            MapZone = GlobalEnums.MapZone.NONE
        });
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Crawbell)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!)
            .Add(Finder.GetItem(ItemNames.Flea)!));
    }

    protected override void OnEnterGame()
    {
        base.OnEnterGame();
        PlayerData.instance.blackThreadWorld = true;
        PlayerData.instance.act3_wokeUp = true;
        PlayerData.instance.act3_enclaveWakeSceneCompleted = true;
    }
}
