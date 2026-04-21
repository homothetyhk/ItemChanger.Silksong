using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Locations;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.Serialization;
using Newtonsoft.Json;
using PrepatcherPlugin;
using Silksong.FsmUtil;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Locations;

// TODO:
// - Benchwarp crash when warping to locked bench
// - Freeze on save+quit - cannot reproduce?

public class CrawSummonsLocation : AutoLocation
{
    /// <summary>
    /// List of all scenes that support Craw Summons
    /// </summary>
    private static readonly string[] CRAW_SUMMONS_SCENES =
    [
        Benchwarp.Data.SceneNames.Belltown, // Bellhart
        Benchwarp.Data.SceneNames.Bellway_Shadow, // Bilewater bellway
        Benchwarp.Data.SceneNames.Bellway_03, // Far fields bellway
        Benchwarp.Data.SceneNames.Bone_East_27, // Far fields bench before Karmelita
        Benchwarp.Data.SceneNames.Shellwood_01b, // Shellwood tall hub room
        Benchwarp.Data.SceneNames.Mosstown_03, // Shellwood room outside Chapel of the Witch
        Benchwarp.Data.SceneNames.Shellwood_08c, // Shellwood lower left toll bench
        Benchwarp.Data.SceneNames.Dust_10, // Sinners road troll bench
        Benchwarp.Data.SceneNames.Dust_11, // Sinners road Styx bench
        Benchwarp.Data.SceneNames.Wisp_04 // Wisp Thicket bench
    ];

    /// <summary>
    /// List of scenes that the Craw Summons location should spawn at. Defaults to all possible
    /// vanilla locations simultaneously.
    /// </summary>
    public required List<string> SceneNames { get; init; } = [..CRAW_SUMMONS_SCENES];

    /// <summary>
    /// List of scenes that have the Craw summon pin present. Should be initialized empty.
    /// </summary>
    [JsonProperty]
    private List<string> ScenesWithSpawnedSummons { get; init; } = [];

    protected override void DoLoad()
    {
        FsmEditGroup editGroup = new();
        foreach (var sceneName in SceneNames)
        {
            ItemChangerHost.Singleton.GameEvents.AddSceneEdit(sceneName, PatchCrawSummonsLocation);
        }

        foreach (var sceneName in CRAW_SUMMONS_SCENES)
        {
            if (SceneNames.Contains(sceneName))
            {
                editGroup.Add(new FsmId(sceneName, "RestBench", "Bench Control"),
                    fsm => ForceSummonsSpawn(fsm, sceneName));
                editGroup.Add(new FsmId(sceneName, "craw_court_summons_pin", "FSM"), FixPinBehaviour);
            }
            else
            {
                editGroup.Add(new FsmId(sceneName, "RestBench", "Bench Control"), ForceNoSummonsSpawn);
            }
        }

        Using(editGroup);
    }

    protected override void DoUnload()
    {
        foreach (var sceneName in SceneNames)
        {
            ItemChangerHost.Singleton.GameEvents.RemoveSceneEdit(sceneName, PatchCrawSummonsLocation);
        }
    }

    private void FixPinBehaviour(PlayMakerFSM fsm)
    {
        // Remove the summons cloth from the pin iff the placement is obtained
        FsmState emptyState = fsm.MustGetState("Empty?");
        emptyState.Actions = [];
        emptyState.AddLambdaMethod(_ =>
        {
            if (Placement!.AllObtained())
                fsm.SendEvent("TRUE");
            else
                fsm.SendEvent("FINISHED");
        });

        // Replace the craw summons item
        FsmState setPickupState = fsm.MustGetState("Set Pickup");
        PlacementSavedItem icItem = ScriptableObject.CreateInstance<PlacementSavedItem>();
        icItem.Placement = Placement!;
        icItem.GiveInfo = GetGiveInfo();
        setPickupState.GetFirstActionOfType<SetCollectablePickupItem>()!.Item = icItem;
    }

    private void PatchCrawSummonsLocation(Scene scene)
    {
        if (ScenesWithSpawnedSummons.Contains(scene.name))
            PlayerDataAccess.CrowSummonsAppearedScene = scene.name;
        else
            PlayerDataAccess.CrowSummonsAppearedScene = "";
    }

    private void ForceSummonsSpawn(PlayMakerFSM fsm, string sceneName)
    {
        void CancelIfNoSummonsSpawn(Action cb)
        {
            if (!QuestManager.TryGetFullQuestBase(Quests.Black_Thread_Pt1_Shamans, out var quest))
            {
                LogWarn($"Unable to locate quest '{quest}'.");
                return;
            }

            if (ScenesWithSpawnedSummons.Contains(sceneName)
                || !PlayerDataAccess.blackThreadWorld
                || !PlayerDataAccess.hitCrowCourtSwitch
                || !quest.Completion.IsCompleted)
            {
                fsm.SendEvent("CANCEL");
            }

            ScenesWithSpawnedSummons.Add(sceneName);
            cb();
        }

        // Replace RunFsm (craw_summons_spawn_check) states with lambda method
        FsmState respawnCheck = fsm.MustGetState("Set Custom Wake Up?");
        respawnCheck.RemoveAction(3);
        respawnCheck.InsertLambdaMethod(3, CancelIfNoSummonsSpawn);

        FsmState sitCheck = fsm.MustGetState("Craw summons check");
        sitCheck.RemoveAction(3);
        sitCheck.InsertLambdaMethod(3, CancelIfNoSummonsSpawn);
    }

    private void ForceNoSummonsSpawn(PlayMakerFSM fsm)
    {
        // Replace RunFsm (craw_summons_spawn_check) states with lambda method that always cancels
        FsmState respawnCheck = fsm.MustGetState("Set Custom Wake Up?");
        respawnCheck.RemoveAction(3);
        respawnCheck.InsertLambdaMethod(3, _ => fsm.SendEvent("CANCEL"));

        FsmState sitCheck = fsm.MustGetState("Craw summons check");
        sitCheck.RemoveAction(3);
        sitCheck.InsertLambdaMethod(3, _ => fsm.SendEvent("CANCEL"));
    }
}