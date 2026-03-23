using ItemChanger.Silksong.Containers;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.Tags;

public class ShinyControlTag : Tag
{
    public ShinyContainer.ShinyControlInfo Info { get; set; } = new();
}
