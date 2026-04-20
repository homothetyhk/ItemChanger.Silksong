using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Costs;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.RawData;

// TODO: When https://github.com/homothetyhk/ItemChanger.Silksong/pull/159 is merged, add definitions for maps given by InteractEvents.
internal static partial class BaseLocationList
{
    // Given by "/weaver_harp_sign_map/Get Map Inspect" in Abyss_12.  Controlled by `InteractEvents` component.
    // public static Location Map__Abyss => TODO();

    // The Bellhart map becomes available at any Shakra location after visiting Bellhart, in any state.
    public static Location Map__Bellhart => CreateShakraMapLocation(
        SceneNames.Belltown,
        LocationNames.Map__Bellhart,
        nameof(PlayerData.MapperLeftBellhart),
        40,
        testOverride: new PDBool(nameof(PlayerData.visitedBellhart)));

    public static Location Map__Bilewater => CreateShakraMapLocation(SceneNames.Shadow_23, LocationNames.Map__Bilewater, nameof(PlayerData.MapperLeftShadow), 90);

    public static Location Map__Blasted_Steps => CreateShakraMapLocation(SceneNames.Coral_12, LocationNames.Map__Blasted_Steps, nameof(PlayerData.MapperLeftJudgeSteps), 70);

    public static Location Map__Choral_Chambers => new MapMachineLocation()
    {
        SceneName = SceneNames.Song_01b,
        Name = LocationNames.Map__Choral_Chambers,
        ObjectName = "Map Machine (2)",
    };

    // Given by "/Group/Collectable Item Pickup Child" in Cog_Bench.  Controlled by `InteractEvents` component.
    // public static Location Map__Cogwork_Core => TODO();

    public static Location Map__Cradle => new DualLocation()
    {
        Name = LocationNames.Map__Cradle,
        Test = new PDBool(nameof(PlayerData.blackThreadWorld)),
        FalseLocation = new MapMachineLocation()
        {
            SceneName = SceneNames.Cradle_02,
            Name = LocationNames.Map__Cradle,
            ObjectName = "Map Machine (1)",
        },
        TrueLocation = new ObjectLocation()
        {
            SceneName = SceneNames.Tube_Hub,
            Name = LocationNames.Map__Cradle,
            ObjectName = "/Black Thread States/Black Thread World/Collectable Item Pickup",
            Correction = new(0, 1f, 0),
            FlingType = Enums.FlingType.Everywhere,
        },
    };

    public static Location Map__Deep_Docks => CreateShakraMapLocation(SceneNames.Bone_East_01, LocationNames.Map__Deep_Docks, nameof(PlayerData.MapperLeftDocks), 50);

    public static Location Map__Far_Fields => CreateShakraMapLocation(SceneNames.Bone_East_21, LocationNames.Map__Far_Fields, nameof(PlayerData.MapperLeftWilds), 50);

    public static Location Map__Grand_Gate => new MapMachineLocation()
    {
        SceneName = SceneNames.Song_19_entrance,
        Name = LocationNames.Map__Grand_Gate,
        ObjectName = "Map Machine (1)",
    };

    public static Location Map__Greymoor => CreateShakraMapLocation(SceneNames.Greymoor_02, LocationNames.Map__Greymoor, nameof(PlayerData.MapperLeftGreymoor), 70);

    public static Location Map__High_Halls => new MapMachineLocation()
    {
        SceneName = SceneNames.Hang_06b,
        Name = LocationNames.Map__High_Halls,
        ObjectName = "Map Machine",
    };

    /// <summary>
    /// Shakra can be encountered in Ant_04_mid in the battle scene if the player has not yet visited Bellhart, Shellwood, or Greymoor.
    /// We respect this condition and instead force Shakra to always be present at Ant_20 also, honoring both scenes.
    /// </summary>
    public static Location Map__Hunter_s_March => CreateShakraMapLocation(
        SceneNames.Ant_20, LocationNames.Map__Hunter_s_March, nameof(PlayerData.MapperLeftHuntersNest), 70,
        testOverride: new ShakraVisitedBool() { SceneNames = [SceneNames.Ant_04_mid, SceneNames.Ant_20] },
        priorityOverride: new ShakraShopPriority() { SceneNames = [SceneNames.Ant_04_mid, SceneNames.Ant_20] });

