using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Locations;
using Silksong.FsmUtil;
using Silksong.FsmUtil.Actions;

namespace ItemChanger.Silksong.Locations;

public class NuuMememtoLocation : AutoLocation
{
    protected override void DoLoad()
    {
        Using(new FsmEditGroup()
        {
            { new(UnsafeSceneName, "Nuu", "Dialogue"), HookGiveMemento }
        });
    }

    protected override void DoUnload()
    {
    }

    private void HookGiveMemento(PlayMakerFSM fsm)
    {
        FsmState giveMementoState = fsm.MustGetState("Give Memento");
        giveMementoState.RemoveFirstActionOfType<CollectableItemCollect>();
        giveMementoState.InsertLambdaMethod(3, GiveAll);
    }
}