using Benchwarp.Data;
using GlobalEnums;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Extensions;
using ItemChanger.Modules;
using ItemChanger.Serialization;
using PrepatcherPlugin;
using Silksong.FsmUtil;
using Silksong.UnityHelper.Extensions;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Modules;

/// <summary>
/// Module which makes several changes to the Slab kidnapping sequence:
/// - Wardenflies always spawn throughout Pharloom, including after already being kidnapped
/// - Getting kidnapped does not remove items
/// </summary>
public class SlabKidnappingModule : Module
{
    /// <summary>
    /// An <see cref="IValueProvider{T}"/> describing whether Slab Wardens should be available throughout Pharloom.
    /// Defaults to constant true.
    /// </summary>
    public IValueProvider<bool> SlabCaptureIsAvailable { get; set; } = new BoxedBool(true);

    protected override void DoLoad()
    {
        ItemChangerHost.Singleton.GameEvents.AddSceneEdit(SceneNames.Bone_East_04c, ForceJailerDocks);
        ItemChangerHost.Singleton.GameEvents.AddSceneEdit(SceneNames.Shadow_21, ForceJailerBilewater);
        ItemChangerHost.Singleton.GameEvents.AddSceneEdit(SceneNames.Greymoor_05, ForceJailerGreymoor);

        Using(new FsmEditGroup()
        {
            {
                new(SceneNames.Bone_East_04c, "Slab Fly Large Cage", "Control"),
                fsm => HookWardenfly(fsm, () => PlayerDataAccess.CurseKilledFlyBoneEast)
            },
            {
                new(SceneNames.Shadow_21, "Slab Fly Large Cage", "Control"),
                fsm => HookWardenfly(fsm, () => PlayerDataAccess.CurseKilledFlySwamp)
            },
            {
                new(SceneNames.Greymoor_05, "Slab Fly Large Cage", "Control"),
                fsm => HookWardenfly(fsm, () => PlayerDataAccess.CurseKilledFlyGreymoor)
            },
        });
    }

    protected override void DoUnload()
    {
        ItemChangerHost.Singleton.GameEvents.AddSceneEdit(SceneNames.Bone_East_04c, ForceJailerDocks);
        ItemChangerHost.Singleton.GameEvents.AddSceneEdit(SceneNames.Shadow_21, ForceJailerBilewater);
        ItemChangerHost.Singleton.GameEvents.RemoveSceneEdit(SceneNames.Greymoor_05, ForceJailerGreymoor);
    }

    private void ForceJailerDocks(Scene scene)
    {
        // This scene uses a TestGameObjectActivator to enable the jailer + disable ant enemies
        // based on a combination of player data, plus DeactivateIfPlayerdataTrue/False components.
        GameObject jailerObj = scene.FindGameObjectByName("Slab Fly Large Cage")!;
        if (SlabCaptureIsAvailable.Value)
        {
            jailerObj.RemoveComponents<DeactivateIfPlayerdataTrue>();
            jailerObj.RemoveComponents<DeactivateIfPlayerdataFalse>();
        }
        
        GameObject sceneControl = scene.FindGameObjectByName("Scene Control")!;
        sceneControl.RemoveComponent<TestGameObjectActivator>();

        sceneControl.FindChild(name: "Slab Jailer Scene")!.SetActive(SlabCaptureIsAvailable.Value);
        sceneControl.FindChild(name: "Bone Hunters Scene")!.SetActive(!SlabCaptureIsAvailable.Value);
    }

