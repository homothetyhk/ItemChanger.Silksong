using Benchwarp.Benches;
using GlobalEnums;
using ItemChanger.Extensions;
using ItemChanger.Silksong;
using ItemChanger.Silksong.Costs;
using ItemChanger.Silksong.Modules;
using ItemChanger.Silksong.Modules.ShopsModule;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;
using PrepatcherPlugin;

namespace ItemChangerTesting.ShopTests;

internal class ShakraShopTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        MenuDescription = "Places items at all Shakra map locations.",
        MenuName = "Shakra Shop Test",
        Folder = TestFolder.ShopTests,
        Revision = 2026042000,
    };

    protected override void OnEnterGame()
    {
        PlayerDataAccess.defeatedBellBeast = true;
        PlayerDataAccess.geo = 10000;
        PlayerDataAccess.hasBrolly = true;
        PlayerDataAccess.hasDash = true;
        PlayerDataAccess.hasDoubleJump = true;
        PlayerDataAccess.hasHarpoonDash = true;
        PlayerDataAccess.hasWalljump = true;
        PlayerDataAccess.silkRegenMax = 3;
        PlayerDataAccess.spinnerDefeated = true;
    }

    public List<string> Scenes = [];
    public int CurrentScene = 0;

    private void AddMapLocation(string locationName, params string[] itemNames)
    {
        var loc = Finder.GetLocation(locationName)!;
        Scenes.Add(loc.SceneName!);

        var placement = loc.Wrap();
        foreach (var item in itemNames) placement.Add(Finder.GetItem(item)!);
        Profile.AddPlacement(placement);
    }

    public override void Setup(TestArgs args)
    {
        Profile.Modules.Add<ShopsModule>().RemovedCategories = [
            DefaultShopItems.Maps,
            DefaultShopItems.Tools,
        ];

        Profile.AddPlacement(Finder.GetLocation(LocationNames.Shakra__Global)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!.WithCost(new RosaryCost(22)))
            .Add(Finder.GetItem(ItemNames.White_Key)!.WithCost(new RosaryCost(33))));

        AddMapLocation(LocationNames.Map__Mosslands, ItemNames.Mossberry);
        AddMapLocation(LocationNames.Map__Marrow, ItemNames.Flintslate);
        AddMapLocation(LocationNames.Map__Deep_Docks, ItemNames.Magma_Bell);
        AddMapLocation(LocationNames.Map__Far_Fields, ItemNames.Drifter_s_Cloak);
        AddMapLocation(LocationNames.Map__Hunter_s_March, ItemNames.Curveclaw, ItemNames.Fractured_Mask);
        AddMapLocation(LocationNames.Map__Greymoor, ItemNames.Crawbell);
        AddMapLocation(LocationNames.Map__Shellwood, ItemNames.Cling_Grip);
        AddMapLocation(LocationNames.Map__Bellhart, ItemNames.Needolin);
        AddMapLocation(LocationNames.Map__Blasted_Steps, ItemNames.Needle_Strike);
        AddMapLocation(LocationNames.Map__Wormways, ItemNames.Needle_Phial);
        AddMapLocation(LocationNames.Map__Mount_Fay, ItemNames.Faydown_Cloak);
        AddMapLocation(LocationNames.Map__Sinner_s_Road, ItemNames.Simple_Key);
        AddMapLocation(LocationNames.Map__Bilewater, ItemNames.Seeker_s_Soul);
        AddMapLocation(LocationNames.Map__Sands_of_Karak, ItemNames.Conchcutter);

        StartAt(new ShakraOffsetStartDef() { SceneName = Scenes[CurrentScene] });
    }

    private void WarpToNextMap()
    {
        CurrentScene = (CurrentScene + 1) % Scenes.Count;
        if (Profile.Modules.Get<StartDefModule>()?.StartDef is ShakraOffsetStartDef shakra)
        {
            shakra.SceneName = Scenes[CurrentScene];
            shakra.SetRespawn();
            Benchwarp.ChangeScene.WarpToRespawn();
        }
    }

    public override IEnumerable<(string, Action)> TestMethods() => [("Next Map", WarpToNextMap)];
}

file class ShakraOffsetStartDef : StartDef
{
    public required string SceneName;

    public override RespawnInfo GetRespawnInfo() => new(SceneName, CoordinateStartDef.RESPAWN_MARKER_NAME, 0, MapZone.NONE);

    protected override void DoLoad()
    {
        base.DoLoad();
        SilksongHost.Instance.GameEvents.OnNextSceneLoaded += OnNextScene;
    }

    protected override void DoUnload()
    {
        SilksongHost.Instance.GameEvents.OnNextSceneLoaded -= OnNextScene;
        base.DoUnload();
    }

    private void OnNextScene(ItemChanger.Events.Args.SceneLoadedEventArgs obj)
    {
        if (obj.Scene.name != SceneName) return;

        foreach (var name in ShakraModule.SHAKRA_OBJECT_NAMES)
        {
            if (obj.Scene.FindGameObjectByName(name) is GameObject shakra)
            {
                CoordinateStartDef.CreateRespawnMarker(obj.Scene, true, shakra.transform.position.x - 3, shakra.transform.position.y + 1);
                return;
            }
        }
    }
}