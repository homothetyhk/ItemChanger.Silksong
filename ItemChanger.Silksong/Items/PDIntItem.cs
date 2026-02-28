using ItemChanger.Items;

namespace ItemChanger.Silksong.Items
{
    /// <summary>
    /// Item which operates on an int field of PlayerData.
    /// </summary>
    public class PDIntItem : Item
    {
        /// <summary>
        /// The name of the PlayerData int field.
        /// </summary>
        public required string IntName { get; init; }
        /// <summary>
        /// The value to increment or set the PD int, according to <see cref="Increment"/>.
        /// </summary>
        public required int Amount { get; init; }
        /// <summary>
        /// If true, the PD int is incremented by <see cref="Amount"/>. Otherwise, the PD int is set to <see cref="Amount"/>.
        /// </summary>
        public required bool Increment { get; init; }

        public override void GiveImmediate(GiveInfo info)
        {
            if (Increment)
            {
                PlayerData.instance.SetInt(IntName, PlayerData.instance.GetInt(IntName) + Amount);
            }
            else
            {
                PlayerData.instance.SetInt(IntName, Amount);
            }
        }

        public override bool Redundant()
        {
            return !Increment && PlayerData.instance.GetInt(IntName) == Amount;
        }
    }
}
