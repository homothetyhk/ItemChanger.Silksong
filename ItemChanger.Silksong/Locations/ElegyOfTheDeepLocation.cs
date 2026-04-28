using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Locations;
using Silksong.FsmUtil;

namespace ItemChanger.Silksong.Locations;

public class ElegyOfTheDeepLocation : AutoLocation
{
    protected override void DoLoad()
    {
        Using(new FsmEditGroup()
        {
            { new(SceneName!, "Snail Shamans Set", "Dialogue"), HookSnailShamans }
        });
    }

    protected override void DoUnload()
    {
    }

    private void HookSnailShamans(PlayMakerFSM fsm)
    {
        // Overwrite dialog tree conditions. Location is obtainable as long as
        // the Abyss has been visited.
        FsmState dialogTreeState = fsm.MustGetState("Talk?");
        dialogTreeState.RemoveFirstActionMatching(action =>
            action is BoolTestMulti boolTestAction
            && boolTestAction.trueEvent.Name == "GET MELODY");
        dialogTreeState.InsertLambdaMethod(6, _ =>
        {
            if (PlayerData.instance.visitedAbyss && !Placement!.AllObtained())
                fsm.SendEvent("GET MELODY");
        });

        // Skip the Elegy popup, grant the placement whilst the screen is black.
        FsmState popupState = fsm.MustGetState("Get Item Msg");
        popupState.InsertLambdaMethod(0, GiveAll);
        popupState.InsertLambdaMethod(1, _ =>
        {
            fsm.SendEvent("GET ITEM MSG END");
        });

        // Avoid granting Elegy.
        FsmState updateQuestState = fsm.MustGetState("Update Quest");
        updateQuestState.RemoveFirstActionOfType<SetPlayerDataVariable>();
    }
}