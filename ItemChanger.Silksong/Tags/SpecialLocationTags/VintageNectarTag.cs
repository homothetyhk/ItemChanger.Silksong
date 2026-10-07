using Benchwarp.Data;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Placements;
using ItemChanger.Silksong.RawData;
using ItemChanger.Tags;
using ItemChanger.Tags.Constraints;
using QuestPlaymakerActions;
using Silksong.FsmUtil;

namespace ItemChanger.Silksong.Tags.SpecialLocationTags;

[LocationTag]
public class VintageNectarTag : Tag
{
    protected override void DoLoad(TaggableObject parent) => Using(new FsmEditGroup { { new(SceneNames.Halfway_01, "HH Bartender", "Dialogue"), ModifyCreigeDialogue } });

    private void ModifyCreigeDialogue(PlayMakerFSM fsm)
    {
        FsmState nectar = fsm.MustGetState("Nectar?");

        // Disable all checks related to obtaining or giving the nectar away; player may have obtained it elsewhere.
        // TODO: May require consolidation with https://github.com/homothetyhk/ItemChanger.Silksong/pull/212.
        nectar.GetActionsOfType<CheckQuestState>().FirstOrDefault(c => c.Quest.Value.name == Quests.Great_Gourmand)?.CompletedEvent = FsmEvent.GetFsmEvent("");
        nectar.GetFirstActionOfType<CollectableItemGetData>()?.enabled = false;
        nectar.GetActionsOfType<PlayerDataVariableTest>().FirstOrDefault(p => p.VariableName.Value == nameof(PlayerData.GourmandGivenNectar))?.enabled = false;
        nectar.InsertMethod(_ =>
        {
            // Insert our own check for the placement being checked.
            if (SilksongHost.Instance.ActiveProfile?.TryGetPlacement(LocationNames.Vintage_Nectar, out Placement? pmt) is true && pmt.CheckVisitedAny(Enums.VisitState.ObtainedAnyItem))
                fsm.SendEvent("CANCEL");
        }, 5);
    }
}
