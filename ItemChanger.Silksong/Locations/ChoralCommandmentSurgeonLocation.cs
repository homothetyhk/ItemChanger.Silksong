using ItemChanger.Containers;
using ItemChanger.Extensions;
using ItemChanger.Locations;
using ItemChanger.Silksong.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Locations;

/// <summary>
/// Custom location for the choral commandment on the ward bed.
/// The pickup makes the bed deactivate if it detects it has already been picked up. We force an alternate location in that case.
/// We also fling the items when possible if the user breaks the bed.
/// </summary>
internal class ChoralCommandmentSurgeonLocation : ContainerLocation
{
    protected override void DoLoad() => Using(new SceneEditGroup { { SceneName!, ModifyScene } });

    protected override void DoUnload() { }

    private void ModifyScene(Scene scene)
    {
        GetContainer(scene, out Container container, out ContainerInfo info);

        GameObject ambush = scene.FindGameObject("Husk Item Ambush")!;
        GameObject bed = ambush.FindChild("Ward Bed (1)")!;

        // Spawn items on break.
        GameObject pickup = bed.FindChild("corpse/Collectable Item Pickup")!;
        container.ModifyContainerInPlace(pickup, info);
        bed.GetComponent<Breakable>().OnBreak.AddListener(() =>
        {
            if (DelayedShinyLocation.MaybeFlingItems(pickup.transform.position, container, info))
            {
                UObject.Destroy(pickup);
                ambush.LocateMyFSM("Control").SendEvent("ITEM PICKED UP");
            }
        });
    }
}
