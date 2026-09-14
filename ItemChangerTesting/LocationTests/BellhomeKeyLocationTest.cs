using Benchwarp.Data;
using ItemChanger.Silksong.Modules;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.StartDefs;
using PrepatcherPlugin;

namespace ItemChangerTesting.LocationTests;

internal class BellhomeKeyLocationTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Bellhome Key Location",
        MenuDescription = "Tests giving various items from Bellhome_Key",
        Revision = 2026091300
    };

    public override void Setup(TestArgs args)
    {
        Modules.GetOrAdd<DivingBellAlwaysAvailableModule>();

        StartAt(new CoordinateStartDef()
        {
            SceneName = SceneNames.Belltown,
            X = 78.26f,
            Y = 7.57f,
            MapZone = GlobalEnums.MapZone.BELLTOWN
        });
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Bellhome_Key)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!)
            .Add(Finder.GetItem(ItemNames.Flea)!)
            .Add(Finder.GetItem(ItemNames.Crest_of_Architect)!).WithAllPersistent());
        
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Start)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Bellhome_Key)!));
    }

    protected override void OnEnterGame()
    {
        base.OnEnterGame();
        
        PlayerDataAccess.spinnerDefeated = true;
        PlayerDataAccess.BelltownHouseState = GlobalEnums.BelltownHouseStates.Full;
        QuestManager.GetQuest(Quests.Beastfly_Hunt).SetCompleted();
        // To test satisfying the quest check after loading the room
        QuestManager.GetQuest(Quests.Shiny_Bell_Goomba).SetReadyToComplete();
    }
}