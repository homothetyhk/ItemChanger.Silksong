using Benchwarp.Data;
using HutongGames.PlayMaker;
using ItemChanger;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ItemChangerTesting.LocationTests;

internal class PinstressLocationTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Pinstress Location",
        MenuDescription = "Tests giving various items from the Pin Badge slot. Spawns next to a sleeping Pinstress in Peak_07; wake her with the Needle, defeat her, and the reward routes through ItemChanger.",
        Revision = 2026050206
    };

    public override void Setup(TestArgs args)
    {
        StartAt(new CoordinateStartDef()
        {
            SceneName = SceneNames.Peak_07,
            X = 38.05f,
            Y = 91.50f,
            MapZone = GlobalEnums.MapZone.NONE
        });
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Pin_Badge)!.Wrap()
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

        pd.SetBool(nameof(pd.pinstressInsideSitting), false);
        pd.SetBool(nameof(pd.pinstressStoppedResting), true);
        pd.SetBool(nameof(pd.pinstressQuestReady), true);
        pd.SetBool(nameof(pd.PinstressPeakQuestOffered), true);
        pd.SetBool(nameof(pd.PinstressPeakBattleOffered), true);

        if (QuestManager.TryGetFullQuestBase(Quests.Pinstress_Battle_Pre, out FullQuestBase pre))
        {
            pre.SetAccepted();
        }

        SceneManager.sceneLoaded -= ForcePinstressArenaOnSceneLoaded;
        SceneManager.sceneLoaded += ForcePinstressArenaOnSceneLoaded;
    }

    // The Pinstress arena in Peak_07 is gated behind the 'Pinstress Control / Control' FSM,
    // which only flips from 'No Pinstress' to the boss-spawning branch once its internal
    // 'Visited Ice Core' bool is set, normally by traversing Brightvein. Short-circuit it here
    // so the test drops the player straight into a fightable Pinstress.
    private static void ForcePinstressArenaOnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != SceneNames.Peak_07) return;
        var gm = GameManager.instance;
        if (gm == null) return;
        gm.StartCoroutine(ForcePinstressArena(scene));
    }

    private static System.Collections.IEnumerator ForcePinstressArena(Scene scene)
    {
        for (int i = 0; i < 30; i++) yield return null;

        foreach (var fsm in Resources.FindObjectsOfTypeAll<PlayMakerFSM>())
        {
            if (fsm == null || fsm.gameObject.scene != scene) continue;
            if (fsm.gameObject.name != "Pinstress Control" || fsm.FsmName != "Control") continue;

            var v = fsm.FsmVariables.GetFsmBool("Visited Ice Core");
            if (v != null) v.Value = true;
            fsm.SendEvent("FINISHED");
            break;
        }

        foreach (var go in Resources.FindObjectsOfTypeAll<GameObject>())
        {
            if (go == null || go.scene != scene) continue;
            if (go.transform.parent == null || go.transform.parent.name != "Pinstress Control") continue;
            if (go.name == "Pinstress Scene") go.SetActive(true);
            else if (go.name == "No Pinstress Scene") go.SetActive(false);
        }
    }
}
