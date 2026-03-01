using Benchwarp.Data;
using HarmonyLib;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Modules;
using ItemChanger.Silksong.FsmStateActions;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using PrepatcherPlugin;
using Silksong.FsmUtil;

namespace ItemChanger.Silksong.Modules.FastTravel;

/// <summary>
/// Module that enables transit from the marrow prior to defeating Bell Beast.
/// </summary>
[SingletonModule]
public class MarrowBellwayTransitModule : Module
{
    private ILHook? _salcHook;
    private FsmEditGroup? _fsmEdits;

    protected override void DoLoad()
    {
        _salcHook = new(
            AccessTools.Method(typeof(SceneAdditiveLoadConditional), nameof(SceneAdditiveLoadConditional.TryTestLoad)),
            ModifySalcPDBoolTest
            );

        _fsmEdits = new()
        {
            { new(SilksongHost.Wildcard, "Beast","Beast Anim"), HideVineBeast },
            { new(SilksongHost.Wildcard, "Bone Beast NPC", "Interaction"), EnterWake }
        };
    }

    private void HideVineBeast(PlayMakerFSM self)
    {
        if (!self.gameObject.scene.name.StartsWith(SceneNames.Bone_05))
        {
            return;
        }

        self.GetState("Idle")!.InsertMethod(0, static a =>
        {
            if (GameManager.instance.entryGateName == PrimitiveGateNames.right1 || GameManager.instance.entryGateName == PrimitiveGateNames.door_fastTravelExit)
            {
                UObject.Destroy(a.Fsm.FsmComponent.gameObject);
            }
        });
    }

    private void EnterWake(PlayMakerFSM self)
    {
        if (!self.gameObject.scene.name.StartsWith(SceneNames.Bone_05))
        {
            return;
        }

        self.GetState("Start State")!.ReplaceActionsOfType<PlayerDataVariableTest>(pdvt =>
        {
            if (pdvt.VariableName.Value == nameof(PlayerData.IsTeleporting))
            {
                return new CustomCheckFsmStateAction(pdvt)
                {
                    // If the bell beast is present, then consider our appearance as being from a teleport
                    GetIsTrue = () => PlayerDataAccess.IsTeleporting || !PlayerDataAccess.defeatedBellBeast
                };
            }

            return pdvt;
        });
    }

    private void ModifySalcPDBoolTest(ILContext il)
    {
        ILCursor cursor = new(il);

        cursor.GotoNext(MoveType.After, i => i.MatchCallOrCallvirt<PlayerDataTest>($"get_{nameof(PlayerDataTest.IsFulfilled)}"));
        cursor.Emit(OpCodes.Ldarg_0);
        cursor.EmitDelegate<Func<bool, SceneAdditiveLoadConditional, bool>>((origValue, self) =>
        {
            if (self.gameObject.scene.name != SceneNames.Bone_05 || self.sceneNameToLoad != "Bone_05_bellway")  // TODO - add to benchwarp
            {
                return origValue;
            }

            if (GameManager.instance.entryGateName == PrimitiveGateNames.right1 || GameManager.instance.entryGateName == PrimitiveGateNames.door_fastTravelExit)
            {
                return true;
            }

            return origValue;
        });
    }

    protected override void DoUnload()
    {
        _salcHook?.Dispose();
        _salcHook = null;
        _fsmEdits?.Dispose();
        _fsmEdits = null;
    }
}
