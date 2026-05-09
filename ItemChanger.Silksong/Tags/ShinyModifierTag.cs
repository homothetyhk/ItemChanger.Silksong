using ItemChanger.Items;
using ItemChanger.Placements;
using ItemChanger.Tags;
using ItemChanger.Tags.Constraints;

namespace ItemChanger.Silksong.Tags;

[ItemTag]
public abstract class ShinyModifierTag : Tag
{
    public abstract void ModifyShinyContainer(Placement placement, Item item, GameObject shiny);
}
