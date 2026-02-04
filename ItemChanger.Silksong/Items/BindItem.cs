using ItemChanger.Items;
using ItemChanger.Silksong.Modules;

namespace ItemChanger.Silksong.Items
{
    public sealed class BindItem : Item
    {
        public override void GiveImmediate(GiveInfo info)
        {
            var module = ActiveProfile?.Modules.Get<BindSkill>();
            if (module == null)
            {
                module = new BindSkill();
                ActiveProfile?.Modules.Add(module);
            }
            module.CanBind = true;
        }

        public override bool Redundant()
        {
            return BindSkill.Instance?.CanBind ?? false;
        }
    }
}
