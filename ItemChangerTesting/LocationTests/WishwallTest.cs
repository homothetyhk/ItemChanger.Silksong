using Benchwarp.Data;
using ItemChanger;
using ItemChanger.Enums;
using ItemChanger.Items;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.RawData;
using ItemChanger.Tags;

namespace ItemChangerTesting.LocationTests;

/// <summary>
/// Tests WishwallLocation using the "Bone Bottom Repairs" quest (Building Materials).
/// This quest has no vanilla reward icon (<c>rewardIconType == None</c>), so it also
/// verifies that IC forces the reward icon to be shown anyway, using the IC item's
/// sprite with <see cref="FullQuestBase.IconTypes.Image"/> material.
///
/// The placement includes a <see cref="Persistence.Persistent"/> Cogfly to test
/// respawn support: after turning in and then transitioning scenes, the Cogfly's
/// obtained state is reset and it should be given again on the next turn-in.
///
/// Test steps:
///   1. Load the test save → arrive near Bellway_01.
///   2. Walk to the Bone Bottom Repairs quest board and interact.
///   3. The quest should appear as completable (200 shards are given on start).
///   4. The quest detail and inventory views should show the Surgeon's Key icon.
///   5. Turn it in → IC should give the Surgeon's Key then the Cogfly, each with a
///      popup, before the quest completion UI opens.
///   6. Transition to another scene and back, then interact with the board again —
///      the Cogfly (persistent) should be offered again.
/// </summary>
internal class WishwallTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Wishwall (Bone Bottom Repairs → Surgeon's Key + respawning Rosary_Necklace)",
        MenuDescription = "Turns in the Bone Bottom Repairs quest; IC gives a Surgeon's Key and " +
                          "a persistent Rosary_Necklace before the quest completion UI opens. " +
                          "After a scene transition the Rosary_Necklace should be giveable again.",
        Revision = 2026040900,
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Bellway_01, PrimitiveGateNames.left1);

        Item Rosary_Necklace = Finder.GetItem(ItemNames.Rosary_Necklace)!;
        Rosary_Necklace.AddTag(new PersistentItemTag { Persistence = Persistence.Persistent });

        Profile.AddPlacement(
            new WishwallLocation
            {
                Name = "Wishwall_Test-Bone_Bottom_Repairs",
                QuestName = Quests.Building_Materials,
                FlingType = ItemChanger.Enums.FlingType.DirectDeposit,
            }.Wrap()
             .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!)
             .Add(Rosary_Necklace));
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
