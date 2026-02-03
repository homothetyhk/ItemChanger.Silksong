using ItemChanger.Modules;
using ItemChanger.Silksong.Items;

namespace ItemChanger.Silksong.Modules
{
    public class BindSkill : Module
    {
        protected override void DoLoad()
        {
            On.HeroController.CanBind += OverrideCanBind;
            Logger.LogInfo("BindSkill module loaded - hook applied");
        }

        protected override void DoUnload()
        {
            On.HeroController.CanBind -= OverrideCanBind;
            Logger.LogInfo("BindSkill module unloaded");
        }

        private bool OverrideCanBind(On.HeroController.orig_CanBind orig, HeroController self)
        {
            bool origResult = orig(self);
            bool result = origResult && BindItem.canBind;
            Logger.LogInfo($"CanBind hook: orig={origResult}, canBind={BindItem.canBind}, result={result}");
            return result;
        }
    }
}
