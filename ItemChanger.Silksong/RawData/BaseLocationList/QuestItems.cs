using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Costs;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
    public static Location Hermit_s_Soul => new DualLocation()
    {
        Name = LocationNames.Hermit_s_Soul,
        TrueLocation = new CoordinateLocation()
        {
            Name = "Bell Hermit act 3 shiny",
            SceneName = SceneNames.Belltown_basement_03,
            X = 97.38f,
            Y = 102.57f,
            Managed = false,
        },
        FalseLocation = new BellHermitLocation()
        {
            SceneName = SceneNames.Belltown_basement_03,
            Name = LocationNames.Hermit_s_Soul,
        },
        Test = new Disjunction(
            new PDBool(nameof(PlayerData.blackThreadWorld)), new PDBool(nameof(PlayerData.soulSnareReady))
        ),
    };

    public static Location Steel_Spines => new DualLocation()
    {
        Name = LocationNames.Steel_Spines,
        SceneName = SceneNames.Dust_Shack,
        Test = new PDBool(nameof(PlayerData.blackThreadWorld)),
        FalseLocation = new BenjinAndCrullSpinesLocation()
        {
            Name = LocationNames.Steel_Spines,
            SceneName = SceneNames.Dust_Shack,
            Tags = [new DefaultCostTag() { Cost = new RosaryCost(160), Inherent = false }]
        },
        TrueLocation = new ObjectLocation()
        {
            Name = LocationNames.Steel_Spines,
            SceneName = SceneNames.Dust_Shack,
            ObjectName = "Collectable Item Extractor Pins",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        }
    };
}