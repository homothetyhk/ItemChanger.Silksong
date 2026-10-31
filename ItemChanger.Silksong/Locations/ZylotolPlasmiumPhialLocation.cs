using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Silksong.FsmUtil;
using ItemChanger.Silksong.Extensions;

namespace ItemChanger.Silksong.Locations;

public class ZylotolPlasmiumPhialLocation : ZylotolLocation
{

    protected override void HookZylotol(PlayMakerFSM fsm)
    {
        Fsm dialogueTemplate = fsm.MustGetState("Common").GetFirstActionOfType<RunFSM>()!.runFsm;

        Fsm questTemplate = dialogueTemplate.MustGetState("Quest 1?").GetFirstActionOfType<RunFSM>()!.runFsm;

        FsmState giveState = questTemplate.MustGetState("Reward");
        giveState.RemoveFirstActionOfType<SavedItemGet>();
        giveState.AddLambdaMethod(this.CreateGiveAllDelegate(fsm.transform));
    }
}