using ItemChanger.Containers;
using ItemChanger.Events;
using ItemChanger.Logging;
using ItemChanger.Modules;
using ItemChanger.Silksong.Modules;
using ItemChanger.Silksong.StartDefs;
using ItemChanger.Silksong.Util;

namespace ItemChanger.Silksong
{
    public class SilksongHost : ItemChangerHost
    {
        internal SilksongHost() 
        {
            MessageUtil.Setup();
            Finder = new();
            Finder.DefineItemSheet(new(RawData.BaseItemList.GetBaseItems(), 0f));
            Finder.DefineLocationSheet(new(RawData.BaseLocationList.GetBaseLocations(), 0f));
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
            PlayerDataEditModule pde = new();
            pde.AddPDEdit(nameof(PlayerData.bindCutscenePlayed), true);

            return [
                pde,
                ];
        }

        private LifecycleEvents.Invoker? lifecycleInvoker;
        private GameEvents.Invoker? gameInvoker;

        protected override void PrepareEvents(LifecycleEvents.Invoker lifecycleInvoker, GameEvents.Invoker gameInvoker)
        {
            this.lifecycleInvoker = lifecycleInvoker;
            this.gameInvoker = gameInvoker;

            On.GameManager.StartNewGame += BeforeStartNewGameHook;
            On.GameManager.ContinueGame += OnContinueGame;
            On.GameManager.BeginSceneTransition += TransitionHook;
            On.GameManager.ResetSemiPersistentItems += OnResetSemiPersistentItems;
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }

        protected override void UnhookEvents(LifecycleEvents.Invoker lifecycleInvoker, GameEvents.Invoker gameInvoker)
        {
            this.lifecycleInvoker = null;
            this.gameInvoker = null;

            On.GameManager.StartNewGame -= BeforeStartNewGameHook;
            On.GameManager.ContinueGame -= OnContinueGame;
            On.GameManager.BeginSceneTransition -= TransitionHook;
            On.GameManager.ResetSemiPersistentItems -= OnResetSemiPersistentItems;
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged -= OnActiveSceneChanged;
        }

        private void OnActiveSceneChanged(UnityEngine.SceneManagement.Scene from, UnityEngine.SceneManagement.Scene to)
        {
            if (to.name == "Menu_Title")
            {
                lifecycleInvoker?.NotifyOnLeaveGame();
                return;
            }

            gameInvoker?.NotifyPersistentUpdate(); // TODO: move to execute before IC.Core
        }

        private void OnResetSemiPersistentItems(On.GameManager.orig_ResetSemiPersistentItems orig, GameManager self)
        {
            gameInvoker?.NotifySemiPersistentUpdate();
            orig(self);
        }

        private void TransitionHook(On.GameManager.orig_BeginSceneTransition orig, GameManager self, GameManager.SceneLoadInfo info)
        {
            string targetScene = info.SceneName;
            string targetGate = info.EntryGateName;

            gameInvoker?.NotifyBeforeNextSceneLoaded(new Events.Args.BeforeSceneLoadedEventArgs(targetScene)); // TODO: add gate info
            // TODO: transition overrides
            orig(self, info);
        }

        private void OnContinueGame(On.GameManager.orig_ContinueGame orig, GameManager self)
        {
            lifecycleInvoker?.NotifyBeforeContinueGame();
            lifecycleInvoker?.NotifyOnEnterGame();
            orig(self);
            lifecycleInvoker?.NotifyAfterContinueGame();
        }

        private void BeforeStartNewGameHook(On.GameManager.orig_StartNewGame orig, GameManager self, bool permadeathMode, bool bossRushMode)
        {
            lifecycleInvoker?.NotifyBeforeStartNewGame();

            PlayerData pd = PlayerData.CreateNewSingleton(addEditorOverrides: false);
            GameManager.instance.playerData = pd;
            pd.permadeathMode = permadeathMode ? GlobalEnums.PermadeathModes.On : GlobalEnums.PermadeathModes.Off;
            Platform.Current.PrepareForNewGame(self.profileID);
            ActiveProfile!.Load();
            lifecycleInvoker?.NotifyOnEnterGame();

            if (ActiveProfile!.Modules.Get<StartDefModule>() is StartDefModule { StartDef: StartDef start })
            {
                start.GetRespawnInfo().SetRespawn();
                self.StartCoroutine(self.RunContinueGame(self.IsMenuScene()));
            }
            else
            {
                self.StartCoroutine(self.RunStartNewGame());
            }

            lifecycleInvoker?.NotifyAfterStartNewGame();
            lifecycleInvoker?.NotifyOnSafeToGiveItems(); // TODO: move
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
}
