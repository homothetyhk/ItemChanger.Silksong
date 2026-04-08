using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.Containers;
using ItemChanger.Tags;
using System;
using System.Collections.Generic;
using System.Text;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
    public static Location Lore_Tablet__Memorium_Entrance => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Memorium_Entrance,
        SceneName = SceneNames.Arborium_01,
        ObjectName = "Inspect Region",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true }
            ]
    };

    public static Location Lore_Tablet__Cradle_Cage_1 => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Cradle_Cage_1,
        SceneName = SceneNames.Cradle_02b,
        ObjectName = "Inspect Region",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true }
            ]
    };

    public static Location Lore_Tablet__Cradle_Cage_2 => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Cradle_Cage_2,
        SceneName = SceneNames.Cradle_02b,
        ObjectName = "Inspect Region (1)",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true }
            ]
    };

    public static Location Lore_Tablet__Cradle_Cage_3 => new ObjectLocation()
    {
        Name = LocationNames.Lore_Tablet__Cradle_Cage_3,
        SceneName = SceneNames.Cradle_02b,
        ObjectName = "Inspect Region (2)",
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.Tablet, Force = true }
            ]
    };
}
