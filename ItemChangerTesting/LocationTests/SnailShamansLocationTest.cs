using Benchwarp.Data;
using ItemChanger;
using ItemChanger.Enums;
using ItemChanger.Items;
using ItemChanger.Silksong.RawData;
using ItemChanger.Tags;
using PrepatcherPlugin;

namespace ItemChangerTesting.LocationTests;

internal class SnailShamansLocationTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Snail Shamans (Elegy of the Deep)",
        MenuDescription = "Tests giving items from Snail Shamans",
        Revision = 2026041200,
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Tut_04, PrimitiveGateNames.left1);
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Elegy_of_the_Deep)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!.WithTag(new PersistentItemTag()
                { Persistence = Persistence.Persistent })));
    }

    protected override void OnEnterGame()
    {
        base.OnEnterGame();

        // Act 3
        PlayerDataAccess.blackThreadWorld = true;
        PlayerDataAccess.act3_enclaveWakeSceneCompleted = true;
        PlayerDataAccess.act3_wokeUp = true;

        // Preconditions for placement obtainable
        PlayerDataAccess.visitedAbyss = true;
        PlayerDataAccess.hasNeedolin = true;
        QuestUtil.SetAccepted(Quests.Black_Thread_Pt4_Return);

        // Convenience for test - skips initial meet dialogue
        QuestUtil.SetCompleted(Quests.Diving_Bell_Pt1_Inspect);
    }
}