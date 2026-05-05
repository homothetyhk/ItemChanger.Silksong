using Benchwarp.Data;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.LocationTests;

internal class ClawMirrorTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Modified Claw_Mirror",
        MenuDescription = "Tests modifying Claw_Mirror in-place to give Surgeon's_Key and Flea. (requires fighting trobbio)",
        Revision = 2026042000,
    };

    public override void Setup(TestArgs args)
    {
        StartAt(new CoordinateStartDef()
        {
            SceneName = SceneNames.Library_13,
            X = 54.30f,
            Y = 14.57f,
            MapZone = GlobalEnums.MapZone.NONE
        });
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Claw_Mirror)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!)
            .Add(Finder.GetItem(ItemNames.Flea)!));
    }

    protected override void OnEnterGame()
    {
        base.OnEnterGame();
        PlayerData.instance.encounteredTrobbio = true;
        PlayerData.instance.defeatedTrobbio = false;
    }
}
