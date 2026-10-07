using ItemChanger.Silksong.Containers;
using ItemChanger.Tags;
using ItemChanger.Tags.Constraints;

namespace ItemChanger.Silksong.Tags;

[LocationTag]
[PlacementTag]
public class ExtendedContainerInfoTag<T> : Tag
{
    public required T Info { get; init; }
}

public class ChestControlTag : ExtendedContainerInfoTag<ChestContainer.ChestControlInfo> { }

public class FleaControlTag : ExtendedContainerInfoTag<FleaContainer.FleaControlInfo> { }

public class ShinyControlTag : ExtendedContainerInfoTag<ShinyContainer.ShinyControlInfo> { }
