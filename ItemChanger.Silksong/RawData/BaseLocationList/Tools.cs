using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Tags;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Costs;
using ItemChanger.Silksong.Locations;
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

    public static Location Reserve_Bind => new DualLocation
    {
        SceneName = SceneNames.Hang_17b,
        Name = LocationNames.Reserve_Bind,
        Test = new PDBool(nameof(PlayerData.defeatedSongChevalierBoss)),
        TrueLocation = new CoordinateLocation
        {
            SceneName = SceneNames.Hang_17b,
            Name = LocationNames.Reserve_Bind,
            X = 44.4f,
            Y = 4.57f,
            Managed = false,
            ForceDefaultContainer = true,
        },
        FalseLocation = new SecondSentinelLocation
        {
            Name = LocationNames.Reserve_Bind,
            SceneName = SceneNames.Hang_17b,
        },
    };
}
