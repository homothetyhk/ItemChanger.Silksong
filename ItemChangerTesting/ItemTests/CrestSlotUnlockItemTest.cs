using ItemChanger.Items;
using ItemChanger.Silksong.RawData;

namespace ItemChangerTesting.ItemTests;

internal class CrestSlotUnlockItemTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.ItemTests,
        MenuName = "Crest Slot Unlock Items",
        MenuDescription = "Tests various crest slot unlock items and previewing locked crests.",
        Revision = 2026070800
    };

    public override void Setup(TestArgs args)
    {
        CommonLocations.StartInBonebottom();

        List<Item> items =
        [
            BaseItemList.Silkspear, // To make the tools pane visible
            BaseItemList.Crest_Slot__Hunter__Red_Tool,
            BaseItemList.Crest_Slot__Hunter__Blue_Tool,
            BaseItemList.Crest_Slot__Hunter__Blue_Tool,
            BaseItemList.Crest_Slot__Reaper__Yellow_Tool,
            BaseItemList.Crest_Slot__Reaper__Silk_Skill,
            BaseItemList.Crest_of_Reaper
        ];

        int i = 0;

        foreach (Item item in items)
        {
            Profile.AddPlacement(CommonLocations.GetBonebottomLocation(i++).Wrap().Add(item));
        }
    }
}
