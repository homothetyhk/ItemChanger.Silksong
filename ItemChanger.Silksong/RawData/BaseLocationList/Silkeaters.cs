using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Tags;
using ItemChanger.Silksong.Tags.SpecialLocationTags;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
    public static Location Silkeater__Bilewater => new ObjectLocation()
    {
        SceneName = SceneNames.Organ_01,
        Name = LocationNames.Silkeater__Bilewater,
        ObjectName = "Silk Grub Large Cocoon",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.SilkGrubCocoon, Force = true }]
    };

    public static Location Silkeater__Blasted_Steps => new ObjectLocation()
    {
        SceneName = SceneNames.Coral_37,
        Name = LocationNames.Silkeater__Blasted_Steps,
        ObjectName = "Room_States/Normal/Silk Grub Large Cocoon",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [
            new OriginalContainerTag() { ContainerType = ContainerNames.SilkGrubCocoon, Force = true },
            new BlastedStepsSilkeaterTag(),  // Force spawn in Steel Soul mode.
        ]
    };

    public static Location Silkeater__Choral_Chambers_East => new ObjectLocation()
    {
        SceneName = SceneNames.Song_24,
        Name = LocationNames.Silkeater__Choral_Chambers_East,
        ObjectName = "Silk Grub Large Cocoon",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.SilkGrubCocoon, Force = true }]
    };

    public static Location Silkeater__Choral_Chambers_West => new ObjectLocation()
    {
        SceneName = SceneNames.Song_09b,
        Name = LocationNames.Silkeater__Choral_Chambers_West,
        ObjectName = "Silk Grub Large Cocoon",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.SilkGrubCocoon, Force = true }]
    };

    public static Location Silkeater__Deep_Docks => new ObjectLocation()
    {
        SceneName = SceneNames.Dock_14,
        Name = LocationNames.Silkeater__Deep_Docks,
        ObjectName = "Silk Grub Large Cocoon",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.SilkGrubCocoon, Force = true }]
    };

    public static Location Silkeater__Greymoor => new ObjectLocation()
    {
        SceneName = SceneNames.Greymoor_04,
        Name = LocationNames.Silkeater__Greymoor,
        ObjectName = "Silk Grub Large Cocoon",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.SilkGrubCocoon, Force = true }]
    };

    public static Location Silkeater__Queen_s_Egg => new StyxCocoonLocation()
    {
        SceneName = SceneNames.Dust_11,
        Name = LocationNames.Silkeater__Queen_s_Egg,
        FarmLevel = 2,
    }.WithTag(new SilkgrubCocoonControlTag() { Info = new() { IgnoreOpenState = true } });

    public static Location Silkeater__Styx => new StyxCocoonLocation()
    {
        SceneName = SceneNames.Dust_11,
        Name = LocationNames.Silkeater__Styx,
        FarmLevel = 1,
    }.WithTag(new SilkgrubCocoonControlTag() { Info = new() { IgnoreOpenState = true } });

    public static Location Silkeater__Terminus => new ObjectLocation()
    {
        SceneName = SceneNames.Tube_Hub,
        Name = LocationNames.Silkeater__Terminus,
        ObjectName = "Silk Grub Large Cocoon",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.SilkGrubCocoon, Force = true }]
    };

    public static Location Silkeater__Whiteward => new ObjectLocation()
    {
        SceneName = SceneNames.Ward_04,
        Name = LocationNames.Silkeater__Whiteward,
        ObjectName = "Silk Grub Large Cocoon",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.SilkGrubCocoon, Force = true }]
    };

    public static Location Silkeater__Whispering_Vaults => new ObjectLocation()
    {
        SceneName = SceneNames.Library_14,
        Name = LocationNames.Silkeater__Whispering_Vaults,
        ObjectName = "Silk Grub Large Cocoon",
        FlingType = Enums.FlingType.Everywhere,
        Correction = default,
        Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.SilkGrubCocoon, Force = true }]
    };
}
