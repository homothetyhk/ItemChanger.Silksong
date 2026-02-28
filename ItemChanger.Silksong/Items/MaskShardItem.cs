using PrepatcherPlugin;
using ItemChanger.Items;

namespace ItemChanger.Silksong.Items
{
    /// <summary>
    /// Item which supports giving one or more mask shards, with appropriate rollover handling.
    /// </summary>
    public class MaskShardItem : Item
    {
        /// <summary>
        /// The number of mask shards to give.
        /// </summary>
        public required int Shards { get; init; }

        public override void GiveImmediate(GiveInfo info)
        {
            for (int i = 0; i < Shards; i++) GiveMaskShard();
        }

        protected virtual void GiveMaskShard()
        {
            if (PlayerDataAccess.heartPieces < 3)
            {
                PlayerDataAccess.heartPieces++;
                return;
            }

            PlayerDataAccess.heartPieces -= (4 - 1);
            if (HeroController.SilentInstance is HeroController hc && hc)
            {
                HeroController.SilentInstance.AddToMaxHealth(1);
                HeroController.SilentInstance.MaxHealth();
                EventRegister.SendEvent("MAX HP UP");
            }
            else
            {
                PlayerData.instance.AddToMaxHealth(1);
            }
        }

        public override bool Redundant() => PlayerDataAccess.maxHealthBase == 10;
    }
}
