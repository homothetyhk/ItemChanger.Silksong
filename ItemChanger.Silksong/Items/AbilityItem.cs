using ItemChanger.Items;

namespace ItemChanger.Silksong.Items
{
    public sealed class AbilityItem : Item
    {
        public required string FieldName { get; init; }
        public string HasSeenField { get; init; }

        public override void GiveImmediate(GiveInfo info)
        {
            var pd = GameManager.instance?.playerData;
            if (pd == null)
            {
                Logger.LogError($"AbilityItem: PlayerData is null");
                return;
            }

            pd.SetBool(FieldName, true);
            
            if (!string.IsNullOrEmpty(HasSeenField))
            {
                pd.SetBool(HasSeenField, true);
            }
        }

        public override bool Redundant()
        {
            var pd = GameManager.instance?.playerData;
            return pd?.GetBool(FieldName) ?? false;
        }
    }
}
