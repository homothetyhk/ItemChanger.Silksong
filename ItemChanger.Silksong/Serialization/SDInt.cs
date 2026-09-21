using ItemChanger.Serialization;
using Newtonsoft.Json;

namespace ItemChanger.Silksong.Serialization;

[method: JsonConstructor]
public record SDInt(string SceneName, string ID, bool SemiPersistent, SceneData.PersistentMutatorTypes Mutator) : IWritableValueProvider<int>
{
    public SDInt(string SceneName, string ID) : this(SceneName, ID, false, SceneData.PersistentMutatorTypes.None) { }

    [JsonIgnore]
    public int Value
    {
        get => SceneData.instance.PersistentInts.TryGetValue(SceneName, ID, out PersistentItemData<int> pid) ? pid.Value : 0;
        set => SceneData.instance.PersistentInts.SetValue(new() { SceneName = SceneName, ID = ID, IsSemiPersistent = SemiPersistent, Mutator = Mutator, Value = value });
    }
}
