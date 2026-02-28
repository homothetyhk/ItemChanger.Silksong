using PrepatcherPlugin;
using ItemChanger.Items;

namespace ItemChanger.Silksong.Items
{
    /// <summary>
    /// Item which supports giving one or more spool fragments, with appropriate rollover handling.
    /// </summary>
    public sealed class SpoolFragmentItem : Item
    {
        /// <summary>
        /// The number of spool fragments to give.
        /// </summary>
        public required int Fragments { get; init; }

        public override void GiveImmediate(GiveInfo info)
        {
            int currentParts = PlayerDataAccess.silkSpoolParts;
            currentParts += Fragments;

            int totalGiven = 0;
            HeroController? hc = HeroController.SilentInstance;
            while (currentParts >= 2)
            {
                currentParts -= 2;
                totalGiven++;
                if (hc)
                {
                    HeroController.SilentInstance.AddToMaxSilk(1);
                }
                else
                {
                    PlayerDataAccess.silkMax += 1;
                }
            }
            PlayerDataAccess.silkSpoolParts = currentParts;
            if (hc && totalGiven > 0 && !PlayerData.instance.IsAnyCursed)
            {
                PlayerDataAccess.IsSilkSpoolBroken = false;
                SilkSpool.Instance.RefreshSilk();
                hc.RefillSilkToMax();
            }
        }

        public override bool Redundant() => PlayerDataAccess.silkMax == 18;
    }
}
