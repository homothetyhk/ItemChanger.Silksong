using ItemChanger.Items;
using ItemChanger.Silksong.Modules;

namespace ItemChanger.Silksong.Items
{
    public enum SwiftStepDirection
    {
        Left,
        Right
    }

    public class SwiftStepItem : Item
    {
        public SwiftStepDirection Direction { get; set; }

        public override void GiveImmediate(GiveInfo info)
        {
            var module = ActiveProfile?.Modules.Get<SwiftStepModule>();
            if (module == null)
            {
                module = new SwiftStepModule();
                ActiveProfile?.Modules.Add(module);
            }
            if (Direction == SwiftStepDirection.Left)
                module.HasLeft = true;
            else
                module.HasRight = true;
        }

        public override bool Redundant()
        {
            var module = SwiftStepModule.Instance;
            if (module == null) return false;
            return Direction == SwiftStepDirection.Left
                ? module.HasLeft
                : module.HasRight;
        }
    }
}
