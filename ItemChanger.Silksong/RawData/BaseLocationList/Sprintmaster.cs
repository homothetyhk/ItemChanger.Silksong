using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.Locations;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
    public static Location Rosary_String__Sprintmaster_Race_1 => new SprintmasterLocation
    {
        SceneName = SceneNames.Sprintmaster_Cave,
        Name = LocationNames.Rosary_String__Sprintmaster_Race_1,
        IsQuestCompletion = false,
    };

    public static Location Beast_Shard__Sprintmaster_Race_2 => new SprintmasterLocation
    {
        SceneName = SceneNames.Sprintmaster_Cave,
        Name = LocationNames.Beast_Shard__Sprintmaster_Race_2,
        IsQuestCompletion = false,
    };

    public static Location Mask_Shard__Sprintmaster => new SprintmasterLocation
    {
        SceneName = SceneNames.Sprintmaster_Cave,
        Name = LocationNames.Mask_Shard__Sprintmaster,
        IsQuestCompletion = true,
    };

    public static Location Sprintmaster_Memento => new SprintmasterLocation
    {
        SceneName = SceneNames.Sprintmaster_Cave,
        Name = LocationNames.Sprintmaster_Memento,
        IsQuestCompletion = false,
    };
}
