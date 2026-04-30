using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Costs;
using ItemChanger.Enums;
using ItemChanger.Locations;
using ItemChanger.Placements;
using ItemChanger.Silksong.Costs;
using ItemChanger.Silksong.Modules.YNBox;
using ItemChanger.Silksong.RawData;
using ItemChanger.Tags;
using QuestPlaymakerActions;
using Silksong.FsmUtil;

namespace ItemChanger.Silksong.Locations;

public class BenjinAndCrullSpinesLocation : AutoLocation
{
    public override bool SupportsCost => true;

    protected override void DoLoad()
    {
        Using(new FsmEditGroup()
        {
            { new(SceneName!, "Dust Traders", "Dialogue"), HookDustTraders }
        });
    }

    protected override void DoUnload()
    {
    }

    private void HookDustTraders(PlayMakerFSM fsm)
    {
        // Reroute to Pins dialogue tree if items are yet to be obtained
        FsmState dialogTreeCheckState = fsm.MustGetState("State?");
        dialogTreeCheckState.RemoveFirstActionOfType<CheckQuestStateV2>();
        dialogTreeCheckState.InsertLambdaMethod(1, _ =>
        {
            FullQuestBase quest = QuestManager.GetQuest(Quests.Doctor_Curse_Cure);
            if (!quest.IsAccepted)
                return;

            fsm.SendEvent(quest.IsCompleted ? "FINISHED" : "PINS");
        });

        // Overwrite "has pins?" check
        // - Skip paying the cost if it has been paid previously but items are still available
        FsmState checkPinsState = fsm.MustGetState("Pins State?");
        checkPinsState.RemoveFirstActionOfType<CollectableItemGetDataV3>();
        checkPinsState.AddTransition("SKIP COST", "Give Pins");
        checkPinsState.InsertLambdaMethod(0, _ =>
        {
            if (Placement!.AllObtained())
                fsm.SendEvent("CANCEL");
            else if (Placement!.CheckVisitedAny(VisitState.ObtainedAnyItem)) // Has obtained some item previously
                fsm.SendEvent("SKIP COST");
        });

        // Override yes/no box
        FsmState buyPinsState = fsm.MustGetState("Buy Pins?");
        buyPinsState.Actions = [];

        // Note: a `DefaultCostTag` on this location doesn't affect the placement's cost if it's part of a MultiLocation
        // Therefore, we add the cost manually
        Cost? spinesCost = (Placement as ISingleCostPlacement)!.Cost;
        if (spinesCost == null && GetTag<DefaultCostTag>(out var defaultCostTag))
        {
            spinesCost = defaultCostTag.Cost;
        }

        if (spinesCost == null)
            spinesCost = new RosaryCost(0);

        buyPinsState.AddLambdaMethod(_ =>
        {
            Placement!.AddVisitFlag(VisitState.Previewed);
            CustomYNEnableModule.Open(
                cost: spinesCost,
                text: Placement!.GetUIName(),
                yes: () => { fsm.SendEvent("TRUE"); },
                no: () => { fsm.SendEvent("FALSE"); });
        });

        // End dialogue early, otherwise dialogue shows over the top of big UI def
        FsmState givePinsState = fsm.MustGetState("Give Pins");
        givePinsState.AddAction(new EndDialogue()
        {
            ReturnControl = false,
            ReturnHUD = false,
            Target = new FsmOwnerDefault() { OwnerOption = OwnerDefaultOption.UseOwner },
            UseChildren = false
        });
        
        // Replace granting spines with obtaining the placement
        givePinsState.RemoveFirstActionOfType<SavedItemGet>();
        givePinsState.AddLambdaMethod(GiveAll);
    }
}