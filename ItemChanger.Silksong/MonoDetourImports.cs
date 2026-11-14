using HutongGames.PlayMaker.Actions;
using MonoDetour.HookGen;
using QuestPlaymakerActions;
using TeamCherry.Localization;

[assembly: MonoDetourTargets(typeof(GameManager), GenerateControlFlowVariants = true)]
[assembly: MonoDetourTargets(typeof(HeroController), GenerateControlFlowVariants = true)]
[assembly: MonoDetourTargets(typeof(InventoryItemConditional))]
[assembly: MonoDetourTargets(typeof(ListenForTauntV2), GenerateControlFlowVariants = true)]
[assembly: MonoDetourTargets(typeof(BasicNPCBase), GenerateControlFlowVariants = true)]
[assembly: MonoDetourTargets(typeof(Language), GenerateControlFlowVariants = true)]
[assembly: MonoDetourTargets(typeof(InteractEvents), GenerateControlFlowVariants = true)]
[assembly: MonoDetourTargets(typeof(SavedItemDisplay), GenerateControlFlowVariants = true)]
[assembly: MonoDetourTargets(typeof(CollectableItemPickup), GenerateControlFlowVariants = true)]
[assembly: MonoDetourTargets(typeof(UIMsgProxy), GenerateControlFlowVariants = true)]
[assembly: MonoDetourTargets(typeof(ToolCrestUIMsg))]
[assembly: MonoDetourTargets(typeof(NPCControlBase), GenerateControlFlowVariants = true)]
[assembly: MonoDetourTargets(typeof(SceneAdditiveLoadConditional), GenerateControlFlowVariants = true)]
[assembly: MonoDetourTargets(typeof(ToolItem), GenerateControlFlowVariants = true)]
[assembly: MonoDetourTargets(typeof(QuestConsumeTargetTake), GenerateControlFlowVariants = true)]
[assembly: MonoDetourTargets(typeof(InventoryToolCrest), GenerateControlFlowVariants = true)]
[assembly: MonoDetourTargets(typeof(InventoryToolCrestList), GenerateControlFlowVariants = true)]
[assembly: MonoDetourTargets(typeof(InventoryToolCrestSlot))]
[assembly: MonoDetourTargets(typeof(InventoryItemToolManager), GenerateControlFlowVariants = true)]
