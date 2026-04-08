using Benchwarp.Data;
using ItemChanger;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.RawData;

namespace ItemChangerTesting.LocationTests;

/// <summary>
/// Tests WishwallLocation using the "Bone Bottom Repairs" quest (Building Materials).
///
/// Test steps:
///   1. Load the test save → arrive near Bellway_01.
///   2. Walk to the Bone Bottom Repairs quest board and interact.
///   3. The quest should appear as completable (200 shards are given on start).
///   4. Turn it in → IC should give a Surgeon's Key instead of the vanilla reward.
/// </summary>
internal class WishwallTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Wishwall (Bone Bottom Repairs → Surgeon's Key)",
        MenuDescription = "Turns in the Bone Bottom Repairs quest at the quest board; " +
                          "IC should give a Surgeon's Key instead of the vanilla reward.",
        Revision = 2026040704,
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Bellway_01, PrimitiveGateNames.left1);

        Profile.AddPlacement(
            new WishwallLocation
            {
                Name = "Wishwall_Test-Bone_Bottom_Repairs",
                QuestName = Quests.Building_Materials,
                FlingType = ItemChanger.Enums.FlingType.DirectDeposit,
            }.Wrap()
             .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!));
    }

    protected override void OnEnterGame()
    {
        base.OnEnterGame();

        // Mark the Bellbeast as defeated so the Bone Bottom quest board spawns.
        PlayerData.instance.defeatedBellBeast = true;

        if (QuestManager.TryGetFullQuestBase(Quests.Building_Materials, out FullQuestBase? quest)
            && quest != null)
        {
            // Mark the quest as seen and accepted.
            quest.SetSeen();
            quest.SetAccepted();

            // Bone Bottom Repairs is a donation quest: its target is a QuestTargetCurrency
            // (shell shards) which does not implement Get().  Set ShellShards directly on
            // PlayerData so CanComplete returns true when the player opens the board.
            PlayerData.instance.ShellShards = 200;
        }
        else
        {
            ItemChangerTestingPlugin.Instance.Logger.LogWarning(
                $"[WishwallTest] Could not find quest '{Quests.Building_Materials}'.");
        }
    }
}