    private void ForceJailerBilewater(Scene scene)
    {
        // This scene uses a PlayerDataTestResponse to enable the jailer + disable bilewater enemies
        // based on a combination of player data, plus DeactivateIfPlayerdataTrue/False components.
        GameObject jailerObj = scene.FindGameObjectByName("Slab Fly Large Cage")!;
        if (SlabCaptureIsAvailable.Value)
        {
            jailerObj.RemoveComponents<DeactivateIfPlayerdataTrue>();
            jailerObj.RemoveComponents<DeactivateIfPlayerdataFalse>();
        }
        
        GameObject sceneControl = scene.FindGameObjectByName("Scene Control")!;
        sceneControl.RemoveComponent<PlayerDataTestResponse>();

        sceneControl.FindChild(name: "Slab Jailer Scene")!.SetActive(SlabCaptureIsAvailable.Value);
        sceneControl.FindChild(name: "Muckmen Control")!.SetActive(!SlabCaptureIsAvailable.Value);
    }

    private void ForceJailerGreymoor(Scene scene)
    {
        // This scene uses a FSM to control whether to spawn the jailer, regular enemies, or Moorwing. It additionally
        // has DeactivateIfPlayerdataTrue/False components on the jailer itself.
        GameObject jailerObj = scene.FindGameObjectByName("Slab Fly Large Cage")!;
        if (SlabCaptureIsAvailable.Value)
        {
            jailerObj.RemoveComponents<DeactivateIfPlayerdataTrue>();
            jailerObj.RemoveComponents<DeactivateIfPlayerdataFalse>();
        }

        GameObject sceneControl = scene.FindGameObjectByName("Scene Control")!;
        PlayMakerFSM fsm = sceneControl.GetFsm("Scene Control")!;

        // Default behaviour: spawn the jailer according to SlabCaptureIsAvailable, except when Moorwing is present.
        FsmState enemySuiteState = fsm.MustGetState("Enemy Suite");
        enemySuiteState.Actions = [];
        enemySuiteState.AddLambdaMethod(_ =>
        {
            if (SlabCaptureIsAvailable.Value)
                fsm.SendEvent("JAILER");
            else
                fsm.SendEvent("NOT JAILER");
        });

        FsmState jailCartState = fsm.MustGetState("Jail Cart?");
        jailCartState.InsertLambdaMethod(0, _ =>
        {
            if (SlabCaptureIsAvailable.Value)
                fsm.SendEvent("CART PRESENT");
        });

        FsmState roostingState = fsm.MustGetState("Roosting");
        roostingState.AddTransition("SPAWN JAILER", "Jailer");
        roostingState.AddLambdaMethod(_ =>
        {
            if (SlabCaptureIsAvailable.Value)
                fsm.SendEvent("SPAWN JAILER");
        });
    }

    private void HookWardenfly(PlayMakerFSM fsm, Func<bool> isWardenflyDefeatedByCurse)
    {
        // Rewire wardenflies spawn logic
        FsmState initState = fsm.MustGetState("Init");
        initState.RemoveTransition("FINISHED");
        initState.AddTransition("HERE", "Dormant");
        initState.AddTransition("DEAD", "Cursed Dead");
        initState.AddTransition("NOT HERE", "Not Here");

        initState.AddLambdaMethod(_ =>
        {
            if (SlabCaptureIsAvailable.Value)
                fsm.SendEvent("HERE");
            else if (isWardenflyDefeatedByCurse())
                fsm.SendEvent("DEAD");
            else
                fsm.SendEvent("NOT HERE");
        });

        // Suppress the usual slab capture function that takes all items.
        FsmState capturedState = fsm.MustGetState("Start Caged Sequence");
        capturedState.RemoveFirstActionOfType<CallStaticMethod>();
        capturedState.InsertLambdaMethod(6, _ =>
        {
            // This is identical to the default slab sequence (HeroSlabCapture::ApplyCaptured) with
            // item stealing and cloakless crest removed
            HeroController.instance.MaxHealth();
            GameManager.instance.SetDeathRespawnSimple("Caged Respawn Marker", 0, false);
            PlayerDataAccess.respawnScene = SceneNames.Slab_03;
            PlayerDataAccess.mapZone = MapZone.THE_SLAB;
            DeliveryQuestItem.BreakAllNoEffects();
        });
    }
}