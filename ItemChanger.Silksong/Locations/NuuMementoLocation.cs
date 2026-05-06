using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Locations;
using ItemChanger.Silksong.Modules;
using Silksong.FsmUtil;
using Silksong.FsmUtil.Actions;

namespace ItemChanger.Silksong.Locations;

public class NuuMementoLocation : AutoLocation
{
    /// <summary>
    /// Number of boss kills required for obtaining this location
    /// </summary>
    public required int RequiredBossKills { get; init; }

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
        // Replace journal completion check
        FsmState completionEvaluateState = fsm.MustGetState("Completion Evaluate");
        completionEvaluateState.ReplaceAction(2, new LambdaAction
        {
            Method = () =>
            {
                if (ActiveProfile!.Modules.GetOrAdd<BossKillsCounterModule>().BossKillCount >= RequiredBossKills)
                    fsm.SendEvent("COMPLETED ALL");
            }
        });

        // Give the placement
        FsmState giveMementoState = fsm.MustGetState("Give Memento");
        giveMementoState.RemoveFirstActionOfType<CollectableItemCollect>();
        giveMementoState.InsertLambdaMethod(3, GiveAll);
        
        // Prevent needing a room reload between obtaining/completing quest and obtaining memento
        fsm.MustGetState("Talk Journal Give").RemoveFirstActionOfType<SetBoolValue>();
        fsm.MustGetState("Talk Journal Given").RemoveFirstActionOfType<SetBoolValue>();
        fsm.MustGetState("Quest Completed").RemoveFirstActionOfType<SetBoolValue>();
    }
}