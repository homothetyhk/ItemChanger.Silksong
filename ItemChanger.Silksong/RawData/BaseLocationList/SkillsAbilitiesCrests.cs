using ItemChanger.Locations;
using ItemChanger.Silksong.Locations;
using Benchwarp.Data;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.Tags;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
    private static Location CreateWeaverCorpseLocation(
        string name,
        string sceneName,
        string logicObjectName = "Shrine Weaver Ability",
        string spriteObjectPath = "Ability Scene/Burst Deactivate"
    )
    {
        return new ObjectLocation()
        {
            Name = name,
            SceneName = sceneName,
            ObjectName = logicObjectName,
            Correction = default,
            Tags =
            [
                new OriginalContainerTag() { ContainerType = ContainerNames.WeaverCorpse },
                new DestroyOnContainerReplaceTag() { ObjectPath = spriteObjectPath },
            ]
        };
    }
    
    public static Location Crest_of_Wanderer => new CrestCorpseLocation
    {
        SceneName = SceneNames.Chapel_Wanderer,
        Name = LocationNames.Crest_of_Wanderer,
    };

    public static Location Crest_of_Reaper => new CrestCorpseLocation
    {
        SceneName = SceneNames.Greymoor_20c,
        Name = LocationNames.Crest_of_Reaper,
    };

    public static Location Crest_of_Beast => new CrestCorpseLocation
    {
        SceneName = SceneNames.Ant_19,
        Name = LocationNames.Crest_of_Beast,
    };

    public static Location Crest_of_Architect => new CrestCorpseLocation
    {
        SceneName = SceneNames.Under_20,
        Name = LocationNames.Crest_of_Architect,
    };

    public static Location Crest_of_Shaman => new CrestCorpseLocation
    {
        SceneName = SceneNames.Tut_05,
        Name = LocationNames.Crest_of_Shaman,
    };

    public static Location Crest_of_Witch => new YarnabyLocation
    {
        SceneName = SceneNames.Belltown_Room_doctor,
        Name = LocationNames.Crest_of_Witch,
    };

    public static Location Drifter_s_Cloak => new DualLocation
    {
        SceneName = SceneNames.Bone_East_Umbrella,
        Name = LocationNames.Drifter_s_Cloak,
        Test = new QuestCompletionBool(Quests.Brolly_Get),
        FalseLocation = new DriftersCloakLocation(),
        TrueLocation = new CoordinateLocation()
        {
            SceneName = SceneNames.Bone_East_Umbrella,
            Name = LocationNames.Drifter_s_Cloak,
            X = 23.75f,
            Y = 8f,
            FlingType = Enums.FlingType.Everywhere,
            Managed = false,
        },
    };

    public static Location Eva => new EvaLocation
    {
        SceneName = SceneNames.Weave_10,
        Name = LocationNames.Eva,
    };
    
    public static Location Silkspear => CreateWeaverCorpseLocation(
        LocationNames.Silkspear,
        SceneNames.Mosstown_02
    );

    public static Location Thread_Storm => CreateWeaverCorpseLocation(
        LocationNames.Thread_Storm,
        SceneNames.Greymoor_22
    );

    public static Location Sharpdart => CreateWeaverCorpseLocation(
        LocationNames.Sharpdart,
        SceneNames.Crawl_05,
        spriteObjectPath: "Ability Scene (2)/Burst Deactivate"
    );

    public static Location Swift_Step => CreateWeaverCorpseLocation(
        LocationNames.Swift_Step,
        SceneNames.Bone_East_05,
        spriteObjectPath: "Ability Scene (1)/Burst Deactivate"
    );
    
    public static Location Cling_Grip => CreateWeaverCorpseLocation(
        LocationNames.Cling_Grip,
        SceneNames.Shellwood_10,
        spriteObjectPath: "Ability Scene (1)/Burst Deactivate"
    );

    public static Location Clawline => CreateWeaverCorpseLocation(
        LocationNames.Clawline,
        SceneNames.Under_18,
        spriteObjectPath: "Ability Scene/Burst Deactivate/Scenery"
    );

    public static Location Silk_Soar => CreateWeaverCorpseLocation(
        LocationNames.Silk_Soar,
        SceneNames.Abyss_08,
        spriteObjectPath: "weaver_spire_base control/Burst Deactivate"
    ).WithTag(
        new RaiseFsmEventOnGiveTag()
        {
            SceneName =  SceneNames.Abyss_08,
            Event = "SHRINE SEQUENCE END",
            ObjectPath = "weaver_spire_base control",
            OnlyIfContainerReplaced = true
        }
    );
}