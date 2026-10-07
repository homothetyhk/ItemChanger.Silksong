using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.Tags;
using ItemChanger.Silksong.Tags.SpecialLocationTags;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
    public static Location Crustnut => new ObjectLocation
    {
        Name = LocationNames.Crustnut,
        SceneName = SceneNames.Coral_41,
        ObjectName = "Group/Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = new(-2f, 0),
        Tags = [
            new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true},
            new DeactivateObjectTag
            {
                SceneName = SceneNames.Coral_41,
                ObjectName = "Group/bone_rubble_large_walls_0002_1 (85)",
                Test = new UnexpectedContainerBool { PlacementName = LocationNames.Crustnut, ExpectedContainerType = ContainerNames.Shiny }
            },
            SmallFleaPrefabs
        ]
    };

    // TODO: Use StringLocation once implemented: https://github.com/homothetyhk/ItemChanger.Silksong/issues/111
    public static Location Grass_Doll => new DelayedShinyLocation
    {
        Name = LocationNames.Grass_Doll,
        SceneName = SceneNames.Bone_East_18b,
        ObjectName = "ant_item_string/Item Holder/Collectable Item Pickup",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        // Do not give early if the string is already broken.
        GiveEarly = new IntComparisonBool { ToCompare = new SDInt(SceneNames.Bone_East_18b, "ant_item_string", 3), Amount = 0, Operator = Enums.ComparisonOperator.Gt },
        // Object is removed by default when the delivery quest is complete; suppress that.
        Tags = [new RemoveComponentTag<TestGameObjectActivator> { SceneName = SceneNames.Bone_East_18b, ObjectName = "ant_item_string" }]
    };

    public static Location Mossberry_Stew => new MossDruidStewLocation
    {
        SceneName = SceneNames.Mosstown_02c,
        Name = LocationNames.Mossberry_Stew,
        FlingType = Enums.FlingType.DirectDeposit,
        PreviewIndex = 0,
    };

    public static Location Twisted_Bud => new ObjectLocation
    {
        Name = LocationNames.Twisted_Bud,
        SceneName = SceneNames.Shadow_20,
        ObjectName = "Collectable Mandrake Scene/Collectable Item Pickup (1)",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [
            new OriginalContainerTag { ContainerType = ContainerNames.Shiny, LowPriority = true },
            new DeactivateObjectTag
            {
                SceneName = SceneNames.Shadow_20,
                ObjectName = "Collectable Mandrake Scene/Crying Audio Control",
                Test = new IsItemMissingBool { PlacementName = LocationNames.Twisted_Bud, ItemName = ItemNames.Twisted_Bud }
            },
            new DeactivateObjectTag
            {
                SceneName = SceneNames.Shadow_20,
                ObjectName = "WW_petrified_0008_1 (20)",
                Test = new UnexpectedContainerBool { PlacementName = LocationNames.Twisted_Bud, ExpectedContainerType = ContainerNames.Shiny }
            },
            SmallFleaPrefabs
        ]
    };

    public static Location Vintage_Nectar => new DualLocation
    {
        Name = LocationNames.Vintage_Nectar,
        Test = new PlacementVisitStateBool { PlacementName = LocationNames.Vintage_Nectar, RequiredFlags = Enums.VisitState.ObtainedAnyItem },
        FalseLocation = new ObjectLocation
        {
            Name = LocationNames.Vintage_Nectar,
            SceneName = SceneNames.Ant_08,
            ObjectName = "Battle Scene/End Scene/Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag { ContainerType = ContainerNames.Shiny, Force = true }, new VintageNectarTag { }]
        },
        TrueLocation = new CreigeDrinkLocation  // Give persistent items when ordering a drink.
        {
            Name = LocationNames.Vintage_Nectar,
            Tags = [new VintageNectarTag { }]
        }
    };
}
