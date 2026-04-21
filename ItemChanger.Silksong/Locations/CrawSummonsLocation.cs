using HutongGames.PlayMaker;
using ItemChanger.Containers;
using ItemChanger.Locations;
using ItemChanger.Silksong.Modules;
using ItemChanger.Tags;
using Silksong.FsmUtil;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Locations;

public class CrawSummonsLocation : ObjectLocation
{
    private DeterministicCrawSummonsModule CrawSummonsModule =>
        ItemChangerHost.Singleton.ActiveProfile!.Modules.GetOrAdd<DeterministicCrawSummonsModule>();

    protected override void DoLoad()
    {
        // Don't call base.DoLoad since SceneName is not specified

        foreach (var scene in CrawSummonsModule.SceneNames)
        {
            ItemChangerHost.Singleton.GameEvents.AddSceneEdit(scene, base.OnSceneLoaded);
        }
    }

    protected override void DoUnload()
    {
        foreach (var scene in CrawSummonsModule.SceneNames)
        {
            ItemChangerHost.Singleton.GameEvents.RemoveSceneEdit(scene, base.OnSceneLoaded);
        }
    }

    public override GameObject ReplaceWithContainer(Scene scene, Container container, ContainerInfo info)
    {
        // Delay replacing with container until after the pin has landed
        GameObject target = FindObject(scene, ObjectName);
        GameObject newContainer = container.GetNewContainer(info);
        newContainer.SetActive(false);

        PlayMakerFSM fsm = target.LocateMyFSM("FSM");
        FsmState emptyState = fsm.MustGetState("Empty?");
        emptyState.Actions = [];
        emptyState.AddLambdaMethod(_ =>
        {
            newContainer.SetActive(true);
            container.ApplyTargetContext(newContainer, target, Correction);
            foreach (IActionOnContainerReplaceTag tag in GetTags<IActionOnContainerReplaceTag>())
            {
                tag.OnReplace(scene, newContainer);
            }

            fsm.SendEvent("TRUE");
        });

        return newContainer;
    }
}