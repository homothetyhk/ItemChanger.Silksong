using Benchwarp.Data;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.LocationTests;

internal class ChapelMaidLocationTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Chapel Maid Location",
        MenuDescription = "Tests giving various items from Maiden_s_Soul",
        Revision = 2026040600
    };

    public override void Setup(TestArgs args)
    {
        StartAt(new CoordinateStartDef()
        {
            SceneName = SceneNames.Bonetown,
            X = 80.32f,
            Y = 7.56f,
            MapZone = GlobalEnums.MapZone.NONE
        });
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Maiden_s_Soul)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!)
            .Add(Finder.GetItem(ItemNames.Flea)!));
    }

    protected override void OnEnterGame()
    {
        base.OnEnterGame();

        PlayerData.instance.churchKeeperIntro = true;
        
        if (QuestManager.TryGetFullQuestBase(Quests.Soul_Snare, out FullQuestBase silkAndSoulQuest))
        {
            silkAndSoulQuest.SetAccepted();
        }
        else
        {
            ItemChangerTestingPlugin.Instance.Logger.LogWarning($"Unable to locate quest {Quests.Soul_Snare}.");
        }
    }
}