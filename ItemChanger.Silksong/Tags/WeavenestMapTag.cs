using ItemChanger.Locations;
using ItemChanger.Silksong.Components;
using ItemChanger.Silksong.RawData;
using ItemChanger.Tags;
using ItemChanger.Tags.Constraints;
using Silksong.FsmUtil;
using Silksong.UnityHelper.Extensions;
using TeamCherry.Localization;

namespace ItemChanger.Silksong.Tags;

[LocationTag]
public class WeavenestMapTag : Tag
{
    public required string ObjectName;

    private Location? _location;

    private static int nextId = 0;
    private int id;

    private LocalisedString PREVIEW_ID => new("Inspect", $"ITEM_CHANGER_WEAVENEST_MAP_{id}");

    protected override void DoLoad(TaggableObject parent)
    {
        _location = (Location)parent;
        id = nextId++;

        _location.InjectPreviewText(PREVIEW_ID, ItemChangerLanguageStrings.TAKE_ITEM_PREVIEW());
        Using(new FsmEditGroup() { { new(_location.SceneName!, ObjectName, "Control"), ModifyTabletFsm } });
    }

    /// <summary>
    /// Weavenest tablets have extra visuals that toggle when the map is taken, tied to global events.
    /// We do FSM urgery so these are instead tied to the placement state only.
    /// </summary>
    private void ModifyTabletFsm(PlayMakerFSM fsm)
    {
        var activeState = fsm.MustGetState("Active?");
        fsm.RemoveGlobalTransition("MAP TAKEN");
        activeState.AddTransition("ALL OBTAINED", "Map Taken");
        activeState.InsertMethod(0, _ =>
        {
            if (_location!.Placement!.AllObtained()) fsm.SendEvent("ALL OBTAINED");
        });

        var readableState = fsm.MustGetState("Readable");
        fsm.RemoveGlobalTransition("MAP TAKE");
        readableState.AddTransition("ALL OBTAINED", "Map Take");
        readableState.AddMethod(() =>
        {
            if (_location!.Placement!.AllObtained()) fsm.SendEvent("ALL OBTAINED");
        }, everyFrame: true);

        // Make these sparkle like a lore tablet, but preserve the 'take item' interaction.
        var shiny = fsm.gameObject.FindChild("Get Map Inspect")!;
        shiny.AddComponent<ItemParticles>().items = _location!.Placement!.Items;

        // Modify dialogue to match.
        var interactEvents = shiny.GetComponent<InteractEvents>();
        interactEvents.inspectText = new();
        interactEvents.yesNoPrompt = PREVIEW_ID;
    }
}
