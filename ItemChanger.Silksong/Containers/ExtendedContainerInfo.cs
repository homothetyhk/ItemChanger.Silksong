using ItemChanger.Containers;
using System.Diagnostics.CodeAnalysis;

namespace ItemChanger.Silksong.Containers;

public class ExtendedContainerInfo<T> : ContainerInfo
{
    public required T ExtendedInfo { get; init; }

    public ExtendedContainerInfo() { }

    [SetsRequiredMembers]
    public ExtendedContainerInfo(ContainerInfo containerInfo, T extendedInfo)
    {
        base.CostInfo = containerInfo.CostInfo;
        base.ContainingScene = containerInfo.ContainingScene;
        base.ContainerType = containerInfo.ContainerType;
        base.GiveInfo = containerInfo.GiveInfo;
        base.RequestedCapabilities = containerInfo.RequestedCapabilities;
        this.ExtendedInfo = extendedInfo;
    }
}
