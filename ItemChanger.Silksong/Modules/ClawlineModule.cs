using Module = ItemChanger.Modules.Module;

namespace ItemChanger.Silksong.Modules
{
    public class ClawlineModule : Module
    {
        public static ClawlineModule? Instance { get; private set; }

        public bool HasLeft { get; set; }
        public bool HasRight { get; set; }

        protected override void DoLoad()
        {
            Instance = this;
            On.HeroController.CanHarpoonDash += OverrideCanHarpoonDash;
            On.HeroController.CanTryHarpoonDash += OverrideCanTryHarpoonDash;
        }

        protected override void DoUnload()
        {
            Instance = null;
            On.HeroController.CanHarpoonDash -= OverrideCanHarpoonDash;
            On.HeroController.CanTryHarpoonDash -= OverrideCanTryHarpoonDash;
        }

        private static bool OverrideCanHarpoonDash(On.HeroController.orig_CanHarpoonDash orig, HeroController self)
        {
            bool canDash = orig(self);
            if (!canDash || Instance == null) return canDash;

            bool facingRight = self.cState.facingRight;
            if (facingRight && !Instance.HasRight) return false;
            if (!facingRight && !Instance.HasLeft) return false;
            return true;
        }

        private static bool OverrideCanTryHarpoonDash(On.HeroController.orig_CanTryHarpoonDash orig, HeroController self)
        {
            bool canTry = orig(self);
            if (!canTry || Instance == null) return canTry;

            bool facingRight = self.cState.facingRight;
            if (facingRight && !Instance.HasRight) return false;
            if (!facingRight && !Instance.HasLeft) return false;
            return true;
        }
    }
}
