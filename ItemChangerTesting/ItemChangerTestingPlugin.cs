using BepInEx;
using BepInEx.Configuration;
using ItemChanger;
using ItemChanger.Events;
using ItemChanger.Silksong;
using Silksong.ModMenu.Elements;
using Silksong.ModMenu.Models;
using Silksong.ModMenu.Plugin;
using Silksong.ModMenu.Screens;

namespace ItemChangerTesting
{
    [BepInDependency(ItemChangerPlugin.Id)]
    [BepInAutoPlugin(id: "io.github.testing.silksong.itemchanger")]
    public partial class ItemChangerTestingPlugin : BaseUnityPlugin, IModMenuCustomMenu
    {
        public required ConfigEntry<int> cfgSaveSlot;
        public required ConfigEntry<TestFolder> cfgTestFolder;
        public required ConfigEntry<int> cfgTestIndex;

        public static ItemChangerTestingPlugin Instance 
        { 
            get => field ?? throw new NullReferenceException($"{nameof(ItemChangerTestingPlugin)} not yet initialized.");
            private set;
        }
        public new BepInEx.Logging.ManualLogSource Logger => base.Logger;

        private void Awake()
        {
            Instance = this;
            cfgSaveSlot = Config.Bind(configDefinition: new ConfigDefinition(section: "Menu", key: "Save Slot"), defaultValue: 1, 
                configDescription: new ConfigDescription("The save slot to use for the test.", acceptableValues: new AcceptableValueRange<int>(1, 4)));
            cfgTestFolder = Config.Bind(configDefinition: new ConfigDefinition(section: "Menu", key: "Test Folder"), defaultValue: (TestFolder)default,
                configDescription: new ConfigDescription("The test folder to search."));
            cfgTestIndex = Config.Bind(configDefinition: new ConfigDefinition(section: "Menu", key: "Test Index"), defaultValue: (int)default,
                configDescription: new ConfigDescription("The index of the test to launch, within its folder."));

            ItemChangerPlugin.OnNewHost += HookLifecycleEvents;
        }

        private bool inGame = false;
        private TextButton? testMethods;

        private static bool FilterMatches(string filter, string doc)
        {
            var tokens = filter.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length == 0) return true;

            doc = doc.ToLower();
            return tokens.All(t => doc.Contains(t));
        }

        public AbstractMenuScreen BuildCustomMenu()
        {
            SimpleMenuScreen screen = new("ItemChangerTesting");
            MenuElementGenerators.CreateIntSliderGenerator()(cfgSaveSlot, out MenuElement? saveSlotSelector);
            ConfigEntryFactory.GenerateEnumChoiceElement(cfgTestFolder, out MenuElement? testFolderSelector);

            ListChoiceModel<Test> model = new([.. Test.GetTests(cfgTestFolder.Value)])
            {
                DisplayFn = (_, t) => t.GetMetadata().MenuName
            };
            if (cfgTestIndex.Value is int savedIndex && savedIndex >= 0 && savedIndex < model.Values.Count) model.Index = savedIndex;
            model.OnValueChanged += _ => cfgTestIndex.Value = model.Index;
            DynamicDescriptionChoiceElement<Test> testSelector = new("Test", model, "The test to launch.", t => t.GetMetadata().MenuDescription);

            TextButton run = new("Erase save slot and launch test.");
            run.OnSubmit += Run;

            testMethods = new("Test Methods");
            testMethods.OnSubmit += ShowTestMethods;
            testMethods.VisibleSelf = inGame;

            screen.Add(saveSlotSelector!);
            TextInput<string> filter = new("Search", TextModels.ForStrings());
            screen.Add(filter);
            screen.Add(testFolderSelector!);
            screen.Add(testSelector!);
            screen.Add(run);
            screen.Add(testMethods);

            void UpdateTests()
            {
                var prevSelection = model.Value;
                var src = Test.GetTests(cfgTestFolder.Value);
                List<Test> eligible = [.. src.Where(t => FilterMatches(filter.Value, t.GetMetadata().MenuName))];
                filter.State = eligible.Count == 0 ? ElementState.INVALID : ElementState.DEFAULT;
                if (eligible.Count == 0) eligible = [.. src];

                int index = eligible.IndexOf(prevSelection);
                model.UpdateValues(eligible, index == -1 ? 0 : index);
            }
            filter.OnValueChanged += _ => UpdateTests();

            void UpdateFolder(object sender, EventArgs args) => UpdateTests();
            cfgTestFolder.SettingChanged += UpdateFolder;
            screen.OnDispose += () => cfgTestFolder.SettingChanged -= UpdateFolder;

            return screen;

            void Run()
            {
                UIManager.instance.HideMenuInstant(screen.MenuScreen);
                try
                {
                    TestDispatcher.StartTest(testSelector.Value);
                }
                catch (Exception e)
                {
                    Logger.LogError($"Error starting test: {e}");
                }
            }
        }

