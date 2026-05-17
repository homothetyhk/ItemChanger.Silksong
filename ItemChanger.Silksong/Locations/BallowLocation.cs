using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Locations;
using ItemChanger.Silksong.Modules;
using Silksong.FsmUtil;

namespace ItemChanger.Silksong.Locations;

public class BallowLocation : AutoLocation
{
    protected override void DoLoad()
    {
        // Diving bell module required to stop Ballow from moving to repair the diving bell
        ActiveProfile!.Modules.GetOrAdd<DivingBellAlwaysAvailableModule>();

        Using(new FsmEditGroup()
        {
            { new(UnsafeSceneName, "Ballow Diving Bell NPC", "Dialogue"), HookBallow }
        });
    }

    protected override void DoUnload()
    {
    }

    private void HookBallow(PlayMakerFSM fsm)
    {
        // Always navigate to the dialogue tree which grants the key if the placement has items
        FsmState postFirstDiveState = fsm.MustGetState("Post Ver?");
        postFirstDiveState.InsertMethod(0, () =>
        {
            if (!Placement!.AllObtained())
            {
                fsm.SendEvent("FALSE");
            }
        });

        FsmState givenKeyState = fsm.MustGetState("Given Key?");
        givenKeyState.RemoveActionsOfType<PlayerDataVariableTest>();
        givenKeyState.InsertMethod(0, () => { fsm.SendEvent(Placement!.AllObtained() ? "TRUE" : "FALSE"); });

        // Replace granting the key with obtaining the placement
        FsmState giveKeyState = fsm.MustGetState("Give Key");
        giveKeyState.RemoveActionsOfType<CollectableItemCollect>();
        giveKeyState.InsertLambdaMethod(3, GiveAll);
    }
}