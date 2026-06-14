using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Tags;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Costs;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.Serialization;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
    public static Location Druid_s_Eye => new MossDruidMix1Location
    {
        Name = LocationNames.Druid_s_Eye,
        SceneName = SceneNames.Mosstown_02c,
        FlingType = Enums.FlingType.DirectDeposit,
        PreviewIndex = 0,
    }.WithTag(new DefaultCostTag { Cost = new MossberryCost { Value = 3 } });

    public static Location Druid_s_Eyes => new MossDruidMix2Location
    {
        Name = LocationNames.Druid_s_Eyes,
        SceneName = SceneNames.Mosstown_02c,
        FlingType = Enums.FlingType.DirectDeposit,
        PreviewIndex = 4,
    }.WithTag(new DefaultCostTag { Cost = new MossberryCost { Value = 7 } });
    
    public static Location Pollip_Pouch => new DualLocation
    {
        SceneName = SceneNames.Room_Witch,
        Name = LocationNames.Pollip_Pouch,
        Test = new QuestCompletedBool { QuestName = Quests.Wood_Witch_Curse },
        TrueLocation = new CoordinateLocation
        {
            SceneName = SceneNames.Room_Witch,
            Name = LocationNames.Pollip_Pouch,
            X = 17.0f,
            Y = 6.57f,
            Managed = false,
        },
        FalseLocation = new GreyrootPollipLocation
        {
            Name = LocationNames.Pollip_Pouch,
            SceneName = SceneNames.Room_Witch,
        },
    };
}