        private Test? lastLoadedTest;
        private AbstractMenuScreen? testMethodsScreen;

        void ShowTestMethods()
        {
            var activeTest = ItemChangerHost.Singleton.ActiveProfile?.Modules.Get<Test>();
            if (activeTest == null)
            {
                Logger.LogError("No active Test module.");
                return;
            }

            if (lastLoadedTest == activeTest)
            {
                MenuScreenNavigation.Show(testMethodsScreen!);
                return;
            }

            List<(string, Action)> hooks = [.. activeTest.TestMethods()];
            if (hooks.Count == 0)
            {
                Logger.LogError($"Test '{activeTest.GetMetadata().MenuName}' has no test methods.");
                return;
            }

            PaginatedMenuScreenBuilder builder = new($"{activeTest.GetMetadata().MenuName} Test Methods");
            foreach (var (name, hook) in hooks)
            {
                TextButton button = new(name);
                button.OnSubmit += hook;
                builder.Add(button);
            }

            testMethodsScreen?.Dispose();
            testMethodsScreen = builder.Build();
            testMethodsScreen.OnDispose += () =>
            {
                testMethodsScreen = null;
                lastLoadedTest = null;
            };

            lastLoadedTest = activeTest;
            MenuScreenNavigation.Show(testMethodsScreen);
        }

        private void HookLifecycleEvents(SilksongHost host)
        {
            // TODO - this probably ought to be in ItemChanger.Core
            LifecycleEvents events = host.LifecycleEvents;
            events.OnLeaveGame += () => Logger.LogInfo("Invoked " + nameof(LifecycleEvents.OnLeaveGame));
            events.OnEnterGame += () => Logger.LogInfo("Invoked " + nameof(LifecycleEvents.OnEnterGame));
            events.OnSafeToGiveItems += () => Logger.LogInfo("Invoked " + nameof(LifecycleEvents.OnSafeToGiveItems));
            events.OnItemChangerHook += () => Logger.LogInfo("Invoked " + nameof(LifecycleEvents.OnItemChangerHook));
            events.OnItemChangerUnhook += () => Logger.LogInfo("Invoked " + nameof(LifecycleEvents.OnItemChangerUnhook));
            events.BeforeStartNewGame += () => Logger.LogInfo("Invoked " + nameof(LifecycleEvents.BeforeStartNewGame));
            events.BeforeContinueGame += () => Logger.LogInfo("Invoked " + nameof(LifecycleEvents.BeforeContinueGame));
            events.AfterStartNewGame += () => Logger.LogInfo("Invoked " + nameof(LifecycleEvents.AfterStartNewGame));
            events.AfterContinueGame += () => Logger.LogInfo("Invoked " + nameof(LifecycleEvents.AfterContinueGame));

            events.OnEnterGame += () =>
            {
                inGame = true;
                testMethods?.VisibleSelf = true;
            };
            events.OnLeaveGame += () =>
            {
                inGame = false;
                testMethods?.VisibleSelf = false;
            };
        }

        public LocalizedText ModMenuName() => "ItemChangerTesting";
    }
}
