using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.Locations;

namespace ItemChanger.Silksong.RawData;
internal static partial class BaseLocationList
{
    public static Location Mask_Shard__Sprintmaster => new SprintmasterLocation
    {
        SceneName = SceneNames.Sprintmaster_Cave,
        Name = LocationNames.Mask_Shard__Sprintmaster,
        IsQuestCompletion = true,
    };

}