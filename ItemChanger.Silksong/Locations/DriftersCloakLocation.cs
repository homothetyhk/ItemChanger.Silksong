using Benchwarp.Data;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Locations;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.RawData;
using Silksong.FsmUtil;
using System.Diagnostics.CodeAnalysis;

namespace ItemChanger.Silksong.Locations;

public class DriftersCloakLocation : AutoLocation
{
    [SetsRequiredMembers]
    public DriftersCloakLocation()
    {
        SceneName = SceneNames.Bone_East_Umbrella;
        Name = LocationNames.Drifter_s_Cloak;
    }

    protected override void DoLoad()
    {
        this.InjectPreviewText(new("Quests", "QUEST_BROLLY_GET_DESC"), ItemChangerLanguageStrings.QUEST_BROLLY_GET_DESC_PREVIEW);
        this.InjectPreviewText(new("Wilds", "SEAMSTRESS_BROLLY_QUEST_OFFER"), ItemChangerLanguageStrings.SEAMSTRESS_BROLLY_QUEST_OFFER_PREVIEW);
        this.InjectPreviewText(new("Wilds", "SEAMSTRESS_BROLLY_QUEST_REOFFER"), ItemChangerLanguageStrings.SEAMSTRESS_BROLLY_QUEST_REOFFER_PREVIEW);
        Using(new FsmEditGroup() {{ new(SceneName!, "Seamstress", "Dialogue"), ModifyFsm }});
    }

    protected override void DoUnload() { }

    private void ModifyFsm(PlayMakerFSM fsm)
    {
        fsm.MustGetState("Offer Quest").InsertMethod(0, _ => Placement?.AddVisitFlag(Enums.VisitState.Previewed));
        fsm.MustGetState("Reoffer Quest").InsertMethod(0, _ => Placement?.AddVisitFlag(Enums.VisitState.Previewed));

        // Note: It's possible for the player to brick their save if they somehow trigger a save after completing the quest but before receiving the reward.
        // This should not generally be possible without debug tools.
        var giveState = fsm.MustGetState("Msg");
        giveState.GetFirstActionOfType<CreateUIMsgGetItem>()!.enabled = false;
        giveState.GetFirstActionOfType<SetFsmString>()!.enabled = false;
        giveState.GetFirstActionOfType<SetPlayerDataVariable>()!.enabled = false;
        giveState.AddMethod(_ =>
        {
            var obj = GameObject.Find("_NPCs/Seamstress");
            Placement?.GiveAll(new()
            {
                FlingType = Enums.FlingType.Everywhere,
                MessageType = Enums.MessageType.SmallPopup | Enums.MessageType.LargePopup,
                Transform = obj != null ? obj.transform : null,
            },
            callback: () => fsm.SendEvent("GET ITEM MSG END"));
        });

        // Skip the cloak anim unless it's a dearest.
        bool Dearest() => Placement?.Items.Any(i => i.Name == ItemNames.Drifter_s_Cloak) ?? false;

        var skipEvent = FsmEvent.GetFsmEvent("SKIP");
        var fadeUpState = fsm.MustGetState("Fade Up");
        fadeUpState.AddTransition("SKIP", "To Idle");
        fadeUpState.InsertMethod(0, _ =>
        {
            if (Dearest()) return;
            
            fadeUpState.GetFirstActionOfType<Tk2dPlayAnimationWithEvents>()?.enabled = false;
            fadeUpState.GetFirstActionOfType<Wait>()?.finishEvent = skipEvent;
        });
    }
}
