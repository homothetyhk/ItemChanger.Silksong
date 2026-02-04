using ItemChanger.Modules;

namespace ItemChanger.Silksong.Modules
{
    public class BindSkill : Module
    {
        public static BindSkill? Instance { get; private set; }

        public bool CanBind { get; set; }

        protected override void DoLoad()
        {
            Instance = this;
            On.HeroController.CanBind += OverrideCanBind;
        }

        protected override void DoUnload()
        {
            Instance = null;
            On.HeroController.CanBind -= OverrideCanBind;
        }

        private static bool OverrideCanBind(On.HeroController.orig_CanBind orig, HeroController self)
        {
            bool origResult = orig(self);
            return origResult && (Instance?.CanBind ?? true);
        }
    }
}
