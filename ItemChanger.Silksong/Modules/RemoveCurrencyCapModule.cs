using HarmonyLib;
using ItemChanger.Modules;
using System.Reflection.Emit;
using UnityEngine;

namespace ItemChanger.Silksong.Modules;

/// <summary>
/// ObjectPool spawning logic has a hard-coded exception to forcibly recycle currency items if the pool is depleted.
/// We disable this behaviour to force new currency objects to spawn instead.
/// </summary>
public class RemoveCurrencyCapModule : Module
{
    protected override void DoLoad() => Using(new HarmonyPatchGroup() { typeof(Patches) });

    protected override void DoUnload() { }

    [HarmonyPatch]
    private static class Patches
    {
        [HarmonyPatch(typeof(ObjectPool), nameof(ObjectPool.Spawn), [typeof(GameObject), typeof(Transform), typeof(Vector3), typeof(Quaternion), typeof(bool)])]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpile(IEnumerable<CodeInstruction> source)
        {
            var methodInfo = typeof(GameObject).GetMethod(nameof(GameObject.GetComponent), types: []).MakeGenericMethod(typeof(CurrencyObjectBase));
            foreach (var instruction in source)
            {
                if (instruction.Calls(methodInfo))
                {
                    // Pretend this isn't a currency object to avoid reuse behaviour.
                    yield return new(OpCodes.Pop);  // Pop the GameObject argument.
                    yield return new(OpCodes.Ldnull);  // Push a null onto the stack for the GetComponent<>() return value.
                }
                else
                    yield return instruction;
            }
        }
    }
}
