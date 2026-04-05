using Benchwarp.Data;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.LocationTests;

internal class BellHermitLocationTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Bell Hermit Location",
        MenuDescription = "Tests giving various items from Hermit_s_Soul",
        Revision = 2026040500
    };

    public override void Setup(TestArgs args)
    {
        StartAt(new CoordinateStartDef()
        {
            SceneName = SceneNames.Belltown_basement_03,
            X = 101.34f,
            Y = 102.57f,
            MapZone = GlobalEnums.MapZone.NONE
        });
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Hermit_s_Soul)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!)
            .Add(Finder.GetItem(ItemNames.Flea)!));
    }
    
    protected override void OnEnterGame()
    {
        base.OnEnterGame();

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