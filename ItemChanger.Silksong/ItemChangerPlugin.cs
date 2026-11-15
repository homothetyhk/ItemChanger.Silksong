using BepInEx;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.Assets;
using Newtonsoft.Json.UnityConverters.Math;
using ItemChanger.Serialization;
using ItemChanger.Silksong.RawData;

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
        private static ItemChangerPlugin? UnsafeInstance;
        public static ItemChangerPlugin Instance => UnsafeInstance != null ? UnsafeInstance 
            : throw new NullReferenceException("ItemChangerPlugin is not loaded!");

        internal new BepInEx.Logging.ManualLogSource Logger => base.Logger;

        private static readonly HashSet<Action<SilksongHost>> onNewHost = [];
        /// <summary>
        /// Invoked when <see cref="SilksongHost"/> is instantiated during Awake. Invoked on every Awake with hot reload.
        /// If a delegate is added after host instantiation, this delegate is immediately invokved.
        /// </summary>
        public static event Action<SilksongHost> OnNewHost
        {
            add
            {
                if (UnsafeInstance != null)
                    value(SilksongHost.Instance);

                onNewHost.Add(value);
            }
            remove => onNewHost.Remove(value);
        }

        private void ReportUnimplemented(Type type, IEnumerable<string> implemented)
        {
            HashSet<string> unimplemented = [];
            foreach (var field in type.GetFields().Where(f => f.IsPublic && f.GetRawConstantValue() is string)) unimplemented.Add((string)field.GetRawConstantValue());
            foreach (var name in implemented) unimplemented.Remove(name);

            // Location may be empty if this is an in-memory assembly.
            var loc = typeof(ItemChangerPlugin).Assembly.Location.Trim();
            if (loc == "")
                return;

            var path = Path.Join(Directory.GetParent(typeof(ItemChangerPlugin).Assembly.Location).FullName, $"Unimplemented-{type.Name}.txt");
            if (File.Exists(path)) File.Delete(path);

            if (unimplemented.Count > 0)
            {
                Logger.LogWarning($"{unimplemented.Count} {type.Name} still unimplemented.");
                string content = string.Join("\n", unimplemented.OrderBy(s => s));
                File.WriteAllText(path, content);
            }
        }

        private void Awake()
        {
            try
            {
                Logger.LogInfo("Loading ItemChanger...");
                UnsafeInstance = this;
                CreateHost();
                ReportUnimplemented(typeof(ItemNames), ItemChangerHost.Singleton.Finder.ItemNames);
                ReportUnimplemented(typeof(LocationNames), ItemChangerHost.Singleton.Finder.LocationNames);
                SerializationHelper.Serializer.Converters.Add(new ColorConverter());
                AssetCache.Init(SilksongHost.Instance);
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
            SilksongHost host = new();
            foreach (Action<SilksongHost> action in onNewHost) action(host);
        }

        private void DefineContainers()
        {
            ItemChangerHost.Singleton.ContainerRegistry.DefineContainer(new FleaContainer());
            ItemChangerHost.Singleton.ContainerRegistry.DefineContainer(new TabletContainer());
            ItemChangerHost.Singleton.ContainerRegistry.DefineContainer(new WeaverCorpseContainer());
            ItemChangerHost.Singleton.ContainerRegistry.DefineContainer(new CrawSummonsContainer());
        }

        private void OnDestroy()
        {
            SilksongHost.Instance.ActiveProfile?.Dispose();
            SilksongHost.DetachSingleton();
            UnsafeInstance = null;
            Logger.LogInfo($"Plugin {Name} ({Id}) unloaded.");
        }
    }
}
