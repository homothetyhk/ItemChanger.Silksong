using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.Containers;
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

    public static Location Silkspear => CreateWeaverCorpseLocation(
        LocationNames.Silkspear,
        SceneNames.Mosstown_02
    );

    public static Location Thread_Storm => CreateWeaverCorpseLocation(
        LocationNames.Thread_Storm,
        SceneNames.Greymoor_22
    );

    public static Location Swift_Step => CreateWeaverCorpseLocation(
        LocationNames.Swift_Step,
        SceneNames.Bone_East_05,
        spriteObjectPath: "Ability Scene (1)/Burst Deactivate"
    );

    public static Location Sharpdart => CreateWeaverCorpseLocation(
        LocationNames.Sharpdart,
        SceneNames.Crawl_05,
        spriteObjectPath: "Ability Scene (2)/Burst Deactivate"
    );

    public static Location Cling_Grip => CreateWeaverCorpseLocation(
        LocationNames.Cling_Grip,
        SceneNames.Shellwood_10,
        spriteObjectPath: "Ability Scene (1)/Burst Deactivate"
    );

    public static Location Clawline => CreateWeaverCorpseLocation(
        LocationNames.Clawline,
        SceneNames.Under_18
    );

    public static Location Silk_Soar => CreateWeaverCorpseLocation(
        LocationNames.Silk_Soar,
        SceneNames.Abyss_08,
        spriteObjectPath: "weaver_spire_base control/Burst Deactivate"
    );
}