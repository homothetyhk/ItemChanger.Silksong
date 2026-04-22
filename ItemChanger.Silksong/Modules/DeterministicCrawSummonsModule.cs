using HutongGames.PlayMaker;
using ItemChanger.Extensions;
using ItemChanger.Modules;
using ItemChanger.Serialization;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.Serialization;
using Newtonsoft.Json;
using PrepatcherPlugin;
using Silksong.FsmUtil;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Modules;

/// <summary>
/// A module which controls the spawning of Craw Summons in act 3.
/// </summary>
public class DeterministicCrawSummonsModule : Module
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
        Benchwarp.Data.SceneNames.Dust_10, // Sinners road broken toll bench
        Benchwarp.Data.SceneNames.Dust_11, // Sinners road Styx bench
        Benchwarp.Data.SceneNames.Wisp_04 // Wisp Thicket bench
    ];

    /// <summary>
    /// List of scenes that the Craw Summons should spawn at. Defaults to all possible
    /// vanilla locations simultaneously.
    /// </summary>
    public IEnumerable<string> SceneNames { get; init; } = CRAW_SUMMONS_SCENES;

    /// <summary>
    /// Conditions for Craw Summons to spawn. Defaults to vanilla conditions.
    /// </summary>
    public IValueProvider<bool> SpawnConditions { get; init; } = new Conjunction(
        new PDBool(nameof(PlayerData.blackThreadWorld)),
        new PDBool(nameof(PlayerData.hitCrowCourtSwitch)),
        new QuestCompletionBool(Quests.Black_Thread_Pt1_Shamans, QuestCompletion.IsCompleted)
    );

    /// <summary>
    /// List of scenes that have the Craw summon pin present. Initialized empty.
    /// </summary>
    [JsonProperty]
    private List<string> ScenesWithSpawnedSummons { get; init; } = [];

    protected override void DoLoad()
    {
        FsmEditGroup editGroup = new();

        foreach (var scene in CRAW_SUMMONS_SCENES)
        {
            if (SceneNames.Contains(scene))
            {
                ItemChangerHost.Singleton.GameEvents.AddSceneEdit(scene, PatchCrawSummonsAppearedScene);
                editGroup.Add(new FsmId(scene, "RestBench", "Bench Control"), fsm => ForceSummonsSpawn(fsm, scene));
            }
            else
            {
                editGroup.Add(new FsmId(scene, "RestBench", "Bench Control"), ForceNoSummonsSpawn);
            }
        }

        Using(editGroup);
    }

    protected override void DoUnload()
    {
        foreach (var scene in SceneNames)
        {
            ItemChangerHost.Singleton.GameEvents.RemoveSceneEdit(scene, PatchCrawSummonsAppearedScene);
        }
    }

    private void PatchCrawSummonsAppearedScene(Scene scene)
    {
        if (ScenesWithSpawnedSummons.Contains(scene.name))
            PlayerDataAccess.CrowSummonsAppearedScene = scene.name;
        else
            PlayerDataAccess.CrowSummonsAppearedScene = "";
    }

    private void ForceSummonsSpawn(PlayMakerFSM fsm, string sceneName)
    {
        // Replace RunFsm (craw_summons_spawn_check) states with equivalent check without any randomness
        FsmState respawnCheck = fsm.MustGetState("Set Custom Wake Up?");
        respawnCheck.RemoveAction(3);
        respawnCheck.InsertLambdaMethod(3, CancelIfRequirementsNotMet);

        FsmState sitCheck = fsm.MustGetState("Craw summons check");
        sitCheck.RemoveAction(3);
        sitCheck.InsertLambdaMethod(3, CancelIfRequirementsNotMet);
        return;

        void CancelIfRequirementsNotMet(Action cb)
        {
            // When Craw Summons spawns while warping to a locked bell bench using BenchWarp, the screen fills black
            // until moving through a scene transition.
            // This fix prevents the Craw Summons from spawning at a locked bench, which probably makes sense anyway.
            Scene activeScene = fsm.gameObject.scene;
            GameObject? bellBench = activeScene.FindGameObjectByName("bell_bench");
            if (bellBench != null && !bellBench.GetComponent<BellBench>().isActivated)
            {
                fsm.SendEvent("CANCEL");
                return;
            }

            if (ScenesWithSpawnedSummons.Contains(sceneName) || !SpawnConditions.Value)
            {
                fsm.SendEvent("CANCEL");
                return;
            }

            ScenesWithSpawnedSummons.Add(sceneName);
            cb();
        }
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