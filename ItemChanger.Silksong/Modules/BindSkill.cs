using ItemChanger.Modules;
using ItemChanger.Silksong.Items;
using PrepatcherPlugin; 

namespace ItemChanger.Silksong.Modules
{
    public class BindSkill : Module
    {
        public bool CanBind { get; private set; }

        protected override void DoLoad()
        {
            CanBind = PlayerData.instance.GetBool(BindItem.BindSkillKey);
            PlayerDataVariableEvents.OnSetBool += OnSetPlayerData;
        }

        protected override void DoUnload()
        {
            PlayerDataVariableEvents.OnSetBool -= OnSetPlayerData;
        }

        private bool OnSetPlayerData(PlayerData pd, string fieldName, bool value)
        {
            if (fieldName == BindItem.BindSkillKey)
            {
                this.CanBind = value;
            }
            return value;
        }
    }
}
