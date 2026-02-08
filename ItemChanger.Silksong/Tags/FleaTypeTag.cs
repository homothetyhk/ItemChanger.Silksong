using ItemChanger.Locations;
using ItemChanger.Silksong.Containers;
using ItemChanger.Tags;
using ItemChanger.Tags.Constraints;

namespace ItemChanger.Silksong.Tags;

/// <summary>
/// Tag recording the flea container type of a flea location.
/// </summary>
[TagConstrainedTo<ContainerLocation>]
public class OriginalFleaTypeTag : Tag
{
    public required FleaContainerType FleaContainerType;
}

/// <summary>
/// Tag indicating that an item is requesting to be given a particular flea type.
/// </summary>
[ItemTag]
public class ItemFleaTypeTag : Tag
{
    public required FleaContainerType FleaContainerType;
}
