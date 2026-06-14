using Benchwarp.Data;
using ItemChanger.Silksong.RawData;

namespace ItemChangerTesting.LocationTests;

/// <summary>
/// Spawns near Library_05 to verify the BreakableContainerLocation for the
/// Whispering Vaults mask shard. Places a Flea at the location for easy identification.
/// Check BepInEx/LogOutput.log for the [BreakableContainerLocation] diagnostic line
/// which reports whether Breakable or BreakableHolder was found on the parent.
/// </summary>
internal class MaskShardWhisperingVaultsTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Mask Shard - Whispering Vaults (Flea)",
        MenuDescription = "Spawns near Library_05. Replaces the Whispering Vaults mask shard with a Flea.",
        Revision = 2026041001,
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Library_05, PrimitiveGateNames.left1);

        Profile.AddPlacement(
            Finder.GetLocation(LocationNames.Mask_Shard__Whispering_Vaults)!
                  .Wrap()
                  .Add(Finder.GetItem(ItemNames.Flea)!));
    }

    protected override void OnEnterGame()
    {
        base.OnEnterGame();
        PlayerData.instance.hasDash = true;
        PlayerData.instance.HasCrawlMap = true;
        PlayerData.instance.hasDoubleJump = true;
        PlayerData.instance.hasWalljump = true;
    }
}
