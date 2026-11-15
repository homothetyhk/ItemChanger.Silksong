using Benchwarp.Data;
using ItemChanger;
using ItemChanger.Extensions;
using ItemChanger.Silksong;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;
using PrepatcherPlugin;
using Unity.Collections;
using UnityEngine.SceneManagement;

namespace ItemChangerTesting.LocationTests;

internal class SpoolFragmentShermaTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Balm For the Wounded Location",
        MenuDescription = "Tests giving various items from Sherma in Whiteward.",
        Revision = 2026093001
    };

    public override void Setup(TestArgs args)
    {
        StartAt(new CoordinateStartDef()
        {
            SceneName = SceneNames.Ward_09,
            X = 31.21f,
            Y = 4.57f,
            MapZone = GlobalEnums.MapZone.WARD
        });
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Spool_Fragment__Sherma)!.Wrap()
            .WithVariousItems().WithAllPersistent());
    }

    protected override void OnEnterGame()
    {
        base.OnEnterGame();

        // couldn't figure out how to weaken the arena enemies, sorry
        PlayerDataAccess.hasDash = true;
        PlayerDataAccess.nailUpgrades = 4;
        PlayerDataAccess.hasChargeSlash = true;
        PlayerDataAccess.isInvincible = true;

        QuestManager.GetQuest(Quests.Save_Sherma).SetAccepted();
    }
}