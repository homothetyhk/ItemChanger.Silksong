using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Silksong.FsmUtil;
using ItemChanger.Silksong.Extensions;

namespace ItemChanger.Silksong.Locations;

public class ZylotolNeedlePhialLocation : ZylotolLocation
{

    protected override void HookZylotol(PlayMakerFSM fsm)
    {
        Fsm dialogueTemplate = fsm.MustGetState("Common").GetFirstActionOfType<RunFSM>()!.runFsm;

        void replaceNeedlePhial(string questStateName)
        {
            Fsm questTemplate = dialogueTemplate.MustGetState(questStateName).GetFirstActionOfType<RunFSM>()!.runFsm;

            FsmState giveState = questTemplate.MustGetState("Give Extractor");
            giveState.RemoveFirstActionOfType<SetToolUnlocked>();
            giveState.AddLambdaMethod(this.CreateGiveAllDelegate(fsm.transform));
        }
        replaceNeedlePhial("Quest 1?");
        replaceNeedlePhial("Quest 2?");
    }
}