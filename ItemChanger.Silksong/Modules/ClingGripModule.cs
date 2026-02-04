using Module = ItemChanger.Modules.Module;

namespace ItemChanger.Silksong.Modules
{
    public class ClingGripModule : Module
    {
        public static ClingGripModule? Instance { get; private set; }

        public bool HasLeft { get; set; }
        public bool HasRight { get; set; }

        protected override void DoLoad()
        {
            Instance = this;
            On.HeroController.CanWallJump += OverrideCanWallJump;
        }

        protected override void DoUnload()
        {
            Instance = null;
            On.HeroController.CanWallJump -= OverrideCanWallJump;
        }

        private static bool OverrideCanWallJump(On.HeroController.orig_CanWallJump orig, HeroController self, bool mustBeNearWall)
        {
            bool canJump = orig(self, mustBeNearWall);
            if (!canJump || Instance == null) return canJump;

            bool facingRight = self.cState.facingRight;
            if (facingRight && !Instance.HasRight) return false;
            if (!facingRight && !Instance.HasLeft) return false;
            return true;
        }
    }
}
