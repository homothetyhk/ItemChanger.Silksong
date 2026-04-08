using HutongGames.PlayMaker.Actions;
using MonoDetour.HookGen;

[assembly: MonoDetourTargets(typeof(GameManager), GenerateControlFlowVariants = true)]
[assembly: MonoDetourTargets(typeof(HeroController), GenerateControlFlowVariants = true)]
[assembly: MonoDetourTargets(typeof(InventoryItemConditional))]
[assembly: MonoDetourTargets(typeof(ListenForTauntV2), GenerateControlFlowVariants = true)]
[assembly: MonoDetourTargets(typeof(BasicNPCBase), GenerateControlFlowVariants = true)]
