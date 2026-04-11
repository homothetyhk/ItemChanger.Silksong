using ItemChanger.Modules;
using Benchwarp.Doors.Obstacles;
using Benchwarp.Doors;
using Benchwarp.Data;
using System.Diagnostics.CodeAnalysis;
using UnityEngine.SceneManagement;
using ItemChanger.Silksong.Extensions;
using Newtonsoft.Json;

namespace ItemChanger.Silksong.Modules.Obstacles;

/// <summary>
/// Module which controls obstacle handling (transition fixes) using data provided by Benchwarp and by other modules.
/// </summary>
[SingletonModule]
public sealed class ObstacleHandlerModule : Module, IObstacleHandler
{
    /// <summary>
    /// ObstacleHandler used on obstacles that are not ignored by <see cref="LocalIgnores"/> or <see cref="GlobalIgnores"/>.
    /// </summary>
    public ObstacleHandler BaseObstacleHandler { get; set; } = new PermanentObstacleHandler();
    /// <summary>
    /// Rules that determine which obstacles to ignore on a per-transition basis.
    /// </summary>
    public Dictionary<TransitionKey, List<IgnoreObstacleRule>> LocalIgnores { get; } = [];
    /// <summary>
    /// Rules that determine which obstacles to ignore across all transitions.
    /// </summary>
    public List<IgnoreObstacleRule> GlobalIgnores { get; } = [];
    /// <summary>
    /// Use to replace room definitions from Benchwarp, or to add definitions for new rooms.
    /// </summary>
    public Dictionary<string, RoomData> OverrideRoomData { get; } = [];
    /// <summary>
    /// Use to replace door definitions from Benchwarp, or to add definitions for new doors.
    /// </summary>
    public Dictionary<TransitionKey, DoorData> OverrideDoorData { get; } = [];
    /// <summary>
    /// Modules subscribe by adding themselves to the list in OnLoad.
    /// Defines obstacles to be handled per-transition, in addition to those associated with its DoorData.
    /// RoomData and DoorData must be defined for any obstacles to be handled.
    /// </summary>
    [JsonIgnore] public List<ObstacleInjectorModule> ObstacleInjectors { get; } = [];
    
    protected override void DoLoad()
    {
        GameEvents.BeforeNextSceneLoaded += BeforeNextScene;
    }

    protected override void DoUnload()
    {
        GameEvents.BeforeNextSceneLoaded -= BeforeNextScene;
    }

    private bool TryResolveObstacleData(string sceneName, string gateName, [MaybeNullWhen(false)] out RoomData roomData, [MaybeNullWhen(false)] out DoorData doorData)
    {
        TransitionKey key = new(sceneName, gateName);
        bool roomFound = OverrideRoomData.TryGetValue(sceneName, out roomData) || DoorList.Rooms.TryGetValue(sceneName, out roomData);
        bool doorFound = OverrideDoorData.TryGetValue(key, out doorData) || DoorList.Doors.TryGetValue(key, out doorData);
        return roomFound && doorFound;
    }

    private void BeforeNextScene(Events.Args.BeforeSceneLoadedEventArgs e)
    {
        if (e is not BeforeSceneLoadedEventArgs args || !args.Info.IsBaseGameSceneLoadInfo()
            || !TryResolveObstacleData(args.TargetScene, args.TargetGate, out RoomData? room, out DoorData? door))
        {
            return;
        }

        ((IObstacleHandler)this).BeforeTransition(room, door);
        AttachToLoad(args.Info, room, door);
    }

    private void AttachToLoad(GameManager.SceneLoadInfo info, RoomData room, DoorData door)
    {
        GameManager.SceneTransitionBegan += OnBeginLoad;
        void OnBeginLoad(SceneLoad load)
        {
            GameManager.SceneTransitionBegan -= OnBeginLoad;
            if (!ReferenceEquals(load.SceneLoadInfo, info)) return;
            HookActivationComplete(load, room, door);
        }
    }

    private void HookActivationComplete(SceneLoad load, RoomData room, DoorData door)
    {
        load.ActivationComplete += OnActivationComplete;
        void OnActivationComplete()
        {
            GameManager.instance.sceneLoad.ActivationComplete -= OnActivationComplete;
            Scene newScene = SceneManager.GetActiveScene();
            ((IObstacleHandler)this).OnSceneChange(newScene, room, door);
        }
    }

    void IObstacleHandler.BeforeTransition(RoomData room, DoorData gate)
    {
        LocalIgnores.TryGetValue(gate.Self, out List<IgnoreObstacleRule>? rules);

        foreach (ObstacleInfo o in GetGateObstacles(room, gate))
        {
            if (!ShouldIgnore(rules, room, gate, o))
            {
                BaseObstacleHandler.HandleObstacleBeforeTransition(room, gate, o);
            }
        }
    }

    void IObstacleHandler.OnSceneChange(Scene scene, RoomData room, DoorData gate)
    {
        LocalIgnores.TryGetValue(gate.Self, out List<IgnoreObstacleRule>? rules);

        foreach (ObstacleInfo o in GetGateObstacles(room, gate))
        {
            if (!ShouldIgnore(rules, room, gate, o))
            {
                BaseObstacleHandler.HandleObstacleOnActiveSceneChange(scene, room, gate, o);
            }
        }
    }

    private IEnumerable<ObstacleInfo> GetGateObstacles(RoomData room, DoorData door)
    {
        return door.Obstacles.Concat(ObstacleInjectors.SelectMany(m => m.GetObstacles(room, door)));
    }

