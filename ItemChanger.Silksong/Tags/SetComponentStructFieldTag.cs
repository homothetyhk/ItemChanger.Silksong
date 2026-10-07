using ItemChanger.Serialization;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Tags;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Tags;

/// <summary>
/// Sets a field on a component to a provided value.
/// </summary>
public class SetComponentStructFieldTag<TComponent, TField> : Tag where TComponent : class where TField : struct
{
    public required string SceneName { get; init; }

    public required ComponentStructFieldOption<TComponent, TField> Field { get; init; }

    public required IValueProvider<TField> Provider { get; init; }

    public IValueProvider<bool> Test { get; init; } = new BoxedBool { Value = true };

    protected override void DoLoad(TaggableObject parent) => Using(new SceneEditGroup { { SceneName, ModifyScene } });

    private void ModifyScene(Scene scene)
    {
        if (Test.Value) Field.Set(scene, Provider.Value);
    }
}
