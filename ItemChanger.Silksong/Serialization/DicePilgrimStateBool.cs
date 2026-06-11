using ItemChanger.Serialization;

namespace ItemChanger.Silksong.Serialization
{
    //implementation to compare dicePilgrimState for the magnetite dice location since IntComparisonBool is currently buggy with PDInt
    public class DicePilgrimStateBool() : IValueProvider<bool>
    {
        PDInt dicePilgrimStatusPD = new PDInt(nameof(PlayerData.dicePilgrimState));
        public bool Value
        {
            get {
                if (dicePilgrimStatusPD.Value == 0) { return true; }//0 = lumble is available, 1 = lumble is dead at original location, 2 = lumble is dead near last judge room
                else return false;
            }
        }
    }
}
