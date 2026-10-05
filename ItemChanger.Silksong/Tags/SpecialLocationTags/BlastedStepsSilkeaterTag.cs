using Benchwarp.Data;
using ItemChanger.Tags;
using ItemChanger.Tags.Constraints;
using PrepatcherPlugin;
using Silksong.UnityHelper.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Tags.SpecialLocationTags;

/// <summary>
/// The silkeater in Blasted Steps does not naturally spawn in Steel Soul mode.
/// It is simple to force it to spawn, there is no overlap with Steel Soul things. Let's do it to simplify the randomimzer.
/// </summary>
[LocationTag]
public class BlastedStepsSilkeaterTag : Tag
{
    protected override void DoLoad(TaggableObject parent) => Using(new SceneEditGroup() { { SceneNames.Coral_37, ForceCocoonSpawn } });

    private void ForceCocoonSpawn(Scene scene)
    {
        if (PlayerDataAccess.permadeathMode == GlobalEnums.PermadeathModes.Off) return;

        GameObject states = scene.FindGameObject("Room States")!;
        states.RemoveComponent<TestGameObjectActivator>();

        states.FindChild("Steel")!.SetActive(true);
        var normal = states.FindChild("Normal")!;
        normal.SetActive(true);
        foreach (Transform child in normal.transform) child.gameObject.SetActive(child.name != "terrain collider" && child.name != "Coral_Stone_0003_pillar (46)");
    }
}
