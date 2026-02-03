using ItemChanger.Items;

namespace ItemChanger.Silksong.Items
{
    public class PlayerDataIntItem : Item
    {
        public string FieldName { get; set; } = "";
        public int Amount { get; set; } = 1;

        public override void GiveImmediate(GiveInfo info)
        {
            var pd = GameManager.instance?.playerData;
            if (pd == null) return;
            
            int currentValue = pd.GetInt(FieldName);
            pd.SetInt(FieldName, currentValue + Amount);
        }

        public override bool Redundant() => false;
    }

    public sealed class MaskShardItem : Item
    {
        private const int THRESHOLD = 4;

        public override void GiveImmediate(GiveInfo info)
        {
            var pd = GameManager.instance?.playerData;
            if (pd == null) return;

            int currentPieces = pd.GetInt("heartPieces");
            currentPieces++;

            if (currentPieces >= THRESHOLD)
            {
                pd.SetInt("heartPieces", 0);
                HeroController.instance?.AddToMaxHealth(1);
                HeroController.instance?.MaxHealth();
                ItemChangerPlugin.Instance.Logger.LogInfo("Collected 4 mask shards - gained a new mask!");
            }
            else
            {
                pd.SetInt("heartPieces", currentPieces);
                ItemChangerPlugin.Instance.Logger.LogInfo($"Mask shard collected ({currentPieces}/{THRESHOLD})");
            }
        }

        public override bool Redundant() => false;
    }

    public sealed class SpoolFragmentItem : Item
    {
        private const int THRESHOLD = 2;

        public override void GiveImmediate(GiveInfo info)
        {
            var pd = GameManager.instance?.playerData;
            if (pd == null) return;

            int currentParts = pd.GetInt("silkSpoolParts");
            currentParts++;

            if (currentParts >= THRESHOLD)
            {
                pd.SetInt("silkSpoolParts", 0);
                pd.SetBool("IsSilkSpoolBroken", false);
                HeroController.instance?.AddToMaxSilk(1);
                ItemChangerPlugin.Instance.Logger.LogInfo("Collected 2 spool fragments - gained more silk capacity!");
            }
            else
            {
                pd.SetInt("silkSpoolParts", currentParts);
                ItemChangerPlugin.Instance.Logger.LogInfo($"Spool fragment collected ({currentParts}/{THRESHOLD})");
            }
        }

        public override bool Redundant() => false;
    }
}
