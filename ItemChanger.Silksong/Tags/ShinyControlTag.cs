using ItemChanger.Silksong.Containers;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.Tags;

public class ShinyControlTag : Tag
{
    public ShinyContainer.ShinyType ShinyType { get; set; } = ShinyContainer.ShinyType.Normal;

    public ShinyContainer.ShinyControlFlags ShinyControlFlags { get; set; } = ShinyContainer.ShinyControlFlags.Default;

    // TODO - green colour?
}
