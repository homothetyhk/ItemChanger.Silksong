using HarmonyLib;
using Module = ItemChanger.Modules.Module;

namespace ItemChanger.Silksong.Modules
{
    public class ClawlineModule : Module
    {
        public static ClawlineModule? Instance { get; private set; }

        public bool HasLeft { get; set; }
        public bool HasRight { get; set; }

        private Harmony? _harmony;

        protected override void DoLoad()
        {
            Instance = this;
            _harmony = new Harmony($"ItemChanger.Silksong.{nameof(ClawlineModule)}");
            _harmony.PatchAll(typeof(Patches));
        }

        protected override void DoUnload()
        {
            Instance = null;
            _harmony?.UnpatchSelf();
            _harmony = null;
        }

        [HarmonyPatch]
        private static class Patches
        {
            [HarmonyPostfix]
            [HarmonyPatch(typeof(HeroController), nameof(HeroController.CanHarpoonDash))]
            private static void OverrideCanHarpoonDash(HeroController __instance, ref bool __result)
            {
                if (!__result || Instance == null) return;

                bool facingRight = __instance.cState.facingRight;
                if (facingRight && !Instance.HasRight) __result = false;
                if (!facingRight && !Instance.HasLeft) __result = false;
            }

            [HarmonyPostfix]
            [HarmonyPatch(typeof(HeroController), nameof(HeroController.CanTryHarpoonDash))]
            private static void OverrideCanTryHarpoonDash(HeroController __instance, ref bool __result)
            {
                if (!__result || Instance == null) return;

                bool facingRight = __instance.cState.facingRight;
                if (facingRight && !Instance.HasRight) __result = false;
                if (!facingRight && !Instance.HasLeft) __result = false;
            }
        }
    }
}
