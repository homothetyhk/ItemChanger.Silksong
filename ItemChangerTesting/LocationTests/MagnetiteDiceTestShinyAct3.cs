using Benchwarp.Data;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.LocationTests;

internal class MagnetiteDiceTestShinyAct3 : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Magnetite_Dice Location (shiny act 3)",
        MenuDescription = "Tests modifying Magnetite_Dice in-place to give Surgeon's_Key and Flea. (shiny location act 3)",
        Revision = 2026050200
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

        PlayerData.instance.blackThreadWorld = true;
        PlayerData.instance.act3_wokeUp = true;
        PlayerData.instance.act3_enclaveWakeSceneCompleted = true;
    }
}