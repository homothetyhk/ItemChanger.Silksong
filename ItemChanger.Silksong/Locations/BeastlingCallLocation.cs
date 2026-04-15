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
        Using(new Hook(
            AccessTools.PropertyGetter(typeof(PlayerData), nameof(PlayerData.BellCentipedeLocked)),
            BellCentipedeLocked
        ));
        Using(new Hook(
            AccessTools.PropertyGetter(typeof(PlayerData), nameof(PlayerData.BellCentipedeLocked)),
            BellCentipedeWaiting
        ));
        Using(new FsmEditGroup()
        {
            { new(SceneName!, "Bell Beast DefeatedCentipede NPC", "Control"), HookBeast },
            {
                new(SceneNames.Bellway_Centipede_additive, "Bell Centipede Bellway Scene", "Control"), HookHoleInBellway
            },
        });
    }

    protected override void DoUnload()
    {
    }

    private void HookBeast(PlayMakerFSM fsm)
    {
        // Remove Beastling Call UI popup
        FsmState pickupMessageState = fsm.MustGetState("Get Item Msg");
        pickupMessageState.RemoveFirstActionOfType<CreateUIMsgGetItem>();

        // Give placement - given slightly early so that UI doesn't get eaten by the scene transition
        pickupMessageState.InsertLambdaMethod(3, GiveAll);
        pickupMessageState.AddLambdaMethod(_ =>
        {
            // Remove Bell Eater once the placement is obtained
            BellwayAutoUnlockModule module =
                ItemChangerHost.Singleton.ActiveProfile!.Modules.GetOrAdd<BellwayAutoUnlockModule>();
            module.BypassCentipede = true;

            // This event is normally sent when Beastling Call UI is closed
            fsm.SendEvent("GET ITEM MSG END");
        });

        // Remove granting Beastling Call
        FsmState timePassesState = fsm.MustGetState("Time Passes");
        timePassesState.RemoveFirstActionOfType<SetPlayerDataVariable>();
    }

    private void HookHoleInBellway(PlayMakerFSM fsm)
    {
        FsmState state = fsm.MustGetState("State");
        state.Actions = [];
        state.AddLambdaMethod(_ =>
        {
            if (Placement!.CheckVisitedAny(VisitState.ObtainedAnyItem))
            {
                fsm.SendEvent("FINISHED");
                return;
            }

            if (PlayerDataAccess.bellCentipedeAppeared)
            {
                fsm.SendEvent("APPEARED");
                return;
            }

            if (PlayerDataAccess.blackThreadWorld)
            {
                fsm.SendEvent("READY TO APPEAR");
                return;
            }
        });
    }

    /// <summary>
    /// Overwritten so that Bell Beast being available is not tied to obtaining Beastling Call
    /// </summary>
    private bool BellCentipedeLocked(Func<PlayerData, bool> orig, PlayerData self)
    {
        LogInfo("BellCentipedeLocked getter called");
        if (!self.bellCentipedeAppeared)
        {
            LogInfo($"Locked: Returning False - Would have returned {orig(self)}");
            return false;
        }

        LogInfo(
            $"Locked2: Returning {!Placement!.CheckVisitedAny(VisitState.ObtainedAnyItem)} - Would have returned {orig(self)}");
        return !Placement!.CheckVisitedAny(VisitState.ObtainedAnyItem);
    }

    /// <summary>
    /// Overwritten so that Bell Beast being available is not tied to obtaining Beastling Call
    /// </summary>
    private bool BellCentipedeWaiting(Func<PlayerData, bool> orig, PlayerData self)
    {
        LogInfo("BellCentipedeWaiting getter called");
        if (!self.blackThreadWorld)
        {
            LogInfo($"Waiting: Returning False - Would have returned {orig(self)}");
            return false;
        }

        LogInfo(
            $"Waiting2: Returning {Placement!.CheckVisitedAny(VisitState.ObtainedAnyItem)} - Would have returned {orig(self)}");
        return Placement!.CheckVisitedAny(VisitState.ObtainedAnyItem);
    }
}