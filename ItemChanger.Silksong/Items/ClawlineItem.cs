using ItemChanger.Items;
using ItemChanger.Silksong.Modules;

namespace ItemChanger.Silksong.Items
{
    public enum ClawlineDirection
    {
        Left,
        Right
    }

    public class ClawlineItem : Item
    {
        public ClawlineDirection Direction { get; set; }

        public override void GiveImmediate(GiveInfo info)
        {
            var module = ActiveProfile?.Modules.Get<ClawlineModule>();
            if (module == null)
            {
                module = new ClawlineModule();
                ActiveProfile?.Modules.Add(module);
            }
            if (Direction == ClawlineDirection.Left)
                module.HasLeft = true;
            else
                module.HasRight = true;
        }

        public override bool Redundant()
        {
            var module = ClawlineModule.Instance;
            if (module == null) return false;
            return Direction == ClawlineDirection.Left
                ? module.HasLeft
                : module.HasRight;
        }
    }
}
