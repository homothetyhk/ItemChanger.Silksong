using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Tags;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
    public static Location Arcane_Egg => new ObjectLocation
    {
        Name = LocationNames.Arcane_Egg,
        SceneName = SceneNames.Abyss_04,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
    };

    public static Location Bone_Scroll__Burning_Bug => new ObjectLocation
    {
        Name = LocationNames.Bone_Scroll__Burning_Bug,
        SceneName = SceneNames.Wisp_08,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
    };

    public static Location Bone_Scroll__Lost_Pilgrim => new ObjectLocation
    {
        Name = LocationNames.Bone_Scroll__Lost_Pilgrim,
        SceneName = SceneNames.Greymoor_21,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
    };

    public static Location Bone_Scroll__Singed_Pilgrim => new ObjectLocation
    {
        Name = LocationNames.Bone_Scroll__Singed_Pilgrim,
        SceneName = SceneNames.Bone_East_14,
        ObjectName = "Collectable Item Pickup (1)",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
    };

    public static Location Bone_Scroll__Underworker => new ObjectLocation
    {
        Name = LocationNames.Bone_Scroll__Underworker,
        SceneName = SceneNames.Under_16,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
    };

    public static Location Choral_Commandment__Light => new ObjectLocation
    {
        Name = LocationNames.Choral_Commandment__Light,
        SceneName = SceneNames.Ward_05,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
    };

    public static Location Choral_Commandment__Surgeon => new ObjectLocation
    {
        Name = LocationNames.Choral_Commandment__Surgeon,
        SceneName = SceneNames.Ward_02b,
        ObjectName = "Husk Item Ambush/Ward Bed (1)/corpse/Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny, Force = true }]
    };

    public static Location Choral_Commandment__White_Wyrm => new ObjectLocation
    {
        Name = LocationNames.Choral_Commandment__White_Wyrm,
        SceneName = SceneNames.Aspid_01,
        ObjectName = "Collectable Item Pickup (2)",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
    };

    public static Location Psalm_Cylinder__Ascendence_Theme => new ObjectLocation
    {
        Name = LocationNames.Psalm_Cylinder__Ascendence_Theme,
        SceneName = SceneNames.Hang_10,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
    };

    public static Location Psalm_Cylinder__Choir_Voices => new ObjectLocation
    {
        Name = LocationNames.Psalm_Cylinder__Choir_Voices,
        SceneName = SceneNames.Library_08,
        ObjectName = "Collectable Item Pickup Librarian",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
    };

    public static Location Psalm_Cylinder__Sermon => new ObjectLocation
    {
        Name = LocationNames.Psalm_Cylinder__Sermon,
        SceneName = SceneNames.Library_09,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
    };

    public static Location Psalm_Cylinder__Surgery => new ObjectLocation
    {
        Name = LocationNames.Psalm_Cylinder__Surgery,
        SceneName = SceneNames.Under_08,
        ObjectName = "Collectable Item Pickup (1)",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
    };

    public static Location Rune_Harp__Burden => new ObjectLocation
    {
        Name = LocationNames.Rune_Harp__Burden,
        SceneName = SceneNames.Hang_12,
        ObjectName = "Black Thread States/Black Thread World/Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
    };

    public static Location Rune_Harp__Escape => new ObjectLocation
    {
        Name = LocationNames.Rune_Harp__Escape,
        SceneName = SceneNames.Bone_East_Weavehome,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
    };

    public static Location Rune_Harp__Eva => new ObjectLocation
    {
        Name = LocationNames.Rune_Harp__Eva,
        SceneName = SceneNames.Weave_08,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
    };

    public static Location Sacred_Cylinder => new ObjectLocation
    {
        Name = LocationNames.Sacred_Cylinder,
        SceneName = SceneNames.Library_10,
        ObjectName = "Collectable Item Pickup - Melody",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Shiny },
            // Remove Hornet dialogue on pickup.
            new RemoveComponentTag<PlayMakerFSM>() { SceneName = SceneNames.Library_10, ObjectName = "Collectable Item Pickup - Melody" }
        ]
    };

    public static Location Weaver_Effigy__Atla => new ObjectLocation
    {
        Name = LocationNames.Weaver_Effigy__Atla,
        SceneName = SceneNames.Slab_12,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
    };

    public static Location Weaver_Effigy__Camora => new ObjectLocation
    {
        Name = LocationNames.Weaver_Effigy__Camora,
        SceneName = SceneNames.Bonetown,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
    };

    public static Location Weaver_Effigy__Keelal => new ObjectLocation
    {
        Name = LocationNames.Weaver_Effigy__Keelal,
        SceneName = SceneNames.Shellwood_25,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
    };
}
