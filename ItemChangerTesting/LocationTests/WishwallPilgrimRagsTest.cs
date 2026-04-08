using Benchwarp.Data;
using ItemChanger;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.RawData;

namespace ItemChangerTesting.LocationTests;

/// <summary>
/// Tests WishwallLocation using the "Garb of the Pilgrims" quest (Pilgrim Rags).
/// This quest has a reward icon configured, so the quest completion popup should
/// display the IC item's sprite (via <see cref="WishwallSavedItem.GetPopupIcon"/>)
/// rather than the vanilla reward sprite.
///
/// Test steps:
///   1. Load the test save → arrive near Bellway_01.
///   2. Walk to the Bone Bottom Repairs quest board and interact.
///   3. The "Garb of the Pilgrims" quest should appear as completable.
///   4. Turn it in → IC should give a Surgeon's Key instead of the vanilla reward,
///      and the completion popup should show the Surgeon's Key icon.
/// </summary>
internal class WishwallPilgrimRagsTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Wishwall (Garb of the Pilgrims → Surgeon's Key, with icon)",
        MenuDescription = "Turns in the Garb of the Pilgrims quest at the Bone Bottom board; " +
                          "IC should give a Surgeon's Key and the completion popup should " +
                          "show the Surgeon's Key icon (this quest has a reward icon configured).",
        Revision = 2026040701,
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Bellway_01, PrimitiveGateNames.left1);

        Profile.AddPlacement(
            new WishwallLocation
            {
                Name = "Wishwall_Test-Pilgrim_Rags",
                QuestName = Quests.Pilgrim_Rags,
                FlingType = ItemChanger.Enums.FlingType.DirectDeposit,
            }.Wrap()
             .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!));
    }

    protected override void OnEnterGame()
    {
        base.OnEnterGame();

        // Mark the Bellbeast as defeated so the Bone Bottom quest board spawns.
        PlayerData.instance.defeatedBellBeast = true;

        if (QuestManager.TryGetFullQuestBase(Quests.Pilgrim_Rags, out FullQuestBase? quest)
            && quest != null)
        {
            // Mark the quest as seen and accepted, then force it into the ready-to-turn-in state.
            quest.SetSeen();
            quest.SetAccepted();
            quest.SetReadyToComplete();
        }
        else
        {
            ItemChangerTestingPlugin.Instance.Logger.LogWarning(
                $"[WishwallPilgrimRagsTest] Could not find quest '{Quests.Pilgrim_Rags}'.");
        }
    }
}
