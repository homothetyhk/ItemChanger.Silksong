using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Locations;
using Newtonsoft.Json;
using Silksong.FsmUtil;

namespace ItemChanger.Silksong.Locations;

public class ChapelMaidLocation : AutoLocation
{
    protected override void DoLoad()
    {
        Using(new FsmEditGroup()
        {
            { new(SceneName!, "Churchkeeper", "Conversation"), HookChapelMaid }
        });
    }

    protected override void DoUnload() { }

    private void HookChapelMaid(PlayMakerFSM fsm)
    {
        // Always give the Maiden's Soul dialogue when there are items to collect
        FsmState snareConvoDialogState = fsm.MustGetState("Snare Soul Dlg");
        snareConvoDialogState.RemoveActionsOfType<CollectableItemGetDataV2>();
        snareConvoDialogState.InsertMethod(0, () =>
        {
            fsm.SetFsmBoolIfExists("Has Any", Placement!.AllObtained());
        });
        
        // Replace granting Maiden's Soul with giving the placement
        FsmState giveSoulState = fsm.MustGetState("Give Soul");
        giveSoulState.RemoveActionsOfType<SavedItemGetV2>();
        giveSoulState.InsertLambdaMethod(3, GiveAll);
    }
}