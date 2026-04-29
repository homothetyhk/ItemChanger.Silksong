using Benchwarp.Data;
using ItemChanger.Silksong.RawData;

namespace ItemChangerTesting.LocationTests;
/* may require finding a way to set a wish to be active for testing [Pain, Anguish and Misery]
internal class ClawMirrorsTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Modified Claw_Mirrors",
        MenuDescription = "Tests modifying Claw_Mirrors in-place to give Surgeon's_Key and Flea. (requires fighting tormented trobbio)",
        Revision = 2026042000,
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Library_13, PrimitiveGateNames.right1);
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Claw_Mirrors)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!)
            .Add(Finder.GetItem(ItemNames.Flea)!));
    }

    protected override void OnEnterGame()
    {
        base.OnEnterGame();
        PlayerData.instance.blackThreadWorld = true;
        PlayerData.instance.act3_wokeUp = true;
        PlayerData.instance.act3_enclaveWakeSceneCompleted = true;
        PlayerData.instance.encounteredTormentedTrobbio = true;
        PlayerData.instance.defeatedTormentedTrobbio = false;
    }
}
*/