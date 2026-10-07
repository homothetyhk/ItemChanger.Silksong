using ItemChanger.Containers;
using ItemChanger.Locations;
using ItemChanger.Tags;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Locations;

/// <summary>
/// An ObjectLocation which uses a stricter and more involved search algorithm to find objects by path.
/// It is both aware of and sensitive to dupes, allowing it to find objects the default Transform.Find() algorithm won't.
/// </summary>
public class StrictObjectLocation : ObjectLocation
{
    /// <inheritdoc/>
    protected override void ModifyContainerInPlace(
        Scene scene,
        Container container,
        ContainerInfo info
    )
    {
        GameObject target = StrictFindObject(scene, ObjectName);
        container.ModifyContainerInPlace(target, info);
    }

    /// <inheritdoc/>
    public override GameObject ReplaceWithContainer(
        Scene scene,
        Container container,
        ContainerInfo info
    )
    {
        GameObject target = StrictFindObject(scene, ObjectName);
        GameObject newContainer = container.GetNewContainer(info);
        container.ApplyTargetContext(newContainer, target, Correction);
        UObject.Destroy(target);
        foreach (IActionOnContainerReplaceTag tag in GetTags<IActionOnContainerReplaceTag>())
        {
            tag.OnReplace(scene, newContainer);
        }
        return newContainer;
    }

    // TODO: Possibly move this into IC.Core?
    public static GameObject? StrictFindObjectMissingOk(Scene scene, string name)
    {
        List<string> parts = [.. name.Split('/')];
        if (parts.Count == 1)
            return FindObject(scene, name);

        List<GameObject> queue = [.. scene.GetRootGameObjects().Where(o => o.name == parts[0])];
        foreach (var subName in parts.Skip(1))
            queue = [.. queue.SelectMany(o => FindChildren(o, subName))];

        if (queue.Count == 0)
            return null;
        else if (queue.Count > 1)
            throw new ArgumentException($"'{name}' is ambiguous in '{scene}' ({queue.Count} matches)");
        else
            return queue[0];

        static IEnumerable<GameObject> FindChildren(GameObject parent, string childName)
        {
            for (int i = 0; i < parent.transform.childCount; i++)
                if (parent.transform.GetChild(i).name == childName)
                    yield return parent.transform.GetChild(i).gameObject;
        }
    }

    public static GameObject StrictFindObject(Scene scene, string name)
    {
        GameObject? obj = StrictFindObjectMissingOk(scene, name);
        if (obj == null)
            throw new NullReferenceException($"Could not find '{name}' in '{scene.name}'");

        return obj;
    }
}
