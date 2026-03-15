using Benchwarp.Data;
using ItemChanger;
using ItemChanger.Silksong.RawData;

namespace ItemChangerTesting.LocationTests;

internal class YarnabyTest : Test
{
    // TODO: check multiple async UIDefs
    // TODO: check respawning items

    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Yarnaby (Crest of Witch location)",
        MenuDescription = "Tests giving various items from Yarnaby",
        Revision = 2026031400,
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Belltown_Room_doctor, PrimitiveGateNames.left1);
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Crest_of_Witch)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!));
    }

    protected override void OnEnterGame()
    {
        base.OnEnterGame();
        if (QuestManager.TryGetFullQuestBase(Quests.Doctor_Curse_Cure, out FullQuestBase doctorQuest))
        {
            doctorQuest.SetReadyToComplete();
        }
        else
        {
            ItemChangerTestingPlugin.Instance.Logger.LogWarning($"Unable to locate quest {Quests.Doctor_Curse_Cure}.");
        }
        ToolItemManager.SetEquippedCrest("Cursed");
    }
}
