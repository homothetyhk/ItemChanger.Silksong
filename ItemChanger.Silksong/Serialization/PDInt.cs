using ItemChanger.Serialization;
using Newtonsoft.Json;

namespace ItemChanger.Silksong.Serialization
{
    public record PDInt(string IntName) : IInteger, IWritableInteger
    {
        [JsonIgnore]
        public int Value
        {
            get => PlayerData.instance.GetInt(IntName); 
            set => PlayerData.instance.SetInt(IntName, value);
        }
    }
}