    private bool ShouldIgnore(List<IgnoreObstacleRule>? localRules, RoomData room, DoorData door, ObstacleInfo info)
    {
        if (localRules is not null)
        {
            foreach (IgnoreObstacleRule rule in localRules)
            {
                if (rule.ShouldIgnore(room, door, info)) return true;
            }
        }
        foreach (IgnoreObstacleRule rule in GlobalIgnores)
        {
            if (rule.ShouldIgnore(room, door, info)) return true;
        }
        return false;
    }

    /// <summary>
    /// Adds a custom rule to ignore obstacles associated with a specific transition.
    /// </summary>
    public void AddLocalIgnore(TransitionKey key, IgnoreObstacleRule rule)
    {
        if (!LocalIgnores.TryGetValue(key, out List<IgnoreObstacleRule> list))
        {
            LocalIgnores.Add(key, list = []);
        }
        list.Add(rule);
    }
    /// <summary>
    /// Ignores all obstacles associated with the specified transition.
    /// </summary>
    public void IgnoreObstacle(TransitionKey key) => AddLocalIgnore(key, IgnoreObstacleRule.Any);
    /// <summary>
    /// Ignores obstacles associated with the specified transition with the specified parameters. Null parameters result in no constraint.
    /// </summary>
    public void IgnoreObstacleWith(TransitionKey key, ObstacleType? type, ObstacleSeverity? severity) => AddLocalIgnore(key, new IgnoreObstacleByParamRule(type, severity));
    /// <summary>
    /// Ignores all obstacles with the specified parameteres. Null parameters result in no constraint.
    /// </summary>
    public void IgnoreObstacleWithGlobally(ObstacleType? type, ObstacleSeverity? severity) => GlobalIgnores.Add(new IgnoreObstacleByParamRule(type, severity));
    /// <summary>
    /// Ignores all obstacles associated with the specified transition, and with ObstacleInfo matching the given up to record equality.
    /// </summary>
    public void IgnoreExactObstacle(TransitionKey key, ObstacleInfo info) => AddLocalIgnore(key, new IgnoreExactObstacleRule(info));
    /// <summary>
    /// Ignores all obstacles matching the given up to record equality.
    /// </summary>
    public void IgnoreExactObstacleGlobally(ObstacleInfo info) => GlobalIgnores.Add(new IgnoreExactObstacleRule(info));
    /// <summary>
    /// Ignores all obstacles associated with the autoclose of the door (specifically, all obstacles in the room of type ClosedAfterProgression).
    /// </summary>
    public void IgnoreChapelDoorAutoclose(ChapelDoorObstacle.ChapelDoor door)
    {
        RoomData data = door switch
        {
            ChapelDoorObstacle.ChapelDoor.ChapelReaper => BaseRoomList.Greymoor_20b,
            ChapelDoorObstacle.ChapelDoor.ChapelBeast => BaseRoomList.Ant_20,
            ChapelDoorObstacle.ChapelDoor.ChapelWanderer => BaseRoomList.Bonegrave,
            ChapelDoorObstacle.ChapelDoor.ChapelArchitect => BaseRoomList.Under_17,
            ChapelDoorObstacle.ChapelDoor.ChapelWitch => BaseRoomList.Shellwood_25,
            _ => throw new NotImplementedException(door.ToString()),
        };

        if (OverrideRoomData.TryGetValue(data.Name, out RoomData? altData)) data = altData;

        foreach (DoorData gate in data.Gates)
        {
            IgnoreObstacleWith(gate.Self, ObstacleType.ClosedAfterProgression, null);
        }
    }

    /// <summary>
    /// Rule to be used with <see cref="LocalIgnores"/> and <see cref="GlobalIgnores"/> to suppress base and injected obstacles.
    /// </summary>
    public abstract record IgnoreObstacleRule
    {
        public static IgnoreObstacleRule Any { get; } = new IgnoreObstacleByParamRule(null, null);

        public abstract bool ShouldIgnore(RoomData room, DoorData door, ObstacleInfo info);
    }

    private sealed record IgnoreObstacleByParamRule(ObstacleType? Type, ObstacleSeverity? Severity) : IgnoreObstacleRule
    {
        public override bool ShouldIgnore(RoomData room, DoorData door, ObstacleInfo info)
        {
            bool ignoredType = Type is null || Type == info.Type;
            bool ignoredSeveity = Severity is null || Severity == info.Severity;
            return ignoredType && ignoredSeveity;
        }
    }

    private sealed record IgnoreExactObstacleRule(ObstacleInfo Info) : IgnoreObstacleRule
    {
        public override bool ShouldIgnore(RoomData room, DoorData door, ObstacleInfo info)
        {
            return info.Equals(Info);
        }
    }

    /// <summary>
    /// Base class for modules that provide additional obstacles for the <see cref="ObstacleHandlerModule"/>.
    /// Implements OnLoad to handle registering with the ObstacleHandlerModule.
    /// </summary>
    public abstract class ObstacleInjectorModule : Module
    {
        protected override void DoLoad() 
        {
            ActiveProfile!.Modules.GetOrAdd<ObstacleHandlerModule>().ObstacleInjectors.Add(this);
        }
        protected override void DoUnload() { }

        /// <summary>
        /// Returns custom obstacles associated with the door.
        /// </summary>
        public abstract IEnumerable<ObstacleInfo> GetObstacles(RoomData room, DoorData door);
    }
}
