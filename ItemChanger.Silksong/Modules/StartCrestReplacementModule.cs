using ItemChanger.Modules;
using MonoMod.RuntimeDetour;
using MonoMod.Cil;
using Mono.Cecil;
using Mono.Cecil.Cil;
using System.Reflection;
using ItemChanger.Silksong.Items;
using ItemChanger.Silksong.Extensions;

namespace ItemChanger.Silksong.Modules;

/// <summary>
/// Module for use when starting the game with an alternative crest.
/// It removes the Hunter Crest from the start inventory and changes the Act 3 start sequence
/// to equip the alternative starting crest.
/// 
/// It does not by itself give or equip the alternative crest at the start of the game;
/// the <see cref="StartCrestExtensions.ApplyStartCrest"/> method should be used instead.
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

public static class StartCrestExtensions
{
    /// <summary>
    /// Set the given crest to be the starting crest, by adding an instance of this module as
    /// well as using StartLocation and PlayerDataEditModule (editing the CurrentCrestID field).
    /// </summary>
    /// <param name="profile">The active profile.</param>
    /// <param name="crestName">The name of the crest. This should be the item name, not the crest ID.</param>
    public static void ApplyStartCrest(this ItemChangerProfile profile, string crestName)
    {
        if (SilksongHost.Instance.Finder.GetItem(crestName) is not ItemChangerSavedItem crestItem)
        {
            throw new ArgumentException($"Crest name {crestName} couldn't be resolved to an item!");
        }
        if (crestItem.Item.Type != Serialization.BaseGameSavedItem.ItemType.ToolCrest)
        {
            throw new ArgumentException($"Crest name {crestName} couldn't be resolved to a crest item!");
        }
        string crestId = crestItem.Item.Id;

        profile.Modules.Add(new StartCrestReplacementModule() { ReplacementCrestID = crestId });
        profile.Modules.GetOrAdd<PlayerDataEditModule>().PDEdits.Enqueue(
            new(nameof(PlayerData.CurrentCrestID), crestId)
            );
        profile.AddToStart(crestItem);
    }
}