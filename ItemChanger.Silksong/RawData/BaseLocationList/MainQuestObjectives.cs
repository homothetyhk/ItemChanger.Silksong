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
    public static Location Architect_s_Melody => new ArchitectPuzzleLocation()
    {
        SceneName = SceneNames.Cog_09,
        Name = LocationNames.Architect_s_Melody,
    };
}
