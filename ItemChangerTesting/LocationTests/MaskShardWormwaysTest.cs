using Benchwarp.Data;
using ItemChanger.Silksong.RawData;

namespace ItemChangerTesting.LocationTests;

/// <summary>
/// Spawns near Bellway_01 with Swift Step (sprint) and the Wormways Map so the
/// player can navigate to the Wormways mask shard location and verify the IC item.
/// Places a Flea at the Mask_Shard__Wormways location for easy identification.
/// </summary>
internal class MaskShardWormwaysTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Mask Shard - Wormways (Flea)",
        MenuDescription = "Spawns near Bellway_01 with sprint and Wormways map. " +
                          "Replaces the Wormways mask shard with a Flea.",
        Revision = 2026040901,
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Peak_04c, PrimitiveGateNames.left1);

        Profile.AddPlacement(
            Finder.GetLocation(LocationNames.Mask_Shard__Mount_Fay)!
                  .Wrap()
                  .Add(Finder.GetItem(ItemNames.Bellway__Greymoor)!));
    }

    protected override void OnEnterGame()
    {
        base.OnEnterGame();
        PlayerData.instance.hasDash = true;      // Swift Step (sprint)
        PlayerData.instance.HasCrawlMap = true;  // Wormways Map
        PlayerData.instance.hasDoubleJump = true; // Double Jump cause nice to have 
        PlayerData.instance.hasWalljump = true;
    }
}
