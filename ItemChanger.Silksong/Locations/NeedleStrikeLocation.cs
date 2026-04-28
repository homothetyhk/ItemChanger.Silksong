using HutongGames.PlayMaker.Actions;
using ItemChanger.Locations;
using ItemChanger.Silksong.RawData;
using Silksong.FsmUtil;

namespace ItemChanger.Silksong.Locations;

public class NeedleStrikeLocation : AutoLocation
{
    protected override void DoLoad()
    {
        this.InjectPreviewText(new("Coral", "PINSTRESS_INTERIOR_GROUND_MEET"), ItemChangerLanguageStrings.PINSTRESS_INTERIOR_GROUND_MEET_PREVIEW);
        this.InjectPreviewText(new("Coral", "PINSTRESS_INTERIOR_GROUND_MEET_ACT_3"), ItemChangerLanguageStrings.PINSTRESS_INTERIOR_GROUND_MEET_ACT_3_PREVIEW);
        this.InjectPreviewText(new("Coral", "PINSTRESS_INTERIOR_GROUND_REOFFER"), ItemChangerLanguageStrings.PINSTRESS_INTERIOR_GROUND_REOFFER_PREVIEW);

        Using(new FsmEditGroup()
        {
            { new(SceneName!, "Pinstress States", "States"), ModifyPinstressStates },
            { new(SceneName!, "Pinstress Interior Ground Sit", "Behaviour"), ModifyPinstressDialogue }
        });
    }

    protected override void DoUnload() { }

    private bool ObtainedAny() => Placement?.Items.Any(i => i.WasEverObtained()) ?? false;

    private void ModifyPinstressStates(PlayMakerFSM fsm)
    {
        var checkState = fsm.MustGetState("Check");
        checkState.GetFirstActionOfType<PlayerDataVariableTest>()?.enabled = false;
        checkState.InsertMethod(0, _ =>
        {
            if (!ObtainedAny()) fsm.SendEvent("GROUND");
        });

        void PlaceRenewables(float x)
        {
            // If Pinstress has renewable items, place them on the ground.
            // She might still be in the hut, but she has no 'give' dialogue to inject them into after needle strike.
            //
            // This could in theory be implemented as a MultiLocation, but it would be ornery to encapsulate the relevant Test in a serializable manner,
            // since it partially depends on whether the location's items have been obtained yet or not.
            CoordinateLocation loc = new()
            {
                SceneName = SceneName,
                Name = Name,
                X = 30.5f,
                Y = 8f,
                Managed = false,
                Placement = Placement,
            };
            loc.GetContainer(UnityEngine.SceneManagement.SceneManager.GetActiveScene(), out var container, out var containerInfo);
            loc.PlaceContainer(container, containerInfo);
        }

        fsm.MustGetState("Gone").AddMethod(_ => PlaceRenewables(30.5f));
        fsm.MustGetState("Stand").AddMethod(_ => PlaceRenewables(33f));
        fsm.MustGetState("Table").AddMethod(_ => PlaceRenewables(30.5f));
    }

    private void ModifyPinstressDialogue(PlayMakerFSM fsm)
    {
        var metState = fsm.MustGetState("Met?");
        metState.GetFirstActionOfType<PlayerDataVariableAction>()?.enabled = false;
        metState.AddMethod(_ =>
        {
            if (!ObtainedAny()) fsm.SendEvent("REOFFER");
        });

        var msgState = fsm.MustGetState("Msg");
        msgState.GetFirstActionOfType<CreateUIMsgGetItem>()?.enabled = false;
        msgState.GetFirstActionOfType<SetFsmString>()?.enabled = false;
        msgState.AddMethod(_ =>
        {
            Placement!.GiveAll(new()
            {
                FlingType = Enums.FlingType.DirectDeposit,
                MessageType = Enums.MessageType.Any,
                Transform = fsm.gameObject.transform,
            },
            callback: () => fsm.SendEvent("GET ITEM MSG END"));
        });

        foreach (var action in fsm.MustGetState("Save").GetActionsOfType<SetPlayerDataVariable>())
        {
            if (action.VariableName.Value != "disablePause") action.enabled = false;
        }
    }
}