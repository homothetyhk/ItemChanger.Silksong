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
