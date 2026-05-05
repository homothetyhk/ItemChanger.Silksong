using Benchwarp.Data;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.LocationTests;

internal class MagnetiteDiceTestShiny : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Magnetite_Dice Location (shiny)",
        MenuDescription = "Tests modifying Magnetite_Dice in-place to give Surgeon's_Key and Flea. (shiny location)",
        Revision = 2026050100
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Coral_33, PrimitiveGateNames.right1);
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Magnetite_Dice)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!)
            .Add(Finder.GetItem(ItemNames.Flea)!));
    }

    protected override void OnEnterGame()
    {
        base.OnEnterGame();

        PlayerData.instance.SetInt(nameof(PlayerData.instance.dicePilgrimState), 1);
    }
}