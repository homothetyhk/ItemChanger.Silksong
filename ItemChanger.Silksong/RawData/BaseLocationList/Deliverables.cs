using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.Tags;
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
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
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
        GiveEarly = new IntComparisonBool { ToCompare = new SDInt(SceneNames.Bone_East_18b, "ant_item_string"), Amount = 0, Operator = Enums.ComparisonOperator.Gt },
        // Object is removed by default when the delivery quest is complete; suppress that.
        Tags = [new RemoveComponentTag<TestGameObjectActivator>() { SceneName = SceneNames.Bone_East_18b, ObjectName = "ant_item_string" }]
    };

    public static Location Mossberry_Stew => new MossDruidStewLocation
    {
        SceneName = SceneNames.Mosstown_02c,
        Name = LocationNames.Mossberry_Stew,
        FlingType = Enums.FlingType.DirectDeposit,
        PreviewIndex = 0,
    };

    // TODO: Handle audio changes when https://github.com/homothetyhk/ItemChanger.Silksong/pull/223 lands.
    public static Location Twisted_Bud => new ObjectLocation
    {
        Name = LocationNames.Twisted_Bud,
        SceneName = SceneNames.Shadow_20,
        ObjectName = "Collectable Mandrake Scene/Collectable Item Pickup (1)",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
    };
}
