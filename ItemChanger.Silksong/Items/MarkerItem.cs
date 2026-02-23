using ItemChanger.Items;

namespace ItemChanger.Silksong.Items
{
    public class MarkerItem : Item
    {
        public string FieldName { get; set; }

        public override void GiveImmediate(GiveInfo info)
        {
            PlayerData.instance.SetBool(FieldName, true);
            PlayerData.instance.SetBool("hasAnyPlaceableMarker", true);
        }

        public override bool Redundant()
        {
            return PlayerData.instance.GetBool(FieldName);
        }
    }
}
