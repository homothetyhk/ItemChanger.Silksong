using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Costs;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
    public static Location Architect_s_Melody => new ArchitectPuzzleLocation()
    {
        SceneName = SceneNames.Cog_09,
        Name = LocationNames.Architect_s_Melody,
    };
    public static Location Conductor_s_Melody => new DualLocation
    {
        Name = LocationNames.Conductor_s_Melody,
        SceneName = SceneNames.Hang_12,
        Test = new PDBool(nameof(PlayerData.blackThreadWorld)),
        FalseLocation = new ConductorBalladorLocation() 
        {
            Name = LocationNames.Conductor_s_Melody,
            SceneName = SceneNames.Hang_12,
        },
        TrueLocation = new CoordinateLocation
        {
            Name = LocationNames.Conductor_s_Melody,
            SceneName = SceneNames.Hang_12,
            X = 20.54f,
            Y = 6.14f,
            Managed = false,
            Tags = [new DefaultCostTag() { Cost = new PDBoolCost(nameof(PlayerData.hasNeedolin), new BoxedString{ Value = "Have Needolin" })}],
        },
    };
    public static Location Vaultkeeper_s_Melody => new VaultkeeperCardiniusLocation()
    {
        SceneName = SceneNames.Library_08,
        Name = LocationNames.Vaultkeeper_s_Melody,
    };
}
