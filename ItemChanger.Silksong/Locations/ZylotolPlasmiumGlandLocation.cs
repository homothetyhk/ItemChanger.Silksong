using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Silksong.FsmUtil;
using ItemChanger.Silksong.Extensions;
using Silksong.FsmUtil.Actions;

namespace ItemChanger.Silksong.Locations;

public class ZylotolPlasmiumGlandLocation : ZylotolLocation
{

    protected override void HookZylotol(PlayMakerFSM fsm)
    {
        FsmState? glandState = fsm.GetState("Take Gland");
        if (glandState == null) return;
        int i = glandState.IndexFirstActionOfType<SavedItemGet>();
        glandState.RemoveFirstActionOfType<SavedItemGet>();
        glandState.InsertLambdaMethod(i, this.CreateGiveAllDelegate(fsm.transform));
    }
}