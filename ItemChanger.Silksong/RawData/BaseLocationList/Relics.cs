using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.Tags;
using ItemChanger.Silksong.Tags.SpecialLocationTags;
using ItemChanger.Tags;
using UnityEngine;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
    public static Location Arcane_Egg => new ObjectLocation
    {
        Name = LocationNames.Arcane_Egg,
        SceneName = SceneNames.Abyss_04,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = new(0.1f, -0.5f),
        Tags = [
            new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true },
            new SetComponentClassFieldTag<SpriteRenderer, Sprite>  // Replace the image with a standless one if we replaced the container.
            {
                SceneName = SceneNames.Abyss_04,
                Field = new ComponentClassFieldOption<SpriteRenderer, Sprite>(SceneNames.Abyss_04, "Abyss_tiny_room_0001_1", nameof(SpriteRenderer.sprite)),
                Provider = new ICSilksongSprite("Images.Abyss_tiny_room_standless"),
                Test = new UnexpectedContainerBool { PlacementName = LocationNames.Arcane_Egg, ExpectedContainerType = ContainerNames.Shiny }
            }
        ]
    };

    public static Location Bone_Scroll__Burning_Bug => new ObjectLocation
    {
        Name = LocationNames.Bone_Scroll__Burning_Bug,
        SceneName = SceneNames.Wisp_08,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.DirectDeposit,
        Correction = new(0, -1.4f),
        Tags = [new OriginalContainerTag { ContainerType = ContainerNames.Shiny }, SmallFleaPrefabs]
    };

    public static Location Bone_Scroll__Lost_Pilgrim => new ObjectLocation
    {
        Name = LocationNames.Bone_Scroll__Lost_Pilgrim,
        SceneName = SceneNames.Greymoor_21,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.StraightUp,
        Correction = new(0, -1.5f),
        Tags = [
            new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true },
            new DeactivateObjectTag
            {
                SceneName = SceneNames.Greymoor_21,
                ObjectName = "corpses_pilgrim_spinner_0009_1",
                Test = new UnexpectedContainerBool { PlacementName = LocationNames.Bone_Scroll__Lost_Pilgrim, ExpectedContainerType = ContainerNames.Shiny }
            },
            SmallFleaPrefabs
        ]
    };

    public static Location Bone_Scroll__Singed_Pilgrim => new ObjectLocation
    {
        Name = LocationNames.Bone_Scroll__Singed_Pilgrim,
        SceneName = SceneNames.Bone_East_14,
        ObjectName = "Collectable Item Pickup (1)",
        FlingType = Enums.FlingType.Everywhere,
        Correction = new(0, -1.4f),
        Tags = [
            new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true },
            new DeactivateObjectTag
            {
                SceneName = SceneNames.Bone_East_14,
                ObjectName = "pilgrim_corpse_0002_2 (1)",
                Test = new UnexpectedContainerBool { PlacementName = LocationNames.Bone_Scroll__Singed_Pilgrim, ExpectedContainerType = ContainerNames.Shiny }
            },
            SmallFleaPrefabs
        ]
    };

    public static Location Bone_Scroll__Underworker => new ObjectLocation
    {
        Name = LocationNames.Bone_Scroll__Underworker,
        SceneName = SceneNames.Under_16,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = new(0, -1.4f),
        Tags = [
            new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true },
            new AdjustPositionTag
            {
                SceneName = SceneNames.Under_16,
                ObjectName = "understore_corpses_0003_1 (1)",
                Adjustment = new(0, 0, 0.01f),
                Test = new UnexpectedContainerBool { PlacementName = LocationNames.Bone_Scroll__Underworker, ExpectedContainerType = ContainerNames.Shiny }
            },
            SmallFleaPrefabs
        ]
    };

    public static Location Choral_Commandment__Light => new ObjectLocation
    {
        Name = LocationNames.Choral_Commandment__Light,
        SceneName = SceneNames.Ward_05,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = new(0, -1.55f),
        Tags = [
            new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true },
            new AdjustPositionTag
            {
                SceneName = SceneNames.Ward_05,
                ObjectName = "ward_corpse_pit_0001_1",
                Adjustment = new(0, 0, 0.01f),
                Test = new UnexpectedContainerBool { PlacementName = LocationNames.Choral_Commandment__Light, ExpectedContainerType = ContainerNames.Shiny }
            }
        ]
    };

    public static Location Choral_Commandment__Surgeon => new DualLocation
    {
        Name = LocationNames.Choral_Commandment__Surgeon,
        Test = new PlacementVisitStateBool { PlacementName = LocationNames.Choral_Commandment__Surgeon, RequiredFlags = Enums.VisitState.ObtainedAnyItem },
        FalseLocation = new ChoralCommandmentSurgeonLocation
        {
            Name = LocationNames.Choral_Commandment__Surgeon,
            SceneName = SceneNames.Ward_02b,
            Tags = [new OriginalContainerTag { ContainerType = ContainerNames.Shiny, Force = true }]
        },
        TrueLocation = new CoordinateLocation
        {
            Name = LocationNames.Choral_Commandment__Surgeon,
            SceneName = SceneNames.Ward_02b,
            X = 51,
            Y = 3.5f,
            Managed = false,
            Tags = [
                new DeactivateObjectTag { SceneName = SceneNames.Ward_02b, ObjectName = "Husk Item Ambush/Ward Bed (1)"},
                new ChoralCommandmentSurgeonHuskAmbushTag { }
            ]
        }
    };

    public static Location Choral_Commandment__White_Wyrm => new ObjectLocation
    {
        Name = LocationNames.Choral_Commandment__White_Wyrm,
        SceneName = SceneNames.Aspid_01,
        ObjectName = "Collectable Item Pickup (2)",
        // TODO: "Fling right" is safe here but there's no implementation of that for containers that aren't Shiny.
        FlingType = Enums.FlingType.StraightUp,
        Correction = new(0, -1.6f),
        Tags = [
            new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true },
            new DeactivateObjectTag
            {
                SceneName = SceneNames.Aspid_01,
                ObjectName = "Bonechurch_middle_bits_0007_corpse_02 (4)",
                Test = new UnexpectedContainerBool { PlacementName = LocationNames.Choral_Commandment__White_Wyrm, ExpectedContainerType = ContainerNames.Shiny }
            }
        ]
    };

    public static Location Psalm_Cylinder__Ascendence_Theme => new ObjectLocation
    {
        Name = LocationNames.Psalm_Cylinder__Ascendence_Theme,
        SceneName = SceneNames.Hang_10,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = new(0.65f, -1.6f),
        Tags = [
            new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true },
            new DeactivateObjectTag
            {
                SceneName = SceneNames.Hang_10,
                ObjectName = "Bonechurch_middle_bits_0007_corpse_02",
                Test = new UnexpectedContainerBool { PlacementName = LocationNames.Psalm_Cylinder__Ascendence_Theme, ExpectedContainerType = ContainerNames.Shiny }
            },
            SmallFleaPrefabs
        ]
    };

    public static Location Psalm_Cylinder__Choir_Voices => new ObjectLocation
    {
        Name = LocationNames.Psalm_Cylinder__Choir_Voices,
        SceneName = SceneNames.Library_08,
        ObjectName = "Collectable Item Pickup Librarian",
        FlingType = Enums.FlingType.Everywhere,
        Correction = new(0, -1.6f),
        Tags = [new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true }, SmallFleaPrefabs]
    };

    public static Location Psalm_Cylinder__Sermon => new ObjectLocation
    {
        Name = LocationNames.Psalm_Cylinder__Sermon,
        SceneName = SceneNames.Library_09,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = new(0, -0.5f),
        Tags = [new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true }]
    };

    public static Location Psalm_Cylinder__Surgery => new ObjectLocation
    {
        Name = LocationNames.Psalm_Cylinder__Surgery,
        SceneName = SceneNames.Under_08,
        ObjectName = "Collectable Item Pickup (1)",
        FlingType = Enums.FlingType.Everywhere,
        Correction = new(0, -0.3f),
        Tags = [new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true }, SmallFleaPrefabs]
    };

    public static Location Rune_Harp__Burden => new ObjectLocation
    {
        Name = LocationNames.Rune_Harp__Burden,
        SceneName = SceneNames.Hang_12,
        ObjectName = "Black Thread States/Black Thread World/Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = new(0, -1.5f),
        Tags = [new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true }]
    };

    public static Location Rune_Harp__Escape => new ObjectLocation
    {
        Name = LocationNames.Rune_Harp__Escape,
        SceneName = SceneNames.Bone_East_Weavehome,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = new(0, -1.6f),
        Tags = [new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true }]
    };

    public static Location Rune_Harp__Eva => new ObjectLocation
    {
        Name = LocationNames.Rune_Harp__Eva,
        SceneName = SceneNames.Weave_08,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = new(0.15f, -0.4f),
        Tags = [new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true }]
    };

    public static Location Sacred_Cylinder => new ObjectLocation
    {
        Name = LocationNames.Sacred_Cylinder,
        SceneName = SceneNames.Library_10,
        ObjectName = "Collectable Item Pickup - Melody",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [
            new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true },
            // Remove Hornet dialogue on pickup.
            new RemoveComponentTag<PlayMakerFSM> { SceneName = SceneNames.Library_10, ObjectName = "Collectable Item Pickup - Melody" },
            new DeactivateObjectTag
            {
                SceneName = SceneNames.Library_10,
                ObjectName = "house_furnishing_0002_1",
                Test = new UnexpectedContainerBool { PlacementName = LocationNames.Sacred_Cylinder, ExpectedContainerType = ContainerNames.Shiny }
            }
        ]
    };

    public static Location Weaver_Effigy__Atla => new ObjectLocation
    {
        Name = LocationNames.Weaver_Effigy__Atla,
        SceneName = SceneNames.Slab_12,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = new(0, -1.2f),
        Tags = [
            new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true },
            new DeactivateObjectTag
            {
                SceneName = SceneNames.Slab_12,
                ObjectName = "death0006",
                Test = new UnexpectedContainerBool { PlacementName = LocationNames.Weaver_Effigy__Atla, ExpectedContainerType = ContainerNames.Shiny }
            }
        ]
    };

    public static Location Weaver_Effigy__Camora => new ObjectLocation
    {
        Name = LocationNames.Weaver_Effigy__Camora,
        SceneName = SceneNames.Bonetown,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true }]
    };

    public static Location Weaver_Effigy__Keelal => new ObjectLocation
    {
        Name = LocationNames.Weaver_Effigy__Keelal,
        SceneName = SceneNames.Shellwood_25,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = new(0, -0.35f, -4.16f),
        Tags = [new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true }]
    };
}
