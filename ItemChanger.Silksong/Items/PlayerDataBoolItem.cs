using ItemChanger.Items;

namespace ItemChanger.Silksong.Items
{
    public sealed class PlayerDataBoolItem : Item
    {
        public string FieldName { get; set; }
        public string HasSeenField { get; set; }

        public override void GiveImmediate(GiveInfo info)
        {
            var pd = GameManager.instance?.playerData;
            if (pd == null)
            {
                ItemChangerPlugin.Instance.Logger.LogError($"PlayerDataBoolItem: PlayerData is null");
                return;
            }

            pd.SetBool(FieldName, true);

            if (!string.IsNullOrEmpty(HasSeenField))
            {
                pd.SetBool(HasSeenField, true);
            }

            ItemChangerPlugin.Instance.Logger.LogInfo($"[PDBool] {FieldName} = true");
        }

        public override bool Redundant()
        {
            var pd = GameManager.instance?.playerData;
            return pd?.GetBool(FieldName) == true;
        }
    }
}
