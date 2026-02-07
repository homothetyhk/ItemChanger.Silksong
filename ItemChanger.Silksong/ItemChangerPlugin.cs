using BepInEx;
using Silksong.DataManager;

namespace ItemChanger.Silksong
{
    [BepInDependency("io.github.benchwarp")]
    [BepInDependency("org.silksong-modding.datamanager")]
    [BepInAutoPlugin(id: "io.github.silksong.itemchanger")]
    public partial class ItemChangerPlugin : BaseUnityPlugin, ISaveDataMod<ItemChangerProfile>
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

        public ItemChangerProfile? SaveData
        {
            get => Host.ActiveProfile;
            set
            {
                // Can't just overwrite Host.ActiveProfile, because the profile needs to be manually
                // Disposed. This applies both when returning to the main menu, and also when using
                // Benchwarp (which reloads the file without passing through the main menu).
                if (Host.ActiveProfile != null)
                {
                    Host.ActiveProfile.Dispose();
                    Host.ActiveProfile = null;
                }
                if (value != null)
                {
                    // IC.Core ought to expose a way to do this?
                    var flags = System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance;
                    typeof(ItemChangerProfile).GetMethod("AttachHost", flags).Invoke(value, new object[] { Host });
                    typeof(ItemChangerProfile).GetMethod("DoHook", flags).Invoke(value, new object[]{});
                    Host.ActiveProfile!.Load();
                }
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