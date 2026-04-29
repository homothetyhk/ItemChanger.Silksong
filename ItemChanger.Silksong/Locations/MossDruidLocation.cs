using ItemChanger.Costs;
using ItemChanger.Locations;
using ItemChanger.Enums;
using ItemChanger.Placements;
using ItemChanger.Silksong.Modules;
using ItemChanger.Silksong.Util;
using HutongGames.PlayMaker;
using Silksong.FsmUtil;

namespace ItemChanger.Silksong.Locations;

public abstract class MossDruidLocation : AutoLocation
{
    protected override void DoLoad()
    {
        Using(new FsmEditGroup()
        {
            {new(UnsafeSceneName, "Moss Creep NPC", "Conversation Control"), HookDruidWithRefreshedItems},
        });
        if (SupportsCost)
        {
            ActiveProfile!.Modules.GetOrAdd<MossDruidPreviewModule>().Add(Placement!);
        }
    }

    protected override void DoUnload() {}

    public override bool SupportsCost => true;

    private void HookDruidWithRefreshedItems(PlayMakerFSM fsm)
    {
        FsmState choiceState = fsm.MustGetState("Choice");
        choiceState.InsertLambdaMethod(0, (finish) =>
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
        CostDialogue.Prompt(
            cost,
            Placement!.GetUIName(),
            () => fsm.SendEvent(acceptEvent),
            () => fsm.SendEvent(declineEvent));
    }

    protected abstract void HookDruid(PlayMakerFSM fsm);
}