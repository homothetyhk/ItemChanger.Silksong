using BepInEx;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Serialization;
using Silksong.DataManager;
using ItemChanger.Serialization;
using System.IO;

namespace ItemChanger.Silksong
{
    [BepInDependency("org.silksong-modding.fsmutil")]
    [BepInDependency("org.silksong-modding.assethelper")]
    [BepInDependency("org.silksong-modding.prepatcher")]
    [BepInDependency("org.silksong-modding.i18n")]
    [BepInDependency("org.silksong-modding.datamanager")]
    [BepInDependency("io.github.homothetyhk.benchwarp")]
    [BepInAutoPlugin(id: "io.github.silksong.itemchanger")]
    public partial class ItemChangerPlugin : BaseUnityPlugin, IRawSaveDataMod
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
                AtlasSpriteBundleRegistry.Hook(ItemChangerHost.Singleton);
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
        
        public bool HasSaveData => Host.ActiveProfile != null;

        public void WriteSaveData(Stream saveFile)
        {
            // WriteSaveData is never called if Host.ActiveProfile is null.
            SerializationHelper.Serialize(saveFile, Host.ActiveProfile!);
        }

        public void ReadSaveData(Stream? saveFile)
        {
            // Can't just overwrite Host.ActiveProfile, because the profile needs to be manually
            // Disposed. This applies both when returning to the main menu, and also when using
            // Benchwarp (which reloads the file without passing through the main menu).
            if (Host.ActiveProfile != null)
            {
                Host.ActiveProfile.Dispose();
                Host.ActiveProfile = null;
            }
            if (saveFile != null)
            {
                var profile = ItemChangerProfile.FromStream(Host, saveFile);
                profile.Load();
            }
        }
    }
}