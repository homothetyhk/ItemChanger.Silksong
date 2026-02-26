using ItemChanger.Items;
using ItemChanger.Silksong.Modules;

namespace ItemChanger.Silksong.Items
{
    public enum SlashDirection
    {
        Left,
        Right,
        Up,
        Down
    }

    public class SlashItem : Item
    {
        public SlashDirection Direction { get; set; }

        public override void GiveImmediate(GiveInfo info)
        {
            var module = ActiveProfile?.Modules.Get<SlashModule>();
            if (module == null)
            {
                module = new SlashModule();
                ActiveProfile?.Modules.Add(module);
            }
            switch (Direction)
            {
                case SlashDirection.Left:
                    module.HasLeft = true;
                    break;
                case SlashDirection.Right:
                    module.HasRight = true;
                    break;
                case SlashDirection.Up:
                    module.HasUp = true;
                    break;
                case SlashDirection.Down:
                    module.HasDown = true;
                    break;
            }
        }

        public override bool Redundant()
        {
            var module = SlashModule.Instance;
            if (module == null) return false;
            return Direction switch
            {
                SlashDirection.Left => module.HasLeft,
                SlashDirection.Right => module.HasRight,
                SlashDirection.Up => module.HasUp,
                SlashDirection.Down => module.HasDown,
                _ => false
            };
        }
    }
}
