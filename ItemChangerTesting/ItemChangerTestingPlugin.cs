using BepInEx;
using BepInEx.Configuration;
using Silksong.ModMenu.Elements;
using Silksong.ModMenu.Plugin;
using Silksong.ModMenu.Screens;

namespace ItemChangerTesting
{
    [BepInAutoPlugin(id: "io.github.testing.silksong.itemchanger")]
    public partial class ItemChangerTestingPlugin : BaseUnityPlugin, IModMenuCustomMenu
    {
        public required ConfigEntry<int> cfgSaveSlot;
        public required ConfigEntry<Tests> cfgTest;
        public static ItemChangerTestingPlugin Instance { get; private set; } = null!;
        public new BepInEx.Logging.ManualLogSource Logger => base.Logger;

        private void Awake()
        {
            Instance = this;
            cfgSaveSlot = Config.Bind(configDefinition: new ConfigDefinition(section: "Menu", key: "Save Slot"), defaultValue: 1, 
                configDescription: new ConfigDescription("The save slot to use for the test.", acceptableValues: new AcceptableValueRange<int>(1, 4)));
            cfgTest = Config.Bind(configDefinition: new ConfigDefinition(section: "Menu", key: "Test"), defaultValue: (Tests)default,
                configDescription: new ConfigDescription("The test to launch."));
        }

        public AbstractMenuScreen BuildCustomMenu()
        {
            SimpleMenuScreen screen = new("ItemChangerTesting");
            MenuElementGenerators.CreateIntSliderGenerator()
                (cfgSaveSlot, out MenuElement? saveSlotSelector);
            MenuElementGenerators.CreateRightDescGenerator()
                (cfgTest, out MenuElement? testSelector);
            TextButton run = new("Erase save slot and launch test.");
            run.OnSubmit += Run;
            screen.Add(saveSlotSelector!);
            screen.Add(testSelector!);
            screen.Add(run);
            return screen;

            void Run()
            {
                UIManager.instance.HideMenuInstant(screen.MenuScreen);
                TestDispatcher.StartTest();
            }
        }

        public string ModMenuName() => "ItemChangerTesting";
    }
}
