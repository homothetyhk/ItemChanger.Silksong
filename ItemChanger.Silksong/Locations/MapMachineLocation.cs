using HutongGames.PlayMaker.Actions;
using ItemChanger.Extensions;
using ItemChanger.Locations;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.RawData;
using Silksong.FsmUtil;
using TeamCherry.Localization;

namespace ItemChanger.Silksong.Locations;

public class MapMachineLocation : AutoLocation
{
    public required string ObjectName;

    // There are multiple map machines that all use the same language key, so we need to make unique proxies.
    private static int nextId = 0;
    private int id;
    private LocalisedString DialogueKey => new("Inspect", $"ITEMCHANGER_CITADEL_MAP_PROMPT_{id}");

    protected override void DoLoad()
    {
        id = nextId++;
        this.InjectPreviewText(DialogueKey, ItemChangerLanguageStrings.CITADEL_MAP_PROMPT_PREVIEW);
        Using(new FsmEditGroup() { { new(SceneName!, ObjectName, "Unlock Behaviour"), ModifyMapMachine } });
    }

    protected override void DoUnload() { }

    private void ModifyMapMachine(PlayMakerFSM fsm)
    {
        var inertState = fsm.MustGetState("Inert");
        inertState.GetFirstActionOfType<SavedItemCanGetMore>()?.enabled = false;
        inertState.AddMethod(_ =>
        {
            if (!Placement!.Items.AnyEverObtained()) return;

            this.SpawnItemsAtCoordinate(fsm.gameObject.transform.position);
            fsm.SendEvent("ACTIVATED");
        });

        var inspectState = fsm.MustGetState("Inspect");
        inspectState.InsertMethod(0, _ => Placement!.AddVisitFlag(Enums.VisitState.Previewed));
        var runDialogue = inspectState.GetFirstActionOfType<RunDialogue>()!;
        runDialogue.Sheet = DialogueKey.Sheet;
        runDialogue.Key = DialogueKey.Key;

        var giveState = fsm.MustGetState("Get Item");
        giveState.GetFirstActionOfType<SavedItemGet>()?.enabled = false;
        giveState.AddLambdaMethod(GiveAll);
    }
}
