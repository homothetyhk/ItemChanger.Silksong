using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Containers;
using ItemChanger.Extensions;
using ItemChanger.Locations;
using ItemChanger.Silksong.Containers;
using Silksong.FsmUtil;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Locations;

public class SurfaceMementoLocation : ObjectLocation
{
    private static readonly Vector3 FLING_POS = new(360, 13.5f);

    protected override void OnSceneLoaded(Scene scene)
    {
        GetContainer(scene, out Container container, out ContainerInfo info);

        // Do not replace the container actively.
        if (container.Name == ContainerNames.Shiny)
            ModifyContainerInPlace(scene, container, info);
        else
        {
            GameObject target = FindObject(scene, ObjectName);
            PlayMakerFSM fsm = scene.FindGameObject("Memory Group").LocateMyFSM("Memory Sequence")!;

            FsmState dropState = fsm.MustGetState("Drop Item Animator");
            dropState.RemoveActionsOfType<AnimatorPlay>();
            dropState.AddMethod(_ =>
            {
                if (DelayedShinyLocation.MaybeFlingItems(FLING_POS, container, info))
                    UObject.Destroy(target);
                else
                {
                    target.transform.position = FLING_POS;
                    ReplaceWithContainer(target.scene, container, info);
                }
            });

            FsmState activatedState = fsm.MustGetState("Activated");
            activatedState.RemoveActionsOfType<AnimatorPlay>();
            activatedState.AddMethod(_ =>
            {
                target.transform.position = FLING_POS;
                ReplaceWithContainer(target.scene, container, info);
            });
        }
    }
}