    public static Location Map__Marrow => CreateShakraMapLocation(SceneNames.Bone_04, LocationNames.Map__Marrow, nameof(PlayerData.MapperLeftBoneForest), 50);

    public static Location Map__Memorium => new MapMachineLocation()
    {
        SceneName = SceneNames.Arborium_11,
        Name = LocationNames.Map__Memorium,
        ObjectName = "Map Machine",
    };

    // The bone-bottom map is always available, even without speaking to Shakra in Bone bottom, so we set noTest=true.
    public static Location Map__Mosslands => CreateShakraMapLocation(SceneNames.Bonetown, LocationNames.Map__Mosslands, nameof(PlayerData.MapperLeftBonetown), 40, noTest: true);

    public static Location Map__Mount_Fay => CreateShakraMapLocation(SceneNames.Peak_02, LocationNames.Map__Mount_Fay, nameof(PlayerData.MapperLeftPeak), 40);

    // Given by "/Aqueduct Map Inspect" in Aqueduct_07.  Controlled by `InteractEvents` component.
    // public static Location Map__Putrefied_Ducts => TODO();

    public static Location Map__Sands_of_Karak => CreateShakraMapLocation(SceneNames.Coral_40, LocationNames.Map__Sands_of_Karak, nameof(PlayerData.MapperLeftCoralCaverns), 90);
    
    public static Location Map__Shellwood => CreateShakraMapLocation(SceneNames.Shellwood_16, LocationNames.Map__Shellwood, nameof(PlayerData.MapperLeftShellwood), 70);

    public static Location Map__Sinner_s_Road => CreateShakraMapLocation(SceneNames.Dust_10, LocationNames.Map__Sinner_s_Road, nameof(PlayerData.MapperLeftDustpens), 90);

    // Given by "Slab Map Inspect" in Slab_20.  Controlled by `InteractEvents` component.
    // public static Location Map__Slab => TODO();

    // Given by "/map_collectable/Understore Map Inspect" in Under_16.  Controlled by `InteractEvents` component.
    // public static Location Map__Underworks => TODO();

    // Given by "/weaver_harp_sign_map/Get Map Inspect" in Weave_12.  Controlled by `InteractEvents` component.
    // public static Location Map__Weavenest_Atla => TODO();

    public static Location Map__Whispering_Vaults => new MapMachineLocation()
    {
        SceneName = SceneNames.Library_04,
        Name = LocationNames.Map__Whispering_Vaults,
        ObjectName = "Map Machine (1)",
    };

    public static Location Map__Whiteward => new MapMachineLocation()
    {
        SceneName = SceneNames.Ward_01,
        Name = LocationNames.Map__Whiteward,
        ObjectName = "Map Machine (1)",
    };

    public static Location Map__Wormways => CreateShakraMapLocation(SceneNames.Crawl_01, LocationNames.Map__Wormways, nameof(PlayerData.MapperLeftCrawl), 70);

    public static Location Map__Verdania => new ObjectLocation()
    {
        SceneName = SceneNames.Clover_20,
        Name = LocationNames.Map__Verdania,
        ObjectName = "/Collectable Item Pickup",
        Correction = new(0, 1f, 0),
        FlingType = Enums.FlingType.Everywhere,
    };

    private static ShakraMapLocation CreateShakraMapLocation(
        string sceneName, string name, string pdBool, int rosaries,
        IValueProvider<bool>? testOverride = null, bool noTest = false,
        IValueProvider<int>? priorityOverride = null)
    {
        ShakraMapLocation loc = new()
        {
            SceneName = sceneName,
            Name = name,
            BaseShopName = nameof(BaseShopList.Shakra),
            Test = testOverride ?? (noTest ? null : new ShakraVisitedBool() { SceneNames = [sceneName] }),
            SuppressedPDBools = [pdBool],
            Priority = priorityOverride ?? new ShakraShopPriority() { SceneNames = [sceneName] },
        };

        loc.AddTag(new DefaultCostTag()
        {
            Cost = new RosaryCost(rosaries),
            Inherent = false,
        });

        return loc;
    }
}
