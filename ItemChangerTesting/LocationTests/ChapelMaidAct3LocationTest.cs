using Benchwarp.Data;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.LocationTests;

internal class ChapelMaidAct3LocationTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Chapel Maid Act 3 Location",
        MenuDescription = "Tests Chapel Maid's location replacement in Act 3",
        Revision = 2026040600
    };

    public override void Setup(TestArgs args)
    {
        StartAt(new CoordinateStartDef()
        {
            SceneName = SceneNames.Bonetown,
            X = 80.32f,
            Y = 7.56f,
            MapZone = GlobalEnums.MapZone.NONE
        });
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Maiden_s_Soul)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!)
            .Add(Finder.GetItem(ItemNames.Flea)!));
    }

    protected override void OnEnterGame()
    {
        base.OnEnterGame();

        PlayerData.instance.churchKeeperIntro = true;
        PlayerData.instance.blackThreadWorld = true;
        PlayerData.instance.act3_enclaveWakeSceneCompleted = true;
        PlayerData.instance.act3_wokeUp = true;
    }
}