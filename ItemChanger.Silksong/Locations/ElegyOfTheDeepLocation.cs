using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Locations;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Serialization;
using Silksong.FsmUtil;
using Silksong.FsmUtil.Actions;

namespace ItemChanger.Silksong.Locations;

public class ElegyOfTheDeepLocation : AutoLocation
{
    /// <summary>
    /// Conditions for this placement to be available. Defaults to the player having visited the Abyss and
    /// obtained the Needolin.
    /// </summary>
    public IValueProvider<bool> LocationPreconditions { get; init; } = new Conjunction(
        new PDBool(nameof(PlayerData.visitedAbyss)),
        new PDBool(nameof(PlayerData.hasNeedolin))
    );

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
        int getMelodyIndex = dialogTreeState.IndexFirstActionMatching(action =>
            action is BoolTestMulti boolTestAction
            && boolTestAction.trueEvent.Name == "GET MELODY");
        dialogTreeState.Actions[getMelodyIndex] = new LambdaAction()
        {
            Method = () =>
            {
                if (LocationPreconditions.Value && !Placement!.AllObtained())
                    fsm.SendEvent("GET MELODY");
            }
        };
        dialogTreeState.AddLambdaMethod(_ => fsm.SendEvent("FINISHED"));

        // Skip the Elegy popup, grant the placement whilst the screen is black.
        FsmState popupState = fsm.MustGetState("Get Item Msg");
        popupState.Actions = [];
        popupState.InsertLambdaMethod(0, _ => GiveAll(() => fsm.SendEvent("GET ITEM MSG END")));

        // Avoid granting Elegy.
        FsmState updateQuestState = fsm.MustGetState("Update Quest");
        updateQuestState.GetFirstActionOfType<SetPlayerDataVariable>()!.Active = false;
    }
}