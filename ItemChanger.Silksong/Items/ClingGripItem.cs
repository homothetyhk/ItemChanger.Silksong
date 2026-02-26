using ItemChanger.Items;
using ItemChanger.Silksong.Modules;

namespace ItemChanger.Silksong.Items
{
    public enum ClingGripDirection
    {
        Left,
        Right
    }

    public class ClingGripItem : Item
    {
        public ClingGripDirection Direction { get; set; }

        public override void GiveImmediate(GiveInfo info)
        {
            var module = ActiveProfile?.Modules.Get<ClingGripModule>();
            if (module == null)
            {
                module = new ClingGripModule();
                ActiveProfile?.Modules.Add(module);
            }
            if (Direction == ClingGripDirection.Left)
                module.HasLeft = true;
            else
                module.HasRight = true;
        }

        public override bool Redundant()
        {
            var module = ClingGripModule.Instance;
            if (module == null) return false;
            return Direction == ClingGripDirection.Left
                ? module.HasLeft
                : module.HasRight;
        }
    }
}
