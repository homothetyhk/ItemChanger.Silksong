using BepInEx;

namespace ItemChanger.Silksong
{
    [BepInDependency("io.github.benchwarp")]
    [BepInAutoPlugin(id: "io.github.silksong.itemchanger")]
    public partial class ItemChangerPlugin : BaseUnityPlugin
    {
        public static ItemChangerPlugin Instance { get => field ?? throw new NullReferenceException("ItemChangerPlugin is not loaded!"); private set; }
        internal new BepInEx.Logging.ManualLogSource Logger => base.Logger;

        private void Awake()
        {
            Instance = this;
            Logger.LogInfo($"Plugin {Name} ({Id}) has loaded!");
        }

        // this fails silently if IC.Core is not installed!
        private void Start()
        {
            try
            {
                new SilksongHost();
                Logger.LogInfo($"Created host!");
                // On.UIManager.StartNewGame += StartItemChangerProfile;
            }
            catch (Exception e)
            {
                Logger.LogError($"Error creating host: {e}");
            }
        }

        private void StartItemChangerProfile(On.UIManager.orig_StartNewGame orig, UIManager self, bool permaDeath, bool bossRush)
        {
            Logger.LogInfo("Creating IC profile...");
            try
            {
                Host.ActiveProfile?.Dispose();
                new ItemChangerProfile(Host);
            }
            catch (Exception e)
            {
                Logger.LogError($"Error creating IC profile: {e}");
            }
            orig(self, permaDeath, bossRush);
        }

        private System.Collections.IEnumerator WaitToDo()
        {
            while (true)
            {
                try
                {
                    yield break;
                }
                catch (Exception e)
                {
                    Logger.LogError(e);
                }
                yield return null;
            }
        }
    }
}