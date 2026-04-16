using ItemChanger.Silksong.RawData;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Silksong.FsmUtil;
using Silksong.FsmUtil.Actions;

namespace ItemChanger.Silksong.Locations;

public class MossDruidStewLocation : MossDruidLocation
{
    protected override void HookDruid(PlayMakerFSM fsm)
    {
        FsmState choiceState = fsm.MustGetState("Choice");
        int i = choiceState.IndexLastActionMatching(act => act is BoolTestMulti test && test.trueEvent.Name == "INGREDIENT");
        choiceState.ReplaceAction(i, new LambdaAction
        {
            Method = () =>
            {
                // The original action checks for both having accepted the quest and not having the stew;
                // we want to replace just the latter condition but must reimplement the former anyway.
                if (!Checked()
                && QuestManager.TryGetFullQuestBase(Quests.Great_Gourmand, out FullQuestBase quest) && quest.IsAccepted)
                {
                    fsm.SendEvent("INGREDIENT");
                }
            }
        });

        FsmState giveIngredientState = fsm.MustGetState("Give Ingredient?");
        giveIngredientState.ReplaceFirstActionOfType<CollectableItemCollect>(new LambdaAction
        {
            Method = GiveAll
        });
    }

    public override bool SupportsCost => false;
}
