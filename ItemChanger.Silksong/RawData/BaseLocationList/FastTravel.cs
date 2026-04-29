using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.Costs;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
    // === Bellway locations ===

    // Free — unlocked after Bell Beast
    public static Location Bellway__Bone_Bottom => new CoordinateLocation
    {
        Name = LocationNames.Bellway__Bone_Bottom,
        SceneName = SceneNames.Bellway_01,
        X = 52.37f,
        Y = 21.59f,
        Managed = false,
    };

    // Free — unlocked after Bell Beast
    public static Location Bellway__The_Marrow => new CoordinateLocation
    {
        Name = LocationNames.Bellway__The_Marrow,
        SceneName = SceneNames.Bone_05,
        X = 137.18f,
        Y = 4.57f,
        Managed = false,
    };

    // 40 rosaries — near Toll Machine at (83.71, 16.15)
    public static Location Bellway__Deep_Docks => new CoordinateLocation
    {
        Name = LocationNames.Bellway__Deep_Docks,
        SceneName = SceneNames.Bellway_02,
        X = 86f,
        Y = 16f,
        Managed = false,
    }.WithTag(new CostTag { Cost = new RosaryCost(40) });

    // 40 rosaries — near Toll Machine at (69.34, 8.15)
    public static Location Bellway__Far_Fields => new CoordinateLocation
    {
        Name = LocationNames.Bellway__Far_Fields,
        SceneName = SceneNames.Bellway_03,
        X = 71f,
        Y = 8f,
        Managed = false,
    }.WithTag(new CostTag { Cost = new RosaryCost(40) });

    // 60 rosaries — near Toll Machine at (69.46, 8.13)
    public static Location Bellway__Greymoor => new CoordinateLocation
    {
        Name = LocationNames.Bellway__Greymoor,
        SceneName = SceneNames.Bellway_04,
        X = 71f,
        Y = 8f,
        Managed = false,
    }.WithTag(new CostTag { Cost = new RosaryCost(60) });

    // 60 rosaries — near Toll Machine at (28.28, 95.24)
    public static Location Bellway__Bellhart => new CoordinateLocation
    {
        Name = LocationNames.Bellway__Bellhart,
        SceneName = SceneNames.Belltown_basement,
        X = 30f,
        Y = 95f,
        Managed = false,
    }.WithTag(new CostTag { Cost = new RosaryCost(60) });

    // 40 rosaries — near Toll Machine at (57.94, 5.14)
    public static Location Bellway__Shellwood => new CoordinateLocation
    {
        Name = LocationNames.Bellway__Shellwood,
        SceneName = SceneNames.Shellwood_19,
        X = 60f,
        Y = 5f,
        Managed = false,
    }.WithTag(new CostTag { Cost = new RosaryCost(40) });

    // 60 rosaries — near Toll Machine at (94.32, 12.23)
    public static Location Bellway__Blasted_Steps => new CoordinateLocation
    {
        Name = LocationNames.Bellway__Blasted_Steps,
        SceneName = SceneNames.Bellway_08,
        X = 96f,
        Y = 12f,
        Managed = false,
    }.WithTag(new CostTag { Cost = new RosaryCost(60) });

    // 40 rosaries — near Toll Machine at (44.53, 5.35)
    public static Location Bellway__The_Slab => new CoordinateLocation
    {
        Name = LocationNames.Bellway__The_Slab,
        SceneName = SceneNames.Slab_06,
        X = 46f,
        Y = 5f,
        Managed = false,
    }.WithTag(new CostTag { Cost = new RosaryCost(40) });

    // 80 rosaries — near Bellway Toll Machine at (39.87, 10.18)
    public static Location Bellway__Grand_Bellway => new CoordinateLocation
    {
        Name = LocationNames.Bellway__Grand_Bellway,
        SceneName = SceneNames.Bellway_City,
        X = 42f,
        Y = 10f,
        Managed = false,
    }.WithTag(new CostTag { Cost = new RosaryCost(80) });

    // 80 rosaries
    public static Location Bellway__Bilewater => new CoordinateLocation
    {
        Name = LocationNames.Bellway__Bilewater,
        SceneName = SceneNames.Bellway_Shadow,
        X = 44.11f,
        Y = 22.57f,
        Managed = false,
    }.WithTag(new CostTag { Cost = new RosaryCost(80) });

    // 80 rosaries — near Toll Machine at (51.49, 21.13)
    public static Location Bellway__Putrified_Ducts => new CoordinateLocation
    {
        Name = LocationNames.Bellway__Putrified_Ducts,
        SceneName = SceneNames.Bellway_Aqueduct,
        X = 53f,
        Y = 21f,
        Managed = false,
    }.WithTag(new CostTag { Cost = new RosaryCost(80) });

    // === Ventrica locations ===

    // Free — locked from outside, unlocks when other stations accessed
    public static Location Ventrica__Terminus => new CoordinateLocation
    {
        Name = LocationNames.Ventrica__Terminus,
        SceneName = SceneNames.Tube_Hub,
        X = 68.77f,
        Y = 39.57f,
        Managed = false,
    };

    // 80 rosaries — near tube entrance at (16.08, 6.24)
    public static Location Ventrica__Memorium => new CoordinateLocation
    {
        Name = LocationNames.Ventrica__Memorium,
        SceneName = SceneNames.Arborium_Tube,
        X = 18f,
        Y = 6f,
        Managed = false,
    }.WithTag(new CostTag { Cost = new RosaryCost(80) });

    // 80 rosaries
    public static Location Ventrica__High_Halls => new CoordinateLocation
    {
        Name = LocationNames.Ventrica__High_Halls,
        SceneName = SceneNames.Hang_06b,
        X = 23.14f,
        Y = 4.57f,
        Managed = false,
    }.WithTag(new CostTag { Cost = new RosaryCost(80) });

    // 80 rosaries — near tube entrance at (16.08, 6.24)
    public static Location Ventrica__First_Shrine => new CoordinateLocation
    {
        Name = LocationNames.Ventrica__First_Shrine,
        SceneName = SceneNames.Song_Enclave_Tube,
        X = 18f,
        Y = 6f,
        Managed = false,
    }.WithTag(new CostTag { Cost = new RosaryCost(80) });

    // 80 rosaries — near tube entrance at (48.01, 4.24)
    public static Location Ventrica__Choral_Chambers => new CoordinateLocation
    {
        Name = LocationNames.Ventrica__Choral_Chambers,
        SceneName = SceneNames.Song_01b,
        X = 50f,
        Y = 4f,
        Managed = false,
    }.WithTag(new CostTag { Cost = new RosaryCost(80) });

    // 80 rosaries — near tube entrance at (81.66, 11.28)
    public static Location Ventrica__Grand_Bellway => new CoordinateLocation
    {
        Name = LocationNames.Ventrica__Grand_Bellway,
        SceneName = SceneNames.Bellway_City,
        X = 83f,
        Y = 11f,
        Managed = false,
    }.WithTag(new CostTag { Cost = new RosaryCost(80) });

    // 80 rosaries — near tube entrance at (66.21, 4.25)
    public static Location Ventrica__Underworks => new CoordinateLocation
    {
        Name = LocationNames.Ventrica__Underworks,
        SceneName = SceneNames.Under_22,
        X = 68f,
        Y = 4f,
        Managed = false,
    }.WithTag(new CostTag { Cost = new RosaryCost(80) });
}
