using Benchwarp.Data;
using ItemChanger;
using ItemChanger.Silksong.Modules.FastTravel;
using ItemChanger.Silksong.RawData;

namespace ItemChangerTesting.ModuleTests;

internal class FastTravelTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.ModuleTests,
        MenuName = "Fast Travel",
        MenuDescription = "Tests the fast travel stuff",
        Revision = 2026041500,
    };

    private static readonly string[] AllBellways =
    [
        LocationNames.Bellway__Bone_Bottom,
        LocationNames.Bellway__The_Marrow,
        LocationNames.Bellway__Deep_Docks,
        LocationNames.Bellway__Far_Fields,
        LocationNames.Bellway__Greymoor,
        LocationNames.Bellway__Bellhart,
        LocationNames.Bellway__Shellwood,
        LocationNames.Bellway__Blasted_Steps,
        LocationNames.Bellway__The_Slab,
        LocationNames.Bellway__Grand_Bellway,
        LocationNames.Bellway__Bilewater,
        LocationNames.Bellway__Putrified_Ducts,
    ];

    private static readonly string[] AllVentricas =
    [
        LocationNames.Ventrica__Terminus,
        LocationNames.Ventrica__Memorium,
        LocationNames.Ventrica__High_Halls,
        LocationNames.Ventrica__First_Shrine,
        LocationNames.Ventrica__Choral_Chambers,
        LocationNames.Ventrica__Grand_Bellway,
        LocationNames.Ventrica__Underworks,
    ];

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Bellway_01, PrimitiveGateNames.left1);

        Modules.CreateBellwayModules();
        Modules.CreateVentricaModules();

        foreach (string name in AllBellways)
        {
            Profile.AddPlacement(Finder.GetLocation(name)!.Wrap()
                .Add(Finder.GetItem(name)!));
        }

        foreach (string name in AllVentricas)
        {
            Profile.AddPlacement(Finder.GetLocation(name)!.Wrap()
                .Add(Finder.GetItem(name)!));
        }
    }
}
