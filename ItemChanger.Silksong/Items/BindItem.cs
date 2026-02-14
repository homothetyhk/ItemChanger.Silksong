using ItemChanger; 
using ItemChanger.Items;

namespace ItemChanger.Silksong.Items
{
    public sealed class BindItem : Item
    {
        public const string BindSkillKey = "hasBindSkill";

        public override void GiveImmediate(GiveInfo info)
        {
            PlayerData.instance.SetBool(BindSkillKey, true);
        }

        public override bool Redundant()
        {
            return PlayerData.instance.GetBool(BindSkillKey);
        }
    }
}
