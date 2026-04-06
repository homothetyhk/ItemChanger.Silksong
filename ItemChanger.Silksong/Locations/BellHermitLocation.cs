using ItemChanger.Locations;
using Newtonsoft.Json;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Silksong.FsmUtil;

namespace ItemChanger.Silksong.Locations;

public class BellHermitLocation : AutoLocation
{
    protected override void DoLoad()
    {
        Using(new FsmEditGroup()
        {
            { new(SceneName!, "Bell Hermit", "Dialogue"), HookBellHermit }
        });
    }

    protected override void DoUnload() { }

    private void HookBellHermit(PlayMakerFSM fsm)
    {
        FsmState snareConvoDialogState = fsm.MustGetState("Snare Soul Dlg");
        snareConvoDialogState.RemoveActionsOfType<CollectableItemGetDataV2>();
        snareConvoDialogState.InsertMethod(1, () =>
        {
            fsm.SetFsmBoolIfExists("Has Any", Placement!.AllObtained());
        });

        FsmState giveSoulState = fsm.MustGetState("Give Soul");
        giveSoulState.RemoveActionsOfType<SavedItemGetV2>();
        giveSoulState.InsertMethod(4, GiveAll);
    }
}