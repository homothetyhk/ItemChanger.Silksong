using ItemChanger.Items;
using ItemChanger.Silksong.Modules;

namespace ItemChanger.Silksong.Items
{
    public class TauntItem : Item
    {
        public override void GiveImmediate(GiveInfo info)
        {
            var module = ActiveProfile?.Modules.Get<TauntModule>();
            if (module == null)
            {
                module = new TauntModule();
                ActiveProfile?.Modules.Add(module);
            }
            module.HasTaunt = true;
        }

        public override bool Redundant()
        {
            return TauntModule.Instance?.HasTaunt ?? false;
        }
    }
}
