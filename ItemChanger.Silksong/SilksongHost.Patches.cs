using Benchwarp.Data;
using GlobalEnums;
using HarmonyLib;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.Modules;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.StartDefs;
using TeamCherry.Localization;
using TeamCherry.SharedUtils;

namespace ItemChanger.Silksong;

public partial class SilksongHost
{
    private readonly Dictionary<FsmId, Action<PlayMakerFSM>?> fsmEdits = [];
    private readonly Dictionary<LanguageString, List<Func<string, string>>> languageEdits = [];

    public const string ITEMCHANGER_EXACT_SHEET = "ItemChanger.Exact";

    private void OnActiveSceneChanged(UnityEngine.SceneManagement.Scene from, UnityEngine.SceneManagement.Scene to)
    {
        if (from.name == SceneNames.Menu_Title)
        {
            GameManager.instance.DoNextFrame(() => lifecycleInvoker?.NotifyOnSafeToGiveItems());
        }

        gameInvoker?.NotifyPersistentUpdate(); // TODO: move to execute before IC.Core
    }

    private void DoModifyTransition(GameManager.SceneLoadInfo info)
    {
        string origTargetScene = info.SceneName;
        string origTargetGate = info.EntryGateName;

        static TransitionPoint? FindTransition(GameManager.SceneLoadInfo info) =>
                UObject.FindObjectsByType<TransitionPoint>(UnityEngine.FindObjectsSortMode.None)
                .FirstOrDefault(t => t.targetScene == info.SceneName && t.entryPoint == info.EntryGateName);

        if (!info.IsBaseGameSceneLoadInfo()) return; // do not modify transitions with custom sceneloadinfo; e.g. Doorwarp

        if (FindTransition(info) is not TransitionPoint tp || !tp)
        {
            return; // TODO: deal with nonstandard transitions, suffixes, etc
        }

        string sourceScene = GameManager.instance.GetSceneNameString();
        string sourceGate = tp.name;
        ModifyTransitionEventArgs modifyArgs = new()
        {
            SourceScene = sourceScene,
            SourceGate = sourceGate,
            OrigTargetScene = origTargetScene,
            OrigTargetGate = origTargetGate,
        };
        try
        {
            Host.ModifyTransition?.Invoke(modifyArgs);
        }
        catch (Exception e)
        {
            LogError($"Error invoking ModifyTransition:\n{e}");
        }
        if (modifyArgs.Modified)
        {
            info.SceneName = modifyArgs.TargetScene;
            info.EntryGateName = modifyArgs.TargetGate; // TODO: handle merged gate denormalization based on facing direction (i.e. mantis village bot1 bot3)
            info.SceneResourceLocation = null;
            try
            {
                Host.OnTransitionOverride?.Invoke(new(modifyArgs));
            }
            catch (Exception e)
            {
                LogError($"Error invoking OnTransitionOverride:\n{e}");
            }
        }
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
            try
            {
                Host.DoModifyTransition(info);
            }
            catch (Exception e)
            {
                LogError($"Error in DoModifyTransition:\n{e}");
            }

            string targetScene = info.SceneName;
            string targetGate = info.EntryGateName;

            Host.gameInvoker?.NotifyBeforeNextSceneLoaded(new BeforeSceneLoadedEventArgs(targetScene, targetGate, info));
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

        // Run this as a prefix to avoid logging messages when the sheet does not exist
        [HarmonyPrefix]
        [HarmonyPatch(typeof(Language), nameof(Language.Get), [typeof(string), typeof(string)])]
        private static bool LanguageGetPrefix(string key, string sheetTitle, ref string __result)
        {
            if (sheetTitle == ITEMCHANGER_EXACT_SHEET)
            {
                __result = key;
                return false;
            }

            return true;
        }

        [HarmonyPatch(typeof(Language), nameof(Language.Get), [typeof(string), typeof(string)])]
        private static void Postfix(string key, string sheetTitle, ref string __result)
        {
            LanguageString id = new(sheetTitle, key);
            if (Host.languageEdits.TryGetValue(id, out List<Func<string, string>> list))
            {
                foreach (Func<string, string> f in list)
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

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Language), nameof(Language.Has), [typeof(string), typeof(string)])]
        private static bool LanguageHas(string key, string sheetTitle, ref bool __result)
        {
            if (sheetTitle == ITEMCHANGER_EXACT_SHEET)
            {
                __result = true;
                return false;
            }

            LanguageString id = new(sheetTitle, key);
            if (Host.languageEdits.ContainsKey(id))
            {
                __result = true;
                return false;
            }

            return true;
        }

    }
}
