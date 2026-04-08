using Benchwarp.Data;
using ItemChanger;
using ItemChanger.Items;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.RawData;

namespace ItemChangerTesting.LocationTests;

/// <summary>
/// Tests WishwallLocation: verifies that IC gives the placement's item instead of
/// the vanilla quest reward when the player turns in a quest at a QuestBoardInteractable.
///
/// Test steps:
///   1. Load save → arrive near the Flintbeetle quest board.
///   2. Walk to the quest board and interact.
///   3. The "Volatile Flintbeetles" quest should appear as completable.
///   4. Select it → confirm → IC should give a Surgeon's Key instead of the vanilla Memory Locket.
/// </summary>
internal class WishwallTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Wishwall (Volatile Flintbeetles → Surgeon's Key)",
        MenuDescription = "Turns in the Volatile Flintbeetles quest at a quest board; " +
                          "IC should give a Surgeon's Key instead of the vanilla Memory Locket.",
        Revision = 2026040700,
    };

    public override void Setup(TestArgs args)
    {
        // TODO: Replace with the scene + gate nearest to the Volatile Flintbeetles quest board
        //       (QuestCompletionDataName = "Rock Rollers").  Verify in-game and update.
        StartNear(SceneNames.Tut_05, PrimitiveGateNames.left1);

        Profile.AddPlacement(
            new WishwallLocation
            {
                Name = LocationNames.Memory_Locket__Quest_Flintbeetles,
                QuestName = Quests.Rock_Rollers,
                FlingType = ItemChanger.Enums.FlingType.DirectDeposit,
            }.Wrap()
             .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!));
    }

    protected override void OnEnterGame()
    {
        base.OnEnterGame();

        // Pre-fulfil the Volatile Flintbeetles quest so it's immediately ready to turn in.
        if (QuestManager.TryGetFullQuestBase(Quests.Rock_Rollers, out FullQuestBase? flintbeetleQuest)
            && flintbeetleQuest != null)
        {
            flintbeetleQuest.SetReadyToComplete();
        }
        else
        {
            ItemChangerTestingPlugin.Instance.Logger.LogWarning(
                $"[WishwallTest] Could not find quest '{Quests.Rock_Rollers}' — test may not work correctly.");
        }
    }
}
