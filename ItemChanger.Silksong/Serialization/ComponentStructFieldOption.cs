using ItemChanger.Extensions;
using ItemChanger.Silksong.Util;
using System.Reflection;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Serialization;

/// <summary>
/// Value provider which fetches a struct field from a component on a GameObject in an active scene, in a Nullable wrapper.
/// If the specified scene is not active, or the object/component are not found, outputs null.
/// </summary>
public record ComponentStructFieldOption<TComponent, TField>(string SceneName, string ObjectPath, string FieldName)
    : ISceneScopedWritableValueProvider<TField?> where TField : struct where TComponent : class
{
    private readonly ReflectionUtil.FieldOrPropertyInfo info = typeof(TComponent).GetFieldOrProperty(FieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

    private TComponent? GetComponent(Scene scene)
    {
        if (!scene.IsValid() || scene.name != SceneName) return null;
        GameObject? go = scene.FindGameObject(ObjectPath);
        if (go == null) return null;
        return go.TryGetComponent(out TComponent component) ? component : null;
    }

    public TField? Get(Scene scene) => GetComponent(scene) is { } component ? (TField)info.GetValue(component) : null;

    public void Set(Scene scene, TField? value)
    {
        if (value.HasValue && GetComponent(scene) is { } component)
            info.SetValue(component, value.Value);
    }
}
