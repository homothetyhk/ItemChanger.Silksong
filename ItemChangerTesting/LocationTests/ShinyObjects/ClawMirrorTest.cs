using Benchwarp.Data;
using ItemChanger.Silksong.RawData;

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
        StartNear(SceneNames.Library_13, PrimitiveGateNames.right1);
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
