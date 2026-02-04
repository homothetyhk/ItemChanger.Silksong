using Module = ItemChanger.Modules.Module;

namespace ItemChanger.Silksong.Modules
{
    public class SwiftStepModule : Module
    {
        public static SwiftStepModule? Instance { get; private set; }

        public bool HasLeft { get; set; }
        public bool HasRight { get; set; }

        protected override void DoLoad()
        {
            Instance = this;
            On.HeroController.HeroDash += OverrideHeroDash;
        }

        protected override void DoUnload()
        {
            Instance = null;
            On.HeroController.HeroDash -= OverrideHeroDash;
        }

        private static void OverrideHeroDash(On.HeroController.orig_HeroDash orig, HeroController self, bool startAlreadyDashing)
        {
            if (Instance != null)
            {
                bool facingRight = self.cState.facingRight;
                if (facingRight && !Instance.HasRight) return;
                if (!facingRight && !Instance.HasLeft) return;
            }
            orig(self, startAlreadyDashing);
        }
    }
}
