using HarmonyLib;
using System.Diagnostics;
using System.Reflection;

namespace ItemChanger.Silksong.Modules;

/// <summary>
/// Prevents tools from being consumed by quest turn-ins
/// </summary>
public class RetainToolsModule : ItemChanger.Modules.Module
{
    protected override void DoLoad() => Using(new HarmonyPatchGroup() { typeof(Patches) });

    protected override void DoUnload() { }

    [HarmonyPatch]
    private static class Patches
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(ToolItem), nameof(ToolItem.Consume))]
        private static bool ToolItemConsumePrefix()
        {
            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(CollectableUIMsg), nameof(CollectableUIMsg.ShowTakeMsg))]
        private static bool ShowTakeMsgPrefix(ICollectableUIMsgItem item, TakeItemTypes takeItemType)
        {
            MethodBase trace = new StackTrace().GetFrame(2).GetMethod();
            return item is not ToolItem || trace.DeclaringType != typeof(QuestPlaymakerActions.QuestConsumeTargetTake);
        }
    }
}
