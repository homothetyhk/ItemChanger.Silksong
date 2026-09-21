using ItemChanger.Extensions;
using ItemChanger.Serialization;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Serialization;

public class BouncePodSequenceFinishedBool : IValueProvider<bool>
{
    public required string SceneName;

    public required string ObjectName;

    public bool Value
    {
        get
        {
            Scene scene = SceneManager.GetSceneByName(SceneName);
            return scene.IsValid() && scene.FindGameObject(ObjectName) is GameObject obj && obj.TryGetComponent(out BouncePodSequence seq) && seq.persistent.LoadedValue >= seq.sequences.Count;
        }
    }
}
