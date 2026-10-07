using ItemChanger.Serialization;
using Newtonsoft.Json;

namespace ItemChanger.Silksong.Serialization;

[method: JsonConstructor]
public record SDInt(string SceneName, string ID, bool SemiPersistent, int DefaultValue, SceneData.PersistentMutatorTypes Mutator) : IWritableValueProvider<int>
{
    public SDInt(string SceneName, string ID) : this(SceneName, ID, false, 0, SceneData.PersistentMutatorTypes.None) { }
    public SDInt(string SceneName, string ID, int DefaultValue) : this(SceneName, ID, false, DefaultValue, SceneData.PersistentMutatorTypes.None) { }

    [JsonIgnore]
    public int Value
    {
        get => SceneData.instance.PersistentInts.TryGetValue(SceneName, ID, out PersistentItemData<int> pid) ? pid.Value : DefaultValue;
        set => SceneData.instance.PersistentInts.SetValue(new() { SceneName = SceneName, ID = ID, IsSemiPersistent = SemiPersistent, Mutator = Mutator, Value = value });
    }
}
