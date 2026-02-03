using ItemChanger.Items;

namespace ItemChanger.Silksong.Items
{
    public class PlayerDataIntItem : Item
    {
        public string FieldName { get; set; }
        public int Amount { get; set; } = 1;

        public override void GiveImmediate(GiveInfo info)
        {
            var pd = GameManager.instance?.playerData;
            if (pd == null)
            {
                ItemChangerPlugin.Instance.Logger.LogError($"PlayerDataIntItem: PlayerData is null");
                return;
            }

            pd.SetInt(FieldName, Amount);
            ItemChangerPlugin.Instance.Logger.LogInfo($"[PDInt] {FieldName} = {Amount}");
        }

        public override bool Redundant()
        {
            var pd = GameManager.instance?.playerData;
            return pd?.GetInt(FieldName) == Amount;
        }
    }
}
