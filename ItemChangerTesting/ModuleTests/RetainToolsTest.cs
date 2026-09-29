using Benchwarp.Data;
using ItemChanger.Silksong.Modules;
using ItemChanger.Silksong.RawData;
using PrepatcherPlugin;

namespace ItemChangerTesting.ModuleTests;

internal class RetainToolsTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.ModuleTests,
        MenuName = "Retain Tools",
        MenuDescription = "Tests keeping Needle Phial and Snare Setter",
        Revision = 2026092800,
    };

    public override void Setup(TestArgs args)
    {
        StartAt(BaseBenchList.PlasmiumLab);

        Modules.Add<RetainToolsModule>();
    }

    protected override void OnEnterGame()
    {
        PlayerDataAccess.enclaveLevel = 1;
        PlayerDataAccess.BlueScientistMet = true;
        QuestManager.GetQuest(Quests.Soul_Snare).SetReadyToComplete();
        QuestManager.GetQuest(Quests.Extractor_Blue).SetReadyToComplete();
    }
}
