using ItemChanger.Serialization;
using Newtonsoft.Json;

namespace ItemChanger.Silksong.Serialization
{
    public record PDBool(string BoolName) : IBool, IWritableBool
    {
        [JsonIgnore]
        public bool Value
        {
            get
            {
                return PlayerData.instance.GetBool(BoolName);
            }
            set
            {
                PlayerData.instance.SetBool(BoolName, value);
            }
        }
    }
}
