using ItemChanger;
using ItemChanger.Modules;
using ItemChanger.Silksong.Modules;
using ItemChanger.Silksong.StartDefs;
using System.Collections.ObjectModel;

namespace ItemChangerTesting
{
    internal abstract class Test : Module
    {
        public static ReadOnlyDictionary<TestFolder, ReadOnlyCollection<Test>> TestGroups { get; } = new(typeof(Test).Assembly.GetTypes()
            .Where(t => t.IsSubclassOf(typeof(Test)) && !t.IsAbstract).Select(t => (Test)Activator.CreateInstance(t))
            .OrderByDescending(t => t.GetMetadata().Revision)
            .GroupBy(t => t.GetMetadata().Folder).ToDictionary(g => g.Key, g => new ReadOnlyCollection<Test>([.. g])));

        public abstract TestMetadata GetMetadata();

        protected Finder Finder => ItemChangerHost.Singleton.Finder;
        protected ModuleCollection Modules => ItemChangerHost.Singleton.ActiveProfile!.Modules;
        protected ItemChangerProfile Profile => ItemChangerHost.Singleton.ActiveProfile!;

        /// <summary>
        /// The entry point of the test. Responsible for setting up any modules or placements to be tested, as well as start location.
        /// </summary>
        public abstract void Setup(TestArgs args);

        protected internal static void StartNear(string scene, string gate)
        {
            ModuleCollection mods = ItemChangerHost.Singleton.ActiveProfile!.Modules;

            if (mods.Get<StartDefModule>() is StartDefModule mod)
            {
                mods.Remove(mod);
            }
            mods.Add(new StartDefModule
            {
                StartDef = new TransitionOffsetStartDef { SceneName = scene, GateName = gate, },
            });
        }

        protected internal static void StartAt(StartDef start)
        {
            ModuleCollection mods = ItemChangerHost.Singleton.ActiveProfile!.Modules;

            if (mods.Get<StartDefModule>() is StartDefModule mod)
            {
                mods.Remove(mod);
            }
            mods.Add(new StartDefModule
            {
                StartDef = start,
            });
        }

        protected override void DoLoad() 
        {
            ItemChangerHost.Singleton.LifecycleEvents.OnEnterGame += OnEnterGame;
        }

        protected override void DoUnload()
        {
            ItemChangerHost.Singleton.LifecycleEvents.OnEnterGame -= OnEnterGame;
        }

        protected virtual void OnEnterGame() { }

        // Arbitrary named hooks associated with the test, to simulate quest completion, etc.
        public virtual IEnumerable<(string, Action)> TestMethods() => [];
    }
}
