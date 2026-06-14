using Benchwarp.Data;
using ItemChanger;
using ItemChanger.Enums;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.Items;
using ItemChanger.Silksong.RawData;
using ItemChanger.Tags;

namespace ItemChangerTesting.LocationTests;

internal class BenjinAndCrullLocationTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Benjin & Crull",
        MenuDescription = "Test both Benjin & Crull locations",
        Revision = 2026043000,
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Dust_Shack, PrimitiveGateNames.left1);
        
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Start)!.Wrap()
            .Add(RosariesItem.MakeRosariesItem(1000)));

        Profile.AddPlacement(Finder.GetLocation(LocationNames.Tacks)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Crest_of_Architect)!.WithTag(new PersistentItemTag()
                { Persistence = Persistence.Persistent })));

        Profile.AddPlacement(Finder.GetLocation(LocationNames.Steel_Spines)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Silkspear)!.WithTag(new PersistentItemTag()
                { Persistence = Persistence.Persistent })));
    }

    protected override void OnEnterGame()
    {
        // Tacks prerequisites
        FullQuestBase roachQuest = QuestManager.GetQuest(Quests.Roach_Killing);
        roachQuest.SetReadyToComplete();

        // Steel spines prerequisites
        FullQuestBase infestationQuest = QuestManager.GetQuest(Quests.Doctor_Curse_Cure);
        infestationQuest.SetAccepted();
    }
}