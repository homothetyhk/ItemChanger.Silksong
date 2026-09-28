using Benchwarp.Data;
using ItemChanger.Silksong.Modules;
using ItemChanger.Silksong.RawData;
using PrepatcherPlugin;

namespace ItemChangerTesting.LocationTests;

internal class ZylotolLocationTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Zylotol",
        MenuDescription = "Tests obtaining items from Zylotol",
        Revision = 2026092901,
    };

    public override void Setup(TestArgs args)
    {
        StartAt(BaseBenchList.PlasmiumLab);

        Profile.AddPlacement(Finder.GetLocation(LocationNames.Needle_Phial)!.Wrap()
            .WithVariousItems().WithAllPersistent());
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Plasmium_Phial)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!).WithAllPersistent());
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Plasmium_Gland)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Flea)!)
            .Add(Finder.GetItem(ItemNames.Mask_Shard)!).WithAllPersistent());
    }

    protected override void OnEnterGame()
    {
        PlayerDataAccess.enclaveLevel = 1;
        PlayerDataAccess.BlueScientistMet = true;
    }

    public override IEnumerable<(string, Action)> TestMethods()
    {
        yield return ("Start Act 3", StartAct3);
        yield return ("Collect Plasmium", () => QuestUtil.SetReadyToComplete(Quests.Extractor_Blue));
        yield return ("Collect Plasmified Blood", () => QuestUtil.SetReadyToComplete(Quests.Extractor_Blue_Worms));
        yield return ("Scientist Dead", () => PlayerDataAccess.BlueScientistDead = true);
    }
}
