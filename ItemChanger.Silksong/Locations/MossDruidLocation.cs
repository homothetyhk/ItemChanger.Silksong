using ItemChanger.Costs;
using ItemChanger.Locations;
using ItemChanger.Enums;
using ItemChanger.Placements;
using ItemChanger.Silksong.Modules;
using ItemChanger.Silksong.Modules.YNBox;
using ItemChanger.Silksong.Util;
using HutongGames.PlayMaker;
using Silksong.FsmUtil;
using ItemChanger.Items;

namespace ItemChanger.Silksong.Locations;

public abstract class MossDruidLocation : AutoLocation
{
    public required int PreviewIndex { get; init; }

    protected override void DoLoad()
    {
        Using(new FsmEditGroup()
        {
            {new(UnsafeSceneName, "Moss Creep NPC", "Conversation Control"), HookDruidWithRefreshedItems},
        });
        if (SupportsCost)
        {
            ActiveProfile!.Modules.GetOrAdd<MossDruidPreviewModule>().Add(PreviewIndex, Placement!);
        }
    }

    protected override void DoUnload() {}

    public override bool SupportsCost => true;

    public override GiveInfo GetGiveInfo()
    {
        GiveInfo gi = base.GetGiveInfo();
        gi.MessageType = MessageType.Any;
        return gi;
    }

    private void HookDruidWithRefreshedItems(PlayMakerFSM fsm)
    {
        // each location should give its refreshed items in a different state, to ensure give operations run synchronously
        FsmState refreshedItems = fsm.AddState($"IC Give Refreshed Items - {Name}");
        FsmState turn = fsm.MustGetState("Turn");
        refreshedItems.AddTransition("FINISHED", turn.Transitions[0].ToState);
        turn.ChangeTransition("FINISHED", refreshedItems.Name);
        
        refreshedItems.InsertLambdaMethod(0, (finish) =>
        {
            Placement!.GiveSome(Placement!.Items.Where(it => !it.IsObtained() && it.WasEverObtained()), GetGiveInfo(), finish);
        });

        HookDruid(fsm);
    }

    // Correct as long as items are given using GiveAll.
    protected bool Checked() => Placement!.CheckVisitedAny(VisitState.ObtainedAnyItem);

    protected void PromptCost(PlayMakerFSM fsm, string acceptEvent, string declineEvent)
    {
        Cost? cost = ((ISingleCostPlacement)Placement!).Cost;
        if (cost == null)
        {
            fsm.SendEvent(acceptEvent);
            return;
        }
        CustomYNEnableModule.Open(
            () => fsm.SendEvent(acceptEvent),
            () => fsm.SendEvent(declineEvent),
            cost,
            Placement!.GetUIName());
    }

    protected abstract void HookDruid(PlayMakerFSM fsm);
}