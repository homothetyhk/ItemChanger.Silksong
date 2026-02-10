using GlobalEnums;
using HarmonyLib;
using ItemChanger.Containers;
using ItemChanger.Events;
using ItemChanger.Logging;
using ItemChanger.Modules;
using ItemChanger.Silksong.Modules;
using ItemChanger.Silksong.StartDefs;
using ItemChanger.Silksong.Util;
using TeamCherry.SharedUtils;

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
            return [
                new ConsistentRandomnessModule()
                ];
        }

        private LifecycleEvents.Invoker? lifecycleInvoker;
        private GameEvents.Invoker? gameInvoker;

        protected override void PrepareEvents(LifecycleEvents.Invoker lifecycleInvoker, GameEvents.Invoker gameInvoker)
        {
            this.lifecycleInvoker = lifecycleInvoker;
            this.gameInvoker = gameInvoker;

            Type gmPatches = typeof(GameManagerPatches);
            Harmony harmony = new(gmPatches.FullName);
            harmony.PatchAll(gmPatches);
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }

        protected override void UnhookEvents(LifecycleEvents.Invoker lifecycleInvoker, GameEvents.Invoker gameInvoker)
        {
            this.lifecycleInvoker = null;
            this.gameInvoker = null;

            Harmony.UnpatchID(typeof(GameManagerPatches).FullName);
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged -= OnActiveSceneChanged;
            MessageUtil.Clear();
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

        [HarmonyPatch]
        private static class GameManagerPatches
        {
            [HarmonyPatch(typeof(GameManager), nameof(GameManager.StartNewGame))]
            [HarmonyPrefix]
            private static bool BeforeStartNewGame(GameManager __instance, bool permadeathMode, bool bossRushMode)
            {
                Host.lifecycleInvoker?.NotifyBeforeStartNewGame();

                PlayerData pd = PlayerData.CreateNewSingleton(addEditorOverrides: false);
                GameManager.instance.playerData = pd;
                pd.SetVariable(nameof(PlayerData.permadeathMode), permadeathMode ? PermadeathModes.On : PermadeathModes.Off);
                Platform.Current.PrepareForNewGame(__instance.profileID);
                Host.ActiveProfile!.Load();
                Host.lifecycleInvoker?.NotifyOnEnterGame();

                if (Host.ActiveProfile!.Modules.Get<StartDefModule>() is StartDefModule { StartDef: StartDef start })
                {
                    pd.SetBool(nameof(PlayerData.bindCutscenePlayed), true); // so that entering Tut_01 later does not trigger the wakeup sequence
                    start.GetRespawnInfo().SetRespawn();
                    __instance.StartCoroutine(__instance.RunContinueGame(__instance.IsMenuScene()));
                }
                else
                {
                    __instance.StartCoroutine(__instance.RunStartNewGame());
                }

                Host.lifecycleInvoker?.NotifyAfterStartNewGame();
                Host.lifecycleInvoker?.NotifyOnSafeToGiveItems(); // TODO: move
                return false;
            }

            [HarmonyPatch(typeof(GameManager), nameof(GameManager.ContinueGame))]
            [HarmonyPrefix]
            private static void BeforeContinueGame()
            {
                Host.lifecycleInvoker?.NotifyBeforeContinueGame();
                Host.lifecycleInvoker?.NotifyOnEnterGame();
            }

            [HarmonyPatch(typeof(GameManager), nameof(GameManager.ContinueGame))]
            [HarmonyPostfix]
            private static void AfterContinueGame()
            {
                Host.lifecycleInvoker?.NotifyAfterContinueGame();
            }

            [HarmonyPrefix]
            [HarmonyPatch(typeof(GameManager), nameof(GameManager.ResetSemiPersistentItems))]
            private static void BeforeResetSemiPersistentItems()
            {
                Host.gameInvoker?.NotifySemiPersistentUpdate();
            }

            [HarmonyPrefix]
            [HarmonyPatch(typeof(GameManager), nameof(GameManager.BeginSceneTransition))]
            private static void BeforeBeginSceneTransition(GameManager __instance, GameManager.SceneLoadInfo info)
            {
                string targetScene = info.SceneName;
                string targetGate = info.EntryGateName;

                Host.gameInvoker?.NotifyBeforeNextSceneLoaded(new Events.Args.BeforeSceneLoadedEventArgs(targetScene)); // TODO: add gate info
                // TODO: transition overrides
            }
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
