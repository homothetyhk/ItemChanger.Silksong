using ItemChanger.Locations;
using ItemChanger.Enums;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Silksong.FsmUtil;
using ItemChanger.Silksong.Extensions;
using Silksong.FsmUtil.Actions;
using ItemChanger.Items;

namespace ItemChanger.Silksong.Locations;

public abstract class ZylotolLocation : AutoLocation
{
    protected override void DoLoad()
    {
        LogInfo("DID LOAD. ADDING EDITS TO SCENE: " + UnsafeSceneName);
        Using(new FsmEditGroup()
        {
            {new(UnsafeSceneName, "Blue Scientist", "Behaviour"), HookZylotolWithRefreshedItems},
            {new(UnsafeSceneName, "Blue Scientist Sit", "Dialogue"), HookZylotolWithRefreshedItems},
        });
    }

    protected override void DoUnload() {}

    public override GiveInfo GetGiveInfo()
    {
        GiveInfo gi = base.GetGiveInfo();
        gi.MessageType = MessageType.Any;
        return gi;
    }

    private void HookZylotolWithRefreshedItems(PlayMakerFSM fsm)
    {
        // each location should give its refreshed items in a different state, to ensure give operations run synchronously
        FsmState refreshedItems = fsm.AddState($"IC Give Refreshed Items - {Name}");
        FsmState endState = fsm.GetState("End Dlg") ?? fsm.MustGetState("End");
        refreshedItems.AddTransition("FINISHED", endState.Transitions[0].ToState);
        endState.ChangeTransition("FINISHED", refreshedItems.Name);
        
        refreshedItems.InsertLambdaMethod(0, (finish) =>
        {
            Placement!.GiveSome(Placement!.Items.Where(it => !it.IsObtained() && it.WasEverObtained()), GetGiveInfo(), finish);
        });

        HookZylotol(fsm);
    }

    protected abstract void HookZylotol(PlayMakerFSM fsm);
}