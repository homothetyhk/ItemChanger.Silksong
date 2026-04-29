using Benchwarp.Data;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Locations;
using Silksong.FsmUtil;

namespace ItemChanger.Silksong.Locations;

public class WidowLocation : AutoLocation
{
    protected override void DoLoad()
    {
        Using(new FsmEditGroup()
        {
            { new (SceneName!, "Spinner Boss", "Control"), HookWidow },
        });
    }
    
    protected override void DoUnload() { }

    private void HookWidow(PlayMakerFSM fsm)
    {
        // Remove granting the Needolin ability
        FsmState finalBindBurstState = fsm.MustGetState("Final Bind Burst");
        finalBindBurstState.RemoveFirstActionMatching(
            (action) => 
                action is HutongGames.PlayMaker.Actions.SetPlayerDataBool setPDAction
            && setPDAction.boolName.Value == "hasNeedolin");
        
        // Instead, grant the item.
        // Item is granted slightly later, when the screen has faded completely to black
        // so we can show a big UIDef if necessary
        FsmState fadeToBlackState = fsm.MustGetState("Fade To Black");
        fadeToBlackState.AddLambdaMethod(GiveAll);
        
        // Remove an unnecessarily long wait time
        fadeToBlackState.RemoveLastActionOfType<Wait>();
        
        // Remove the Needolin popup
        FsmState getNeedolinState = fsm.MustGetState("Get Needolin");
        getNeedolinState.RemoveLastActionOfType<SpawnPowerUpGetMsg>();
        
        // Avoid going to the memory scene entirely
        // It would be a softlock without movement + Needolin
        FsmState toMemorySceneState = fsm.MustGetState("To Memory Scene");
        toMemorySceneState.RemoveLastActionOfType<BeginSceneTransition>();
        
        // Instead, reload the room as if Hornet has just been through the memory sequence
        toMemorySceneState.AddAction(new BeginSceneTransition()
        {
            sceneName = SceneNames.Belltown_Shrine,
            entryGateName = "door_wakeOnGround",
            entryDelay = 0f,
            visualization = GameManager.SceneLoadVisualizations.ThreadMemory,
            preventCameraFadeOut = true,
        });
        
        // Re-enable inventory access - it is disabled during the Widow death sequence
        toMemorySceneState.AddAction(new HutongGames.PlayMaker.Actions.SetPlayerDataBool()
        {
            boolName = "disableInventory",
            value = false,
        });
    }
}