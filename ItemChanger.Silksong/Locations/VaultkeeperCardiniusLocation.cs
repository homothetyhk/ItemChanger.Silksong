using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Extensions;
using ItemChanger.Placements;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.Serialization;
using PrepatcherPlugin;
using QuestPlaymakerActions;
using Silksong.FsmUtil;
using UnityEngine;

namespace ItemChanger.Silksong.Locations;

public class VaultkeeperCardiniusLocation : ThreefoldMelodyLocation
{
    /// <summary>
    /// Dialog to be displayed if the Sacred Cylinder is played while Hornet does not have the Needolin.
    /// </summary>
    public LanguageString? NoNeedolinHint { get; init; } = ItemChangerLanguageStrings.LIBRARIAN_NO_NEEDOLIN_HINT();
    protected override void DoLoad()
    {
        Using(new FsmEditGroup()
        {
            {new(UnsafeSceneName, "Librarian", "Dialogue"), HookVaultkeeper},
        });
    }

    protected override void DoUnload()
    {
        if (sacredCylinder != null) sacredCylinder.eventConditionItem = sacredCylinderTrueEventConditionItem!;
    }

    private void HookVaultkeeper(PlayMakerFSM fsm)
    {
        FsmState? melodyStartState = fsm.GetState("Needolin Pre");
        // Annoyingly, there are three "Librarian" objects with "Dialogue" FSMs in this scene
        if (melodyStartState == null) return;

        // When the player plays the Sacred Cylinder from the Gramaphone [sic] menu,
        // if it's event condition is met, it will send out an event to register,
        // which this FSM listens for to start the Melody learning sequence
        // This means the check for if the player already has the Vaultkeeper's Melody is baked into the Sacred Cylinder itself
        // so we need to grab a reference to it, then edit it to check the placement instead of the VK Melody quest
        // TODO: find a safer way to effectively replace the hasMelodyLibrarian check with a placement check
        sacredCylinder =
            (CollectableRelic)fsm.MustGetState("Given First?").GetFirstActionOfType<CollectableRelicCheck>()!.Relic.value;

        // Save the old eventCondtionItem so we can put it back later in DoUnload
        sacredCylinderTrueEventConditionItem = sacredCylinder.eventConditionItem;
        // Replace it with a dummy item that calls Placement.AllObtained for its CanGetMore function
        sacredCylinder.eventConditionItem = ScriptableObject.CreateInstance<FakeSavedItem>();
        ((FakeSavedItem)sacredCylinder.eventConditionItem).placement = Placement!;

        // The vanilla game doesn't enforce a Needolin requirement for this cutscene, so we need to make one ourselves
        // Cardinius already says some dialogue before the Needolin prompt, so follow that up with custom dialogue about
        // not having Needolin and abort the sequence early
        // Generally copied from Elegy location hint. May be worth adding a utility function somewhere to automate this?
        FsmState needolinHintCheck = fsm.AddState("Needolin Hint?");
        FsmState needolinHint = fsm.AddState("Needolin Hint");
        melodyStartState.ChangeTransition("CONVO_END", "Needolin Hint?");
        needolinHintCheck.AddTransition("HINT", needolinHint.Name);
        needolinHintCheck.AddTransition("FINISHED", "Needolin");
        needolinHintCheck.AddMethod(() =>
        {
            if (!PlayerDataAccess.hasNeedolin && NoNeedolinHint != null)
            {
                fsm.SendEvent("HINT");
            }
        });
        FsmStateAction endMelodySequence = new SetBoolValue() {
            boolValue = false, boolVariable = fsm.GetBoolVariable("In Melody Sequence")
        };
        needolinHintCheck.AddAction(endMelodySequence);
        needolinHint.AddRunDialogueAction(NoNeedolinHint!);
        needolinHint.AddAction(endMelodySequence);
        needolinHint.AddTransition("CONVO_END", "Dlg End");

        FsmState learnMelodyState = fsm.MustGetState("Needolin");
        Transform gramophone = learnMelodyState.GetFirstActionOfType<HeroTurnToFace>()!
            .Target.gameObject.value.FindChild("Hit Response")!.transform;
        ReplaceMelody(learnMelodyState.GetFirstActionOfType<RunFSM>()!, gramophone);

        // Remove quest update notification (unless it's a Dearest)
        if (Placement?.Items.Any(i => i.Name == ItemNames.Vaultkeeper_s_Melody) != true)
        {
            fsm.MustGetState("Dlg End").RemoveFirstActionOfType<ShowQuestUpdatedStandalone>();
        }
    }

    private CollectableRelic? sacredCylinder;
    private SavedItem? sacredCylinderTrueEventConditionItem;
    
    /// <summary>
    /// A stub implementation of SavedItem that uses Placement.AllObtained() to determine CanGetMore
    /// </summary>
    private class FakeSavedItem : SavedItem
    {
        public Placement placement;
        public override void Get(bool showPopup = true)
        {
            throw new NotImplementedException();
        }

        public override bool CanGetMore()
        {
            return !placement.AllObtained();
        }
    }
}


