using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Locations.MultiLocationEnums;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.Tags;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
    public static Location Cogheart_Piece__Choral_Chambers => new DelayedShinyLocation
    {
        Name = LocationNames.Cogheart_Piece__Choral_Chambers,
        SceneName = SceneNames.Song_26,
        ObjectName = "_Props/Music Box Sequence - EDITED/music_box_reward_pillar/Collectable Holder/Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        GiveEarly = new Negation { Bool = new BouncePodSequenceFinishedBool { SceneName = SceneNames.Song_26, ObjectName = "_Props/Music Box Sequence - EDITED" } }
    };

    public static Location Cogheart_Piece__Memorium => new DelayedShinyLocation
    {
        Name = LocationNames.Cogheart_Piece__Memorium,
        SceneName = SceneNames.Arborium_10,
        ObjectName = "Music Box Sequence - EDITED/music_box_reward_pillar/Collectable Holder/Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        GiveEarly = new Negation { Bool = new BouncePodSequenceFinishedBool { SceneName = SceneNames.Arborium_10, ObjectName = "Music Box Sequence - EDITED" } }
    };

    public static Location Cogheart_Piece__Whispering_Vaults => new DelayedShinyLocation
    {
        Name = LocationNames.Cogheart_Piece__Whispering_Vaults,
        SceneName = SceneNames.Library_16,
        ObjectName = "Music Box Sequence - EDITED/music_box_reward_pillar/Collectable Holder/Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        GiveEarly = new Negation { Bool = new BouncePodSequenceFinishedBool { SceneName = SceneNames.Library_16, ObjectName = "Music Box Sequence - EDITED" } }
    };

    public static Location Craw_Summons => new CrawSummonsLocation()
    {
        Name = LocationNames.Craw_Summons,
        FlingType = Enums.FlingType.Everywhere,
        ObjectName = "craw_court_summons_pin",
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.CrawSummons }]
    };

    public static Location Key_of_Apostate => new DualLocation
    {
        Name = LocationNames.Key_of_Apostate,
        Test = new PlacementVisitStateBool
        {
            PlacementName = LocationNames.Key_of_Apostate,
            RequiredFlags = Enums.VisitState.ObtainedAnyItem
        },
        FalseLocation = new DelayedShinyLocation
        {
            Name = LocationNames.Key_of_Apostate,
            SceneName = SceneNames.Aqueduct_04,
            ObjectName = "Breakable_cage/Collectable Item Pickup Slab Key",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            GiveEarly = new BoxedBool { Value = true },
            Tags = [new RemoveComponentTag<DeactivateIfPlayerdataTrue> { SceneName = SceneNames.Aqueduct_04, ObjectName = "Breakable_cage" }]
        },
        TrueLocation = new CoordinateLocation
        {
            Name = LocationNames.Key_of_Apostate,
            SceneName = SceneNames.Aqueduct_04,
            X = 11.09f,
            Y = 38.78f,
            Managed = false,
            Tags = [new DestroyObjectTag { SceneName = SceneNames.Aqueduct_04, ObjectName = "Breakable_cage" }]
        }
    };

    public static Location Key_of_Heretic => new MultiLocation<SlabBattleState>
    {
        Name = LocationNames.Key_of_Heretic,
        SceneName = SceneNames.Slab_16,
        Selector = EnumValueProviders.SlabBattleStateProvider,
        Locations = new Dictionary<SlabBattleState, Location>()
        {
            [SlabBattleState.Cloakless] = new DelayedShinyLocation
            {
                Name = LocationNames.Key_of_Heretic,
                SceneName = SceneNames.Slab_16,
                ObjectName = "Event Control/Battle Cloakless Scene/Wave 9 - Item/Item Placer/Collectable Item Pickup",
                FlingType = Enums.FlingType.Everywhere,
                Correction = default,
                GiveEarly = new BoxedBool { Value = true }
            },
            [SlabBattleState.Cloaked] = new DelayedShinyLocation
            {
                Name = LocationNames.Key_of_Heretic,
                SceneName = SceneNames.Slab_16,
                ObjectName = "Event Control/Battle Cloaked Scene/Wave 5 - Item/Item Placer/Collectable Item Pickup",
                FlingType = Enums.FlingType.Everywhere,
                Correction = default,
                GiveEarly = new BoxedBool { Value = true }
            },
            [SlabBattleState.Completed] = new ObjectLocation
            {
                Name = LocationNames.Key_of_Heretic,
                SceneName = SceneNames.Slab_16,
                ObjectName = "Event Control/Battle Completed Scene/Collectable Item Pickup Return",
                FlingType = Enums.FlingType.Everywhere,
                Correction = default,
                Tags = [
                    new OriginalContainerTag { ContainerType = ContainerNames.Shiny, Force = true },
                    new SlabBattleForceSpawnCompletedTag()  // Force "Battle Completed Scene" to spawn even if the Broodmother quest is active.
                ]
            }
        }
    };

    public static Location Key_of_Indolent => new DelayedShinyLocation
    {
        Name = LocationNames.Key_of_Indolent,
        SceneName = SceneNames.Slab_14,
        ObjectName = "slab_item_chain/breakable/Collectable Item Pickup (1)",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        GiveEarly = new Negation { Bool = new SDBool(SceneNames.Slab_14, "slab_item_chain") }
    };

    public static Location Simple_Key__Sands_of_Karak => new ObjectLocation
    {
        Name = LocationNames.Simple_Key__Sands_of_Karak,
        SceneName = SceneNames.Bellshrine_Coral,
        ObjectName = "Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
    };

    public static Location Simple_Key__Sinner_s_Road => new RoachkeeperSimpleKeyLocation
    {
        Name = LocationNames.Simple_Key__Sinner_s_Road,
        SceneName = SceneNames.Dust_06,
        ObjectName = "Roachkeeper Key Control/Collectable Item SimpleKey",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        GiveEarly = new Negation { Bool = new RoachkeeperSimpleKeyLocation.IsRoachkeeperDefeatedBool { LocationName = LocationNames.Simple_Key__Sinner_s_Road } }
    };

    public static Location Surgeon_s_Key => new DualLocation
    {
        Name = LocationNames.Surgeon_s_Key,
        Test = new SDBool(SceneNames.Ward_07, "Junk Hatch"),
        FalseLocation = new DelayedShinyLocation
        {
            Name = LocationNames.Surgeon_s_Key,
            SceneName = SceneNames.Ward_07,
            ObjectName = "Group/Junk Hatch/Return Corpse/Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            GiveEarly = new BoxedBool { Value = true }
        },
        TrueLocation = new ObjectLocation
        {
            Name = LocationNames.Surgeon_s_Key,
            SceneName = SceneNames.Ward_07,
            ObjectName = "Group/Junk Hatch/Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [
                new OriginalContainerTag() { ContainerType = ContainerNames.Shiny, Force = true },
                new ShinyControlTag() { Info = new() { ShinyFling = ShinyContainer.ShinyFling.FloatInPlace } }
            ]
        }
    };
}