using ItemChanger.Items;

namespace ItemChanger.Silksong.Items
{
    /// <summary>
    /// Item which sets the bool to unlock a specific map marker, as well as a generic bool to inform the game that any marker has been unlocked.
    /// </summary>
    public class MarkerItem : PDBoolItem
    {
        public override void GiveImmediate(GiveInfo info)
        {
            base.GiveImmediate(info);
            PlayerData.instance.SetBool(nameof(PlayerData.hasMarker), true);
        }

        public override bool Redundant()
        {
            return base.Redundant() && PlayerData.instance.GetBool(nameof(PlayerData.hasMarker));
        }
    }
}
