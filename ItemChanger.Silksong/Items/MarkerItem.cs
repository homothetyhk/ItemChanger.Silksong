using ItemChanger.Items;

namespace ItemChanger.Silksong.Items
{
    public class MarkerItem : Item
    {
        public string FieldName { get; set; }

        public override void GiveImmediate(GiveInfo info)
        {
            var pd = GameManager.instance?.playerData;
            if (pd == null) return;
            
            pd.SetBool(FieldName, true);
            pd.SetBool("hasAnyPlaceableMarker", true);
            
            ItemChangerPlugin.Instance.Logger.LogInfo($"Obtained marker: {Name}");
        }

        public override bool Redundant()
        {
            var pd = GameManager.instance?.playerData;
            if (pd == null) return false;
            return pd.GetBool(FieldName);
        }
    }
}
