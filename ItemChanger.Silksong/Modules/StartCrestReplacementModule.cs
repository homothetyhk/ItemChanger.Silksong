using ItemChanger.Modules;
using MonoMod.RuntimeDetour;
using MonoMod.Cil;
using Mono.Cecil;
using Mono.Cecil.Cil;
using System.Reflection;

namespace ItemChanger.Silksong.Modules;

/// <summary>
/// Module for use when starting the game with an alternative crest.
/// It removes the Hunter Crest from the start inventory and changes the Act 3 start sequence
/// to equip the alternative starting crest.
/// 
/// It does not by itself give or equip the alternative crest at the start of the game;
/// that should be done using StartLocation and PlayerDataEditModule
/// (editing the CurrentCrestID field) respectively.
/// </summary>
[SingletonModule]
public class StartCrestReplacementModule : ItemChanger.Modules.Module
{
    /// <summary>
    /// The replacement crest's ID, as accepted by ToolItemManager.GetCrestByName.
    /// </summary>
    public required string ReplacementCrestID;

    protected override void DoLoad()
    {
        Host.LifecycleEvents.AfterStartNewGame += RemoveStartingCrest;
        Using(new ILHook(typeof(GameManager).GetMethod(
            nameof(GameManager.StartAct3),
            BindingFlags.NonPublic | BindingFlags.Instance
        ), EquipReplacementCrestInAct3));
    }

    protected override void DoUnload() {
        Host.LifecycleEvents.AfterStartNewGame -= RemoveStartingCrest;
    }

    private static void RemoveStartingCrest()
    {
        // PlayerData.SetupNewPlayerData gives and equips the Hunter Crest before we can do
        // anything about it; best we can do is override it here.
        PlayerData.instance.ToolEquips = new();
    }

    private void EquipReplacementCrestInAct3(ILContext il)
    {
        bool IsTargetInstruction(Instruction i)
        {
            return i.OpCode == OpCodes.Call
                && i.Operand is MethodReference mr
                && mr.Name == "get_HunterCrest3";
        }

        ILCursor cursor = new(il);
        cursor.GotoNext(IsTargetInstruction)
            .Remove()
            .EmitDelegate(GetStartingCrest);
        cursor.GotoNext(IsTargetInstruction)
            .Remove()
            .EmitDelegate(GetStartingCrest);
    }

    private ToolCrest GetStartingCrest() => ToolItemManager.GetCrestByName(ReplacementCrestID);
}