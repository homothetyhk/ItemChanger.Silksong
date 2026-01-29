using ItemChanger;
using ItemChanger.Silksong.Modules;
using ItemChanger.Silksong.StartDefs;
using Benchwarp.Data;
using System.ComponentModel;
using ItemChanger.Locations;
using ItemChanger.Silksong.RawData;

namespace ItemChangerTesting;

public enum Tests
{
    [Description("Tests a TransitionOffsetStartDef at Tut_02, right1")]
    StartInTut_02,
    [Description("Tests giving Surgeon's_Key from a coordinate shiny")]
    Surgeon_s_Key_from_spawned_shiny,
    [Description("Tests giving Mossberry Stew from a coordinate shiny")]
    Mossberry_Stew_from_spawned_shiny,

    [Description("Tests giving Wanderer Crest from a coordinate shiny")]
    Crest_of_Wanderer_from_spawned_shiny,
    [Description("Tests giving Cloakless from a coordinate shiny")]
    Cloakless_from_spawned_shiny,
    [Description("Tests giving Cross Stitch from a coordinate shiny")]
    Cross_Stitch_from_spawned_shiny,
    [Description("Tests giving Tacks from a coordinate shiny")]
    Tacks_from_spawned_shiny,
    [Description("Tests giving Multibinder from a coordinate shiny")]
    Multibinder_from_spawned_shiny,
    [Description("Tests giving Compass from a coordinate shiny")]
    Compass_from_spawned_shiny,
    [Description("Tests giving Arcane Egg from a coordinate shiny")]
    Arcane_Egg_from_spawned_shiny,

    [Description("Tests modifying the Pale_Oil-Whispering_Vaults shiny in-place")]
    Surgeon_s_Key_at_Whispering_Vaults,
}

public static class TestDispatcher
{
    private static void Init()
    {
        GameManager.instance.profileID = ItemChangerTestingPlugin.Instance.cfgSaveSlot.Value;
        GameManager.instance.ClearSaveFile(ItemChangerTestingPlugin.Instance.cfgSaveSlot.Value, (b) => { });
        UIManager.instance.StartCoroutine(UIManager.instance.HideCurrentMenu());
        ItemChangerHost.Singleton.ActiveProfile?.Dispose();
        new ItemChangerProfile(host: ItemChangerHost.Singleton);
    }

    private static void StartNear(string scene, string gate)
    {
        ItemChangerHost.Singleton.ActiveProfile!.Modules.Remove<StartDefModule>();
        ItemChangerHost.Singleton.ActiveProfile!.Modules.Add(new StartDefModule
        {
            StartDef = new TransitionOffsetStartDef { SceneName = scene, GateName = gate, }
        });
    }

    private static void Run()
    {
        UIManager.instance.StartNewGame(false, false);
    }

    public static void StartTest()
    {
        Init();
        ItemChangerProfile prof = ItemChangerHost.Singleton.ActiveProfile!;
        Finder finder = ItemChangerHost.Singleton.Finder;
        switch (ItemChangerTestingPlugin.Instance.cfgTest.Value)
        {
            case Tests.StartInTut_02:
                StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
                break;
            case Tests.Surgeon_s_Key_from_spawned_shiny:
                StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Test",
                    SceneName = SceneNames.Tut_02,
                    X = 133.6f,
                    Y = 32.6f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Surgeon_s_Key)!));
                break;

            case Tests.Mossberry_Stew_from_spawned_shiny:
                StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Test",
                    SceneName = SceneNames.Tut_02,
                    X = 133.6f,
                    Y = 32.6f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Mossberry_Stew)!));
                break;

            case Tests.Cloakless_from_spawned_shiny:
                StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Test",
                    SceneName = SceneNames.Tut_02,
                    X = 133.6f,
                    Y = 32.6f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Cloakless)!));
                break;

            case Tests.Crest_of_Wanderer_from_spawned_shiny:
                StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Test",
                    SceneName = SceneNames.Tut_02,
                    X = 133.6f,
                    Y = 32.6f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Crest_of_Wanderer)!));
                break;

            case Tests.Cross_Stitch_from_spawned_shiny:
                StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Test",
                    SceneName = SceneNames.Tut_02,
                    X = 133.6f,
                    Y = 32.6f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Cross_Stitch)!));
                break;

            case Tests.Tacks_from_spawned_shiny:
                StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Test",
                    SceneName = SceneNames.Tut_02,
                    X = 133.6f,
                    Y = 32.6f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Tacks)!));
                break;

            case Tests.Multibinder_from_spawned_shiny:
                StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Test",
                    SceneName = SceneNames.Tut_02,
                    X = 133.6f,
                    Y = 32.6f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Multibinder)!));
                break;

            case Tests.Compass_from_spawned_shiny:
                StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Test",
                    SceneName = SceneNames.Tut_02,
                    X = 133.6f,
                    Y = 32.6f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Compass)!));
                break;

            case Tests.Arcane_Egg_from_spawned_shiny:
                StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Test",
                    SceneName = SceneNames.Tut_02,
                    X = 133.6f,
                    Y = 32.6f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Arcane_Egg)!));
                break;

            case Tests.Surgeon_s_Key_at_Whispering_Vaults:
                StartNear(SceneNames.Library_03, PrimitiveGateNames.left1);
                prof.AddPlacement(finder.GetLocation(LocationNames.Pale_Oil__Whispering_Vaults)!
                    .Wrap().Add(finder.GetItem(ItemNames.Surgeon_s_Key)!));
                break;
        }
        Run();
    }
}
