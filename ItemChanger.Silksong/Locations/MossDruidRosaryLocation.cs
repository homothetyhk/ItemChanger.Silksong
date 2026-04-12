using ItemChanger.Locations;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Silksong.FsmUtil;

namespace ItemChanger.Silksong.Locations;

public class MossDruidRosaryLocation : MossDruidLocation
{
    /// <summary>
    /// The 1-based index of this location among the Moss Druid rosary payouts. Should be between 1 and 3
    /// (inclusive) unless another mod adds more such payouts.
    /// </summary>
    public required int Index { get; init; }

    protected override void HookDruid(PlayMakerFSM fsm)
    {
        FsmState offerAnswerState = fsm.MustGetState("Offer Answer");
        int i = offerAnswerState.IndexLastActionOfType<DialogueYesNoItemV4>();
        if (i == -1)
        {
            i = offerAnswerState.actions.Length;
        }
        else
        {
            offerAnswerState.RemoveAction(i);
        }
        offerAnswerState.InsertMethod(i, () =>
        {
            if (PlayerData.instance.GetInt(nameof(PlayerData.druidMossBerriesSold)) == Index - 1)
            {
                PromptCost(fsm, "ACCEPT", "REFUSE");
            }
        });

        FsmState payCompleteState = fsm.MustGetState("Pay Complete");
        // This is idempotent, so it will work even if there are multiple of these locations.
        payCompleteState.RemoveFirstActionMatching(act =>
            act is CallMethodProper call 
            && call.methodName.Value == nameof(HeroController.AddGeo));
        i = payCompleteState.IndexFirstActionMatching(act =>
            act is CallMethodProper call
            && call.methodName.Value == nameof(HeroController.StartAnimationControl));
        if (i == -1)
        {
            throw new InvalidOperationException("call to StartAnimationControl not found");
        }
        payCompleteState.InsertMethod(i, () =>
        {
            if (PlayerData.instance.GetInt(nameof(PlayerData.druidMossBerriesSold)) == Index)
            {
                GiveAll();
            }
        });
    }
}
