using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Tags;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Costs;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Serialization;

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

    public static Location Tacks => new DualLocation
    {
        Name = LocationNames.Tacks,
        SceneName = SceneNames.Dust_Shack,
        Test = new PDBool(nameof(PlayerData.blackThreadWorld)),
        FalseLocation = new BenjinAndCrullTacksLocation() 
        {
            Name = LocationNames.Tacks,
            SceneName = SceneNames.Dust_Shack,
        },
        TrueLocation = new ObjectLocation()
        {
            Name = LocationNames.Tacks,
            SceneName = SceneNames.Dust_Shack,
            ObjectName = "Collectable Item Dustpilo",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        },
    };
  
    public static Location Pin_Badge => new PinBadgeLocation
    {
        SceneName = SceneNames.Peak_07,
        Name = LocationNames.Pin_Badge,
    };

    public static Location Pollip_Pouch => new DualLocation
    {
        SceneName = SceneNames.Room_Witch,
        Name = LocationNames.Pollip_Pouch,
        Test = new QuestCompletionBool(Quests.Wood_Witch_Curse),
        TrueLocation = new CoordinateLocation
        {
            SceneName = SceneNames.Room_Witch,
            Name = LocationNames.Pollip_Pouch,
            X = 17.0f,
            Y = 6.57f,
            Managed = false,
            ForceDefaultContainer = true,
        },
        FalseLocation = new GreyrootPollipLocation
        {
            Name = LocationNames.Pollip_Pouch,
            SceneName = SceneNames.Room_Witch,
        },
    };
  
    public static Location Longclaw => new DualLocation
    {
        Name = LocationNames.Longclaw,
        SceneName = SceneNames.Room_Huntress,
        // If Huntress's quest was completed before Act 3, neither Huntress nor Runt will be present in Act 3
        Test = new Conjunction
        (
            new PDBool(nameof(PlayerData.blackThreadWorld)),
            new QuestCompletionBool(Quests.Huntress_Quest)
        ),
        FalseLocation = new HuntressQuestLocation() 
        {
            Name = LocationNames.Longclaw,
            SceneName = SceneNames.Room_Huntress,
        },
        TrueLocation = new CoordinateLocation
        {
            Name = LocationNames.Longclaw,
            SceneName = SceneNames.Room_Huntress,
            X = 17.85f,
            Y = 6.57f,
            Managed = false,
            ForceDefaultContainer = true,
        },
    };
}
