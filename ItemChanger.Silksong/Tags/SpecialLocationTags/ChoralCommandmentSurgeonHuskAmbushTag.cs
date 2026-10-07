using Benchwarp.Data;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.Tags.SpecialLocationTags;

/// <summary>
/// Make the husk wake up if the item has been collected.
/// This is normally handled by the CollectableItemPickup.OnPreviouslyPickedUp events, but that doesn't apply for us.
/// </summary>
public class ChoralCommandmentSurgeonHuskAmbushTag : Tag
{
    protected override void DoLoad(TaggableObject parent) => Using(new FsmEditGroup { { new(SceneNames.Ward_02b, "Husk Item Ambush", "Control"), fsm => fsm.SendEvent("ITEM PICKED UP") } });
}
