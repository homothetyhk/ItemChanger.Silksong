using HarmonyLib;
using Module = ItemChanger.Modules.Module;

namespace ItemChanger.Silksong.Modules
{
    public class SwiftStepModule : Module
    {
        public static SwiftStepModule? Instance { get; private set; }

        public bool HasLeft { get; set; }
        public bool HasRight { get; set; }

        private Harmony? _harmony;

        protected override void DoLoad()
        {
            Instance = this;
            _harmony = new Harmony($"ItemChanger.Silksong.{nameof(SwiftStepModule)}");
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
            [HarmonyPrefix]
            [HarmonyPatch(typeof(HeroController), nameof(HeroController.HeroDash))]
            private static bool OverrideHeroDash(HeroController __instance)
            {
                if (Instance == null) return true;

                bool facingRight = __instance.cState.facingRight;
                if (facingRight && !Instance.HasRight) return false;
                if (!facingRight && !Instance.HasLeft) return false;
                return true;
            }
        }
    }
}
