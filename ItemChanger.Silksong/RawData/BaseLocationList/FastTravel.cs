using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.Costs;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
    // Coordinates were captured by standing Hornet at the desired check position;
    // they reflect Hornet's transform position so containers (chests, etc.) place
    // sensibly when this location is replaced.

    // === Bellway locations ===

    // Free — unlocked after Bell Beast
    public static Location Bellway__Bone_Bottom => new CoordinateLocation
    {
        Name = LocationNames.Bellway__Bone_Bottom,
        SceneName = SceneNames.Bellway_01,
        X = 69.90f,
        Y = 9.60f,
        Managed = false,
    };

    // Free — unlocked after Bell Beast
    public static Location Bellway__The_Marrow => new CoordinateLocation
    {
        Name = LocationNames.Bellway__The_Marrow,
        SceneName = SceneNames.Bone_05,
        X = 108.62f,
        Y = 6.57f,
        Managed = false,
    };

    // 40 rosaries
    public static Location Bellway__Deep_Docks => new CoordinateLocation
    {
        Name = LocationNames.Bellway__Deep_Docks,
        SceneName = SceneNames.Bellway_02,
        X = 86.75f,
        Y = 17.57f,
        Managed = false,
    }.WithTag(new CostTag { Cost = new RosaryCost(40) });

    // 40 rosaries
    public static Location Bellway__Far_Fields => new CoordinateLocation
    {
        Name = LocationNames.Bellway__Far_Fields,
        SceneName = SceneNames.Bellway_03,
        X = 67.14f,
        Y = 9.57f,
        Managed = false,
    }.WithTag(new CostTag { Cost = new RosaryCost(40) });

    // 60 rosaries
    public static Location Bellway__Greymoor => new CoordinateLocation
    {
        Name = LocationNames.Bellway__Greymoor,
        SceneName = SceneNames.Bellway_04,
        X = 66.83f,
        Y = 9.57f,
        Managed = false,
    }.WithTag(new CostTag { Cost = new RosaryCost(60) });

    // 60 rosaries
    public static Location Bellway__Bellhart => new CoordinateLocation
    {
        Name = LocationNames.Bellway__Bellhart,
        SceneName = SceneNames.Belltown_basement,
        X = 31.74f,
        Y = 96.57f,
        Managed = false,
    }.WithTag(new CostTag { Cost = new RosaryCost(60) });

    // 40 rosaries
    public static Location Bellway__Shellwood => new CoordinateLocation
    {
        Name = LocationNames.Bellway__Shellwood,
        SceneName = SceneNames.Shellwood_19,
        X = 54.91f,
        Y = 6.57f,
        Managed = false,
    }.WithTag(new CostTag { Cost = new RosaryCost(40) });

    // 60 rosaries
    public static Location Bellway__Blasted_Steps => new CoordinateLocation
    {
        Name = LocationNames.Bellway__Blasted_Steps,
        SceneName = SceneNames.Bellway_08,
        X = 97.81f,
        Y = 13.57f,
        Managed = false,
    }.WithTag(new CostTag { Cost = new RosaryCost(60) });

    // 40 rosaries
    public static Location Bellway__The_Slab => new CoordinateLocation
    {
        Name = LocationNames.Bellway__The_Slab,
        SceneName = SceneNames.Slab_06,
        X = 47.95f,
        Y = 6.72f,
        Managed = false,
    }.WithTag(new CostTag { Cost = new RosaryCost(40) });

    // 80 rosaries
    public static Location Bellway__Grand_Bellway => new CoordinateLocation
    {
        Name = LocationNames.Bellway__Grand_Bellway,
        SceneName = SceneNames.Bellway_City,
        X = 36.49f,
        Y = 11.57f,
        Managed = false,
    }.WithTag(new CostTag { Cost = new RosaryCost(80) });

    // 80 rosaries
    public static Location Bellway__Bilewater => new CoordinateLocation
    {
        Name = LocationNames.Bellway__Bilewater,
        SceneName = SceneNames.Bellway_Shadow,
        X = 47.90f,
        Y = 22.57f,
        Managed = false,
    }.WithTag(new CostTag { Cost = new RosaryCost(80) });

    // 80 rosaries
    public static Location Bellway__Putrified_Ducts => new CoordinateLocation
    {
        Name = LocationNames.Bellway__Putrified_Ducts,
        SceneName = SceneNames.Bellway_Aqueduct,
        X = 48.56f,
        Y = 22.57f,
        Managed = false,
    }.WithTag(new CostTag { Cost = new RosaryCost(80) });

    // === Ventrica locations ===

    // Free — locked from outside, unlocks when other stations accessed
    public static Location Ventrica__Terminus => new CoordinateLocation
    {
        Name = LocationNames.Ventrica__Terminus,
        SceneName = SceneNames.Tube_Hub,
        X = 71.91f,
        Y = 39.57f,
        Managed = false,
    };

    // 80 rosaries
    public static Location Ventrica__Memorium => new CoordinateLocation
    {
        Name = LocationNames.Ventrica__Memorium,
        SceneName = SceneNames.Arborium_Tube,
        X = 20.10f,
        Y = 6.57f,
        Managed = false,
    }.WithTag(new CostTag { Cost = new RosaryCost(80) });

    // 80 rosaries
    public static Location Ventrica__High_Halls => new CoordinateLocation
    {
        Name = LocationNames.Ventrica__High_Halls,
        SceneName = SceneNames.Hang_06b,
        X = 28.43f,
        Y = 4.57f,
        Managed = false,
    }.WithTag(new CostTag { Cost = new RosaryCost(80) });

    // 80 rosaries
    public static Location Ventrica__First_Shrine => new CoordinateLocation
    {
        Name = LocationNames.Ventrica__First_Shrine,
        SceneName = SceneNames.Song_Enclave_Tube,
        X = 19.27f,
        Y = 6.57f,
        Managed = false,
    }.WithTag(new CostTag { Cost = new RosaryCost(80) });

    // 80 rosaries
    public static Location Ventrica__Choral_Chambers => new CoordinateLocation
    {
        Name = LocationNames.Ventrica__Choral_Chambers,
        SceneName = SceneNames.Song_01b,
        X = 44.28f,
        Y = 4.57f,
        Managed = false,
    }.WithTag(new CostTag { Cost = new RosaryCost(80) });

    // 80 rosaries
    public static Location Ventrica__Grand_Bellway => new CoordinateLocation
    {
        Name = LocationNames.Ventrica__Grand_Bellway,
        SceneName = SceneNames.Bellway_City,
        X = 76.61f,
        Y = 11.57f,
        Managed = false,
    }.WithTag(new CostTag { Cost = new RosaryCost(80) });

    // 80 rosaries
    public static Location Ventrica__Underworks => new CoordinateLocation
    {
        Name = LocationNames.Ventrica__Underworks,
        SceneName = SceneNames.Under_22,
        X = 69.74f,
        Y = 4.57f,
        Managed = false,
    }.WithTag(new CostTag { Cost = new RosaryCost(80) });
}
