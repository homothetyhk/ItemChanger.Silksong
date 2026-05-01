using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.Locations;

namespace ItemChanger.Silksong.RawData;
internal static partial class BaseLocationList
{
    public static Location Sprintmaster_Memento => new SprintmasterLocation
    {
        SceneName = SceneNames.Sprintmaster_Cave,
        Name = LocationNames.Sprintmaster_Memento,
        IsQuestCompletion = false,
    };
}
