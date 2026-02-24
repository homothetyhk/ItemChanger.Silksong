using ItemChanger.Items;

namespace ItemChanger.Silksong.Items
{
    public class PlayerDataIntItem : Item
    {
        public string FieldName { get; set; } = "";
        public int Amount { get; set; } = 1;
        public bool Increment { get; set; } = true;

        public override void GiveImmediate(GiveInfo info)
        {
            int value = Increment
                ? PlayerData.instance.GetInt(FieldName) + Amount
                : Amount;

            PlayerData.instance.SetInt(FieldName, value);
        }

        public override bool Redundant()
        {
            return PlayerData.instance.GetInt(FieldName) == Amount;
        }
    }
}
