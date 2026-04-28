using Benchwarp.Data;
using HarmonyLib;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Enums;
using ItemChanger.Locations;
using ItemChanger.Silksong.Modules.FastTravel;
using MonoMod.RuntimeDetour;
using PrepatcherPlugin;
using Silksong.FsmUtil;

namespace ItemChanger.Silksong.Locations;

public class BeastlingCallLocation : AutoLocation
{
    protected override void DoLoad()
    {
        Using(new FsmEditGroup()
        {
            { new(SceneName!, "Bell Beast DefeatedCentipede NPC", "Control"), HookBellEaterDefeat },
        });
    }

    protected override void DoUnload()
    {
    }

    private void HookBellEaterDefeat(PlayMakerFSM fsm)
    {
        // Remove Beastling Call UI popup
        FsmState pickupMessageState = fsm.MustGetState("Get Item Msg");
        pickupMessageState.RemoveFirstActionOfType<CreateUIMsgGetItem>();

        // Give placement - given slightly early so that UI popup doesn't get eaten by the scene transition
        pickupMessageState.InsertMethod(3, _ => GiveAll(() => fsm.SendEvent("GET ITEM MSG END")));

        // Remove granting Beastling Call
        FsmState timePassesState = fsm.MustGetState("Time Passes");
        timePassesState.RemoveFirstActionOfType<SetPlayerDataVariable>();
    }
}