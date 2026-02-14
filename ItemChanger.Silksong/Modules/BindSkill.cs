using ItemChanger.Modules;
using PrepatcherPlugin; 

namespace ItemChanger.Silksong.Modules
{
    public class BindSkill : Module
    {
        public bool CanBind { get; private set; }

        public override void Initialize()
        {
            CanBind = PlayerData.instance.GetBool(BindItem.BindSkillKey);
            PlayerDataVariableEvents.OnSetBool += OnSetPlayerData;
        }

        public override void Unload()
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
