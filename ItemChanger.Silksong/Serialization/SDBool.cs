using ItemChanger.Serialization;
using Newtonsoft.Json;

namespace ItemChanger.Silksong.Serialization
{
    public record SDBool(string SceneName, string ID, bool SemiPersistent, SceneData.PersistentMutatorTypes Mutator) : IBool, IWritableBool
    {
        public SDBool(string SceneName, string ID) : this(SceneName, ID, false, SceneData.PersistentMutatorTypes.None) { }

        [JsonIgnore]
        public bool Value
        {
            get
            {
                return SceneData.instance.PersistentBools.TryGetValue(SceneName, ID, out PersistentItemData<bool> pid)
                    && pid.Value;
            }
            set
            {
                SceneData.instance.PersistentBools.SetValue(new() { SceneName = SceneName, ID = ID, IsSemiPersistent = SemiPersistent, Mutator = Mutator, Value = value });
            }
        }
    }
}
