using HarmonyLib;
using ItemChanger;
using ItemChanger.Events.Args;
using ItemChanger.Modules;
using ItemChanger.Silksong;
using ItemChanger.Silksong.Modules;
using ItemChanger.Silksong.StartDefs;
using PrepatcherPlugin;
using System.Collections.ObjectModel;
using UnityEngine.SceneManagement;

namespace ItemChangerTesting
{
    internal abstract class Test : Module
    {
        private static readonly ReadOnlyDictionary<TestFolder, ReadOnlyCollection<Test>> testGroups = new(typeof(Test).Assembly.GetTypes()
            .Where(t => t.IsSubclassOf(typeof(Test)) && !t.IsAbstract).Select(t => (Test)Activator.CreateInstance(t))
            .OrderByDescending(t => t.GetMetadata().Revision)
            .GroupBy(t => t.GetMetadata().Folder).ToDictionary(g => g.Key, g => new ReadOnlyCollection<Test>([.. g])));

        public static IEnumerable<Test> GetTests(TestFolder folder)
        {
            if (folder == TestFolder.AllTests)
                return testGroups.Values.SelectMany(t => t).OrderByDescending(t => t.GetMetadata().Revision);
            else
                return testGroups[folder];
        }

        public abstract TestMetadata GetMetadata();

        protected Finder Finder => ItemChangerHost.Singleton.Finder;
        protected ModuleCollection Modules => ItemChangerHost.Singleton.ActiveProfile!.Modules;
        protected ItemChangerProfile Profile => ItemChangerHost.Singleton.ActiveProfile!;
 
        protected void StartAct3()
        {
            PlayerDataAccess.act3_enclaveWakeSceneCompleted = true;
            PlayerDataAccess.act3MapUpdated = true;
            PlayerDataAccess.act3_wokeUp = true;
            PlayerDataAccess.blackThreadWorld = true;
        }

        /// <summary>
        /// For ease of testing, all enemies & bosses are reduced to 1 hp by default. Set this to false to suppress that behaviour.
        /// </summary>
        protected virtual bool WeakenEnemies => true;

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

        protected static void StartAt(Benchwarp.Benches.BenchData benchData) => StartAt(new BenchwarpStartDef(benchData));

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

        private static Test? ActiveTest;

        protected override void DoLoad() 
        {
            ActiveTest = this;
            Using(new HarmonyPatchGroup() { typeof(Patches) });

            ItemChangerHost.Singleton.LifecycleEvents.OnEnterGame += OnEnterGame;
        }

        protected override void DoUnload()
        {
            ItemChangerHost.Singleton.LifecycleEvents.OnEnterGame -= OnEnterGame;
            ActiveTest = null;
        }

        protected virtual void OnEnterGame() { }

        // Arbitrary named hooks associated with the test, to simulate quest completion, etc.
        public virtual IEnumerable<(string, Action)> TestMethods() => [];

        [HarmonyPatch]
        private static class Patches
        {
            [HarmonyPatch(typeof(HealthManager), nameof(HealthManager.TakeDamage))]
            [HarmonyPrefix]
            private static bool Prefix(HealthManager __instance)
            {
                if (ActiveTest is { } test && test.WeakenEnemies)
                {
                    // Reduce all enemy health to 1 before taking damage.
                    // Doing this via a patch handles all cases, including dynamic spawns/minions, hp adjustments, phases, etc.
                    __instance.hp = Math.Min(1, __instance.hp);
                }

                return true;
            }
        }
    }
}
