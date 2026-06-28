using Benchwarp.Data;
using ItemChanger.Silksong.Modules;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.StartDefs;
using PrepatcherPlugin;

namespace ItemChangerTesting.LocationTests;

internal class DivingBellKeyLocationTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Diving Bell Key Location",
        MenuDescription = "Tests giving various items from Diving_Bell_Key",
        Revision = 2026040600
    };

    public override void Setup(TestArgs args)
    {
        Modules.GetOrAdd<DivingBellAlwaysAvailableModule>();

        StartAt(new CoordinateStartDef()
        {
            SceneName = SceneNames.Dock_12,
            X = 32.74f,
            Y = 54.39f,
            MapZone = GlobalEnums.MapZone.NONE
        });
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Diving_Bell_Key)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!)
            .Add(Finder.GetItem(ItemNames.Flea)!));
        
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Start)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Diving_Bell_Key)!));
    }

    protected override void OnEnterGame()
    {
        base.OnEnterGame();
        
        PlayerDataAccess.blackThreadWorld = true;
        PlayerDataAccess.act3_wokeUp = true;
        PlayerDataAccess.act3_enclaveWakeSceneCompleted = true;
        PlayerDataAccess.BallowMovedToDivingBell = true;

        PlayerDataAccess.hasSuperJump = true;
    }
}