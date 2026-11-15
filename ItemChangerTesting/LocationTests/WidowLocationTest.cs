using Benchwarp.Data;
using GlobalEnums;
using ItemChanger;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.LocationTests;

internal class WidowLocationTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Widow",
        MenuDescription = "Tests obtaining items at Widow.",
        Revision = 2026041000,
    };

    public override void Setup(TestArgs args)
    {
        StartAt(new CoordinateStartDef()
        {
            SceneName = SceneNames.Belltown_Shrine,
            X = 45.43f,
            Y = 8.57f,
            MapZone = MapZone.NONE
        });
        
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Needolin)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Flea)!));
    }

    protected override void OnEnterGame()
    {
        base.OnEnterGame();
        
        if (QuestManager.TryGetFullQuestBase(Quests.The_Threadspun_Town, out FullQuestBase threadspunTownQuest))
        {
            threadspunTownQuest.SetAccepted();
        }
        else
        {
            ItemChangerTestingPlugin.Instance.Logger.LogWarning($"Unable to locate quest {Quests.The_Threadspun_Town}.");
        }
    }
}