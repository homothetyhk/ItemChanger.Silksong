using Benchwarp.Data;
using GlobalEnums;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Modules;
using ItemChanger.Serialization;
using ItemChanger.Silksong.FsmStateActions;
using ItemChanger.Silksong.Serialization;
using Silksong.FsmUtil;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Modules.FastTravel;

/// <summary>
/// Module that, automatically unlocks all entrances to the Bell Eater arena
/// and enables fast travel without defeating them.
/// </summary>
[SingletonModule]
public class BellEaterBypassModule : Module
{
    /// <summary>
    /// A value provider controlling whether the Bell Eater arena is accessible (from all bellway stations).
    /// Defaults to the world being in Act 3 and Needolin being obtained.
    /// </summary>
    public IValueProvider<bool> BellEaterAvailable { get; init; } = new Conjunction(
        new PDBool(nameof(PlayerData.blackThreadWorld)),
        new PDBool(nameof(PlayerData.hasNeedolin))
    );

    public static readonly IReadOnlyDictionary<string, FastTravelLocations> FastTravelScenes =
        new Dictionary<string, FastTravelLocations>()
        {
            { SceneNames.Bellway_01, FastTravelLocations.Bonetown }, // Bone Bottom
            { SceneNames.Bone_05, FastTravelLocations.Bone }, // The Marrow
            { SceneNames.Bellway_02, FastTravelLocations.Docks }, // Deep Docks
            { SceneNames.Bellway_03, FastTravelLocations.BoneforestEast }, // Far Fields
            { SceneNames.Bellway_04, FastTravelLocations.Greymoor }, // Greymoor
            { SceneNames.Belltown_basement, FastTravelLocations.Belltown }, // Bellhart
            { SceneNames.Shellwood_19, FastTravelLocations.Shellwood }, // Shellwood
            { SceneNames.Bellway_08, FastTravelLocations.CoralTower }, // Blasted Steps
            { SceneNames.Bellway_Shadow, FastTravelLocations.Shadow }, // Bilewater
            { SceneNames.Bellway_City, FastTravelLocations.City }, // Grand Bellway
            { SceneNames.Slab_06, FastTravelLocations.Peak }, // The Slab
            { SceneNames.Bellway_Aqueduct, FastTravelLocations.Aqueduct }, // Putrified Ducts
        };

    private FastTravelLocations _arenaReturnLocation = FastTravelLocations.None;

    protected override void DoLoad()
    {
        foreach (var kvp in FastTravelScenes)
        {
            ItemChangerHost.Singleton.GameEvents.AddSceneEdit(kvp.Key, SetArenaReturnScene);
        }

        Using(new FsmEditGroup()
        {
            {
                new(SceneNames.Bellway_Centipede_additive, "Bell Centipede Bellway Scene", "Control"),
                HookBellwayEntrypoint
            },
            { new(SilksongHost.Wildcard, "Bone Beast NPC", "Interaction"), HookCallBellBeast },
            { new(SceneNames.Bellway_Centipede_Arena, "top1", "Set Target"), HookReturnFromArena },
            {
                new(SceneNames.Bellway_Centipede_Arena, "Bell Beast DefeatedCentipede NPC", "Control"),
                HookReturnFromSuccessfulFight
            },
        });
    }

    protected override void DoUnload()
    {
        foreach (var kvp in FastTravelScenes)
        {
            ItemChangerHost.Singleton.GameEvents.RemoveSceneEdit(kvp.Key, SetArenaReturnScene);
        }
    }


    private void HookBellwayEntrypoint(PlayMakerFSM fsm)
    {
        // Ensure Bell Eater fight entry is available
        var state = fsm.MustGetState("State");
        state.Actions = [];
        state.AddLambdaMethod(_ => { fsm.SendEvent(BellEaterAvailable.Value ? "APPEARED" : "FINISHED"); });
        var thisScene = fsm.MustGetState("This Scene?");
        thisScene.Actions = [];
        thisScene.AddLambdaMethod(_ => { fsm.SendEvent("TRUE"); });
    }

    private void HookCallBellBeast(PlayMakerFSM fsm)
    {
        // Allow calling Bell Beast regardless of Bell Eater status
        fsm.GetState("Centipede?")!.ReplaceActionsOfType<PlayerDataVariableTest>(oldTest =>
            new CustomCheckFsmStateAction(oldTest) { GetIsTrue = () => false });
        fsm.GetState("Appear Delay")!.ReplaceActionsOfType<PlayerDataVariableTest>(oldTest =>
            new CustomCheckFsmStateAction(oldTest) { GetIsTrue = () => false });
    }

    // When the player leaves the Bell Beast arena (either through the transition or after killing Bell Eater), the
    // game attempts to return the player to the last bellway station at which they called the Bell Beast.
    // The following 2 hooks ensure the player is returned to the same bellway station that they entered.
    // It also prevents a softlock when the player leaves the arena without ever having called the bell beast.
    private void HookReturnFromArena(PlayMakerFSM fsm)
    {
        FsmState setTarget = fsm.MustGetState("Set Target");
        setTarget.GetFirstActionOfType<GetFastTravelScene>()!.Location = _arenaReturnLocation;
    }

    private void HookReturnFromSuccessfulFight(PlayMakerFSM fsm)
    {
        FsmState setTarget = fsm.MustGetState("Time Passes");
        setTarget.GetFirstActionOfType<GetFastTravelScene>()!.Location = _arenaReturnLocation;
    }

    private void SetArenaReturnScene(Scene scene)
    {
        if (!FastTravelScenes.TryGetValue(scene.name, out var fastTravelLocation))
        {
            LogWarn($"Scene {scene.name} does not contain a bellway station.");
            return;
        }

        _arenaReturnLocation = fastTravelLocation;
    }
}