using Benchwarp.Data;
using ItemChanger.Enums;
using ItemChanger.Locations;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.Tags;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
    public static Location CreateFleaLocation(
        string name,
        string sceneName,
        string objectName,
        FleaContainerType fleaType,
        // Null => I haven't checked what it should be
        // This should be set to non-null if the location is replaceable
        float? elevation = null,
        bool replaceable = true,
        FlingType flingType = FlingType.Everywhere
        )
    {
        if (elevation == null && replaceable)
        {
            ItemChangerPlugin.Instance.Logger.LogWarning($"Location {name} needs its elevation checked!");
        }

        return new ObjectLocation()
        {
            Name = name,
            SceneName = sceneName,
            ObjectName = objectName,
            FlingType = flingType,
            Correction = new UnityEngine.Vector3(0, elevation ?? 0, 0),
            Tags = [
                new OriginalContainerTag() { ContainerType = ContainerNames.Flea, Force = !replaceable },
                new VanillaFleaTag(),
                new OriginalFleaTypeTag() { FleaContainerType = fleaType }
            ]
        };
    }

    public static Location Flea__Dust_12 => CreateFleaLocation(
        LocationNames.Flea__dust_12,
        SceneNames.Dust_12,
        "Flea Rescue Sleeping",
        FleaContainerType.Sleeping,
        -0.29f  // Is this + or - ?
    );

    public static Location Flea__Bone_East_05 => CreateFleaLocation(
        LocationNames.Flea__bone_east_05,
        SceneNames.Bone_East_05,
        "Flea Rescue Barrel",
        FleaContainerType.Barrel,
        0f
        );

    public static Location Flea__Crawl_06 => CreateFleaLocation(
        LocationNames.Flea__crawl_06,
        SceneNames.Crawl_06,
        "Aspid Collector (3)",
        FleaContainerType.Aspid,
        replaceable: false
        );

    public static Location Flea__Belltown_04 => CreateFleaLocation(
        LocationNames.Flea__belltown_04,
        SceneNames.Belltown_04,
        "Bell Wall Flea/Flea Rescue Generic",
        FleaContainerType.GenericWall
    ).WithTag(new DestroyOnContainerReplaceTag() { ObjectPath = "Bell Wall Flea/Bell Wall Tall (5)" });

    public static Location Flea__Dock_16 => new DualLocation()
    {
        Name = LocationNames.Flea__dock_16,
        TrueLocation = CreateFleaLocation(
            LocationNames.Flea__dock_16,
            SceneNames.Dock_16,
            "Flea Rescue BlackSilk",
            FleaContainerType.BlackSilk
        ),
        FalseLocation = CreateFleaLocation(
            LocationNames.Flea__dock_16,
            SceneNames.Dock_16,
            "Flea Rescue Sleeping",
            FleaContainerType.Sleeping,
            elevation: -0.29f  // + or - ?
        ),
        Test = new PDBool(nameof(PlayerData.blackThreadWorld))
    };
}

file static class Ext
{
    public static Location WithTag(this Location self, Tag t)
    {
        self.AddTag(t);
        return self;
    }
}
