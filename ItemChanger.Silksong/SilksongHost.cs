using Benchwarp.Data;
using GlobalEnums;
using HarmonyLib;
using ItemChanger.Containers;
using ItemChanger.Events;
using ItemChanger.Logging;
using ItemChanger.Modules;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.Modules;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.StartDefs;
using ItemChanger.Silksong.Util;
using TeamCherry.Localization;
using TeamCherry.SharedUtils;

namespace ItemChanger.Silksong
{
    public class SilksongHost : ItemChangerHost
    {
        public static SilksongHost Instance => (SilksongHost)ItemChangerHost.Singleton;

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
                new ConsistentRandomnessModule()
                ];
        }

        private LifecycleEvents.Invoker? lifecycleInvoker;
        private GameEvents.Invoker? gameInvoker;

        public const string Wildcard = "*";

        /// <summary>
        /// Registers a delegate to run whenever a FSM matching the given (scene name, object name, FSM name) tuple is loaded.
        /// <br/>The scene and object names can be Wildcard ("*") instead to match any scene or any object, respectively.
        /// <br/>Fsm edits can equivalently be managed by <see cref="FsmEditGroup"/>.
        /// </summary>
        public void AddFsmEdit(FsmId id, Action<PlayMakerFSM> edit)
        {
            fsmEdits[id] = fsmEdits.GetValueOrDefault(id) + edit;
        }

        /// <summary>
        /// Unregisters a delegate added by <see cref="AddFsmEdit(FsmId, Action{PlayMakerFSM})"/>.
        /// </summary>
        public void RemoveFsmEdit(FsmId id, Action<PlayMakerFSM> edit)
        {
            if (fsmEdits.TryGetValue(id, out Action<PlayMakerFSM>? result))
            {
                result -= edit;
                if (result != null)
                {
                    fsmEdits[id] = result;
                }
                else
                {
                    fsmEdits.Remove(id);
                }
            }
        }

        private readonly Dictionary<FsmId, Action<PlayMakerFSM>?> fsmEdits = [];

        public void AddLanguageEdit(LanguageString id, Func<string, string> edit)
        {
            if (!languageEdits.TryGetValue(id, out List<Func<string, string>> list))
            {
                languageEdits.Add(id, list = []);
            }
            list.Add(edit);
        }

        public void RemoveLanguageEdit(LanguageString id, Func<string, string> edit)
        {
            if (languageEdits.TryGetValue(id, out List<Func<string, string>> list))
            {
                list.Remove(edit);
                if (list.Count == 0) languageEdits.Remove(id);
            }
        }

        private readonly Dictionary<LanguageString, List<Func<string, string>>> languageEdits = [];

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
            Harmony.UnpatchID(typeof(Patches).FullName);
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged -= OnActiveSceneChanged;
            MessageUtil.Clear();
        }

        private void OnActiveSceneChanged(UnityEngine.SceneManagement.Scene from, UnityEngine.SceneManagement.Scene to)
        {
            if (from.name == SceneNames.Menu_Title)
            {
                GameManager.instance.DoNextFrame(() => lifecycleInvoker?.NotifyOnSafeToGiveItems());
            }

            gameInvoker?.NotifyPersistentUpdate(); // TODO: move to execute before IC.Core
        }

        [HarmonyPatch]
        private static class Patches
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
                return false;
            }

            [HarmonyPatch(typeof(GameManager), nameof(GameManager.ContinueGame))]
            [HarmonyPrefix]
            private static void BeforeContinueGame()
            {
                Host.lifecycleInvoker?.NotifyBeforeContinueGame();
                Host.ActiveProfile!.Load();
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

            [HarmonyPatch(typeof(PlayMakerFSM), nameof(PlayMakerFSM.Start))]
            [HarmonyPrefix]
            private static void Prefix(PlayMakerFSM __instance)
            {
                PlayMakerFSM fsm = __instance;
                string sceneName = fsm.gameObject.scene.name;
                string objectName = fsm.gameObject.name;
                string fsmName = fsm.FsmName;
                List<FsmId> matchingIds = [
                    new(sceneName, objectName, fsmName),
                    new(Wildcard, objectName, fsmName),
                    new(sceneName, Wildcard, fsmName),
                    new(Wildcard, Wildcard, fsmName)
                ];
                try
                {
                    foreach (FsmId id in matchingIds)
                    {
                        Instance.fsmEdits.GetValueOrDefault(id)?.Invoke(fsm);
                    }
                }
                catch (Exception err)
                {
                    Instance.Logger.LogError($"Error applying FSM edit to FSM {fsmName} in object {objectName} in scene {sceneName}: {err}");
                }
            }

            [HarmonyPatch(typeof(Language), nameof(Language.Get), [typeof(string), typeof(string)])]
            private static void Postfix(string key, string sheetTitle, ref string __result)
            {
                LanguageString id = new(sheetTitle, key);
                if (Host.languageEdits.TryGetValue(id, out List<Func<string, string>> list))
                {
                    foreach (Func<string, string > f in list)
                    {
                        try
                        {
                            __result = f(__result);
                        }
                        catch (Exception e)
                        {
                            Host.Logger.LogError($"Error performing language edit on key {key} in sheet {sheetTitle}:\n{e}");
                        }
                    }
                }
            }

            [HarmonyPatch(typeof(GameManager), nameof(GameManager.ReturnToMainMenu))]
            [HarmonyPriority(-100)]  // This has to come before DataManager causes IC to unhook
            private static void Prefix()
            {
                Host.lifecycleInvoker?.NotifyOnLeaveGame();
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
