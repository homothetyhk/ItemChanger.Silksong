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

    public sealed class MaskShardItem : Item
    {
        private const int THRESHOLD = 4;

        public override void GiveImmediate(GiveInfo info)
        {
            var pd = PlayerData.instance;
            int currentPieces = pd.GetInt("heartPieces");
            currentPieces++;

            if (currentPieces >= THRESHOLD)
            {
                pd.SetInt("heartPieces", 0);
                HeroController.instance?.AddToMaxHealth(1);
                HeroController.instance?.MaxHealth();
            }
            else
            {
                pd.SetInt("heartPieces", currentPieces);
            }
        }

        public override bool Redundant() => false;
    }

    public sealed class SpoolFragmentItem : Item
    {
        private const int THRESHOLD = 2;

        public override void GiveImmediate(GiveInfo info)
        {
            var pd = PlayerData.instance;
            int currentParts = pd.GetInt("silkSpoolParts");
            currentParts++;

            if (currentParts >= THRESHOLD)
            {
                pd.SetInt("silkSpoolParts", 0);
                pd.SetBool("IsSilkSpoolBroken", false);
                HeroController.instance?.AddToMaxSilk(1);
            }
            else
            {
                pd.SetInt("silkSpoolParts", currentParts);
            }
        }

        public override bool Redundant() => false;
    }
}
