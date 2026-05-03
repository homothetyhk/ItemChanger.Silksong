using Benchwarp.Data;
using ItemChanger;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.RawData;

namespace ItemChangerTesting.LocationTests;

internal class CreigeLocationTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Creige Location",
        MenuDescription = "Tests giving various items from the Creige Crafting Kit slot. Spawns next to Creige in Halfway_01 with the Crawbug Clearing wish ready-to-complete; talk to him to claim the reward.",
        Revision = 2026050302
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Halfway_01, PrimitiveGateNames.right1);
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Crafting_Kit__Creige)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!)
            .Add(Finder.GetItem(ItemNames.Flea)!));
    }

    protected override void OnEnterGame()
    {
        base.OnEnterGame();

        var pd = PlayerData.instance;
        if (pd == null) return;

        pd.SetInt(nameof(pd.nailUpgrades), 4);
        pd.SetInt(nameof(pd.maxHealthBase), 99);
        pd.SetInt(nameof(pd.maxHealth), 99);
        pd.SetInt(nameof(pd.health), 99);
        pd.SetInt(nameof(pd.geo), 9999);

        pd.SetBool(nameof(pd.hasBrolly), true);
        pd.SetBool(nameof(pd.hasChargeSlash), true);
        pd.SetBool(nameof(pd.hasDash), true);
        pd.SetBool(nameof(pd.hasDoubleJump), true);
        pd.SetBool(nameof(pd.hasHarpoonDash), true);
        pd.SetBool(nameof(pd.hasNeedolin), true);
        pd.SetBool(nameof(pd.hasNeedolinMemoryPowerup), true);
        pd.SetBool("hasSilkSpoolAppeared", true);
        pd.SetBool(nameof(pd.hasSuperJump), true);
        pd.SetBool(nameof(pd.hasWalljump), true);

        QuestUtil.SetReadyToComplete(Quests.Crow_Feathers_Pre);
        QuestUtil.SetReadyToComplete(Quests.Crow_Feathers);
    }
}
