using ItemChanger.Items;

namespace ItemChanger.Silksong.Items
{
    public sealed class BindItem : Item
    {
        public static bool canBind { get; set; }

        public override void GiveImmediate(GiveInfo info)
        {
            canBind = true;
        }

        public override bool Redundant()
        {
            return canBind;
        }
    }
}
