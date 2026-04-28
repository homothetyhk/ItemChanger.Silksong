using HarmonyLib;
using ItemChanger.Containers;
using ItemChanger.Events;
using ItemChanger.Logging;
using ItemChanger.Modules;
using ItemChanger.Silksong.Modules;
using ItemChanger.Silksong.Modules.Obstacles;
using ItemChanger.Silksong.Modules.YNBox;
using ItemChanger.Silksong.Util;

namespace ItemChanger.Silksong;

public partial class SilksongHost : ItemChangerHost
{
    internal SilksongHost() 
    {
        MessageUtil.Setup();
        Finder = new();
        Finder.DefineItemSheet(new(RawData.BaseItemList.GetBaseItems(), 0f));
        Finder.DefineLocationSheet(new(RawData.BaseLocationList.GetBaseLocations(), 0f));
        ItemChangerPlugin.Instance.BeforeProfileDispose += () => Instance.lifecycleInvoker?.NotifyOnLeaveGame();
    }

    public override ILogger Logger { get; } = new PluginLogger();

    public override ContainerRegistry ContainerRegistry { get; } = new()
    {
        DefaultSingleItemContainer = Containers.ShinyContainer.Instance,
        DefaultMultiItemContainer = Containers.ChestContainer.Instance,
    };

    public override Finder Finder { get; }

    public override IEnumerable<Module> BuildDefaultModules()
    {
        return [
            new CustomYNEnableModule(),
            new YNBoxDamageControlModule(),
            new UIProxyControlBlockerModule(),
            new SkipEndDialogueAnimModule(),
            new CrestUIMsgRepairModule(),
            new ConsistentRandomnessModule(),
            new ObstacleHandlerModule(),
            new ChapelDoorObstacleModule(),
            ];
    }

    private LifecycleEvents.Invoker? lifecycleInvoker;
    private GameEvents.Invoker? gameInvoker;


    protected override void PrepareEvents(LifecycleEvents.Invoker lifecycleInvoker, GameEvents.Invoker gameInvoker)
    {
        this.lifecycleInvoker = lifecycleInvoker;
        this.gameInvoker = gameInvoker;

        Type patches = typeof(Patches);
        Harmony harmony = new(patches.FullName);
        harmony.PatchAll(patches);
        UnityEngine.SceneManagement.SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    protected override void UnhookEvents(LifecycleEvents.Invoker lifecycleInvoker, GameEvents.Invoker gameInvoker)
    {
        this.lifecycleInvoker = null;
        this.gameInvoker = null;

        foreach (FsmId id in fsmEdits.Keys)
        {
            Logger.LogWarn($"FSM edit not cleaned up for {id.FsmName} in object {id.ObjectName} in scene {id.SceneName}");
        }
        fsmEdits.Clear();
        foreach (Serialization.LanguageString id in languageEdits.Keys)
        {
            Logger.LogWarn($"Language edit not cleaned up for key {id.Key} in sheet {id.Sheet}");
        }
        languageEdits.Clear();
        Harmony.UnpatchID(typeof(Patches).FullName);
        UnityEngine.SceneManagement.SceneManager.activeSceneChanged -= OnActiveSceneChanged;
        MessageUtil.Clear();
    }
}

file class PluginLogger : ILogger
{
    void ILogger.LogError(string? message)
    {
        ItemChangerPlugin.Instance.Logger.LogError(message);
    }

    void ILogger.LogFine(string? message)
    {
#if DEBUG 
        ItemChangerPlugin.Instance.Logger.LogDebug(message);
#endif
    }

    void ILogger.LogDebug(string? message)
    {
        ItemChangerPlugin.Instance.Logger.LogDebug(message);
    }

    void ILogger.LogInfo(string? message)
    {
        ItemChangerPlugin.Instance.Logger.LogInfo(message);
    }

    void ILogger.LogWarn(string? message)
    {
        ItemChangerPlugin.Instance.Logger.LogWarning(message);
    }
}
