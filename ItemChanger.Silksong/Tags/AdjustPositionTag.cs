using ItemChanger.Extensions;
using ItemChanger.Serialization;
using ItemChanger.Tags;
using ItemChanger.Tags.Constraints;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Tags;

/// <summary>
/// Adjust the position of an existing object.
/// </summary>
[LocationTag]
public class AdjustPositionTag : Tag
{
    /// <summary>
    /// The scene where the object is found.
    /// </summary>
    public required string SceneName { get; init; }

    /// <summary>
    /// The name or path to the object.
    /// </summary>
    public required string ObjectName { get; init; }

    /// <summary>
    /// The delta to apply to the object's position.
    /// </summary>
    public required Vector3 Adjustment { get; init; }

    /// <summary>
    /// If false, do not do the adjustment.
    /// </summary>
    public IValueProvider<bool> Test { get; init; } = new BoxedBool { Value = true };

    /// <summary>
    /// The space to apply the adjustment within.
    /// </summary>
    public Space Space { get; init; } = Space.World;

    protected override void DoLoad(TaggableObject parent) => Using(new SceneEditGroup { { SceneName, DoAdjustment } });

    private void DoAdjustment(Scene scene)
    {
        if (!Test.Value)
            return;

        GameObject? go = scene.FindGameObject(ObjectName);
        if (go == null)
            return;

        go.transform.Translate(Adjustment, Space);
    }
}
