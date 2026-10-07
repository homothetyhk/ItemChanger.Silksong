using Benchwarp.Data;
using ItemChanger.Enums;
using ItemChanger.Items;
using ItemChanger.Locations;
using Silksong.FsmUtil;

namespace ItemChanger.Silksong.Locations;

public class CreigeDrinkLocation : AutoLocation
{
    protected override void DoLoad() => Using(new FsmEditGroup { { new(SceneNames.Halfway_01, "HH Bartender", "Dialogue"), ModifyCreigeDialogue } });

    protected override void DoUnload() { }

    private void ModifyCreigeDialogue(PlayMakerFSM fsm)
    {
        // Give items with the purchase of a drink.
        fsm.MustGetState("Charge Burst").AddMethod(_ =>
        {
            GiveInfo info = new()
            {
                FlingType = FlingType.DirectDeposit,
                MessageType = MessageType.SmallPopup
            };
            Placement!.GiveAll(info);
        });
    }
}
