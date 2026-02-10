using BepInEx;
using ItemChanger.Silksong.Containers;

namespace ItemChanger.Silksong
{
    [BepInDependency("org.silksong-modding.fsmutil")]
    [BepInDependency("org.silksong-modding.assethelper")]
    [BepInDependency("org.silksong-modding.prepatcher")]
    [BepInDependency("org.silksong-modding.i18n")]
    [BepInDependency("org.silksong-modding.datamanager")]
    [BepInDependency("io.github.homothetyhk.benchwarp")]
    [BepInAutoPlugin(id: "io.github.silksong.itemchanger")]
    public partial class ItemChangerPlugin : BaseUnityPlugin
    {
        public static ItemChangerPlugin Instance { get => field ?? throw new NullReferenceException("ItemChangerPlugin is not loaded!"); private set; }
        internal new BepInEx.Logging.ManualLogSource Logger => base.Logger;

        private void Awake()
        {
            try
            {
                Logger.LogInfo("Loading ItemChanger...");
                Instance = this;
                RequestAssets();
                CreateHost();
                Logger.LogInfo($"Plugin {Name} ({Id}) has loaded!");
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                throw;
            }
        }

        private void Start()
        {
            try
            {
                DefineContainers();
            }
            catch (Exception e)
            {
                Logger.LogError($"Error creating host: {e}");
            }
        }

        private void CreateHost()
        {
            new SilksongHost();
        }

        private void DefineContainers()
        {
            ItemChangerHost.Singleton.ContainerRegistry.DefineContainer(new FleaContainer());
        }
        
        // The following is unused reference code for how to create a profile on new game 
        /*
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
        */
    }
}